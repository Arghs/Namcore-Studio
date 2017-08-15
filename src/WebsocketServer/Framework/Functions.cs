using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TouchTableServer.Model;
using TouchTableServer.Tools;

namespace TouchTableServer.Framework
{
    public static class Functions
    {
        public static void NotifyControl(string message, Session session)
        {
            session?.GetClient(Client.ClientType.ControlClient)?.SendMsg(message);
        }

        public static GameConfig GetGameConfig(Client.ClientType gameType, Config cfg)
        {
            switch (gameType)
            {
                case Client.ClientType.GameStanzen:
                    return new StanzenConfig
                    {
                        Timelimit = cfg.Timelimit,
                        NeutralCreditPercentage = 0.5,
                        ActiveSheet = cfg.ActiveSheet,
                        BeltSpeed = 1000
                    };
                case Client.ClientType.GamePressen:
                    return new PressenConfig()
                    {
                        Timelimit = cfg.Timelimit,
                        NeutralCreditPercentage = 0.5,
                        ActiveSheet = cfg.ActiveSheet,
                        BeltSpeed = 1000
                    };
                case Client.ClientType.GameSchweissen:
                    return new SchweissenConfig()
                    {
                        Timelimit = cfg.Timelimit,
                        NeutralCreditPercentage = 0.5,
                        ActiveSheet = cfg.ActiveSheet
                    };
                case Client.ClientType.GameLogistik:
                    return new LogistikConfig()
                    {
                        Timelimit = cfg.Timelimit,
                        ActiveSheet = cfg.ActiveSheet,
                        BeltSpeed = 4000
                    };
                case Client.ClientType.GameKontrolle:
                    return new KontrolleConfig()
                    {
                        Timelimit = cfg.Timelimit,
                        PotentialErrorRate = 50
                    };

                case Client.ClientType.GameKarosseriebau:
                    return new KarosseriefertigungConfig()
                    {
                        Timelimit = cfg.Timelimit
                    };
                case Client.ClientType.GameEinkauf:
                    return new EinkaufConfig()
                    {
                        Timelimit = cfg.Timelimit
                    };
                case Client.ClientType.GameMotorbau:
                    return new MotorbauConfig()
                    {
                        Timelimit = cfg.Timelimit
                    };
                case Client.ClientType.GameLackiererei:
                    return new LackierenConfig()
                    {
                        Timelimit = cfg.Timelimit
                    };
                case Client.ClientType.GameElektrik:
                    return new ElektrikConfig()
                    {
                        Timelimit = cfg.Timelimit
                    };
                case Client.ClientType.GameMontage:
                    return new MontageConfig()
                    {
                        Timelimit = cfg.Timelimit
                    };
                case Client.ClientType.GameSystemueberwachung:
                    return new SystemueberwachungConfig()
                    {
                        Timelimit = cfg.Timelimit
                    };
                default:
                    return new GameConfig();
            }
        }

        public static GameResponse GetGameResponse(Client.ClientType gameType, JToken payload)
        {
            GameResponse gs = new GameResponse();
            try
            {
                //switch (gameType)
                //{
                //    case Client.ClientType.GameStanzen:
                       
                //    case Client.ClientType.GamePressen:
                      
                //    case Client.ClientType.GameSchweissen:
                //    case Client.ClientType.GameLogistik:
                //    case Client.ClientType.GameKontrolle:
                //    case Client.ClientType.GameKarosseriebau:
                //    case Client.ClientType.GameEinkauf:
                //    case Client.ClientType.GameMotorbau:
                //    case Client.ClientType.GameLackiererei:
                //    case Client.ClientType.GameElektrik:
                //    case Client.ClientType.GameMontage:
                //    case Client.ClientType.GameSystemueberwachung:
                //    default:
                //        Logging.LogMsg(Logging.LogLevel.WARNING, "Failed to cast GameResponse. GameType not matching. GameType: {0}", gameType);
                //        return gs;
                //}
                return payload.ToObject<GameResponse>();
            }
            catch (Exception)
            {
                Logging.LogMsg(Logging.LogLevel.CRITICAL, "Failed to cast GameResponse. GameType: {0}", gameType);
                return null;
                throw;
            }
            
        }

        private static Client.ClientType GetClientTypeByWindowId(int phase, int windowId)
        {
            switch (phase)
            {
                case 1:
                    switch (windowId)
                    {
                        case 1:
                            return Client.ClientType.GameStanzen;
                        case 2:
                            return Client.ClientType.GamePressen;
                        case 3:
                            return Client.ClientType.GameSchweissen;
                        case 4:
                            return Client.ClientType.GameLogistik;
                    }
                    break;
                case 2:
                    switch (windowId)
                    {
                        case 1:
                            return Client.ClientType.GameKontrolle;
                        case 2:
                            return Client.ClientType.GameKarosseriebau;
                        case 3:
                            return Client.ClientType.GameEinkauf;
                        case 4:
                            return Client.ClientType.GameMotorbau;
                    }
                    break;
                case 3:
                    switch (windowId)
                    {
                        case 1:
                            return Client.ClientType.GameLackiererei;
                        case 2:
                            return Client.ClientType.GameSystemueberwachung;
                        case 3:
                            return Client.ClientType.GameElektrik;
                        case 4:
                            return Client.ClientType.GameMontage;
                    }
                    break;
            }
            return Client.ClientType.Invalid;
        }

        private static String GetJobByClientIdent(Client.ClientType ident)
        {
            switch (ident)
            {
                case Client.ClientType.GameStanzen:
                    return "TBD";
                case Client.ClientType.GamePressen:
                    return "TBD";
                case Client.ClientType.GameSchweissen:
                    return "TBD";
                case Client.ClientType.GameLogistik:
                    return "TBD";
                case Client.ClientType.GameKontrolle:
                    return "TBD";
                case Client.ClientType.GameKarosseriebau:
                    return "TBD";
                case Client.ClientType.GameEinkauf:
                    return "TBD";
                case Client.ClientType.GameMotorbau:
                    return "TBD";
                case Client.ClientType.GameLackiererei:
                    return "TBD";
                case Client.ClientType.GameElektrik:
                    return "TBD";
                case Client.ClientType.GameMontage:
                    return "TBD";
                case Client.ClientType.GameSystemueberwachung:
                    return "TBD";
                default:
                    return "Unbekannt";
            }
        }

        private static Dictionary<int, GameResponse> GetGameFeedbackByPhase(Session session)
        {
            int phase = session.ActivePhase;
            var feedbacks = new Dictionary<int, GameResponse>();
            for (var i = 1; i < 5; i++)
            {
                Client.ClientType key = GetClientTypeByWindowId(phase, i);
                if (session.GameEndResponses.ContainsKey(key))
                feedbacks.Add(i, session.GameEndResponses[key]);
            }
            return feedbacks;
        }   

        public static ShowFeedbackCommand GetGameFeedback(Session session)
        {
            int phase = session.ActivePhase;
            Dictionary<int, GameResponse> gameFeedbacks = GetGameFeedbackByPhase(session);
            ShowFeedbackCommand sdc = new ShowFeedbackCommand();
            sdc.Phase = phase;
            sdc.WindowFeedbacks = new List<SetFeedbackCommand>();
            foreach (KeyValuePair<int, GameResponse> kvp in gameFeedbacks)
            {
                SetFeedbackCommand sfc = new SetFeedbackCommand();
                sfc.WindowId = kvp.Key;
                sfc.JobTitle = GetJobByClientIdent(GetClientTypeByWindowId(phase, kvp.Key));
                sfc.TotalScore = kvp.Value.CommulativeScore;
                if (sfc.TotalScore <= 100) sfc.FeedbackGrade = FeedbackStatus.SEHRGUT;
                if (sfc.TotalScore < 2 / 3 * 100) sfc.FeedbackGrade = FeedbackStatus.GUT;
                if (sfc.TotalScore < 1 / 3 * 100) sfc.FeedbackGrade = FeedbackStatus.SCHLECHT;
                sfc.GradeSet = 1;
                sfc.CorrectAttempts = kvp.Value.CorrectAttempts;
                sfc.BadAttempts = kvp.Value.FailedAttempts;
                sfc.AveragePrecision = kvp.Value.AveragePrecision;
                sdc.WindowFeedbacks.Add(sfc);
            }
            return sdc;
        }

        public static ShowFeedbackCommand GetRandomFeedback(Session session)
        {
            int phase = session.ActivePhase;
            ShowFeedbackCommand sdc = new ShowFeedbackCommand();
            sdc.Phase = phase;
            sdc.WindowFeedbacks = new List<SetFeedbackCommand>();
            Random r = new Random();
            for (int i = 1; i < 5; i++)
            {
                SetFeedbackCommand sfc = new SetFeedbackCommand();
                sfc.WindowId = i;
                sfc.JobTitle = GetJobByClientIdent(GetClientTypeByWindowId(phase, i));
                sfc.TotalScore = r.NextDouble() * 100;
                if (sfc.TotalScore <= 100) sfc.FeedbackGrade = FeedbackStatus.SEHRGUT;
                if (sfc.TotalScore < 2/3*100) sfc.FeedbackGrade = FeedbackStatus.GUT;
                if (sfc.TotalScore < 1/3*100) sfc.FeedbackGrade = FeedbackStatus.SCHLECHT;
                sfc.GradeSet = r.Next(1, 2);
                sfc.CorrectAttempts = r.Next(0, 50);
                sfc.BadAttempts = r.Next(0, 50);
                sfc.AveragePrecision = sfc.CorrectAttempts / (double) (sfc.CorrectAttempts + sfc.BadAttempts) * 100;
                sdc.WindowFeedbacks.Add(sfc);
            }
            return sdc;
        }

        public static ShowTeamFeedbackCommand GetTeamFeedback(Session session)
        {
            ShowTeamFeedbackCommand sdc = new ShowTeamFeedbackCommand();
            sdc.WindowFeedbacks = new List<SetTeamFeedbackCommand>();
            Random r = new Random();
            for (int i = 1; i < 5; i++)
            {
                SetTeamFeedbackCommand sfc = new SetTeamFeedbackCommand();
                sfc.WindowId = i;
                sfc.Phase1Score = r.NextDouble() * 100;
                sfc.Phase2Score = r.NextDouble() * 100;
                sfc.Phase3Score = r.NextDouble() * 100;
                sfc.TotalScore = (sfc.Phase1Score + sfc.Phase2Score + sfc.Phase3Score) / 3;
                if (sfc.TotalScore <= 100) sfc.FeedbackGrade = FeedbackStatus.SEHRGUT;
                if (sfc.TotalScore < (200 / 3.0)) sfc.FeedbackGrade = FeedbackStatus.GUT;
                if (sfc.TotalScore < (100 / 3.0)) sfc.FeedbackGrade = FeedbackStatus.SCHLECHT;

                sdc.WindowFeedbacks.Add(sfc);
            }
            return sdc;
        }

        public static ShowTeamFeedbackCommand GetRandomTeamFeedback(Session session)
        {
            ShowTeamFeedbackCommand sdc = new ShowTeamFeedbackCommand();
            sdc.WindowFeedbacks = new List<SetTeamFeedbackCommand>();
            Random r = new Random();
            for (int i = 1; i < 5; i++)
            {
                SetTeamFeedbackCommand sfc = new SetTeamFeedbackCommand();
                sfc.WindowId = i;
                sfc.Phase1Score = r.NextDouble() * 100;
                sfc.Phase2Score = r.NextDouble() * 100;
                sfc.Phase3Score = r.NextDouble() * 100;
                sfc.TotalScore = (sfc.Phase1Score + sfc.Phase2Score + sfc.Phase3Score) / 3;
                if (sfc.TotalScore <= 100) sfc.FeedbackGrade = FeedbackStatus.SEHRGUT;
                if (sfc.TotalScore < (200 / 3.0)) sfc.FeedbackGrade = FeedbackStatus.GUT;
                if (sfc.TotalScore < (100 / 3.0)) sfc.FeedbackGrade = FeedbackStatus.SCHLECHT;

                sdc.WindowFeedbacks.Add(sfc);
            }
            return sdc;
        }

    }
}
