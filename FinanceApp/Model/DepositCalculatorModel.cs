using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FinanceApp.Model
{
    public class DepositCalculatorModel
    {
        public double InitialAmount { get; set; }
        public string Currency { get; set; }
        public double InterestRate { get; set; }
        public int DurationInYears { get; set; }
        public decimal AdditionalAmount { get; set; }

     
        private DateTime startDate = DateTime.Today;

        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }
        public List<MonthlyCalculation> MonthlyCalculations { get; set; }

        public DepositCalculatorModel()
        {
            MonthlyCalculations = new List<MonthlyCalculation>();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
