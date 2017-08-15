namespace TouchTableServer.Model
{
    public class GameConfig
    {
        public int Timelimit;
        public int ActiveSheet;
        public bool TriggerInterrupt;
        public int InterruptDurotation;
    }

    public class StanzenConfig : GameConfig
    {
        
        public double NeutralCreditPercentage;
        public int BeltSpeed;
    }

    public class PressenConfig : GameConfig
    {
        public double NeutralCreditPercentage;
        public int BeltSpeed;
    }

    public class SchweissenConfig : GameConfig
    {
        public double NeutralCreditPercentage;
    }

    public class LogistikConfig : GameConfig
    {
        public int BeltSpeed;
    }

    public class KontrolleConfig : GameConfig
    {
        public int PotentialErrorRate;
    }

    public class KarosseriefertigungConfig : GameConfig
    {
        
    }

    public class EinkaufConfig : GameConfig
    {
        
    }

    public class MotorbauConfig : GameConfig
    {
    }

    public class LackierenConfig : GameConfig
    {
    }

    public class ElektrikConfig : GameConfig
    {
    }

    public class MontageConfig : GameConfig
    {
    }

    public class SystemueberwachungConfig : GameConfig
    {
        public double ErrorFrequency;
        public double AverageErrorSeverity;
    }
}
