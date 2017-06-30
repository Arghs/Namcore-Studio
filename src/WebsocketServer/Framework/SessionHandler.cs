using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Timers;
using TouchTableServer.Model;
using TouchTableServer.Tools;
namespace TouchTableServer.Framework
{
    public class SessionHandler
    {
        public static Dictionary<int, Session> AvaialbeSessions;
        public Session ActiveSession;
        public bool SessionActive = false;

        public SessionEvents SessionEvents;

        public Timer GameStartTimer;
        public Timer GameEndTimer;
        public Stopwatch GameEndStopwatch;
        public Timer SheetSequenceTimer;
        public Stopwatch SheetSequenceStopwatch;
        public Timer GameReportTimer;

        public double GameEndTimerRemaining;
        public double SheetSequenceTimerRemaining;

        public bool GameEndTimerActive = false;
        public bool SheetSequenceTimerActive = false;

        public void StartSession(int groupId, int phase)
        {
            Logging.LogMsg(Logging.LogLevel.NORMAL, "Starting Session Group {0}, Phase {1}", groupId, phase);
            if (AvaialbeSessions == null) AvaialbeSessions = new Dictionary<int, Session>();

            if (AvaialbeSessions.ContainsKey(groupId))
            {
                //TODO Session zurücksetzen?
                return;
            }
            ActiveSession = new Session()
            {
                SessionConfig = new Config(),
                Clients = new Dictionary<Client.ClientType, Client>(),
                GameEndResponses = new Dictionary<Client.ClientType, GameResponse>(),
                GameUpdateResponse = new Dictionary<Client.ClientType, GameResponse>(),
                GroupId = groupId,
                HandlerInstance = this,
                ActivePhase = phase
            };
            ActiveSession.SessionConfig.LoadConfig();
            AvaialbeSessions.Add(groupId, ActiveSession);
            SessionEvents = new SessionEvents(this, ActiveSession);

            GameEndTimerActive = false;
            SheetSequenceTimerActive = false;

            GameStartTimer = new Timer(10000) {AutoReset = false};
            GameStartTimer.Elapsed += new ElapsedEventHandler(StartGameSessionEvent);

            
            //_gameStartTimer.Start();


        }

        public void ReportGameStatusEvent(object source, ElapsedEventArgs e)
        {
            SessionEvents.ReportGameStatus();
        }
        

        public void StartGameSessionEvent(object source, ElapsedEventArgs e)
        {
            SessionEvents.StartGameSession();
        }

        public void EndGameSession(object source, ElapsedEventArgs e)
        {
            SessionEvents.ForceEndGameSession();
        }

        public void NextPhase()
        {
            StartSession(ActiveSession.GroupId, ActiveSession.ActivePhase + 1);
        }

        public void SetPhase(int phase)
        {
            ActiveSession.ActivePhase = phase;
            ActiveSession.SessionConfig.LoadConfig();
        }

        public void ChangeSheetEvent(object source, ElapsedEventArgs e)
        {
            SessionEvents.ChangeSheet();
        }

        public bool AddClientToSession(Client client)
        {
            if (ActiveSession.Clients.ContainsKey(client.ClientIdent))
            {
                // Client already added!
                Logging.LogMsg(Logging.LogLevel.WARNING, "Overriding client: {0}", client.ClientIdent);
                client = ActiveSession.Clients[client.ClientIdent].SyncClient(ref client);
                ActiveSession.Clients[client.ClientIdent] = client;
                return true;
            }
            ActiveSession.Clients.Add(client.ClientIdent, client);
            return true;
        }
    }
}
