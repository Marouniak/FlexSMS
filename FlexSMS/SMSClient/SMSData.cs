using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMaruniak.FlexSMS {
    public class SMSData {
        private string from;
        private string to;
        private string text;
        private string messageID = "";
        private int sequence = -1;
        private bool isDeliveryReceipt = false;
        private string inputID = "";

        public SMSData(string from, string to, string text):this(from, to, text, "0") {
            this.text = text;
        }

        public SMSData(string from, string to, string text, string inputID) {
            this.from = from;
            this.to = to;
            this.text = text;
            this.inputID = inputID;
        }

        public string From {
            get {
                return from;
            }
            set {
                from = value;
            }
        }

        public string To {
            get {
                return to;
            }
            set {
                to = value;
            }
        }

        public string Text {
            get {
                return text;
            }
            set {
                text = value;
            }
        }

        public string MessageID {
            get {
                return messageID;
            }
            set {
                messageID = value;
            }
        }

        public int Sequence {
            get {
                return sequence;
            }
            set {
                sequence = value;
            }
        }

        public bool IsDeliveryReceipt {
            get {
                return isDeliveryReceipt;
            }
            set {
                isDeliveryReceipt = value;
            }
        }

        public string InputID {
            get {
                return inputID;
            }
            set {
                inputID = value;
            }
        }


    }

    public class SMSDataList : IEnumerable {
        private SMSData[] _smslist;

        public SMSDataList(SMSData[] pArray) {
            _smslist = new SMSData[pArray.Length];

            for (int i = 0; i < pArray.Length; i++) {
                _smslist[i] = pArray[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return (IEnumerator)GetEnumerator();
        }

        public SMSDataEnum GetEnumerator() {
            return new SMSDataEnum(_smslist);
        }
    }

    public class SMSDataEnum : IEnumerator {
        public SMSData[] _smslist;

        // Enumerators are positioned before the first element
        // until the first MoveNext() call.
        int position = -1;

        public SMSDataEnum(SMSData[] list) {
            _smslist = list;
        }

        public bool MoveNext() {
            position++;
            return (position < _smslist.Length);
        }

        public void Reset() {
            position = -1;
        }

        object IEnumerator.Current {
            get {
                return Current;
            }
        }

        public SMSData Current {
            get {
                try {
                    return _smslist[position];
                } catch (IndexOutOfRangeException) {
                    throw new InvalidOperationException();
                }
            }
        }
    }


}
