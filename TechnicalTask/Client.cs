using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace TechnicalTask
{
    public class Client
    {
        static string _addres = "88.212.241.115";
        static int _port = 2012;

        TcpClient _client = new TcpClient();
        NetworkStream _stream;

        public void Connect()
        {
            _client.Connect(_addres, _port);
            _stream = _client.GetStream(); 
        }

        public void SendMessage(string message)
        {
            StreamWriter writer = new StreamWriter(_stream);
                writer.WriteLine(message);
                writer.Flush();
        }

        public string GetMessage()
        {
            //byte[] data = new byte[256];
            //StringBuilder builder = new StringBuilder();
            //int bytes = 0;
            //do
            //{
            //    bytes = _stream.Read(data, 0, data.Length);
            //    builder.Append(Encoding.GetEncoding(20866).GetString(data, 0, bytes));
            //}
            //while (_stream.DataAvailable);

            //return builder.ToString();

            StreamReader reader = new StreamReader(_stream);
            string message = reader.ReadLine();
            return message;
        }

        public void Disconnect()
        {
            if (_stream != null)
                _stream.Close();
            if (_client != null)
                _client.Close();
        }
    }
}
