using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using TouchTableServer.Framework;

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

        //public void InitPipes()
        //{
        //    string master = _client.GetOpcodeCmd(Opcodes.ServerOpcodes.SMSG_WRAPPER_INIT_PIPES);
        //    List<SetPipeColorCommand> spcc = new List<SetPipeColorCommand>
        //    {
        //        new SetPipeColorCommand() {PipeStartPoint = 1, PipeEndPoint = 3, PipeStatus = PipeStatus.NORMAL},
        //        new SetPipeColorCommand() {PipeStartPoint = 3, PipeEndPoint = 4, PipeStatus = PipeStatus.NORMAL},
        //        new SetPipeColorCommand() {PipeStartPoint = 4, PipeEndPoint = 2, PipeStatus = PipeStatus.NORMAL}
        //    };
        //    JToken cmd = JToken.FromObject(new InitPipesCommand()
        //    {
        //        Phase = _client.SessionPointer.ActivePhase,
        //        Pipes = spcc
        //    });
        //    _client.SendMsg(_client.AddPayload(master, cmd));
        //}

        public void UpdatePipes(Client c)
        {
            GameResponse gr = c.SessionPointer.GameUpdateResponse[c.ClientIdent];
            PipeStatus status = PipeStatus.NORMAL;
            if (gr.CommulativeScore <= 100) status = PipeStatus.NORMAL;
            if ((double) gr.CommulativeScore < 2.0 * 100.0 / 3.0) status = PipeStatus.WARNING;
            if ((double) gr.CommulativeScore < 1.0 * 100.0 / 3.0) status = PipeStatus.CRITICAL;
            SetPipe((PipeIdent) (c.WindowId + 1), status);
        }

        public void SetupPipes(PipeStatus s = PipeStatus.NORMAL)
        {
            for (var i = 1; i <= 5; i++)
            {
                Thread.Sleep(1);
                SetPipe((PipeIdent) i, s);
            }
        }

        public void SetPipe(PipeIdent id, PipeStatus status)
        {
            string master = _client.GetOpcodeCmd(Opcodes.ServerOpcodes.SMSG_WRAPPER_SET_PIPE);
            JToken cmd = JToken.FromObject(new SetPipeColorCommand()
            {
                PipeId = id,
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

        public void ShowFeedback(bool test = false)
        {
            string master = _client.GetOpcodeCmd(Opcodes.ServerOpcodes.SMSG_WRAPPER_SHOW_FEEDBACK);
            var cmd = JToken.FromObject(test ? Functions.GetRandomFeedback(_client.SessionPointer) : Functions.GetGameFeedback(_client.SessionPointer));
            _client.SendMsg(_client.AddPayload(master, cmd));
        }

        public void ShowTeamFeedback(bool test = false)
        {
            string master = _client.GetOpcodeCmd(Opcodes.ServerOpcodes.SMSG_WRAPPER_SHOW_TEAMFEEDBACK);
            var cmd = JToken.FromObject(test ? Functions.GetRandomTeamFeedback(_client.SessionPointer) : Functions.GetRandomTeamFeedback(_client.SessionPointer));
            _client.SendMsg(_client.AddPayload(master, cmd));
        }
    }
}
