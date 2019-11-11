using System;

namespace TodoApp.Models
{
    public class Recurrence
    {
        public Recurrence(int iD, string description, TimeSpan recurrenceSpan, int recurrenceMonths)
        {
            ID = iD;
            Description = description;
            RecurrenceSpan = recurrenceSpan;
            RecurrenceMonths = recurrenceMonths;
        }

        public int ID { get; set; }
        public string Description { get; set; }
        public TimeSpan RecurrenceSpan { get; set; }
        public int RecurrenceMonths { get; set; }

        public override bool Equals(object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            Recurrence p = obj as Recurrence;
            if ((System.Object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return ID == p.ID;
        }
    }
}
