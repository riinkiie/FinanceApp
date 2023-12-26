using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Model
{
    public class ReportItem
    {
        public string Currency { get; set; }
        public decimal EarnedAmount { get; set; }
        public decimal SpentAmount { get; set; }
    }
}
