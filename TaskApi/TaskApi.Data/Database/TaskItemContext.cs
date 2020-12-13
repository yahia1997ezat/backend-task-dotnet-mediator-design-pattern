using System;
using Microsoft.EntityFrameworkCore;
using TaskApi.Domain;

namespace TaskApi.Data.Database
{
    public sealed class TaskItemContext : DbContext
    {
        public TaskItemContext()
        {
        }

        public TaskItemContext(DbContextOptions<TaskItemContext> options)
            : base(options)
        {
            var tasks = new[]
            {
                new TaskItem
                {
                    Id= Guid.Parse("9f35b48d-cb87-4783-bfdb-21e36012930a"),
                    Title="Task 1",
                    Description="Description 1",
                    CreatedOn = new DateTime(2020,12,11),
                    CompletedOn=new DateTime(2020,12,13),
                    RejectedOn= null,
                    RejectedReason= null,
                    AssignedToUser= "Yahia",
                    Status = Status.Completed
                },
                new TaskItem
                {
                    Id=  Guid.Parse("bffcf83a-0224-4a7c-a278-5aae00a02c1e"),
                    Title="Task 1",
                    Description="Description 2",
                    CreatedOn = DateTime.Now,
                    CompletedOn= null,
                    RejectedOn= null,
                    RejectedReason= null,
                    AssignedToUser= "Yahia",
                },
                new TaskItem
                {
                    Id=Guid.Parse("58e5cd7d-856b-4224-bdff-bd8f85bf5a6d"),
                    Title="Task 1",
                    Description="Description 3",
                    CreatedOn = DateTime.Now,
                    CompletedOn= null,
                    RejectedOn= null,
                    RejectedReason= null,
                    AssignedToUser= "Yahia",
                }
            };

            TaskItem.AddRange(tasks);
            SaveChanges();
        }

        public DbSet<TaskItem> TaskItem { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<TaskItem>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            });
        }
    }
}