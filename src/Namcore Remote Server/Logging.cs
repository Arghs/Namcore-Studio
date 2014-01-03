using System;
using Namcore_Remote_Server.Configuration;
using DefaultConsole = System.Console;
using System.Text;

namespace Namcore_Remote_Server
{
    public class Log
    {
        public static string ServerType { get; set; }

        static public void Message()
        {
            SetLogger(LogType.Default, "");
        }

        static public void Message(LogType type, string text, params object[] args)
        {
            SetLogger(type, text, args);
        }

        static void SetLogger(LogType type, string text, params object[] args)
        {
            DefaultConsole.OutputEncoding = UTF8Encoding.UTF8;

            switch (type)
            {
                case LogType.Normal:
                    DefaultConsole.ForegroundColor = ConsoleColor.Green;
                    text = text.Insert(0, "System: ");
                    break;
                case LogType.Error:
                    DefaultConsole.ForegroundColor = ConsoleColor.Red;
                    text = text.Insert(0, "Error: ");
                    break;
                case LogType.Dump:
                    DefaultConsole.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogType.Init:
                    DefaultConsole.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case LogType.DB:
                    DefaultConsole.ForegroundColor = ConsoleColor.DarkMagenta;
                    break;
                case LogType.Cmd:
                    DefaultConsole.ForegroundColor = ConsoleColor.Green;
                    break;
                case LogType.Debug:
                    DefaultConsole.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                default:
                    DefaultConsole.ForegroundColor = ConsoleColor.White;
                    break;
            }

           
                if (type.Equals(LogType.Init) | type.Equals(LogType.Default))
                    DefaultConsole.WriteLine(text, args);
                else if (type.Equals(LogType.Dump) || type.Equals(LogType.Cmd))
                    DefaultConsole.WriteLine(text, args);
                else
                    DefaultConsole.WriteLine("[" + DateTime.Now.ToLongTimeString() + "] " + text, args);
            
        }
    }
}