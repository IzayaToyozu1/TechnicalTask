using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading;

namespace TechnicalTask
{
    public class ServerData
    {
        Client _client;
        int _data = 0;
        string _dataSend;

        public List<int> DataBase { get; }

        public event Action<string> Logs;

        public ServerData (List<int> dataBase) 
        { 
            DataBase = dataBase;
        }

        public void Processing()
        {
            _client = new Client();
            try
            {
                _client.Connect();
                
                while (true)
                {
                    if (_data == 0)
                    {
                        _data = 1;
                        _dataSend = GetNumber();
                        if (_dataSend == "0") break;
                        if(DataBase.Count == 1000)
                            Console.WriteLine("ВСе получилось");
                    }                        
                    _client.SendMessage(_dataSend);
                    GetDataAndWrite();
                }
                _client.Disconnect();
            }
            catch(Exception e)
            {
                Logs?.Invoke(e.Message);
                _client.Disconnect();
                Processing();
            }
        }

        private string GetNumber()
        {
            int numb = Program.NumbInquiry();
            if (numb > 0)
            {
                return numb.ToString() /*+ "\n"*/;
            }
            else
                return "0";
        }

        private void GetDataAndWrite()
        {
            string numb = _client.GetMessage();
            Regex regex = new Regex(@"\d+", RegexOptions.Singleline);
            int result;
            if (int.TryParse(regex.Match(numb).Value, out result))
            {
                Program.Save(result);
                Logs?.Invoke($"Отправлен запрос {_dataSend}, ответ от сервера {result}.");
                DataBase.Add(result);
                _data = 0;
            }
        }
    }
}
