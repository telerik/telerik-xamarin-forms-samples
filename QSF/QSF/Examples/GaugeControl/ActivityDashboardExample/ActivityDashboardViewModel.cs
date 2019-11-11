using QSF.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace QSF.Examples.GaugeControl.ActivityDashboardExample
{
    public class ActivityDashboardViewModel : ExampleViewModel
    {
        public ActivityDashboardViewModel()
        {
            this.MoveData = GetMoveData();
            this.ExerciseData = GetExerciseData();
            this.StandData = GetStandData();
            this.MoveIndex = this.MoveData.Average(a => a.ActivityIndex);
            this.ExerciseIndex = this.ExerciseData.Average(a => a.ActivityIndex);
            this.StandIndex = this.StandData.Average(a => a.ActivityIndex);
        }

        public List<ActivityDataItem> MoveData { get; set; }
        public List<ActivityDataItem> ExerciseData { get; set; }
        public List<ActivityDataItem> StandData { get; set; }
        public double MoveIndex { get; set; }
        public double ExerciseIndex { get; set; }
        public double StandIndex { get; set; }

        private static List<ActivityDataItem> GetMoveData()
        {
            List<ActivityDataItem> data = new List<ActivityDataItem>();
            data.Add(new ActivityDataItem("Thu", 0.7));
            data.Add(new ActivityDataItem("Fri", 0.8));
            data.Add(new ActivityDataItem("Sat", 0.6));
            data.Add(new ActivityDataItem("Sun", 0.55));
            data.Add(new ActivityDataItem("Mon", 0.75));
            data.Add(new ActivityDataItem("Tue", 0.6));
            data.Add(new ActivityDataItem("Today", 0.35));

            return data;
        }

        private static List<ActivityDataItem> GetExerciseData()
        {
            List<ActivityDataItem> data = new List<ActivityDataItem>();
            data.Add(new ActivityDataItem("Thu", 0.3));
            data.Add(new ActivityDataItem("Fri", 0.5));
            data.Add(new ActivityDataItem("Sat", 0.45));
            data.Add(new ActivityDataItem("Sun", 0.6));
            data.Add(new ActivityDataItem("Mon", 0.35));
            data.Add(new ActivityDataItem("Tue", 0.35));
            data.Add(new ActivityDataItem("Today", 0.1));

            return data;
        }

        private static List<ActivityDataItem> GetStandData()
        {
            List<ActivityDataItem> data = new List<ActivityDataItem>();
            data.Add(new ActivityDataItem("Thu", 0.2));
            data.Add(new ActivityDataItem("Fri", 0.4));
            data.Add(new ActivityDataItem("Sat", 0.3));
            data.Add(new ActivityDataItem("Sun", 0.45));
            data.Add(new ActivityDataItem("Mon", 0.35));
            data.Add(new ActivityDataItem("Tue", 0.6));
            data.Add(new ActivityDataItem("Today", 0.55));

            return data;
        }
    }
}
