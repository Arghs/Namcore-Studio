using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouchTableServer.Model;
using TouchTableServer.Tools;
using System.Timers;

namespace TouchTableServer.Framework
{
    public class SessionEvents
    {
        private readonly SessionHandler _handler;
        private readonly Session _activeSession;
       

        public SessionEvents(SessionHandler handler, Session activeSession)
        {
            _handler = handler;
            _activeSession = activeSession;
        }

        public void StartIntro(int phase = -1)
        {
            Logging.LogMsg(Logging.LogLevel.NORMAL, "Starting Intro. Group {0}, Phase {1}", _handler.ActiveSession.GroupId, phase);
            Functions.NotifyControl("Starting Intro", _activeSession);
            if (phase > -1) _handler.SetPhase(phase);
            _handler.ActiveSession.GetClient(Client.ClientType.Wrapper)?.WE?.StartIntro();
        }

        public void LoadGames(int phase = -1)
        {
            Logging.LogMsg(Logging.LogLevel.NORMAL, "Loading Games. Group {0}, Phase {1}", _handler.ActiveSession.GroupId, phase);
            Functions.NotifyControl("Loading Games", _activeSession);
            if (phase > -1) _handler.SetPhase(phase);
            _handler.ActiveSession.GetClient(Client.ClientType.Wrapper)?.WE?.LoadGames();
        }

        public void InitPipes(int phase = -1)
        {
            Logging.LogMsg(Logging.LogLevel.NORMAL, "Initializing Pipes. Group {0}, Phase {1}", _handler.ActiveSession.GroupId, phase);
            Functions.NotifyControl("Initializing Pipes", _activeSession);
            if (phase > -1) _handler.SetPhase(phase);
            _handler.ActiveSession.GetClient(Client.ClientType.Wrapper)?.WE?.InitPipes();
        }

        public void StartGameSession(int phase = -1, int timelimit = -1)
        {
            if (timelimit == -1) timelimit = _activeSession.SessionConfig.Timelimit;
            if (phase != -1) _handler.SetPhase(phase);
            _activeSession.SessionConfig.ActiveSheetSquenceIdx = 0;
            _activeSession.SessionConfig.ActiveSheet = _activeSession.SessionConfig.SheetSqeuence[0].SheetId;

            _handler.GameEndStopwatch = new Stopwatch();
            _handler.GameEndStopwatch.Stop();
            _handler.GameEndStopwatch.Reset();

            _handler.GameEndTimer = new Timer(timelimit) { AutoReset = false };
            _handler.GameEndTimer.Elapsed += new ElapsedEventHandler(_handler.EndGameSession);
            _handler.GameEndTimer.Start();
            _handler.GameEndStopwatch.Start();
            _handler.GameEndTimerActive = true;

            _handler.SheetSequenceStopwatch = new Stopwatch();
            _handler.SheetSequenceStopwatch.Stop();
            _handler.SheetSequenceStopwatch.Reset();
            _handler.SheetSequenceTimer = new Timer(_activeSession.SessionConfig.SheetSqeuence[_activeSession.SessionConfig.ActiveSheetSquenceIdx + 1].SwitchTime) { AutoReset = false };
            _handler.SheetSequenceTimer.Elapsed += new ElapsedEventHandler(_handler.ChangeSheetEvent);
            //_sheetSequenceTimer.Start();
            
            _handler.GameReportTimer = new Timer(5000) { AutoReset = true };
            _handler.GameReportTimer.Elapsed += new ElapsedEventHandler(_handler.ReportGameStatusEvent);
            _handler.GameReportTimer.Start();

            _handler.SessionActive = true;

            Logging.LogMsg(Logging.LogLevel.NORMAL, "Starting Game Session");
            Functions.NotifyControl("Starting Session", _activeSession);
            var gc = _activeSession.GetInitializedGameClients();
            if (gc != null)
            {
                foreach (var g in gc)
                {
                    g.GE.StartGame();
                }
            }

            _activeSession.GetClient(Client.ClientType.Wrapper)?.WE?.InitPipes();
        }

        public void PauseSession()
        {
            Functions.NotifyControl("Pausing Session", _activeSession);
            Logging.LogMsg(Logging.LogLevel.NORMAL, "Pausing Game Session");
            if (_handler.GameEndTimerActive)
            {
                _handler.GameEndStopwatch.Stop();
                _handler.GameEndTimer.Enabled = false;
                _handler.GameEndTimerRemaining = _handler.GameEndTimer.Interval -
                                             _handler.GameEndStopwatch.ElapsedMilliseconds;
            }
            if (_handler.SheetSequenceTimerActive)
            {
                _handler.SheetSequenceStopwatch.Stop();
                _handler.SheetSequenceTimer.Enabled = false;
                _handler.SheetSequenceTimerRemaining = _handler.SheetSequenceTimer.Interval -
                                                       _handler.SheetSequenceStopwatch.ElapsedMilliseconds;
            }
           
            var gc = _activeSession.GetInitializedGameClients();
            if (gc == null) return;
            foreach (var g in gc)
            {
                g.GE.PauseGame();
            }
        }

        public void ContinueSession()
        {
            Functions.NotifyControl("Continue Session", _activeSession);
            if (_handler.GameEndTimerActive)
            {
                _handler.GameEndStopwatch.Reset();
                _handler.GameEndTimer.Interval = _handler.GameEndTimerRemaining;
                _handler.GameEndTimer.Enabled = true;
                _handler.GameEndStopwatch.Start();
            }
            if (_handler.SheetSequenceTimerActive)
            {
                _handler.SheetSequenceStopwatch.Reset();
                _handler.SheetSequenceTimer.Interval = _handler.SheetSequenceTimerRemaining;
                _handler.SheetSequenceTimer.Enabled = true;
                _handler.SheetSequenceStopwatch.Start();
            }

            Logging.LogMsg(Logging.LogLevel.NORMAL, "Continue Game Session");
            var gc = _activeSession.GetInitializedGameClients();
            if (gc == null) return;
            foreach (var g in gc)
            {
                g.GE.ContinueGame();
            }
        }

        public void ForceEndGameSession()
        {
            _handler.GameEndStopwatch.Stop();
            _handler.GameEndStopwatch.Reset();
            _handler.GameEndTimer.Stop();
            _handler.SheetSequenceStopwatch.Stop();
            _handler.SheetSequenceStopwatch.Reset();
            _handler.SheetSequenceTimer.Stop();
            _handler.GameReportTimer.Stop();

            _handler.GameEndTimerActive = false;
            _handler.SheetSequenceTimerActive = false;

            _handler.SessionActive = false;

            Functions.NotifyControl("Ending Game Session", _activeSession);
            Logging.LogMsg(Logging.LogLevel.NORMAL, "Ending Game Session");

            var gc = _activeSession.GetInitializedGameClients();
            if (gc != null)
            {
                foreach (var g in gc)
                {
                    g.GE.StopGame();
                }
            }
            //if (_activeSession.ActivePhase < 3) _handler.NextPhase();
        }

        public void ReportGameStatus()
        {
            Logging.LogMsg(Logging.LogLevel.NORMAL, "Asking clients for game status");
            if (_handler.ActiveSession.Clients.Count <= 0) return;
            var gc = _handler.ActiveSession.GetInitializedGameClients();
            if (gc == null) return;
            foreach (var g in gc)
            {
                g.GE.GetGameStatus();
            }
        }

        public void ChangeSheet()
        {
            var newIdx = _activeSession.SessionConfig.ActiveSheetSquenceIdx + 1;
            if (_activeSession.SessionConfig.SheetSqeuence.Count <= newIdx) return;
            var newSheet = _activeSession.SessionConfig.SheetSqeuence[newIdx];
            _activeSession.SessionConfig.ActiveSheetSquenceIdx++;
            _activeSession.SessionConfig.ActiveSheet = newSheet.SheetId;
            Functions.NotifyControl("Changing Sheet to: " + newSheet.SheetId, _activeSession);
            Logging.LogMsg(Logging.LogLevel.NORMAL, "Changing Sheet to: {0}", newSheet.SheetId);
            var gc = _activeSession.GetInitializedGameClients();
            if (gc != null)
            {
                foreach (var g in gc)
                {
                    g.GE.UpdateSheet();
                }
            }
            if (_activeSession.SessionConfig.SheetSqeuence.Count <= newIdx + 1) return;
            var nextSheet = _activeSession.SessionConfig.SheetSqeuence[newIdx + 1];
            _handler.SheetSequenceStopwatch.Stop();
            _handler.SheetSequenceStopwatch.Reset();
            _handler.SheetSequenceTimer.Interval = nextSheet.SwitchTime;
            _handler.SheetSequenceTimer.Start();
            _handler.SheetSequenceStopwatch.Start();
        }

        public void ChangeSpecificSheet(int id)
        {
            Functions.NotifyControl("Changing Sheet to: " + id, _activeSession);
            Logging.LogMsg(Logging.LogLevel.NORMAL, "Changing Sheet to: {0}", id);
            _activeSession.SessionConfig.ActiveSheet = id;
            var gc = _activeSession.GetInitializedGameClients();
            if (gc == null) return;
            foreach (var g in gc)
            {
                g.GE.UpdateSheet();
            }
        }
    }
}
