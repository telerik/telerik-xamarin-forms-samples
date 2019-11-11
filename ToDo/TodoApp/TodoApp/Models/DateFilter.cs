using System;
using System.Collections.Generic;
using System.Linq;

namespace TodoApp.Models
{
    public abstract class DateFilter
    {
        protected DateFilter(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public abstract bool Passes(TodoItem item);

        public FilterResults Filter(IReadOnlyCollection<TodoItem> items)
        {
            return new FilterResults(this, items.Where(item => this.Passes(item)).ToArray());
        }
    }

    public class FilterResults
    {
        public FilterResults(DateFilter filter, IReadOnlyCollection<TodoItem> items)
        {
            Filter = filter;
            Items = items;
        }

        public DateFilter Filter { get; }
        public IReadOnlyCollection<TodoItem> Items { get; }
        public int ItemsCount => Items?.Count ?? 0;
    }

    public class OverdueDateFilter : DateFilter
    {
        public OverdueDateFilter() : base("Overdue")
        { }

        public override bool Passes(TodoItem item)
        {
            return item.Start != null && item.Start.Value < DateTime.Now;
        }
    }

    public class TomorrowDateFilter : DateFilter
    {
        public TomorrowDateFilter() : base("Tomorrow")
        { }

        public override bool Passes(TodoItem item)
        {
            return item.Start != null && item.Start.Value.Date == DateTime.Today.AddDays(1);
        }
    }

    public class NextWeekDateFilter : DateFilter
    {
        public NextWeekDateFilter() : base("Next Week")
        { }

        private static DayOfWeek FirstDayOfWeek = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek;

        public override bool Passes(TodoItem item)
        {
            if (item.Start == null)
                return false;

            DateTime nextWeekStart = GetNextWeekday(DateTime.Today, FirstDayOfWeek);
            DateTime nextWeekEnd = nextWeekStart.AddDays(7);
            return item.Start.Value.Date >= nextWeekStart && item.Start.Value.Date < nextWeekEnd;
        }

        private static DateTime GetNextWeekday(DateTime start, DayOfWeek day)
        {
            int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
            return start.AddDays(daysToAdd);
        }
    }

    public class ForthcomingDateFilter : DateFilter
    {
        public ForthcomingDateFilter() : base("Forthcoming")
        { }

        public override bool Passes(TodoItem item)
        {
            return item.Start != null && item.Start.Value > DateTime.Now;
        }
    }

    public class EmptyDateFilter : DateFilter
    {
        public EmptyDateFilter() : base("No Date Specified")
        { }

        public override bool Passes(TodoItem item)
        {
            return item.Start == null;
        }
    }

    public class TodayDateFilter : DateFilter
    {
        public TodayDateFilter() : base("Today")
        { }

        public override bool Passes(TodoItem item)
        {
            return item.Start != null && item.Start.Value.Date == DateTime.Today;
        }
    }
}
