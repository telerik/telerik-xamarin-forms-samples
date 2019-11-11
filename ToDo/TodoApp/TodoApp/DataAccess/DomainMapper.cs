using System;
using System.Linq;

namespace TodoApp.DataAccess
{
    public static class DomainMapper
    {
        // Category
        public static Models.Category ToModel(this DTO.CategoryDTO dto, bool addCompletedItems, Models.DateFilter dateFilter)
        {
            var todoItems = dto.TodoItems?.Where(c => addCompletedItems || !c.Completed)?.Select(c => c.ToModel(false));
            if (dateFilter != null)
            {
                todoItems = todoItems.Where(c => dateFilter.Passes(c));
            }
            var category = new Models.Category(dto.ID, dto.Name, Xamarin.Forms.Color.FromUint(dto.Color), todoItems);
            foreach (var item in category.Items)
            {
                item.Category = category;
            }
            return category;
        }

        public static DTO.CategoryDTO ToDTO(this Models.Category category)
        {
            return new DTO.CategoryDTO()
            {
                ID = category.ID,
                Color = ColorToUInt(category.Color),
                Name = category.Name
            };
        }

        // Priority
        public static Models.Priority ToModel(this DTO.PriorityDTO dto)
        {
            return new Models.Priority(dto.ID, dto.Name, dto.Priority, dto.Icon);
        }

        public static DTO.PriorityDTO ToDTO(this Models.Priority priority)
        {
            return new DTO.PriorityDTO()
            {
                ID = priority.ID,
                Name = priority.Name,
                Priority = priority.Weight,
                Icon = priority.Icon,
            };
        }

        // Alert
        public static Models.Alert ToModel(this DTO.AlertDTO dto)
        {
            return new Models.Alert(dto.ID, dto.TimeBeforeStart);
        }

        public static DTO.AlertDTO ToDTO(this Models.Alert priority)
        {
            return new DTO.AlertDTO()
            {
                ID = priority.ID,
                TimeBeforeStart = priority.TimeBeforeStart
            };
        }

        // Recurrence
        public static Models.Recurrence ToModel(this DTO.RecurrenceDTO dto)
        {
            return new Models.Recurrence(dto.ID, dto.Description, dto.Recurrence, dto.RecurrenceMonths);
        }

        public static DTO.RecurrenceDTO ToDTO(this Models.Recurrence recurrence)
        {
            return new DTO.RecurrenceDTO()
            {
                ID = recurrence.ID,
                Description = recurrence.Description,
                Recurrence = recurrence.RecurrenceSpan,
                RecurrenceMonths = recurrence.RecurrenceMonths
            };
        }

        // TodoItem
        public static Models.TodoItem ToModel(this DTO.TodoItemDTO dto, bool setCategory)
        {
            Models.Category category = setCategory ? dto.Category?.ToModel(true, null) : null;
            Models.TodoItemAlert alert = dto.Alert != null && dto.Alert.Alert != null ?
                    new Models.TodoItemAlert(dto.Alert.Alert.ToModel(), dto.Alert.PlaySound) : null;
            Models.TodoItemRecurrence recurrence = dto.Recurrence != null && dto.Recurrence.Recurrence != null ?
                    new Models.TodoItemRecurrence(dto.Recurrence.Recurrence.ToModel(), dto.Recurrence.RepeatAfterCompletion) : null;
            Models.Priority priority = dto.Priority?.ToModel();

            return new Models.TodoItem(dto.ID, dto.Name, dto.Content, dto.Completed, dto.Start, dto.Duration, dto.AllDay, category, alert, recurrence, priority);
        }

        public static DTO.TodoItemDTO ToDTO(this Models.TodoItem item)
        {
            return new DTO.TodoItemDTO()
            {
                ID = item.ID,
                Name = item.Name,
                AllDay = item.AllDay,
                Content = item.Content,
                Start = item.Start,
                Duration = item.Duration,
                Completed = item.Completed,
                CategoryID = item.Category.ID,
                PriorityID = item.Priority?.ID,
                Alert = item.Alert != null ? new DTO.TodoItemAlertDTO()
                {
                    AlertID = item.Alert.Alert.ID,
                    PlaySound = item.Alert.PlaySound
                } : null,
                Recurrence = item.Recurrence != null ? new DTO.TodoItemRecurrenceDTO()
                {
                    RecurrenceID = item.Recurrence.Recurrence.ID,
                    RepeatAfterCompletion = item.Recurrence.RepeatAfterCompletion
                } : null,
            };
        }

        public static uint ColorToUInt(Xamarin.Forms.Color color)
        {
            return (uint)((Convert.ToByte(color.A * 255) << 24) | (Convert.ToByte(color.R * 255) << 16) |
                          (Convert.ToByte(color.G * 255) << 8) | (Convert.ToByte(color.B * 255) << 0));
        }
    }
}
