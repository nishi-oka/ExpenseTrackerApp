using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Models;
using ExpenseTracker.Data;
using System.Linq;

namespace ExpenseTracker.Controllers
{
	public class ExpenseController : Controller
	{
		private readonly ExpenseService _service = new();

		public IActionResult Index()
		{
			var expenses = _service.GetAll();
			return View(expenses);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Expense expense)
		{
			var expenses = _service.GetAll();
			expense.Id = expenses.Count + 1;
			expenses.Add(expense);
			_service.Save(expenses);

			return RedirectToAction("Index");
		}
	}
}
