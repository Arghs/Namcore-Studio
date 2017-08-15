using System.Collections.Generic;

namespace TouchTableServer.Framework
{
    public class Config
    {
        public int Timelimit { get; set; }
        public int InterruptDurotation { get; set; }
        public int ActiveSheet { get; set; }
        public int ActiveSheetSquenceIdx { get; set; }
        public List<SheetTime> SheetSqeuence { get; set; }

        public void LoadConfig()
        {
            // TODO: Load from DB
            Timelimit = 400;
            InterruptDurotation = 3;
            ActiveSheetSquenceIdx = 0;

            SheetSqeuence = new List<SheetTime>();
            SheetSqeuence.Add(new SheetTime(0, 1));
            SheetSqeuence.Add(new SheetTime(5000, 2));
            SheetSqeuence.Add(new SheetTime(5000, 3));

            ActiveSheet = SheetSqeuence[ActiveSheetSquenceIdx].SheetId;
        }
    }

    public struct SheetTime
    {
        public int SwitchTime;
        public int SheetId;

        public SheetTime(int time, int id)
        {
            SwitchTime = time;
            SheetId = id;
        }
    }
}
