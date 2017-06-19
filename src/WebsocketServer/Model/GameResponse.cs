namespace TouchTableServer.Model
{
    public class GameResponse
    {
        
    }

    public class StanzenResponse : GameResponse
    {
        public int CorrectSheetCount;
        public int MissedSheetCount;
        public double AveragePrecision;
    }

    public class PressenResponse : GameResponse
    {
        public int CorrectSheetCount;
        public int MissedSheetCount;
        public double AveragePrecision;
    }

    public class SchweissenResponse : GameResponse
    {
        public int TotalSheetCount;
        public double AveragePrecision;
    }

    public class LogistikResponse : GameResponse
    {
        public int CorrectPackageCount;
        public double WrongPackageCount;
    }

    public class KontrolleResponse : GameResponse
    {
        public int CorrectDecisionCount;
        public int BadSheetsPassedCount;
        public int GoodSheetsSortedOutCount;
    }

    public class KarosseriefertigungResponse : GameResponse
    {
        public int CorrectAttempts;
        public int FailedAttempts;
    }

    public class EinkaufResponse : GameResponse
    {
        public int CorrectAttempts;
        public int FailedAttempts;
    }

    public class MotorbauResponse : GameResponse
    {
        public int CorrectAttempts;
        public int FailedAttempts;
    }

    public class LackierenResponse : GameResponse
    {
        public int CorrectAttempts;
        public int FailedAttempts;
    }

    public class ElektrikResponse : GameResponse
    {
        public int CorrectAttempts;
        public int FailedAttempts;
    }

    public class MontageResponse : GameResponse
    {
        public int CorrectAttempts;
        public int FailedAttempts;
    }

    public class SystemueberwachungResponse : GameResponse
    {
        public int CorrectAttempts;
        public int PlayerScore;
    }
}
