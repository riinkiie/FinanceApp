using FinanceApp.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Timers;
using System.Windows.Input;

namespace FinanceApp.ViewModel
{
    public class AllTransactionsPageViewModel : INotifyPropertyChanged
    {
        private DataBaseContext dbContext;

        private ObservableCollection<Transaction> transactionItems;
        public ObservableCollection<Transaction> TransactionItems
        {
            get { return transactionItems; }
            set
            {
                transactionItems = value;
                OnPropertyChanged(nameof(TransactionItems));
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

        private string selectedTransactionType = "All";
        public string SelectedTransactionType
        {
            get { return selectedTransactionType; }
            set
            {
                // Проверяем, изменился ли фактически тип транзакции
                if (selectedTransactionType != value)
                {
                    selectedTransactionType = value;
                    OnPropertyChanged(nameof(SelectedTransactionType));
                }
            }
        }

        public ICommand ApplyDateRangeCommand => new RelayCommand(_ => UpdateTransactionItems());

        private Timer updateTimer;

        public AllTransactionsPageViewModel()
        {
            dbContext = new DataBaseContext();
            SetDateRange(-1); // Установка начальных значений дат для последнего месяца

            // Подписываемся на события добавления расходов и доходов
            Expense.ExpenseAdded += (sender, expense) => UpdateTransactionItems();
            Income.IncomeAdded += (sender, income) => UpdateTransactionItems();

            // Инициализация таймера с интервалом в 500 миллисекунд (подстройте по необходимости)
            updateTimer = new Timer(500);
            updateTimer.Elapsed += (sender, e) =>
            {
                // Останавливаем таймер
                updateTimer.Stop();

                // Обновляем элементы транзакций
                UpdateTransactionItems();
            };
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

        private void UpdateTransactionItems()
        {
            // Получение данных из таблиц Income и Expense
            var incomes = dbContext.Income.Where(i => i.Date >= StartDate && i.Date <= EndDate).ToList();
            var expenses = dbContext.Expense.Where(e => e.Date >= StartDate && e.Date <= EndDate).ToList();

            // Преобразование доходов и расходов в список транзакций
            var incomeTransactions = incomes.Select(i => new Transaction { Id = i.Id, Amount = i.Amount, Currency = i.Currency, Date = i.Date, Category = i.Category }).ToList();
            var expenseTransactions = expenses.Select(e => new Transaction { Id = e.Id, Amount = e.Amount, Currency = e.Currency, Date = e.Date, Category = e.Category }).ToList();

            // Обработка фильтрации по типу транзакции
            switch (SelectedTransactionType)
            {
                case "All":
                    // Объединение всех данных
                    var allTransactions = incomeTransactions.Concat(expenseTransactions);
                    TransactionItems = new ObservableCollection<Transaction>(allTransactions);
                    break;
                case "Income":
                    // Показывать только доходы
                    TransactionItems = new ObservableCollection<Transaction>(incomeTransactions);
                    break;
                case "Expense":
                    // Показывать только расходы
                    TransactionItems = new ObservableCollection<Transaction>(expenseTransactions);
                    break;
                default:
                    break;
            }
        }
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
