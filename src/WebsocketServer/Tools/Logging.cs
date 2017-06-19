using System;

namespace TouchTableServer.Tools
{
    public class LogEventArgs : EventArgs
    {
        private int level;
        private string msg;

        public LogEventArgs(int level, string msg)
        {
            this.level = level;
            this.msg = msg;
        }

        public int Level
        {
            get { return this.level; }
        }

        public string Message
        {
            get { return this.msg; }
        }
    }
    public static class Logging
    {
        public static event LogEventHandler LogEvent;
        public delegate void LogEventHandler(object sender, LogEventArgs e);

        static void OnLogEvent(LogEventArgs e)
        {
            if (LogEvent != null) LogEvent(null, e);
        }

        public static void LogMsg(LogLevel level, params object[] msg)
        {
            var completeMessage = "";
            completeMessage = msg[0].ToString();
            if (msg.Length > 1)
            {
                for (var i = 0; i < msg.Length - 1; i++)
                {
                    try
                    {
                        completeMessage = completeMessage.Replace("{" + i + "}", msg[i + 1].ToString());
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            ConsoleColor col = ConsoleColor.White;
            switch (level)
            {
                case LogLevel.DEBUG:
                    col = ConsoleColor.Cyan;
                    break;
                case LogLevel.NORMAL:
                    col = ConsoleColor.White;
                    break;
                case LogLevel.WARNING:
                    col = ConsoleColor.Yellow;
                    break;
                case LogLevel.CRITICAL:
                    col = ConsoleColor.Red;
                    break;
            }
            try
            {
                LogEventArgs args = new LogEventArgs((int)level, $"[{DateTime.Now.ToString("HH:mm:ss:fff")}] {completeMessage.ToString()}");
                OnLogEvent(args);
                //Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss:fff")}] {completeMessage.ToString()}", col);
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }

        public enum LogLevel
        {
            DEBUG,
            NORMAL,
            WARNING,
            CRITICAL
        }
    }
}
