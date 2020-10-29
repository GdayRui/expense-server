using System;
using System.Collections.Generic;

namespace DailyExpenseRui.Models
{
    public class Expense
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Cost { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; } // not required

        public int CategoryID { get; set; }

        public Category Category { get; set; }
    }
}
