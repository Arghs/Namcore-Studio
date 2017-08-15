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
        public List<SetFeedbackCommand> WindowFeedbacks { get; set; }
    }

    public class ShowTeamFeedbackCommand
    {
        public List<SetTeamFeedbackCommand> WindowFeedbacks { get; set; }
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
        public PipeIdent PipeId { get; set; }
        public PipeStatus PipeStatus { get; set; }
    }

    public class SetFeedbackCommand
    {
        public int WindowId { get; set; }
        public String JobTitle { get; set; }
        public double TotalScore { get; set; }
        public FeedbackStatus FeedbackGrade { get; set; }
        public int GradeSet { get; set; }
        public int CorrectAttempts { get; set; }
        public int BadAttempts { get; set; }
        public double AveragePrecision { get; set; }
    }

    public class SetTeamFeedbackCommand
    {
        public int WindowId { get; set; }
        public double TotalScore { get; set; }
        public FeedbackStatus FeedbackGrade { get; set; }
        public double Phase1Score { get; set; }
        public double Phase2Score { get; set; }
        public double Phase3Score { get; set; }
    }

    public enum FeedbackStatus
    {
        SCHLECHT = 1,
        GUT = 2,
        SEHRGUT = 3
    }

    public enum PipeIdent
    {
        PIPE1 = 1, // In Window 3
        PIPE2 = 2, // Connects Window 3 and 4
        PIPE3 = 3, // Connects Window 4 and 2
        PIPE4 = 4, // Connects Window 2 and 1
        PIPE5 = 5 // Out Window 1
    }

    public enum PipeStatus
    {
        INVISIBLE = 0,
        NORMAL = 1,
        WARNING = 2,
        CRITICAL = 3
    }
}
