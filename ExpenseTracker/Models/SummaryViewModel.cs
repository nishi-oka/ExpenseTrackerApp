//月間サマリー

using System.Collections.Generic;

namespace ExpenseTracker.Models
{
	public class SummaryViewModel
	{
		public int SelectedYear { get; set; }
		public int SelectedMonth { get; set; }
		public List<Expense> Expenses { get; set; } = new();
		public string[] Categories { get; set; } = Array.Empty<string>();
		public Dictionary<string, decimal> CategoryTotals { get; set; } = new();
		public decimal TotalExpenses { get; set; }
		public decimal TotalIncome { get; set; }
		public decimal SavingsGoal { get; set; }
		public double AchievementRate { get; set; }
	}
}
