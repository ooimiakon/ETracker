using System;
namespace ETracker.Models
{
	public class ExpenseItem
	{
        public long recordID { get; set; }
        public string title { get; set; }
        public double amount { get; set; }
        public string time { get; set; }
        public bool isExpense { get; set; } //yes为支出 no为收入
        public string category { get; set; }
        public string content { get; set; }
    }
}

