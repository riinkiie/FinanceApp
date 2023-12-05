using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Model
{
    public class BalanceItem
    {
        public string Currency { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
    
        public decimal Balance => TotalIncome - TotalExpense; // Добавлено свойство Balance
    }
}

