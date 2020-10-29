using System;
using System.Collections.Generic;

namespace DailyExpenseRui.ViewModel
{
    public class FilterViewModel
    {
        public string ExpenseName { get; set; }
        public List<string> CategoryNames { get; set; }
        public double? MinCost { get; set; }
        public double? MaxCost { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
