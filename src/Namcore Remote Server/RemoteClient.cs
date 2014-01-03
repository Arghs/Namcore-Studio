using System.Collections.Generic;
using System.Net.Sockets;

namespace Namcore_Remote_Server
{
    public  class Globals
    {
        public static List<RemoteClient> ConnectedClients { get; set; }
       
    }
    public sealed class RemoteClient
    {
        public Account Account { get; set; }
        public List<string> MessageQueue;
        public bool Authenticated { get; set; }
        public TcpClient TcpClient { get; set; }
        public RemoteClient ()
        {
            Authenticated = false;
        }
    }

    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string IP { get; set; }
        public AccountRights Rights { get; set; }
        public bool Locked { get; set; }
    }

    public enum AccountRights
    {
        NONE = 0,
        READ = 1
    }
}