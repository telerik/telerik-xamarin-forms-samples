using Microsoft.EntityFrameworkCore;

namespace TodoApp.DataAccess
{
    public class TodoItemContext : DbContext
    {
        public static TodoItemContext Create(string dbPath)
        {
            TodoItemContext ctx = new DataAccess.TodoItemContext(dbPath);
            ctx.Database.EnsureCreated();
            ctx.Database.Migrate();
            SeedData.AddSampleData(ctx);
            return ctx;
        }

        protected TodoItemContext(string dbPath)
        {
            _dbPath = dbPath;
        }

        private string _dbPath;

        public DbSet<DTO.TodoItemDTO> TodoItems { get; set; }
        public DbSet<DTO.TodoItemAlertDTO> TodoItemAlerts { get; set; }
        public DbSet<DTO.TodoItemRecurrenceDTO> TodoItemRecurrences { get; set; }
        public DbSet<DTO.CategoryDTO> Categories { get; set; }
        public DbSet<DTO.AlertDTO> Alerts { get; set; }
        public DbSet<DTO.PriorityDTO> Priorities { get; set; }
        public DbSet<DTO.RecurrenceDTO> Recurrences { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DTO.TodoItemAlertDTO>()
                .HasOne(c => c.TodoItem)
                .WithOne(c => c.Alert);

            modelBuilder.Entity<DTO.TodoItemRecurrenceDTO>()
                .HasOne(c => c.TodoItem)
                .WithOne(c => c.Recurrence);
        }
    }
}
