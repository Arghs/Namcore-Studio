using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouchTableServer.Model
{
    public class ShowFeedbackCommand
    {
        public int Phase { get; set; }

    }

    public class StartIntroCommand
    {
        public int Phase { get; set; }
    }

    public class LoadGamesCommand
    {
        public int Phase { get; set; }
    }

    public class InitPipesCommand
    {
        public int Phase { get; set; }
        public List<SetPipeColorCommand> Pipes { get; set; }
    }

    public class SetPipeColorCommand
    {
        public int PipeStartPoint { get; set; }
        public int PipeEndPoint { get; set; }
        public PipeStatus PipeStatus { get; set; }
    }

    public enum PipeStatus
    {
        NORMAL = 1,
        WARNING = 2,
        CRITICAL = 3
    }
}
