using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace SocketServer
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            StartListening();
            //Application.Run(new Form1());
        }

        public static void StartListening()
        {

            try
            {
                Int32 port = 13000;
                IPAddress localAddr = IPAddress.Parse("192.168.1.12");

                //TcpListener server = new TcpListener(0); 
                TcpListener server = new TcpListener(localAddr, port);
                server.Start();
                Byte[] bytes = new Byte[256];
                String data = null;
                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();
                    data = null;
                    NetworkStream stream = client.GetStream();
                    Int32 i;
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        MessageBox.Show(data);
                        data = data.ToUpper();
                        Byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);
                        stream.Write(msg, 0, msg.Length);
                        MessageBox.Show(data);
                    }
                    client.Close();
                }


                /*IPHostEntry localMachineInfo = Dns.GetHostEntry(Dns.GetHostName());
                
                System.Windows.Forms.MessageBox.Show(Dns.GetHostAddresses("enter")[0].ToString(),
                    "Это сетевое имя вашего компьютера");

                int port = 13000;
                IPEndPoint myEndpoint = new IPEndPoint(
                    localMachineInfo.AddressList[0], port);
                Socket serverSocket = new Socket(myEndpoint.Address.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);
                serverSocket.Bind(myEndpoint);
                serverSocket.Listen((int)SocketOptionName.MaxConnections);*/
            }
            catch (SocketException e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}