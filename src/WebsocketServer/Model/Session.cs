using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using TouchTableServer.Framework;

namespace TouchTableServer.Model
{
    public class Session
    {
        public Session()
        {
            Guid = Guid.NewGuid();
        }

        public Guid Guid;
        public int GroupId;
        public int ActivePhase;
        public bool IsTruckSession;
        public DateTime StartTime;
        public DateTime EndTime;
        public Config SessionConfig;
        public SessionHandler HandlerInstance;

        public Dictionary<Client.ClientType, Client> Clients;
        public Dictionary<Client.ClientType, GameResponse> GameUpdateResponse;
        public Dictionary<Client.ClientType, GameResponse> GameEndResponses;

        public Client GetClient(Client.ClientType ctype)
        {
            return Clients.ContainsKey(ctype) ? Clients[ctype] : null;
        }

        public List<Client> GetGameClients()
        {
            return Clients.Values.Where(ctype => ctype.ClientIdent != Client.ClientType.ControlClient).ToList();
        }

        public List<Client> GetInitializedGameClients()
        {
            return Clients.Values.Where(ctype => ctype.ClientIdent != Client.ClientType.ControlClient && ctype.Initialized).ToList();
        }

        public void SendToClient(Client.ClientType ctype, string msg)
        {
            if (Clients.ContainsKey(ctype)) Clients[ctype].SendMsg(msg);
        }

        public bool CheckCompatibility(Client.ClientType ctype)
        {
            if (ctype == Client.ClientType.ControlClient) return true;
            if (ctype == Client.ClientType.Wrapper) return true;
            switch (ActivePhase)
            {
                case 1:
                    switch (ctype)
                    {
                        case Client.ClientType.GameStanzen:
                        case Client.ClientType.GamePressen:
                        case Client.ClientType.GameSchweissen:
                        case Client.ClientType.GameLogistik:
                            return true;
                    }
                    break;
                case 2:
                    switch (ctype)
                    {
                        case Client.ClientType.GameKontrolle:
                        case Client.ClientType.GameKarosseriebau:
                        case Client.ClientType.GameEinkauf:
                        case Client.ClientType.GameMotorbau:
                            return true;
                    }
                    break;
                case 3:
                    switch (ctype)
                    {
                        case Client.ClientType.GameLackiererei:
                        case Client.ClientType.GameElektrik:
                        case Client.ClientType.GameMontage:
                        case Client.ClientType.GameSystemueberwachung:
                            return true;
                    }
                    break;
            }
            return false;
        }
    }
}
