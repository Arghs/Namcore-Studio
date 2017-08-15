using System;
using System.Collections.Generic;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TouchTableServer.Framework;
using TouchTableServer.Tools;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace TouchTableServer.Model
{
    public class Client : WebSocketBehavior
    {
        public enum ClientType
        {
            Invalid = 0,
            ControlClient = 1,
            Wrapper = 2,

            GameStanzen = 3,
            GamePressen = 4,
            GameSchweissen = 5,
            GameLogistik = 6,

            GameKontrolle = 7,
            GameKarosseriebau = 8,
            GameEinkauf = 9,
            GameMotorbau = 10,

            GameLackiererei = 11,
            GameElektrik = 12,
            GameMontage = 13,
            GameSystemueberwachung = 14
        };
        
        private static int _number;
        private int _targetSession;
        public GameEvents GE;
        public bool Initialized;
        public RemoteControlHandler RCH;
        public WrapperEvents WE;
        public int Id { get; set; }
        public ClientType ClientIdent { get; set; }

        public int WindowId
        {
            get
            {
                switch (ClientIdent)
                {
                    case ClientType.GameStanzen:
                    case ClientType.GameKontrolle:
                    case ClientType.GameLackiererei:
                        return 1;
                    case ClientType.GamePressen:
                    case ClientType.GameKarosseriebau:
                    case ClientType.GameElektrik:
                        return 2;
                    case ClientType.GameSchweissen:
                    case ClientType.GameEinkauf:
                    case ClientType.GameMontage:
                        return 3;
                    case ClientType.GameLogistik:
                    case ClientType.GameMotorbau:
                    case ClientType.GameSystemueberwachung:
                        return 4;
                    default:
                        return 1;

                }
            }
        }

        public int Version { get; set; }
        public int GroupId { get; set; }
        public Session SessionPointer { get; set; }
        public int ActiveUserSheet { get; set; }
        public bool UserReady { get; set; }

        protected override void OnClose(CloseEventArgs e)
        {
            //Sessions.Broadcast(String.Format("{0} got logged off...", ClientIdent.ToString()));
            SessionPointer.Clients.Remove(ClientIdent);
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            if (ClientIdent == ClientType.ControlClient)
            {
                Logging.LogMsg(Logging.LogLevel.NORMAL, "ControlClient: {0}", e.Data);
                RCH?.HandleMessage(e.Data);
                return;
            }
            JToken token = JObject.Parse(e.Data);
            try
            {
                var opcode = (Opcodes.ClientOpcodes)(int)token.SelectToken("opcode");
                HandleOpcode(opcode, token);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
            
            //Sessions.Broadcast(String.Format("{0}: {1}", ClientIdent.ToString(), e.Data));
        }

        protected void HandleOpcode(Opcodes.ClientOpcodes opcode, JToken data)
        {
            Logging.LogMsg(Logging.LogLevel.DEBUG, "Handling Opcode {0}", opcode);
            var payload = data.SelectToken("payload");
            GameResponse gs;
            switch (opcode)
            {
                case Opcodes.ClientOpcodes.CMSG_CONNECTION_READY:
                    if ((int) ClientIdent >= 2 && (int) ClientIdent <= 14)
                    {
                        try
                        {
                            Version = (int) payload.SelectToken("senderVersion");
                            GroupId = (int) payload.SelectToken("groupId");
                            if (_targetSession > 0) GroupId = _targetSession;
                        }
                        catch (Exception)
                        {
                            Logging.LogMsg(Logging.LogLevel.CRITICAL, "Invalid Packet from Client: {0}", ClientIdent);
                            SendMsg(GetErrorCmd(Opcodes.ServerOpcodes.SMSG_ERR_INVALID_PACKET));
                            return;
                            throw;
                        }
                        if (!SessionHandler.AvailableSessions.ContainsKey(GroupId))
                        {
                            Logging.LogMsg(Logging.LogLevel.CRITICAL, "No Session found for GroupId: {0}, Client: {1}",
                                GroupId, ClientIdent);
                            SendMsg(GetErrorCmd(Opcodes.ServerOpcodes.SMSG_ERR_NO_SESSION_FOR_GROUPID));
                        }
                        else
                        {
                            SessionPointer = SessionHandler.AvailableSessions[GroupId];
                            if (!SessionPointer.CheckCompatibility(ClientIdent))
                            {
                                Logging.LogMsg(Logging.LogLevel.CRITICAL,
                                    "Invalid Client in this phase. Client: {0}, Phase: {1}", ClientIdent,
                                    SessionPointer.ActivePhase);
                                SendMsg(GetErrorCmd(Opcodes.ServerOpcodes.SMSG_ERR_CLIENT_NOT_SUPPORTED));
                                break;
                            }
                            if (!SessionPointer.HandlerInstance.AddClientToSession(this))
                            {
                                Logging.LogMsg(Logging.LogLevel.CRITICAL, "Blocking Client connection, Client: {0}",
                                    ClientIdent);
                                SendMsg(GetErrorCmd(Opcodes.ServerOpcodes.SMSG_ERR_CONNECTION_BLOCKED));
                            }
                            else
                            {
                                Logging.LogMsg(Logging.LogLevel.NORMAL, "Adding Client to Session: {0}, Client: {1}",
                                    GroupId, ClientIdent);
                                if (ClientIdent == ClientType.Wrapper)
                                    WE.StartIntro();
                                else if (ClientIdent != ClientType.ControlClient)
                                    GE.InitGame();
                            }
                        }
                        //Sessions.SendTo(GetInitOpcode(), Id.ToString());
                    }
                    break;
                case Opcodes.ClientOpcodes.CMSG_GAME_INITIALIZED:
                    Initialized = true;
                    break;
                case Opcodes.ClientOpcodes.CMSG_USER_READY:
                    if (!Initialized)
                    {
                        SendMsg(GetErrorCmd(Opcodes.ServerOpcodes.SMSG_ERR_CLIENT_NOT_INITIALIZED));
                        return;
                    }
                    UserReady = true;
                    if (SessionPointer.HandlerInstance.CheckAllClientsReady() && !SessionPointer.HandlerInstance.SessionActive)
                        SessionPointer.HandlerInstance.SessionEvents.StartGameSession(SessionPointer.ActivePhase);
                    break;
                case Opcodes.ClientOpcodes.CMSG_GAME_USER_CHANGED_SHEET:
                    try
                    {
                        ActiveUserSheet = (int) payload.SelectToken("ActiveUserSheet");
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    break;
                case Opcodes.ClientOpcodes.CMSG_GAME_REPORT_STATE:
                    if (!Initialized)
                    {
                        SendMsg(GetErrorCmd(Opcodes.ServerOpcodes.SMSG_ERR_CLIENT_NOT_INITIALIZED));
                        return;
                    }
                    gs = Functions.GetGameResponse(ClientIdent, payload);
                    if (gs == null)
                    {
                        SendMsg(GetErrorCmd(Opcodes.ServerOpcodes.SMSG_ERR_INVALID_PACKET,
                            "Ungültiges GameResponse Format."));
                    }
                    else
                    {
                        if (SessionPointer.GameUpdateResponse.ContainsKey(ClientIdent))
                            SessionPointer.GameUpdateResponse[ClientIdent] = gs;
                        else
                        {
                            try
                            {
                                SessionPointer.GameUpdateResponse.Add(ClientIdent, gs);
                            } catch(Exception e) { }
                           
                        }
                        SessionPointer.HandlerInstance.ActiveSession.GetClient(Client.ClientType.Wrapper)?.WE.UpdatePipes(this);

                    }
                    break;
                case Opcodes.ClientOpcodes.CMSG_GAME_END:
                    if (!Initialized)
                    {
                        SendMsg(GetErrorCmd(Opcodes.ServerOpcodes.SMSG_ERR_CLIENT_NOT_INITIALIZED));
                        return;
                    }
                    gs = Functions.GetGameResponse(ClientIdent, payload);
                    if (gs == null)
                    {
                        SendMsg(GetErrorCmd(Opcodes.ServerOpcodes.SMSG_ERR_INVALID_PACKET,
                            "Ungültiges GameResponse Format."));
                    }
                    else
                    {
                        if (SessionPointer.GameEndResponses.ContainsKey(ClientIdent))
                            SessionPointer.GameEndResponses[ClientIdent] = gs;
                        else
                            SessionPointer.GameEndResponses.Add(ClientIdent, gs);
                        SessionPointer.HandlerInstance.SessionEvents.ShowGameFeedback(SessionPointer.ActivePhase);
                    }
                    break;
                default:
                    // Invalid Opcode
                    Logging.LogMsg(Logging.LogLevel.WARNING, "Unknown Opcode: {0}, Client: {1}", opcode, ClientIdent);
                    SendMsg(GetErrorCmd(Opcodes.ServerOpcodes.SMSG_ERR_UNKNOWN_OPCODE));
                    Sessions.CloseSession(ID);
                    break;
            }
        }

        public void SendMsg(string msg)
        {
            if (ClientIdent != ClientType.ControlClient)
                Functions.NotifyControl($"[TO:{ClientIdent}] " + msg, SessionPointer);
            Send(msg);
        }

        public string AddPayload(string cmd, JToken payload)
        {
            JToken token = JObject.Parse(cmd);
            token["payload"] = payload;
            return JsonConvert.SerializeObject(token);
        }

        public string GetOpcodeCmd(Opcodes.ServerOpcodes opcode)
        {
            var cmd = new JObject
            {
                {"opcode", JToken.FromObject(opcode)},
                {"senderIdent", JToken.FromObject(0)},
                {"payload", null}
            };
            return JsonConvert.SerializeObject(cmd);
        }

        private string GetErrorCmd(Opcodes.ServerOpcodes opcode, string message = null)
        {
            if (message == null)
            {
                switch (opcode)
                {
                    case Opcodes.ServerOpcodes.SMSG_ERR_CONNECTION_BLOCKED:
                        message = "Der Server hat die Client Verbindung unterbrochen.";
                        break;
                    case Opcodes.ServerOpcodes.SMSG_ERR_INVALID_PACKET:
                        message = "Ungültige Anforderung an den Server.";
                        break;
                    case Opcodes.ServerOpcodes.SMSG_ERR_NO_SESSION_FOR_GROUPID:
                        message = "Für die angegebene GroupId konnte keine Sitzung gefunden werden.";
                        break;
                    case Opcodes.ServerOpcodes.SMSG_ERR_UNKNOWN_OPCODE:
                        message = "Unbekannter Opcode.";
                        break;
                    case Opcodes.ServerOpcodes.SMSG_ERR_INVALID_CLIENTID:
                        message = "Ungültige ClientID.";
                        break;
                    case Opcodes.ServerOpcodes.SMSG_ERR_CLIENT_NOT_INITIALIZED:
                        message = "Client nicht initialisiert.";
                        break;
                    case Opcodes.ServerOpcodes.SMSG_ERR_CLIENT_NOT_SUPPORTED:
                        message = "Client in dieser Spiel-Phase nicht unterstützt.";
                        break;
                }
            }
            var cmd = new JObject
            {
                {"opcode", JToken.FromObject(opcode)},
                {"senderIdent", JToken.FromObject(0)},
                {"payload", JToken.FromObject(new ClientError {Message = message})}
            };
            return JsonConvert.SerializeObject(cmd);
        }

        private void GetClientType()
        {
            try
            {
                var id = Context.QueryString["id"];
                if (Context.QueryString.Contains("session"))
                {
                    var session = Context.QueryString["session"];
                    _targetSession = int.Parse(session);
                }
                ClientIdent = (ClientType) int.Parse(id);
                Id = Interlocked.Increment(ref _number);
                if (ClientIdent == ClientType.ControlClient)
                {
                    RCH = new RemoteControlHandler(this);
                    SessionPointer = SessionHandler.AvailableSessions[_targetSession];
                    SessionPointer.HandlerInstance.AddClientToSession(this);
                }
                if (ClientIdent == ClientType.Wrapper) WE = new WrapperEvents(this);
                GE = new GameEvents(this);
                var master = GetOpcodeCmd(Opcodes.ServerOpcodes.SMSG_HANDSHAKE_CLIENT);
                var cmd = JToken.FromObject(new HandshakeClient());
                SendMsg(AddPayload(master, cmd));
            }
            catch (Exception)
            {
                SendMsg(GetErrorCmd(Opcodes.ServerOpcodes.SMSG_ERR_INVALID_CLIENTID));
                throw;
            }
        }

        protected override void OnOpen()
        {
            UserReady = false;
            GetClientType();
        }

        public Client SyncClient(ref Client c)
        {
            c.Initialized = Initialized;
            c.GroupId = GroupId;
            c.Id = Id;
            c.SessionPointer = SessionPointer;
            c.Version = Version;
            c.UserReady = UserReady;
            return c;
        }
    }
}