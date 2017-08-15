using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using TouchTableServer.Framework;
using TouchTableServer.Tools;

namespace TouchTableServer.Model
{
    public class GameEvents
    {
        private readonly Client _client;

        public GameEvents(Client c)
        {
            _client = c;
        }

        public void StartGame()
        {
            _client.SendMsg(_client.GetOpcodeCmd(Opcodes.ServerOpcodes.SMSG_START_GAME));
        }

        public void InitGame()
        {
            _client.SendMsg(GetCommand_GameConfig());
        }

        public void GetGameStatus()
        {
            _client.SendMsg(_client.GetOpcodeCmd(Opcodes.ServerOpcodes.SMSG_REPORT_GAME_STATE));
        }
       
        public void UpdateSheet()
        {
            _client.SendMsg(GetCommand_GameConfig(Opcodes.ServerOpcodes.SMSG_GAME_UPDATE));
        }

        public void PauseGame()
        {
            string master = _client.GetOpcodeCmd(Opcodes.ServerOpcodes.SMSG_PAUSE_GAME);
            JToken cmd = JToken.FromObject(new PauseCmd() {Message = "Test"});
            _client.SendMsg(_client.AddPayload(master, cmd));
        }

        public void ContinueGame()
        {
            _client.SendMsg(_client.GetOpcodeCmd(Opcodes.ServerOpcodes.SMSG_CONTINUE_SESSION));
        }

        public void StopGame()
        {
            _client.UserReady = false;
            _client.SendMsg(_client.GetOpcodeCmd(Opcodes.ServerOpcodes.SMSG_STOP_GAME));
        }

        public void InterruptGame()
        {
            Functions.NotifyControl("Interrupting Game: " + _client.ClientIdent, _client.SessionPointer);
            Logging.LogMsg(Logging.LogLevel.NORMAL, "Interrupting Game client. Client: {0}, Session: {1}", _client.ClientIdent, _client.SessionPointer.GroupId);
            string master = _client.GetOpcodeCmd(Opcodes.ServerOpcodes.SMSG_GAME_UPDATE);
            var config = Functions.GetGameConfig(_client.ClientIdent, _client.SessionPointer.SessionConfig);
            config.TriggerInterrupt = true;
            config.InterruptDurotation = _client.SessionPointer.SessionConfig.InterruptDurotation;
            JToken cmd = JToken.FromObject(config);
            _client.SendMsg(_client.AddPayload(master, cmd));
        }

        private string GetCommand_GameConfig(Opcodes.ServerOpcodes opcode = Opcodes.ServerOpcodes.SMSG_INIT_GAME)
        {
            string master = _client.GetOpcodeCmd(opcode);
            JToken cmd = JToken.FromObject(Functions.GetGameConfig(_client.ClientIdent, _client.SessionPointer.SessionConfig));
            return _client.AddPayload(master, cmd);
        }
    }
}
