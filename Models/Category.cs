using System;
using System.Collections.Generic;

namespace DailyExpenseRui.Models
{
    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public ICollection<Expense> Expenses { get; set; }
    }
}
