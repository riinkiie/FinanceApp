using FinanceApp.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace FinanceApp.ViewModel
{
    public class ReportPageViewModel : INotifyPropertyChanged
    {
        private DataBaseContext dbContext;

        private ObservableCollection<ReportItem> reportItems;
        public ObservableCollection<ReportItem> ReportItems
        {
            get { return reportItems; }
            set
            {
                reportItems = value;
                OnPropertyChanged(nameof(ReportItems));
            }
        }

        private DateTime startDate;
        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }

        private DateTime endDate;
        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                endDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }

        private int selectedTimeRange;
        public int SelectedTimeRange
        {
            get { return selectedTimeRange; }
            set
            {
                selectedTimeRange = value;
                OnPropertyChanged(nameof(SelectedTimeRange));
                UpdateDateRange();
            }
        }

        public ICommand ApplyDateRangeCommand => new RelayCommand(_ => UpdateReportItems());

        public ReportPageViewModel()
        {
            dbContext = new DataBaseContext();
            SetDateRange(-1); // Установка начальных значений дат для последнего месяца

            // Подписываемся на события добавления расходов и доходов
            Expense.ExpenseAdded += (sender, expense) => UpdateReportItems();
            Income.IncomeAdded += (sender, income) => UpdateReportItems();
        }

        private void UpdateDateRange()
        {
            SetDateRange(selectedTimeRange);
        }

        private void SetDateRange(int months)
        {
            StartDate = DateTime.Now.AddMonths(months).Date;
            EndDate = DateTime.Now.Date;
        }

        private void UpdateReportItems()
        {
            var incomes = dbContext.Income.Where(i => i.Date >= StartDate && i.Date <= EndDate).ToList();
            var expenses = dbContext.Expense.Where(e => e.Date >= StartDate && e.Date <= EndDate).ToList();

            var reportItems = new ObservableCollection<ReportItem>();

            foreach (var currency in incomes.Select(i => i.Currency).Union(expenses.Select(e => e.Currency)).Distinct())
            {
                var earnedAmount = incomes.Where(i => i.Currency == currency).Sum(i => i.Amount);
                var spentAmount = expenses.Where(e => e.Currency == currency).Sum(e => e.Amount);

                var item = new ReportItem
                {
                    Currency = currency,
                    EarnedAmount = earnedAmount,
                    SpentAmount = spentAmount
                };

                reportItems.Add(item);
            }

            ReportItems = reportItems;
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
