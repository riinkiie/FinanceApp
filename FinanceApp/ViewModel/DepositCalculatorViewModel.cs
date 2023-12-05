using FinanceApp.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace FinanceApp.ViewModel
{
    public class DepositCalculatorViewModel : INotifyPropertyChanged
    {
        private DepositCalculatorModel depositCalculatorModel;
        private MonthlyCalculation selectedMonthlyCalculation;
  
        public DepositCalculatorModel DepositModel
        {
            get { return depositCalculatorModel; }
            set
            {
                depositCalculatorModel = value;
                OnPropertyChanged(nameof(DepositModel));
            }
        }

        public ObservableCollection<MonthlyCalculation> MonthlyCalculations { get; set; }

        public MonthlyCalculation SelectedMonthlyCalculation
        {
            get { return selectedMonthlyCalculation; }
            set
            {
                selectedMonthlyCalculation = value;
                OnPropertyChanged(nameof(SelectedMonthlyCalculation));
            }
        }

        
        public ObservableCollection<int> DepositDurationMonths { get; set; }

        public ICommand CalculateCommand => new RelayCommand(obj => CalculateMonthly());
    
        public DepositCalculatorViewModel()
        {
            DepositModel = new DepositCalculatorModel();
            MonthlyCalculations = new ObservableCollection<MonthlyCalculation>();
            DepositDurationMonths = new ObservableCollection<int>();
        }

        private void CalculateMonthly()
        {
            MonthlyCalculations.Clear();
            DepositDurationMonths.Clear();

            decimal initialAmount = (decimal)DepositModel.InitialAmount;
            decimal interestRate = (decimal)(DepositModel.InterestRate / 100);
            int durationInMonths = DepositModel.DurationInYears * 12;

            decimal totalAmount = initialAmount;
            DateTime currentDate = DepositModel.StartDate;

            for (int month = 1; month <= durationInMonths; month++)
            {
                // Начисление процентов начиная со второго месяца
                if (month > 1)
                {
                    // Добавляем предыдущие начисленные проценты и дополнительные средства
                    decimal previousMonthlyInterest = MonthlyCalculations[month - 2].MonthlyInterest;
                    totalAmount += previousMonthlyInterest + DepositModel.AdditionalAmount;

                    // Рассчитываем ежемесячные проценты
                    decimal monthlyInterest = totalAmount * (interestRate / 12);

                    // Обновляем общую сумму вклада
                    totalAmount += monthlyInterest;
                }
                else
                {
                    // Первый месяц - без начисления процентов
                    totalAmount += DepositModel.AdditionalAmount;
                }

                MonthlyCalculations.Add(new MonthlyCalculation
                {
                    Month = month,
                    MonthDate = currentDate,
                    MonthlyInterest = month > 1 ? totalAmount * (interestRate / 12) : 0,
                    TotalAmountPerMonth = totalAmount,
                    Currency = DepositModel.Currency,
                    DepositAmount = DepositModel.AdditionalAmount
                });

                DepositDurationMonths.Add(month);
                currentDate = currentDate.AddMonths(1);
            }
        }




        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
