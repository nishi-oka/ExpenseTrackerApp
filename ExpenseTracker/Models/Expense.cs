//支出データ用
using System;

namespace ExpenseTracker.Models
{
	public class Expense
	{
		public int Id { get; set; }   // 一意のID
		public DateTime RegistrationDate { get; set; } = DateTime.Now; // 登録日
		public DateTime PurchaseDate { get; set; } = DateTime.Now; // 購入日
		public string Category { get; set; } = string.Empty;// カテゴリ
		public decimal Amount { get; set; }  // 金額
		public string Note { get; set; } = string.Empty;  // メモ
	}
}
