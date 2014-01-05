using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Namcore_Remote_Server.Configuration;

namespace Namcore_Remote_Server
{
    public class Server
    {
        private readonly TcpListener _tcpListener;
        private TcpClient _activeClient;
        private Thread listenThread;

        public Server()
        {
            try
            {
                Globals.ConnectedClients = new List<RemoteClient>();
                _tcpListener = new TcpListener(IPAddress.Parse(MyConfig.BindIP), MyConfig.BindPort);
                Log.Message(LogType.Normal, "Listening on {0}:{1}", MyConfig.BindIP, MyConfig.BindPort);
                listenThread = new Thread(ListenForClients);
                listenThread.Start();
              
            }
            catch (Exception e)
            {
                Log.Message(LogType.Error, "{0}", e.Message);
                Log.Message();
            }
        }

        private void ListenForClients()
        {
            _tcpListener.Start();

            while (true)
            {
                //blocks until a client has connected to the server
                TcpClient client = _tcpListener.AcceptTcpClient();
                Log.Message(LogType.Normal, "Client connected");
                //add client to list
                var userClient = new RemoteClient();
                userClient.TcpClient = client;
                Globals.ConnectedClients.Add(userClient);
                //create a thread to handle communication 
                //with connected client
                var clientThread = new Thread(HandleClientComm);
                clientThread.Start(userClient);
            }
        }

        private void HandleClientComm(object client)
        {
            var remoteClient = (RemoteClient) client;
            TcpClient tcpClient = remoteClient.TcpClient;
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
                    _activeClient.Close();
                    Globals.ConnectedClients.Remove(remoteClient);
                    break;
                }

                //message has successfully been received
                var encoder = new ASCIIEncoding();
                string receivedMessage = encoder.GetString(message, 0, bytesRead);
                if (receivedMessage.StartsWith("!auth:"))
                {
                    string[] parts = receivedMessage.Split('#');
                    if (parts.Length == 3)
                    {
                         remoteClient.Account = new Account {Name = parts[1], Password = parts[2]};
                        SendMessage(_activeClient, "Pass!");
                        remoteClient.Authenticated = true;
                        Log.Message(LogType.Normal, "Client {0} authenticated!", parts[1]);
                    }
                    else
                    {
                        SendMessage(_activeClient, "Denied!");
                        remoteClient.Authenticated = false;
                        Log.Message(LogType.Normal, "Client not authenticated!");
                    }
                } 
                else
                    {
                        switch (remoteClient.Authenticated)
                        {
                            case true:
                                Log.Message(LogType.Normal, "[" + remoteClient.Account.Name + "] " + receivedMessage);
                                break;
                            case false:
                                Log.Message(LogType.Normal, "[Unknown] " + receivedMessage);
                                break;
                        }
                    }
            }

            tcpClient.Close();
            Globals.ConnectedClients.Remove(remoteClient);
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