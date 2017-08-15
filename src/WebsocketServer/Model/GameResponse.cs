namespace TouchTableServer.Model
{
    public class GameResponse
    {
        public int CorrectAttempts;
        public int FailedAttempts;
        public double AveragePrecision;

        public int CommulativeScore
        {
            get { return (int) AveragePrecision; }
        }

    

        public void CalcResults()
        {
          
        }
    }
   
}
