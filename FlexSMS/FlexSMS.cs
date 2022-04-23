using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AMaruniak.FlexSMS {

    // Создание интерфейса для класса
    [Guid("937b004d-03ab-4b54-b6a1-c2406c36826f")]
    internal interface IFlexSMS {
        [DispId(1)]
        bool Connect(string pathSettings);
        void Disconnect();
        bool SendSms(string from, string to, string text);
        void AddSMS(string from, string to, string text, string inputID);
        bool SendAllSMS();
        string[,] GetSMSFullSMSList();
        string[,] GetSMSSentSMSList();
        void Wait();
        void Wait(int intervalSecund);
        void EnableLog(bool enableLog);
        string GetSettingsInfo();
    }

    // Определяем интерфейс для COM-событий
    [Guid("6c6e23b4-3499-42c4-b9d3-52050facd1d2"), InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IFlexSMSEvents {
        event LogEventHandler OnLogEvent;
        }


    [Guid("ba9d94d2-f210-4080-a150-a9d4cf48eca3"), ClassInterface(ClassInterfaceType.None), ComSourceInterfaces(typeof(IFlexSMSEvents))]
    public class FlexSMS : IFlexSMS {
        private string description = "";
        private SmsClient smsclient = null;
        private bool enabledLog = false;
        //StreamWriter sw = null;
        public event LogEventHandler OnLogEvent;

        #region public procedure
        public bool Connect(string pathSettings) {
            bool res = false;
            try {
                smsclient = new SmsClient(pathSettings, LogLevels.LogDebug);
                //smsclient.Connect();
                smsclient.OnDeliverSM += new DeliverSmEventHandler(OnDeliverSM);
                smsclient.OnSubmitSMResp += new SubmitSmRespEventHandler(OnSubmitSMResp);
                smsclient.OnLog += new LogEventHandler(OnLog);
                this.description = smsclient.Description;

                res = true;
            } catch {
                res = false;
                //this.description = "not connected";
            }

            /*
            string fileName = pathSettings + "\\smsc.log";
            sw = File.AppendText(fileName);
            if (enabledLog) { 
                sw.WriteLine("\n\nНачало сеанса:   " + DateTime.Now.ToShortDateString() + "    " + DateTime.Now.ToShortTimeString());
                sw.Flush();
            }
            */
            return res;
        }

        public void Wait() {
            Wait(2);
        }

        public void Wait(int intervalSecund) {
            Thread.Sleep(intervalSecund*1000);
        }

        public void Disconnect() {
            if (smsclient != null) {
                smsclient.Disconnect();
                //sw.Close();
            }
        }

        public void EnableLog(bool enableLog) {
            this.enabledLog = enableLog;
        }

        public string GetSettingsInfo() {
            if (smsclient == null) {
                return "";
            };

            return this.description;
        }

        public void AddSMS(string from, string to, string text, string inputID) {
            if (smsclient == null) {
                return;
            };

            smsclient.AddSMS(from, to, text, inputID);
        }

        public bool SendAllSMS() {
            bool res = false;
            if (smsclient == null) {
                return res;
            };

            if (smsclient.SMSList.Count() == 0) {
                return res;
            };

            res = true;
            foreach (SMSData sms in smsclient.SMSList) {
                //res = res & smsclient.SendSms(sms.From, sms.To, sms.Text);
                res = res & smsclient.SendSms(sms);
                //Console.WriteLine("sms.Sequence " + sms.Sequence);
            }

            return res;
        }
        
        public bool SendSms(string from, string to, string text) {
            if (smsclient==null) {
                return false;
            };

            bool res = smsclient.SendSms(from, to, text);
            return res;

        }

        #endregion

        #region events
        private void OnDeliverSM(DeliverSmEventArgs args) {
            //System.Console.WriteLine("\nСобытие Deliver SM! ");
            if (enabledLog) {
                //sw.WriteLine(" " + DateTime.Now + ", Deliver SM");
                //sw.WriteLine("      To:                 " + args.To);
                //sw.WriteLine("      SequenceNumber:     " + args.SequenceNumber);
                //sw.WriteLine("      ReceiptedMessageID: " + args.ReceiptedMessageID);
                //sw.WriteLine("      IsDeliveryReceipt:  " + args.IsDeliveryReceipt);
                //sw.Flush();
            }
        }

        private void OnSubmitSMResp(SubmitSmRespEventArgs args) {
            //System.Console.WriteLine("\nСобытие Submit SM Resp! ");
            if (enabledLog) {
                //sw.WriteLine(" " + DateTime.Now + ", Submit SM Resp");
                //sw.WriteLine("      MessageID:  " + args.MessageID);
                //sw.WriteLine("      Sequence:   " + args.Sequence);
                //sw.WriteLine("      Status:     " + args.Status);
                //sw.Flush();
            }
        }

        private void OnLog(LogEventArgs args) {
            if (enabledLog) {
                //sw.WriteLine(" " + DateTime.Now + ": " + args.Message);
                //sw.Flush();
            }
            if (OnLogEvent != null)
                OnLogEvent(args);
        }
        
        #endregion events

        #region Отладка
        public void WriteSMSList() {
            int n = 0;
            foreach (SMSData sms in smsclient.SMSList) {
                Console.WriteLine("SMS: " + (n++));
                Console.WriteLine("   To: {0}    Sequence: {1}   MessageID: {2}    IsDeliveryReceipt: {3}", sms.To, sms.Sequence, sms.MessageID, sms.IsDeliveryReceipt);
                if (enabledLog) {

                    //sw.WriteLine("SMS: " + (n++));
                    //sw.WriteLine("   To: {0}    Sequence: {1}   MessageID: {2}    IsDeliveryReceipt: {3}", sms.To, sms.Sequence, sms.MessageID, sms.IsDeliveryReceipt);
                    //sw.Flush();
                }
            }
        }

        public void WriteSMSListRecieved() {
            int n = 0;
            foreach (SMSData sms in smsclient.SMSListRecieved) {
                Console.WriteLine("SMS: " + (n++));
                Console.WriteLine("   To: {0}    Sequence: {1}   MessageID: {2}    IsDeliveryReceipt: {3}", sms.To, sms.Sequence, sms.MessageID, sms.IsDeliveryReceipt);
                if (enabledLog) {

                    //sw.WriteLine("SMS: " + (n++));
                    //sw.WriteLine("   To: {0}    Sequence: {1}   MessageID: {2}    IsDeliveryReceipt: {3}", sms.To, sms.Sequence, sms.MessageID, sms.IsDeliveryReceipt);
                    //sw.Flush();
                }
            }
        }
        
#endregion 

        public string[,] GetSMSSentSMSList() {
            int totalSMS = smsclient.SMSList.Count();
            string[,] resArray = new string[totalSMS, 5];

            int ind = 0;
            foreach (SMSData sms in smsclient.SMSList) {
                resArray[ind, 0] = sms.To;
                resArray[ind, 1] = sms.Sequence.ToString();
                resArray[ind, 2] = sms.MessageID;
                resArray[ind, 3] = sms.IsDeliveryReceipt.ToString();
                resArray[ind, 4] = sms.InputID;

                ind++;
            }
            return resArray;
        }
        
        public string[,] GetSMSFullSMSList() {
            int totalSMS = smsclient.SMSListRecieved.Count();
            string[,] resArray = new string[totalSMS, 5];

            int ind = 0;
            foreach (SMSData sms in smsclient.SMSListRecieved) {
                resArray[ind, 0] = sms.To;
                resArray[ind, 1] = sms.Sequence.ToString();
                resArray[ind, 2] = sms.MessageID;
                resArray[ind, 3] = sms.IsDeliveryReceipt.ToString();
                resArray[ind, 4] = sms.InputID;

                ind++;
            }
            return resArray;
        }            

    }
}
