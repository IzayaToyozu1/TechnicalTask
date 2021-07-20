using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Threading;

namespace TechnicalTask
{
    class Program
    {
        static int _inquiry = 2019;
        static object locker = new object();
        static Calculation calc;
        static List<int> _dataServer;

        static void Main(string[] args)
        {
            _dataServer = new List<int>();
            calc = new Calculation();
            Console.Write("Введите количество потоков: ");
            int threadNumb = Convert.ToInt32(Console.ReadLine());
            ThreadingClient(threadNumb);

            while (true)
                if (_dataServer.Count == 2018)
                {
                    Console.WriteLine("все числа");
                    break;
                }
            

            _dataServer.Sort();
            Console.WriteLine(calc.Mediana(_dataServer));
            Console.ReadLine();
        }

        private static void ConsoleMessage_Logs(string message)
        {
            Console.WriteLine(message);
        }

        static void ThreadingClient(int numbThread) 
        {
            for(int i = 0; i < numbThread; i++)
            {
                ServerData client = new ServerData(_dataServer);
                client.Logs += ConsoleMessage_Logs;
                Thread thread = new Thread(new ThreadStart(client.Processing));
                thread.Start();
            }
        }

        public static int NumbInquiry()
        {
            lock (locker)
            {
                _inquiry--;
                return _inquiry;
            }
        }

        public static void Save(int numb)
        {
            lock(locker)
            using(StreamWriter writer = new StreamWriter("text.txt", true))
            {
                writer.WriteLine(numb);
            }
        }
    }
}
