using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    class Client
    {
        string ip = "127.0.0.1";
        string name;

        Int32 port = 8200;
        TcpClient client;
        NetworkStream stream;
        bool isConnected = false;

        //enter usename
        public Client()
        {
            Console.WriteLine("Enter your name : ");
            name = Console.ReadLine();
        }
        //coneect to server
        public void Connect()
        {
            //exeption
            try
            {
                client = new TcpClient(ip, port);
                stream = client.GetStream();

                Console.WriteLine("User Connected");

                isConnected = true;

                Thread msgData = new Thread(ClientMessage);
                msgData.Start();
                Thread readData = new Thread(ServerDataRead);
                readData.Start();
                

            }
            catch (Exception )
            {
                Console.WriteLine("Failed Connect to Server");
            }
        }

        //client input message
        void ClientMessage()
        {
            string msg;
            while(true)
            {
                msg = Console.ReadLine();
                string finalmsg = $"{name}: {msg}";
                Byte[] msgByte= System.Text.Encoding.ASCII.GetBytes(finalmsg);
                stream.Write(msgByte, 0, msgByte.Length);
            }

           
        }

        //server received data
        void ServerDataRead()
        {
            string data = null;
            Byte[] bytes = new Byte[1024];
            int i;
            try
            {
                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    string hex = BitConverter.ToString(bytes);
                    data = Encoding.ASCII.GetString(bytes, 0, i);
                    Console.WriteLine($"{data}");
                }
            }
            catch (Exception )
            {
                Console.WriteLine("Disconnected from server");
                client.Close();
            }
        }
    }

}
