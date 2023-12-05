using FinanceApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FinanceApp.ViewModel
{
    public class BalancePageViewModel : INotifyPropertyChanged
    {
        private DataBaseContext dbContext;

        private ObservableCollection<BalanceItem> balanceItems;

        public ObservableCollection<BalanceItem> BalanceItems
        {
            get { return balanceItems; }
            set
            {
                balanceItems = value;
                OnPropertyChanged(nameof(BalanceItems));
            }
        }

        public BalancePageViewModel()
        {
            dbContext = new DataBaseContext();
            UpdateBalanceItems();
            // Подписываемся на события добавления расходов и доходов
            Expense.ExpenseAdded += (sender, expense) => UpdateBalanceItems();
            Income.IncomeAdded += (sender, income) => UpdateBalanceItems();
        }

        private void UpdateBalanceItems()
        {
            var incomes = dbContext.Income.ToList();
            var expenses = dbContext.Expense.ToList();

            // Преобразование данных о доходах
            var incomeItems = incomes.Select(i => new
            {
                Currency = i.Currency,
                Amount = i.Amount,
                IsIncome = true
            });

            // Преобразование данных о расходах
            var expenseItems = expenses.Select(e => new
            {
                Currency = e.Currency,
                Amount = e.Amount,
                IsIncome = false
            });

            // Объединение данных о доходах и расходах
            var combinedItems = incomeItems.Concat(expenseItems);

            BalanceItems = new ObservableCollection<BalanceItem>(
                from item in combinedItems
                group item by item.Currency into g
                select new BalanceItem
                {
                    Currency = g.Key,
                    TotalIncome = g.Where(i => i.IsIncome).Sum(i => i.Amount),
                    TotalExpense = g.Where(i => !i.IsIncome).Sum(i => i.Amount),
                });
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        // Подписываемся на события добавления расходов и доходов
        
    }
}