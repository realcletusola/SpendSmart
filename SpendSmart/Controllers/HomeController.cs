using Microsoft.AspNetCore.Mvc;
using SpendSmart.Models;
using System.Diagnostics;

namespace SpendSmart.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // Db context for SpendSmart
        private readonly SpendSmartDbContext _context;

        // Include Db context to Home controller
        public HomeController(ILogger<HomeController> logger, SpendSmartDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        // expense controller
        public IActionResult Expenses()
        {
            // get all expense as list 
            var allExpenses = _context.Expenses.ToList();

            // return all expense
            return View(allExpenses);
        }

        // create/edit expense controller
        public IActionResult CreateEditExpense(int? id)
        {
            // check if id is not null
            if (id == null)
            {
                // get expense by id
                var expenseInDb = _context.Expenses.SingleOrDefault(e => e.Id == id);
                // return expense 
                return View(expenseInDb);
            }

            // return view if id is null
            return View();
        }

        // delete expense controller
        public IActionResult DeleteExpense(int id)
        {
            // get expense
            var expenseInDb = _context.Expenses.SingleOrDefault(e => e.Id == id);
            // remove expense 
            _context.Expenses.Remove(expenseInDb);
            // save changes to database 
            _context.SaveChanges();

            // redirect to expense page
            return RedirectToAction("Expenses");
        }

        // expense submit form controller
        public IActionResult CreateEditExpenseForm(Expense model)
        {

            if (model.Id == 0)
            {
                //create 
                _context.Expenses.Add(model);
            }
            else
            {
                // edit 
                _context.Expenses.Update(model);
            }
                // save changes to database 
            _context.SaveChanges();

            // redirect to expense page
            return RedirectToAction("Expenses");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
