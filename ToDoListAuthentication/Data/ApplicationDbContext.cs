using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoListAuthentication.Models;

namespace ToDoListAuthentication.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ToDo> ToDos { get; set; }
        public DbSet<Status> Statuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Status>().HasData(
                new Status { StatusId = "toDo", StatusName = "To Do" },
                new Status { StatusId = "inProgress", StatusName = "In Progress" },
                new Status { StatusId = "qa", StatusName = "Quality Assurance" },
                new Status { StatusId = "done", StatusName = "Done" }
                );
        }
    }
}
