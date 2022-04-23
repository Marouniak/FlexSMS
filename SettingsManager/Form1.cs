using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;


namespace AMaruniak.FlexSMS {
    public partial class frmMain : Form {
        private SMSC smsc = null;

        public frmMain() {
            InitializeComponent();
        }

        private void txbPort_KeyPress(object sender, KeyPressEventArgs e) {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(8)) {
                e.Handled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e) {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "Файл конфигурации (*.cfg)|*.cfg";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK) {
                try {
                    FormToObj();
                    XmlSerializer serializer = new XmlSerializer(typeof(SMSC));
                    StringWriter writer = new StringWriter();
                    serializer.Serialize(writer, smsc);
                    string serializedXML = writer.ToString();
                    //MessageBox.Show(serializedXML);
                    
                    Aes s = AesManaged.Create();
                    var c = s.CreateEncryptor();
                    byte[] bt = Encoding.UTF8.GetBytes(serializedXML);
                    string enc1 = Convert.ToBase64String(bt);
                    //MessageBox.Show(enc1);
                    //MessageBox.Show(Encoding.UTF8.GetString(Convert.FromBase64String(enc1)));

                    using (TextWriter streamWriter = new StreamWriter(saveFileDialog1.FileName)) {
                        streamWriter.Write(enc1);
                    }
                    
                    MessageBox.Show("Конфигурация сохранена");

                } catch (Exception ex) {
                    MessageBox.Show("Ошибка: Невозможно записать файл на диск. Описание: " + ex.Message);
                }
            }




        }

        private void btnLoad_Click(object sender, EventArgs e) {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            //openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "Файл конфигурации (*.cfg)|*.cfg";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                try {
                    /*
                    XmlSerializer serializer = new XmlSerializer(typeof(SMSC));

                    using (TextReader tr = new StreamReader(openFileDialog1.FileName)) {
                        string enc1 = tr.ReadToEnd();
                        string serializedXML = Encoding.UTF8.GetString(Convert.FromBase64String(enc1));

                        using (TextReader reader = new StringReader(serializedXML)) {
                            smsc = (SMSC)serializer.Deserialize(reader);
                        }

                        ObjToForm();
                        MessageBox.Show("Конфигурация восстановлена");
                    }
                    */
                    smsc = Tools.LoadSMSCFromFile(openFileDialog1.FileName);

                        ObjToForm();
                        MessageBox.Show("Конфигурация восстановлена");




                } catch (Exception ex) {
                    MessageBox.Show("Ошибка: Невозможно прочитать файл с диска. Описание: " + ex.Message);
                }
            }

        }


        private void ObjToForm() {
            if (smsc == null) {
                return;
            }

            txbDescription.Text = smsc.Description;
            txbHost.Text = smsc.Host;
            txbPassword.Text = smsc.Password;
            txbPort.Text = smsc.Port.ToString();
            txbSystemId.Text = smsc.SystemId;
        }

        private void FormToObj() {
            smsc = new SMSC(txbDescription.Text, txbHost.Text, int.Parse(txbPort.Text), txbSystemId.Text, txbPassword.Text, "0", 0, 0, "", 0);
            smsc.AddrTon = 5;
        }

        private void frmMain_Load(object sender, EventArgs e) {

        }

    }
}
