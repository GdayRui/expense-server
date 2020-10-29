using System;
using System.Collections.Generic;
using System.Linq;
using DailyExpenseRui.Data;
using DailyExpenseRui.Models;
using DailyExpenseRui.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace DailyExpenseRui.Services
{
    public class ExpenseService : IExpenseService
    {
        private ExpenseContext context;
        public ExpenseService(ExpenseContext context)
        {
            this.context = context;
        }

        public ExpenseViewModel AddExpense(ExpenseViewModel expense)
        {
            var category = GetCategory(expense.CategoryName);

            if (category == null)
            {
                throw new Exception("There is no such category name");
            }

            var expenseEntity = new Expense()
            {
                Name = expense.Name,
                Cost = expense.Cost,
                Date = expense.Date,
                CategoryID = category.ID
            };

            context.Expenses.Add(expenseEntity);
            context.SaveChanges();

            expense.ID = expenseEntity.ID;

            return expense;
        }

        public List<ExpenseViewModel> GetAll()
        {

            var expenseList = context.Expenses
            .Include(e => e.Category)
            .Select(e => new ExpenseViewModel()
            {
                ID = e.ID,
                Name = e.Name,
                Cost = e.Cost,
                Notes = e.Notes,
                //CategoryName = context.Categories.FirstOrDefault(c => c.ID == e.CategoryID).Name
                CategoryName = e.Category.Name
            }).ToList();

            return expenseList;
        }

        public ExpenseViewModel GetByID(int id)
        {
            var expense = context.Expenses
                .Where(e => e.ID == id)
                .Include(e => e.Category)
                .FirstOrDefault();

            if (expense == null)
            {
                //throw new Exception("The expense is not found.");
                return null;
            }

            var result = new ExpenseViewModel()
            {
                ID = expense.ID,
                Name = expense.Name,
                CategoryName = expense.Category.Name,
                Notes = expense.Notes,
                Date = expense.Date
            };

            return result;
        }

        public void Delete(int id)
        {
            var expense = context.Expenses
                .Where(e => e.ID == id)
                .Include(e => e.Category)
                .FirstOrDefault();

            context.Expenses.Remove(expense);
            context.SaveChanges();

        }

        public ExpenseViewModel Update(ExpenseViewModel expense)
        {
            var expenseEntity = context.Expenses
                .Where(e => e.ID == expense.ID)
                .FirstOrDefault();

            if (expenseEntity == null)
            {
                throw new ArgumentException("Cannot find this expense record.");
            }

            var category = GetCategory(expense.CategoryName);

            if (category == null)
            {
                throw new ArgumentException("This category name does not exsit.");
            }

            expenseEntity.Name = expense.Name;
            expenseEntity.CategoryID = category.ID;
            expenseEntity.Cost = expense.Cost;
            expenseEntity.Date = expense.Date;
            expenseEntity.Notes = expense.Notes;

            //context.Expenses.Update(expenseEntity);
            context.SaveChanges();

            return expense;
        }

        private Category GetCategory(string name)
        {
            return context.Categories
                .Where(c => c.Name == name)
                .FirstOrDefault();
        }

        public List<ExpenseViewModel> GetFilterResult(FilterViewModel filter)
        {
            // null check
            if (filter == null)
            {
                return null;
            }

            var query = context.Expenses.Select(e => e);

            if(string.IsNullOrEmpty(filter.ExpenseName))
            {
                query = query.Where(e => e.Name.Contains(filter.ExpenseName));
            }

            if(filter.StartDate != null)
            {
                query = query.Where(e => e.Date >= filter.StartDate);
            }

            if(filter.EndDate != null)
            {
                query = query.Where(e => e.Date <= filter.EndDate);
            }

            if(filter.CategoryNames != null && filter.CategoryNames.Count > 0)
            {
                query = query.Where(e => filter.CategoryNames.Contains(e.Category.Name));
            }

            if(filter.MaxCost.HasValue)
            {
                query = query.Where(e => e.Cost <= filter.MaxCost.Value);
            }

            if (filter.MinCost.HasValue)
            {
                query = query.Where(e => e.Cost >= filter.MinCost.Value);
            }

            return query.Select(e => new ExpenseViewModel()
            {
                Name = e.Name,
                ID = e.ID,
                CategoryName = e.Category.Name,
                Cost = e.Cost,
                Date = e.Date,
                Notes = e.Notes
            }).ToList();
        }
    }
}
