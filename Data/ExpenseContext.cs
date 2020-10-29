using System;
using Microsoft.EntityFrameworkCore;

using DailyExpenseRui.Models;

namespace DailyExpenseRui.Data
{
    public class ExpenseContext:DbContext
    {

        public ExpenseContext(DbContextOptions<ExpenseContext> options) : base(options)
        {
        }

        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Expense>().ToTable("Expense");
            modelBuilder.Entity<Category>().ToTable("Category");
        }
    }
}
