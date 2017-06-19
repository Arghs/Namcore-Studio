using System;
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
                        BeltSpeed = 100,
                        TopHitZone = 100,
                        MidHitZone = 500
                    };
                case Client.ClientType.GamePressen:
                    return new PressenConfig()
                    {
                        Timelimit = cfg.Timelimit,
                        NeutralCreditPercentage = 0.5,
                        ActiveSheet = cfg.ActiveSheet,
                        BeltSpeed = 100,
                        TopHitZone = 100,
                        MidHitZone = 500
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
                        BeltSpeed = 100
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
                default:
                    return new GameConfig();
            }
        }

        public static GameResponse GetGameResponse(Client.ClientType gameType, JToken payload)
        {
            GameResponse gs = new GameResponse();
            try
            {
                switch (gameType)
                {
                    case Client.ClientType.GameStanzen:
                        return payload.ToObject<StanzenResponse>();
                    case Client.ClientType.GamePressen:
                        return payload.ToObject<PressenResponse>();
                    case Client.ClientType.GameSchweissen:
                        return payload.ToObject<SchweissenResponse>();
                    case Client.ClientType.GameLogistik:
                        return payload.ToObject<LogistikResponse>();
                    case Client.ClientType.GameKontrolle:
                        return payload.ToObject<KontrolleResponse>();
                    case Client.ClientType.GameKarosseriebau:
                        return payload.ToObject<KarosseriefertigungResponse>();
                    case Client.ClientType.GameEinkauf:
                        return payload.ToObject<EinkaufResponse>();
                    case Client.ClientType.GameMotorbau:
                        return payload.ToObject<MotorbauResponse>();
                    case Client.ClientType.GameLackiererei:
                        return payload.ToObject<LackierenResponse>();
                    case Client.ClientType.GameElektrik:
                        return payload.ToObject<ElektrikResponse>();
                    case Client.ClientType.GameMontage:
                        return payload.ToObject<MontageResponse>();
                    case Client.ClientType.GameSystemueberwachung:
                        return payload.ToObject<SystemueberwachungResponse>();
                    default:
                        Logging.LogMsg(Logging.LogLevel.WARNING, "Failed to cast GameResponse. GameType not matching. GameType: {0}", gameType);
                        return gs;
                }
            }
            catch (Exception)
            {
                Logging.LogMsg(Logging.LogLevel.CRITICAL, "Failed to cast GameResponse. GameType: {0}", gameType);
                return null;
                throw;
            }
            
        }

    }
}
