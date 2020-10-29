using System;
using System.Linq;
using DailyExpenseRui.Models;

namespace DailyExpenseRui.Data
{
    public class DbInitializer
    {
        public static void Initialize(ExpenseContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Expenses.Any())
            {
                return;   // DB has been seeded
            }

            var expenseList = new Expense[]
            {
            new Expense{Name="Carson",Cost=2.5,Notes="Carson",Date=DateTime.Parse("2005-09-01"),CategoryID=1},
            new Expense{Name="Meredith",Cost=2.5,Notes="Alonso",Date=DateTime.Parse("2002-09-01"),CategoryID=1},
            new Expense{Name="Arturo",Cost=2.5,Notes="Anand",Date=DateTime.Parse("2003-09-01"),CategoryID=2},
            new Expense{Name="Gytis",Cost=2.5,Notes="Barzdukas",Date=DateTime.Parse("2002-09-01"),CategoryID=1}
            };
            foreach (Expense e in expenseList)
            {
                context.Expenses.Add(e);
            }
            context.SaveChanges();

            var categories = new Category[]
            {
            new Category{Name="Shopping"},
            new Category{Name="Petrol"}
            };
            foreach (Category c in categories)
            {
                context.Categories.Add(c);
            }
            context.SaveChanges();

            
        }
    }
}
