using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TodoApp.DataAccess
{
    public static class SeedData
    {
        public static void AddSampleData(TodoItemContext context)
        {
            if (!context.Recurrences.Any())
            {
                context.Recurrences.AddRange(new DTO.RecurrenceDTO[]
                {
                    new DTO.RecurrenceDTO() { ID = 1, Description = "Never", Recurrence = TimeSpan.FromDays(-1) },
                    new DTO.RecurrenceDTO() { ID = 2, Description = "Repeat Daily", Recurrence = TimeSpan.FromDays(1) },
                    new DTO.RecurrenceDTO() { ID = 3, Description = "Repeat Weekly", Recurrence = TimeSpan.FromDays(7) },
                    new DTO.RecurrenceDTO() { ID = 4, Description = "Repeat Every 2 Weeks", Recurrence = TimeSpan.FromDays(14) },
                    new DTO.RecurrenceDTO() { ID = 5, Description = "Repeat Monthly", RecurrenceMonths = 1 },
                });
            }

            if (!context.Alerts.Any())
            {
                context.Alerts.AddRange(new DTO.AlertDTO[]
                {
                    new DTO.AlertDTO() { ID = 1, TimeBeforeStart = TimeSpan.FromMinutes(-1) },
                    new DTO.AlertDTO() { ID = 2, TimeBeforeStart = TimeSpan.FromMinutes(5) },
                    new DTO.AlertDTO() { ID = 3, TimeBeforeStart = TimeSpan.FromMinutes(10) },
                    new DTO.AlertDTO() { ID = 4, TimeBeforeStart = TimeSpan.FromMinutes(15) },
                    new DTO.AlertDTO() { ID = 5, TimeBeforeStart = TimeSpan.FromMinutes(30) },
                });
            }

            if (!context.Priorities.Any())
            {
                context.Priorities.AddRange(new DTO.PriorityDTO[]
                {
                    new DTO.PriorityDTO() { ID = 1, Priority = 1, Name = "Low", Icon = "\u2160" },
                    new DTO.PriorityDTO() { ID = 3, Priority = 4, Name = "High", Icon = "\u2161" },
                    new DTO.PriorityDTO() { ID = 4, Priority = 5, Name = "Very High", Icon = "\u2162" },
                    new DTO.PriorityDTO() { ID = 5, Priority = 7, Name = "Urgent", Icon = "\uF11D" },
                });
            }

            if (!context.Categories.Any() && GetAutoincrementValue(context, nameof(context.Categories)) <= 0)
            {
                context.Categories.AddRange(new DTO.CategoryDTO[]
                {
                    new DTO.CategoryDTO() { ID = 1, Name = "Personal", Color = DomainMapper.ColorToUInt(Xamarin.Forms.Color.FromRgb(0xF4, 0x5D, 0x64)) },
                    new DTO.CategoryDTO() { ID = 2, Name = "Work", Color = DomainMapper.ColorToUInt(Xamarin.Forms.Color.FromRgb(0x4D, 0xA3, 0xE0)) },
                    new DTO.CategoryDTO() { ID = 3, Name = "Shopping", Color = DomainMapper.ColorToUInt(Xamarin.Forms.Color.FromRgb(0x8A, 0x8A, 0x99))},
                    new DTO.CategoryDTO() { ID = 4, Name = "Sport", Color = DomainMapper.ColorToUInt(Xamarin.Forms.Color.FromRgb(0x48, 0x47, 0x5A)) },
                    new DTO.CategoryDTO() { ID = 5, Name = "Kids", Color = DomainMapper.ColorToUInt(Xamarin.Forms.Color.FromRgb(0x5B, 0xE5, 0x2D)) },
                });
            }

            if (!context.TodoItems.Any() && GetAutoincrementValue(context, nameof(context.TodoItems)) <= 0)
            {
                context.TodoItems.AddRange(new DTO.TodoItemDTO[]
                {
                    new DTO.TodoItemDTO()
                    {
                        CategoryID = 1,
                        Name = "Write Homework in French",
                        Content = "Latest about responsive design",
                        Start = new DateTime(2017, 10, 09),
                        Duration = TimeSpan.FromHours(1),
                        Alert = new DTO.TodoItemAlertDTO() { AlertID = 5 },
                        Recurrence = new DTO.TodoItemRecurrenceDTO() { RecurrenceID = 3 }
                    },
                    new DTO.TodoItemDTO()
                    {
                        CategoryID = 1,
                        Name = "Listen to the FB online course",
                        Content = "Even if it is boring..."
                    },
                    new DTO.TodoItemDTO()
                    {
                        CategoryID = 1,
                        Name = "Prepare template for Interior Design Course",
                        Content = "One idea: use cat images. They always work."
                    },
                    new DTO.TodoItemDTO()
                    {
                        CategoryID = 1,
                        Name = "Prepare for English test",
                        Content = "Revise chapters 12, 13 and 14"
                    },
                    new DTO.TodoItemDTO()
                    {
                        CategoryID = 1,
                        Name = "Check the status of T-shirt online order",
                        Content = "Tracking number 12345698751"
                    },
                    new DTO.TodoItemDTO()
                    {
                        CategoryID = 1,
                        Name = "Buy tickets for the U2 concert",
                        Content = "And find a excuse not to attend it"
                    },
                    new DTO.TodoItemDTO()
                    {
                        CategoryID = 1,
                        Name = "Call grandma tonight",
                        Content = "Yes granny, I ate"
                    },
                    new DTO.TodoItemDTO()
                    {
                        CategoryID = 1,
                        Name = "Buy notebooks from the stationary store",
                        Content = "It's always a good idea to keep a backup"
                    },
                    new DTO.TodoItemDTO()
                    {
                        CategoryID = 1,
                        Name = "Make reservations for Velingrad holiday",
                        Content = "Here I go again"
                    },
                    new DTO.TodoItemDTO()
                    {
                        CategoryID = 5,
                        Name = "English Course for Kids",
                        Content = "Lesson 1: the song from Frozen"
                    },
                    new DTO.TodoItemDTO()
                    {
                        CategoryID = 5,
                        Name = "Prepare template for Kids party",
                        Content = "Buy baloons"
                    },
                    new DTO.TodoItemDTO()
                    {
                        CategoryID = 5,
                        Name = "Attend \"Starwars\" with Kids at Serdika Center",
                        Content = "Thank you Disney!"
                    },
                    new DTO.TodoItemDTO()
                    {
                        CategoryID = 4,
                        Name = "Buy Exercise Bike",
                        Content = "At least we can use it as a wardrobe"
                    },
                    new DTO.TodoItemDTO()
                    {
                        CategoryID = 4,
                        Name = "Go for a 5 mile run",
                        Content = "After that: McDonald's"
                    },
                    new DTO.TodoItemDTO()
                    {
                        CategoryID = 4,
                        Name = "Exercise at the gym",
                        Content = "Exchange gossips"
                    },
                    new DTO.TodoItemDTO()
                    {
                        CategoryID = 2,
                        Name = "Finish the TODO app",
                        Content = "Make it #1 in the store"
                    },
                    new DTO.TodoItemDTO()
                    {
                        CategoryID = 2,
                        Name = "Learn Angular",
                        Content = "Finish the angular tour of heroes project"
                    }
                });
            }

            context.SaveChanges();
        }

        private static int GetAutoincrementValue(TodoItemContext context, string tableName)
        {
            int value = -1;
            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = $"select seq from sqlite_sequence where name=\"{tableName}\"";
                context.Database.OpenConnection();
                value = System.Convert.ToInt32(command.ExecuteScalar());
            }

            return value;
        }
    }
}
