using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Namcore_Remote_Server
{
    public class Server
    {
        private readonly TcpListener _tcpListener;
        private Thread listenThread;
        private TcpClient _activeClient;

        public Server()
        {
            _tcpListener = new TcpListener(IPAddress.Any, 3000);
            listenThread = new Thread(ListenForClients);
            listenThread.Start();
            while (true)
            {
                var str = Console.ReadLine();
                SendMessage(_activeClient, str);
            }
            
            
        }

        private void ListenForClients()
        {
            _tcpListener.Start();

            while (true)
            {
                //blocks until a client has connected to the server
                TcpClient client = _tcpListener.AcceptTcpClient();

                //create a thread to handle communication 
                //with connected client
                var clientThread = new Thread(HandleClientComm);
                clientThread.Start(client);
            }
        }

        private void HandleClientComm(object client)
        {
            var tcpClient = (TcpClient) client;
            _activeClient = tcpClient;
            NetworkStream clientStream = tcpClient.GetStream();

            var message = new byte[4096];

            while (true)
            {
                int bytesRead = 0;

                try
                {
                    //blocks until a client sends a message
                    bytesRead = clientStream.Read(message, 0, 4096);
                }
                catch
                {
                    //a socket error has occured
                    break;
                }

                if (bytesRead == 0)
                {
                    //the client has disconnected from the server
                    break;
                }

                //message has successfully been received
                var encoder = new ASCIIEncoding();
                Console.WriteLine(encoder.GetString(message, 0, bytesRead));
            }

            tcpClient.Close();
        }

        private void SendMessage(TcpClient tcpClient, string message)
        {
            NetworkStream clientStream = tcpClient.GetStream();
            var encoder = new ASCIIEncoding();
            byte[] buffer = encoder.GetBytes(message);
            clientStream.Write(buffer, 0, buffer.Length);
            clientStream.Flush();
        }
    }
}