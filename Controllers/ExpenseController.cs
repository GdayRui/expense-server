using System;
using System.Threading.Tasks;
using DailyExpenseRui.Data;
using DailyExpenseRui.Models;
using DailyExpenseRui.Services;
using DailyExpenseRui.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DailyExpenseRui.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService expenseService;
        public ExpenseController(IExpenseService expenseService)
        {
            this.expenseService = expenseService;
        }

        // POST: expense
        [HttpPost]
        public IActionResult Create(ExpenseViewModel expense)
        {
            try
            {
                var result = expenseService.AddExpense(expense);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: expense
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = expenseService.GetAll();
            return Ok(result);
        }

        // GET: expense/id
        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            var result = expenseService.GetByID(id);
            if (result == null)
            {
                return NotFound("This expense record is not found");
            }
            return Ok(result);
        }

        // DELETE: expense/id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            expenseService.Delete(id);
            return Ok();
        }

        // PUT: expense
        [HttpPut]
        public IActionResult Update(ExpenseViewModel expense)
        {
            try
            {
                var result = expenseService.Update(expense);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
                //return BadRequest(ex.Message);
            }
        }

        // GET:
        [HttpPost("filter")]
        public IActionResult GetFilterResult(FilterViewModel filter)
        {
            try
            {
                var result = expenseService.GetFilterResult(filter);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
