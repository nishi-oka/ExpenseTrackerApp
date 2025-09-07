//支出データ用
using System;

namespace ExpenseTracker.Models
{
	public class Expense
	{
		public int Id { get; set; }   // 一意のID
		public DateTime Date { get; set; }   // 日付
		public string Category { get; set; } // カテゴリ
		public decimal Amount { get; set; }  // 金額
		public string Note { get; set; }     // メモ
	}
}
