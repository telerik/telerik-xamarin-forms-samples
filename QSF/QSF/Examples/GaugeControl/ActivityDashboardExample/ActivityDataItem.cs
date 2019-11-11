namespace QSF.Examples.GaugeControl.ActivityDashboardExample
{
    public class ActivityDataItem
    {
        public ActivityDataItem(string day, double activityIndex)
        {
            this.Day = day;
            this.ActivityIndex = activityIndex;
        }

        public string Day
        {
            get;
            set;
        }

        public double ActivityIndex
        {
            get;
            set;
        }
    }
}
