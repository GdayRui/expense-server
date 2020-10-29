using System;
namespace DailyExpenseRui.ViewModel
{
    public class ExpenseViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public double Cost { get; set; }
        public string Notes { get; set; }
        public string CategoryName { get; set; }
    }
}
