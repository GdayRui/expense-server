using System;
using System.Collections.Generic;
using DailyExpenseRui.ViewModel;

namespace DailyExpenseRui.Services
{
    public interface IExpenseService
    {
        ExpenseViewModel AddExpense(ExpenseViewModel expense);
        List<ExpenseViewModel> GetAll();
        ExpenseViewModel GetByID(int id);
        void Delete(int id);
        ExpenseViewModel Update(ExpenseViewModel expense);

        List<ExpenseViewModel> GetFilterResult(FilterViewModel filter);
    }
}
