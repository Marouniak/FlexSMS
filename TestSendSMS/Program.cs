using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMaruniak.FlexSMS;


namespace AMaruniak.FlexSMS.Test {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Тест");

            FlexSMS fc = new FlexSMS();

            string path = "D:\\bases\\klien\\Карабинер\\Компонента\\FlexSMS\\TestSendSMS\\bin\\Debug\\smsc.cfg";
            //string path = "f:\\ttt";
            fc.EnableLog(true);
            bool IsConnected = fc.Connect(path);
            Console.WriteLine("Connected= " + IsConnected);
            Console.WriteLine("Description= " + fc.GetSettingsInfo());
            fc.Wait();
            

            if (IsConnected) {


                Console.Write("Для отправки SMS нажать Enter. Для отмены - N.... ");
                string userResponse = Console.ReadLine();
                if (!userResponse.ToLower().StartsWith("n")) {
                    string timeSuffix = DateTime.Now.ToShortTimeString();

                    string from = "A.Maruniak";
                    string to = "380503338638";
                    string text = "Test Flex SMS in C#. " + timeSuffix;
                    //bool isSent = fc.SendSms(from, to, text);
                    //Console.WriteLine("SMS отправлено: " + isSent);
                    text = "ОЧЧЕНЬ Длинное тестовое: --++ ... /// сообщение на русском языке для тестирования отправки таких сообщений, -987 654,321;" + timeSuffix;
                    fc.AddSMS(from, to, text, "2");

                    /*
                    to = "380503903061";
                    text = "№2 Тестовое сообщение Flex SMS на C#. " + timeSuffix;
                    fc.AddSMS(from, to, text, "4");
                    

                    to = "380985296492";
                    text = "№3 Тестовое сообщение Flex SMS на C#. " + timeSuffix;
                    fc.AddSMS(from, to, text, "2");
                    
                    to = "380637008837";
                    text = "№4 Тестовое сообщение FlexSMS. " + timeSuffix;
                    fc.AddSMS(from, to, text, "3");
                     */
                    // вариант 2
                    bool isSentAll = fc.SendAllSMS();
                    Console.WriteLine("SMS отправлено: " + isSentAll);
                   

                    Console.Write("Нажать Enter для просмотра результатов.... ");
                    Console.ReadLine();
                    fc.Disconnect();
                    fc.WriteSMSList();
                } else {
                    fc.WriteSMSList();

                }
            }


            Console.WriteLine("SAFE ARRAY Результатов");
            string[,] smsArray = fc.GetSMSFullSMSList();
            printArray(smsArray);

            Console.Write("Нажать Enter для завершения.... ");
            Console.ReadLine();



        }

        static void printArray(string[,] arr) {
            for (int i = 0; i < arr.GetLength(0); i++) {
                for (int j = 0; j < arr.GetLength(1); j++) {
                    Console.Write(arr[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

    }
}
