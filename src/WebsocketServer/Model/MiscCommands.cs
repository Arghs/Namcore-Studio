using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TouchTableServer.Model
{

    public class HandshakeClient
    {
        public int Version => Assembly.GetExecutingAssembly().GetName().Version.Build;
        public string ContentType = "application/json";
        public string CharSet = "UTF-8";
    }

    public class ClientError
    {
        public string Message;
    }
}
