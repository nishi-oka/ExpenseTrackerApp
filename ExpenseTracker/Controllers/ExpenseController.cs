using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Models;
using ExpenseTracker.Data;
using System.Linq;

namespace ExpenseTracker.Controllers
{
	public class ExpenseController : Controller
	{
		//登録画面
		private readonly ExpenseService _service = new ExpenseService();

		public IActionResult Index()
		{
			var expenses = _service.GetAll();
			return View(expenses);
		}

		[HttpGet]
		public IActionResult Create()
		{
			var model = new Expense
			{
				RegistrationDate = DateTime.Now,
				PurchaseDate = DateTime.Now
			};
			return View(model);
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

		//月間サマリー画面
		[HttpGet]
		public IActionResult Summary(int? year, int? month)
		{
			var selectedYear = year ?? DateTime.Now.Year;
			var selectedMonth = month ?? DateTime.Now.Month;

			var expenses = _service.GetAll()
					.Where(e => e.PurchaseDate.Year == selectedYear && e.PurchaseDate.Month == selectedMonth)
					.ToList();

			var categories = new string[]
			{
								"住宅", "光熱費-水道", "光熱費-電気","光熱費-ガス","通信費-携帯", "通信費-Wifi", "食費-スーパー", "食費-外食","日用品-消耗品","日用品-衣服",
								"駐車料-駐車代", "駐車料-修理","レジャー/教育費-外出/旅行", "レジャー/教育費-教育費", "趣味/交際費-嗜好品", "趣味/交際費-趣味", "趣味/交際費-美容", "趣味/交際費-交際費", "保険/医療費-保険", "保険/医療費-医療費","投資-NISA","投資-その他"
			};

			var categoryTotals = categories.ToDictionary(
					cat => cat,
					cat => expenses.Where(e => e.Category == cat).Sum(e => e.Amount)
			);

			var totalExpenses = expenses.Sum(e => e.Amount);
			var totalIncome = 0m; // 後で収入データを追加する場合に使用
			var savingsGoal = 50000m; // 任意で変更
			var achievementRate = totalIncome > 0 ? Math.Max(0, ((totalIncome - totalExpenses) / savingsGoal) * 100) : 0;

			var model = new SummaryViewModel
			{
				SelectedYear = selectedYear,
				SelectedMonth = selectedMonth,
				Expenses = expenses,
				Categories = categories,
				CategoryTotals = categoryTotals,
				TotalExpenses = totalExpenses,
				TotalIncome = totalIncome,
				SavingsGoal = savingsGoal,
				AchievementRate = (double)achievementRate
			};

			return View(model);
		}
	}
}
