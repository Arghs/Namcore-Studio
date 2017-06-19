using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TouchTableServer.Model
{
    public class WrapperEvents
    {
        private readonly Client _client;

        public WrapperEvents(Client c)
        {
            _client = c;
        }

        public void StartIntro()
        {
            string master = _client.GetOpcodeCmd(Opcodes.ServerOpcodes.SMSG_WRAPPER_START_INTRO);
            JToken cmd = JToken.FromObject(new LoadGamesCommand() { Phase = _client.SessionPointer.ActivePhase });
            _client.SendMsg(_client.AddPayload(master, cmd));
        }

        public void InitPipes()
        {
            string master = _client.GetOpcodeCmd(Opcodes.ServerOpcodes.SMSG_WRAPPER_INIT_PIPES);
            List<SetPipeColorCommand> spcc = new List<SetPipeColorCommand>
            {
                new SetPipeColorCommand() {PipeStartPoint = 1, PipeEndPoint = 3, PipeStatus = PipeStatus.NORMAL},
                new SetPipeColorCommand() {PipeStartPoint = 3, PipeEndPoint = 4, PipeStatus = PipeStatus.NORMAL},
                new SetPipeColorCommand() {PipeStartPoint = 4, PipeEndPoint = 2, PipeStatus = PipeStatus.NORMAL}
            };
            JToken cmd = JToken.FromObject(new InitPipesCommand()
            {
                Phase = _client.SessionPointer.ActivePhase,
                Pipes = spcc
            });
            _client.SendMsg(_client.AddPayload(master, cmd));
        }

        public void SetPipe(int start, int end, PipeStatus status)
        {
            string master = _client.GetOpcodeCmd(Opcodes.ServerOpcodes.SMSG_WRAPPER_SET_PIPE);
            JToken cmd = JToken.FromObject(new SetPipeColorCommand()
            {
                PipeStartPoint = start,
                PipeEndPoint = end,
                PipeStatus = status
            });
            _client.SendMsg(_client.AddPayload(master, cmd));
        }

        public void LoadGames()
        {
            string master = _client.GetOpcodeCmd(Opcodes.ServerOpcodes.SMSG_WRAPPER_LOAD_GAMES);
            JToken cmd = JToken.FromObject(new LoadGamesCommand() { Phase = _client.SessionPointer.ActivePhase });
            _client.SendMsg(_client.AddPayload(master, cmd));
        }
    }
}
