// データ保存（最初はJSONファイル）
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using ExpenseTracker.Models;

namespace ExpenseTracker.Data
{
	public class ExpenseService
	{
		private readonly string _filePath = "expenses.json";

		public List<Expense> GetAll()
		{
			if (!File.Exists(_filePath)) return new List<Expense>();
			var json = File.ReadAllText(_filePath);
			return JsonSerializer.Deserialize<List<Expense>>(json) ?? new List<Expense>();
		}

		public void Save(List<Expense> expenses)
		{
			var json = JsonSerializer.Serialize(expenses);
			File.WriteAllText(_filePath, json);
		}

		public void Add(Expense expense)
		{
			var expenses = GetAll();
			expenses.Add(expense);
			File.WriteAllText(_filePath, JsonSerializer.Serialize(expenses, new JsonSerializerOptions { WriteIndented = true }));
		}
	}
}
