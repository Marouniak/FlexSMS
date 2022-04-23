using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Xml.Serialization;

namespace AMaruniak.FlexSMS {

    public class SmsClient {
        private string description = "";
        private SMPPClient smppClient;
        private int waitForResponse = 30000;
        private SortedList<int, AutoResetEvent> events = new SortedList<int, AutoResetEvent>();
        private SortedList<int, int> statusCodes = new SortedList<int, int>();
        private List<SMSData> smsList = new List<SMSData>();
        private List<SMSData> smsListRecieved = new List<SMSData>();
        private string pathSettings;

        #region Properties
        public int WaitForResponse {
            get { return waitForResponse; }
            set { waitForResponse = value; }
        }

        public string Description {
            get { return description; }
        }

        public List<SMSData> SMSList {
            get { return smsList; }
        }

        public List<SMSData> SMSListRecieved {
            get { return smsListRecieved; }
        }
        #endregion Properties

        #region Public functions
        public SmsClient(string pathSettings) {
            smppClient = new SMPPClient();

            smppClient.OnDeliverSm += new DeliverSmEventHandler(onDeliverSm);
            smppClient.OnSubmitSmResp += new SubmitSmRespEventHandler(onSubmitSmResp);

            smppClient.OnLog += new LogEventHandler(onLog);
            smppClient.LogLevel = LogLevels.LogErrors;
            smppClient.LogLevel = LogLevels.LogErrors;

            this.pathSettings = pathSettings;
            LoadConfig();

            smppClient.Connect();

        }
        public SmsClient(string pathSettings, int logLevel) : this(pathSettings) {
            smppClient.LogLevel = logLevel;
        }

        public void Connect() {
            smppClient.Connect();
        }

        public void Disconnect() {
            smppClient.Disconnect();
        }

        public bool SendSms(string from, string to, string text) {
            bool result = false;
            if (smppClient.CanSend) {
                AutoResetEvent sentEvent;
                int sequence;
                lock (events) {
                    sequence = smppClient.SendSms(from, to, text);
                    sentEvent = new AutoResetEvent(false);
                    events[sequence] = sentEvent;
                }
                if (sentEvent.WaitOne(waitForResponse, true)) {
                    lock (events) {
                        events.Remove(sequence);
                    }
                    int statusCode;
                    bool exist;
                    lock (statusCodes) {
                        exist = statusCodes.TryGetValue(sequence, out statusCode);
                    }
                    if (exist) {
                        lock (statusCodes) {
                            statusCodes.Remove(sequence);
                        }
                        if (statusCode == StatusCodes.ESME_ROK)
                            result = true;
                    }
                }
            }
            return result;
        }

        public bool SendSms(SMSData sms) {
            bool result = false;
            if (smppClient.CanSend) {
                AutoResetEvent sentEvent;
                int sequence;
                lock (events) {
                    sequence = smppClient.SendSms(sms.From, sms.To, sms.Text);
                    sms.Sequence = sequence;
                    sentEvent = new AutoResetEvent(false);
                    events[sequence] = sentEvent;
                }
                if (sentEvent.WaitOne(waitForResponse, true)) {
                    lock (events) {
                        events.Remove(sequence);
                    }
                    int statusCode;
                    bool exist;
                    lock (statusCodes) {
                        exist = statusCodes.TryGetValue(sequence, out statusCode);
                    }
                    if (exist) {
                        lock (statusCodes) {
                            statusCodes.Remove(sequence);
                        }
                        if (statusCode == StatusCodes.ESME_ROK)
                            result = true;
                    }
                }
            }
            return result;
        }

        #endregion Public functions

        #region Events

        //public event NewSmsEventHandler OnDeliverSM;
        public event DeliverSmEventHandler OnDeliverSM;
        public event SubmitSmRespEventHandler OnSubmitSMResp;

        public event LogEventHandler OnLog;

        #endregion Events

        #region Private functions
        private void onDeliverSm(DeliverSmEventArgs args) {
            smppClient.sendDeliverSmResp(args.SequenceNumber, StatusCodes.ESME_ROK);
            SetMessageDeliveryStatus(args.SequenceNumber, args.ReceiptedMessageID, args.IsDeliveryReceipt, args.From, args.To, args.TextString);
            if (OnDeliverSM != null)
            //    OnDeliverSM(new NewSmsEventArgs(args.From, args.To, args.TextString));
                OnDeliverSM(args);

        }
        private void onSubmitSmResp(SubmitSmRespEventArgs args) {
            AutoResetEvent sentEvent;
            bool exist;
            lock (events) {
                exist = events.TryGetValue(args.Sequence, out sentEvent);
            }
            if (exist) {
                lock (statusCodes) {
                    statusCodes[args.Sequence] = args.Status;
                }
                sentEvent.Set();
            }
            SetMessageID(args.Sequence, args.MessageID);
            if (OnSubmitSMResp != null) {
                OnSubmitSMResp(args);
            }

        }
        private void onLog(LogEventArgs args) {
            //Console.WriteLine("Log " + args.Message);
            if (OnLog != null)
                OnLog(args);
        }
        private void LoadConfig() {
            try {

                /*
                XmlSerializer serializer = new XmlSerializer(typeof(SMSC));
                if (!File.Exists(pathSettings + "\\smsc.cfg")) {
                    using (TextWriter writer = new StreamWriter(pathSettings + "\\smsc.cfg")) {
                        serializer.Serialize(writer, new SMSC("example", "127.0.0.1", 12345, "alpha", "test", "test", 0, 0, "", 0));
                    }
                    onLog(new LogEventArgs("Please edit smsc.cfg and enter your data."));
                }
                using (FileStream fs = new FileStream(pathSettings + "\\smsc.cfg", FileMode.Open)) {
                    SMSC smsc = (SMSC)serializer.Deserialize(fs);
                    smppClient.AddSMSC(smsc);
                }*/
                
                //SMSC smsc = Tools.LoadSMSCFromFile(pathSettings + "\\smsc.cfg");
                SMSC smsc = Tools.LoadSMSCFromFile(pathSettings);
                smppClient.AddSMSC(smsc);
                this.description = smsc.Description;
            } catch (Exception ex) {
                this.description = "error";
                onLog(new LogEventArgs("Ошибка загрузки конфигурации smsc.cfg : " + ex.Message));
            }

        }

        #endregion

        #region SMSList
        public void AddSMS(string from, string to, string text) {
            SMSData newSMS = new SMSData(from, to, text);
            smsList.Add(newSMS);
        }

        public void AddSMS(string from, string to, string text, string inputID) {
            SMSData newSMS = new SMSData(from, to, text, inputID);
            smsList.Add(newSMS);
        }

        public void AddSMS(SMSData smsdata) {
            smsList.Add(smsdata);
        }

        public bool RemoveSMS(SMSData smsdata) {
            bool res = smsList.Remove(smsdata);
            return res;
        }

        public void AddSMSReciept(SMSData smsdata) {
            smsListRecieved.Add(smsdata);
        }

        public void SetMessageID(int sequence, string messageID) {

            SMSData sms = smsList.Find(
                delegate(SMSData smsdata) {
                    return smsdata.Sequence == sequence;
                }
            );
            if (sms != null) {
                sms.MessageID = messageID;
            }

        }

        public void SetMessageDeliveryStatus(int sequence, string messageID, bool isDeliveryReceipt, string from, string to, string text) {

        /*    SMSData sms = smsList.Find(
                delegate(SMSData smsdata) {
                    return smsdata.MessageID == messageID;
                }
            );
            if (sms != null) {
                sms.IsDeliveryReceipt = isDeliveryReceipt;
            } else {
                sms = new SMSData(from, to, text);
                sms.MessageID = messageID;
                sms.IsDeliveryReceipt = isDeliveryReceipt;
                sms.Sequence = sequence;
                AddSMS(sms);

            }
            */

            SMSData sms = new SMSData(from, to, text);
            sms.MessageID = messageID;
            sms.IsDeliveryReceipt = isDeliveryReceipt;
            sms.Sequence = sequence;
            AddSMSReciept(sms);


        }
        #endregion
    }




}
