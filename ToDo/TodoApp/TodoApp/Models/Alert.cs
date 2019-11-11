using System;

namespace TodoApp.Models
{
    public class Alert
    {
        public Alert(int iD, TimeSpan timeBeforeStart)
        {
            ID = iD;
            TimeBeforeStart = timeBeforeStart;
            this.Description = CreateDescription();
        }

        public int ID { get; set; }
        public TimeSpan TimeBeforeStart { get; set; }
        public string Description { get; private set; }

        private string CreateDescription()
        {
            if (TimeBeforeStart.TotalMinutes < 0)
                return "Never";

            return $"{this.TimeBeforeStart.Minutes} min before start";
        }

        public override bool Equals(object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            Alert p = obj as Alert;
            if ((System.Object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return ID == p.ID;
        }
    }
}
