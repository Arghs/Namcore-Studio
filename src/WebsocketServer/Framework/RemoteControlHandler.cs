using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouchTableServer.Model;
using TouchTableServer.Tools;

namespace TouchTableServer.Framework
{
    public class RemoteControlHandler
    {
        private readonly Client _client;
        public RemoteControlHandler(Client client)
        {
            _client = client;
        }

        public void HandleMessage(string message)
        {
            Logging.LogMsg(Logging.LogLevel.DEBUG, "ControlClient: " + message);
            SessionHandler handler = _client.SessionPointer.HandlerInstance;

            try
            {
                string[] cmd = message.Split(Convert.ToChar(";"));
                int phase;
                switch (cmd[0])
                {
                    case "startintro":
                        phase = int.Parse(cmd[1]);
                        handler.SessionEvents.StartIntro(phase);
                        break;
                    case "loadgames":
                        phase = int.Parse(cmd[1]);
                        handler.SessionEvents.LoadGames(phase);
                        break;
                    case "initpipes":
                        phase = int.Parse(cmd[1]);
                        handler.SessionEvents.InitPipes(phase);
                        break;
                    case "setpipe":
                        var start = int.Parse(cmd[1]);
                        var end = int.Parse(cmd[2]);
                        PipeStatus status = (PipeStatus)int.Parse(cmd[3]);
                        handler.ActiveSession.GetClient(Client.ClientType.Wrapper)?.WE.SetPipe(start, end, status);
                        break;
                    case "initsession":
                        if (handler == null) return;
                        if (handler.SessionActive)
                        {
                            _client.SendMsg("Invalid action: Stop Phase first!");
                            return;
                        }
                        if (cmd.Length >= 3 && !string.IsNullOrEmpty(cmd[2]))
                        {
                            handler.ActiveSession.SessionConfig.Timelimit = int.Parse(cmd[2]);
                        }
                        var gc = handler.ActiveSession.GetInitializedGameClients();
                        if (gc == null) return;
                        foreach (var g in gc)
                        {
                            g.GE.InitGame();
                        }

                        break;
                    case "startsession":
                        if (handler == null) return;
                        if (handler.SessionActive)
                        {
                            _client.SendMsg("Invalid action: Stop Phase first!");
                            return;
                        }
                        phase = int.Parse(cmd[1]);
                        handler.SessionEvents.StartGameSession(phase);
                        break;
                    case "pausesession":
                        if (handler == null) return;
                        handler.SessionEvents.PauseSession();
                        break;
                    case "continuesession":
                        if (handler == null) return;
                        handler.SessionEvents.ContinueSession();
                        break;
                    case "endsession":
                        if (handler == null) return;
                        handler.SessionEvents.ForceEndGameSession();
                        break;
                    case "breakclient":
                        if (handler == null) return;
                        if (cmd.Length < 2 || string.IsNullOrEmpty(cmd[1]))
                        {
                            _client.SendMsg("Invalid action: Specify ClientId!");
                            return;
                        }
                        Client.ClientType clientid = (Client.ClientType)int.Parse(cmd[1]);
                        handler.ActiveSession.GetClient(clientid)?.GE.InterruptGame();
                        break;
                    case "start":
                        if (handler == null) return;
                        handler.SessionEvents.StartGameSession();
                        break;
                    case "end":
                        if (handler == null) return;
                        handler.SessionEvents.ForceEndGameSession();
                        break;
                    case "setsheet":
                        if (handler == null) return;
                        handler.SessionEvents.ChangeSpecificSheet(Convert.ToInt32(cmd[1]));
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
