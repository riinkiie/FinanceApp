using FinanceApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace FinanceApp.ViewModel
{
    public class AllTransactionsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Transaction> transactions;
        private DateTime filterStartDate;
        private DateTime filterEndDate;
        private string selectedTransactionType;

        public ObservableCollection<Transaction> Transactions
        {
            get { return transactions; }
            set
            {
                transactions = value;
                OnPropertyChanged(nameof(Transactions));
            }
        }

        public DateTime FilterStartDate
        {
            get { return filterStartDate; }
            set
            {
                filterStartDate = value;
                OnPropertyChanged(nameof(FilterStartDate));
                FilterTransactions();
            }
        }

        public DateTime FilterEndDate
        {
            get { return filterEndDate; }
            set
            {
                filterEndDate = value;
                OnPropertyChanged(nameof(FilterEndDate));
                FilterTransactions();
            }
        }

        public ObservableCollection<string> TransactionTypes { get; set; }

        public string SelectedTransactionType
        {
            get { return selectedTransactionType; }
            set
            {
                selectedTransactionType = value;
                OnPropertyChanged(nameof(SelectedTransactionType));
                FilterTransactions();
            }
        }

        public AllTransactionsViewModel()
        {
            // Инициализация коллекции транзакций и других свойств
            Transactions = new ObservableCollection<Transaction>();
            TransactionTypes = new ObservableCollection<string> { "All", "Income", "Expense" };

            // Подгрузка всех транзакций
            LoadAllTransactions();
        }

        private void LoadAllTransactions()
        {
            using (var dbContext = new DataBaseContext())
            {
                var allTransactions = dbContext.Income
                    .Select(i => new Transaction
                    {
                        Id = i.Id,
                        Amount = i.Amount,
                        Currency = i.Currency,
                        Date = i.Date,
                        Category = i.Category,
                        TransactionType = "Income"
                    })
                    .Union(dbContext.Expense
                        .Select(e => new Transaction
                        {
                            Id = e.Id,
                            Amount = e.Amount,
                            Currency = e.Currency,
                            Date = e.Data,
                            Category = e.Category,
                            TransactionType = "Expense"
                        }))
                    .OrderByDescending(t => t.Date)
                    .ToList();

                Transactions = new ObservableCollection<Transaction>(allTransactions);
            }
        }

        private void FilterTransactions()
        {
            // Очистим коллекцию перед загрузкой новых данных
            Transactions.Clear();

            // Фильтрация транзакций в соответствии с выбранными параметрами
            using (var dbContext = new DataBaseContext())
            {
                var filteredTransactions = dbContext.Income
                    .Where(t => t.Date >= FilterStartDate && t.Date <= FilterEndDate)
                    .Select(i => new Transaction
                    {
                        Id = i.Id,
                        Amount = i.Amount,
                        Currency = i.Currency,
                        Date = i.Date,
                        Category = i.Category,
                        TransactionType = "Income"
                    })
                    .Union(dbContext.Expense
                        .Where(t => t.Data >= FilterStartDate && t.Data <= FilterEndDate)
                        .Select(e => new Transaction
                        {
                            Id = e.Id,
                            Amount = e.Amount,
                            Currency = e.Currency,
                            Date = e.Data,
                            Category = e.Category,
                            TransactionType = "Expense"
                        }))
                    .OrderByDescending(t => t.Date)
                    .ToList();

                if (SelectedTransactionType != "All")
                {
                    // Фильтрация по типу транзакции
                    filteredTransactions = filteredTransactions
                        .Where(t => t.TransactionType == SelectedTransactionType)
                        .ToList();
                }

                // Добавим отфильтрованные транзакции в коллекцию
                foreach (var transaction in filteredTransactions)
                {
                    Transactions.Add(transaction);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
