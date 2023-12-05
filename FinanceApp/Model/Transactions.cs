using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Model
{
    public class Transaction
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public DateTime Date { get; set; }

        public string Category { get; set; }

    
        public string TransactionType { get; set; }
    }
}
