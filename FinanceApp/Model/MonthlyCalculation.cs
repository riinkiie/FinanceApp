using System;

namespace FinanceApp.Model
{
    public class MonthlyCalculation
    {
        public int Month { get; set; }
        public decimal MonthlyInterest { get; set; }
        public decimal TotalAmountPerMonth { get; set; }
        public decimal DepositAmount { get; set; }
        public string Currency { get; set; }
        public DateTime MonthDate { get; set; }
    }
}
