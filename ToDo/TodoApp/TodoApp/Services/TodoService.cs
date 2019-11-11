using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.DataAccess;
using TodoApp.Models;
using Xamarin.Forms;

namespace TodoApp.Services
{
    public interface ITodoService
    {
        Task<TodoItem> AddTodoItemAsync(TodoItem newItem);
        Task<TodoItem> UpdateTodoItemAsync(TodoItem updatedItem);
        Task<bool> DeleteAsync(TodoItem newItem);
        Task<Category> AddCategoryAsync(Category newCategory);
        Task<Category> UpdateCategoryAsync(Category updatedCategory);
        Task<bool> DeleteAsync(Category category);
        IReadOnlyCollection<Category> GetCategories();
        IReadOnlyCollection<Category> GetCategoriesWithItems(bool addCompletedItems, DateFilter dateFilter);
        IReadOnlyCollection<Priority> GetPriorities();
        IReadOnlyCollection<Alert> GetAlerts();
        IReadOnlyCollection<Recurrence> GetRecurrences();
        IReadOnlyCollection<TodoItem> GetTodoItemsInCategory(Category category);
        IReadOnlyCollection<TodoItem> GetTodoItems();
        IReadOnlyCollection<TodoItem> SearchForItems(string term);
        TodoItem GetTodoItem(int id);
    }

    public class TodoService : ITodoService
    {
        public TodoService()
        {
            this.context = App.CreateDatabase();
        }

        private TodoItemContext context;
        public static string ActionAdd => "add";
        public static string ActionUpdate => "update";
        public static string ActionDelete => "delete";

        public IReadOnlyCollection<Category> GetCategories()
        {
            return context.Categories.Select(c => c.ToModel(true, null)).ToArray();
        }

        public IReadOnlyCollection<Models.Category> GetCategoriesWithItems(bool addCompletedItems, DateFilter dateFilter)
        {
            return context.Categories
                .Include(c => c.TodoItems)
                .ThenInclude(c => c.Alert)
                .ThenInclude(c => c.Alert)
                .Include(c => c.TodoItems)
                .ThenInclude(c => c.Recurrence)
                .ThenInclude(c => c.Recurrence)
                .Include(c => c.TodoItems)
                .ThenInclude(c => c.Priority)
                .Select(c => c.ToModel(addCompletedItems, dateFilter)).ToArray();
        }

        public IReadOnlyCollection<Priority> GetPriorities()
        {
            return context.Priorities
                .OrderBy(c => c.Priority)
                .Select(c => c.ToModel()).ToArray();
        }

        public IReadOnlyCollection<Alert> GetAlerts()
        {
            return context.Alerts
                .OrderBy(c => c.TimeBeforeStart)
                .Select(c => c.ToModel()).ToArray();
        }

        public IReadOnlyCollection<Recurrence> GetRecurrences()
        {
            return context.Recurrences
                .Select(c => c.ToModel()).ToArray();
        }

        public IReadOnlyCollection<TodoItem> GetTodoItemsInCategory(Category category)
        {
            var items = context.TodoItems
                .Where(c => c.CategoryID == category.ID)
                .Select(c => c.ToModel(false))
                .ToArray();
            foreach (var item in items)
            {
                item.Category = category;
            }
            foreach (var item in items.Except(category.Items))
            {
                category.Items.Add(item);
            }
            return items;
        }

        public TodoItem GetTodoItem(int id)
        {
            return context.TodoItems
                .Where(c => c.ID == id)
                .Include(c => c.Category)
                .Include(c => c.Alert)
                .ThenInclude(c => c.Alert)
                .Include(c => c.Recurrence)
                .ThenInclude(c => c.Recurrence)
                .Include(c => c.Priority)
                .SingleOrDefault()?.ToModel(true);
        }

        public Category GetCategory(int id)
        {
            return context.Categories
                .Where(c => c.ID == id)
                .SingleOrDefault()?.ToModel(true, null);
        }

        public async Task<TodoItem> AddTodoItemAsync(TodoItem newItem)
        {
            var dto = newItem.ToDTO();
            var entity = this.context.TodoItems.Add(dto);
            try
            {
                await this.context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                throw e;
            }
            TodoItem storedItem = GetTodoItem(entity.Entity.ID);
            MessagingCenter.Send<ITodoService, TodoItem>(this, ActionAdd, storedItem);
            return storedItem;
        }

        public async Task<TodoItem> UpdateTodoItemAsync(TodoItem updatedItem)
        {
            var dto = updatedItem.ToDTO();
            var localDto = this.context.TodoItems.SingleOrDefault(c => c.ID == dto.ID);
            if (localDto != null)
            {
                localDto.CopyFrom(dto);
                dto = localDto;
            }
            var entity = this.context.TodoItems.Update(dto);
            try
            {
                await this.context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                throw e;
            }
            TodoItem storedItem = GetTodoItem(entity.Entity.ID);
            MessagingCenter.Send<ITodoService, TodoItem>(this, ActionUpdate, storedItem);
            return storedItem;
        }

        public async Task<bool> DeleteAsync(TodoItem item)
        {
            if (item == null)
                return false;

            var localDto = this.context.TodoItems.SingleOrDefault(c => c.ID == item.ID);
            if (localDto == null)
                return false;

            this.context.TodoItems.Remove(localDto);
            try
            {
                await this.context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                throw e;
            }
            MessagingCenter.Send<ITodoService, TodoItem>(this, ActionDelete, item);
            return true;
        }

        public async Task<Category> AddCategoryAsync(Category newCategory)
        {
            var dto = newCategory.ToDTO();
            var entity = this.context.Categories.Add(dto);
            try
            {
                await this.context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                throw e;
            }
            Category storedItem = GetCategory(entity.Entity.ID);
            MessagingCenter.Send<ITodoService, Category>(this, ActionAdd, storedItem);
            return storedItem;
        }

        public async Task<Category> UpdateCategoryAsync(Category updatedCategory)
        {
            var dto = updatedCategory.ToDTO();
            var localDto = this.context.Categories.SingleOrDefault(c => c.ID == dto.ID);
            if (localDto != null)
            {
                localDto.CopyFrom(dto);
                dto = localDto;
            }
            var entity = this.context.Categories.Update(dto);
            try
            {
                await this.context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                throw e;
            }
            Category storedItem = GetCategory(entity.Entity.ID);
            MessagingCenter.Send<ITodoService, Category>(this, ActionUpdate, storedItem);
            return storedItem;
        }

        public async Task<bool> DeleteAsync(Category category)
        {
            if (category == null)
                return false;

            var localDto = this.context.Categories.SingleOrDefault(c => c.ID == category.ID);
            if (localDto == null)
                return false;

            this.context.Categories.Remove(localDto);
            try
            {
                await this.context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                throw e;
            }
            MessagingCenter.Send<ITodoService, Category>(this, ActionDelete, category);
            return true;
        }

        public IReadOnlyCollection<TodoItem> GetTodoItems()
        {
            return context.TodoItems
                .Include(c => c.Category)
                .Select(c => c.ToModel(true))
                .ToArray();
        }

        public IReadOnlyCollection<TodoItem> SearchForItems(string term)
        {
            term = term.ToLower();
            var items = context.TodoItems
                .Where(c => c.Name.ToLower().Contains(term))
                .Include(c => c.Category)
                .Select(c => c.ToModel(true))
                .ToArray();
            return items;
        }
    }
}
