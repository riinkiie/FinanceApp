using FinanceApp.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FinanceApp.ViewModel
{
    public class ExpensePageViewModel : INotifyPropertyChanged
    {
        private MainWindowViewModel mainWindowViewModel;
        private DataBaseContext dbContext;

        private string id;
        private string amount;
        private DateTime selectedDate = DateTime.Today;
        private Expense selectedExpense;
        private string selectedCategory;
        private string selectedCurrency;


        public ObservableCollection<Expense> Expenses { get; set; }

        public ObservableCollection<string> Currencies { get; set; }
        public ObservableCollection<string> Categories { get; set; }

        public string Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Amount
        {
            get { return amount; }
            set
            {
                amount = value;
                OnPropertyChanged(nameof(Amount));
            }
        }

        public DateTime SelectedDate
        {
            get { return selectedDate; }
            set
            {
                selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
            }
        }

        public string SelectedCategory
        {
            get { return selectedCategory; }
            set
            {
                selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }

   
        public string SelectedCurrency
        {
            get { return selectedCurrency; }
            set
            {
                selectedCurrency = value;
                OnPropertyChanged(nameof(SelectedCurrency));
            }
        }
        public Expense SelectedExpense
        {
            get { return selectedExpense; }
            set
            {
                selectedExpense = value;
                OnPropertyChanged(nameof(SelectedExpense));
            }
        }

        public RelayCommand SaveCommand { get; }
        public RelayCommand DeleteCommand { get; }

        public ExpensePageViewModel(MainWindowViewModel mainWindowViewModel)
        {
            dbContext = new DataBaseContext();
            this.mainWindowViewModel = mainWindowViewModel;

            Expenses = new ObservableCollection<Expense>();
            Currencies = new ObservableCollection<string> { "USD", "EUR", "GBP" };
            Categories = new ObservableCollection<string> { "Food", "Utilities", "Transportation" };

            LoadExpenses();

            SaveCommand = new RelayCommand(SaveExpense, CanSaveExpense);
            DeleteCommand = new RelayCommand(DeleteExpense, CanDeleteExpense);
        }

        private void LoadExpenses()
        {
            foreach (var expense in dbContext.Expense)
            {
                Expenses.Add(expense);
            }
        }

        private bool CanSaveExpense(object parameter)
        {
            return true;
        }

        private void SaveExpense(object parameter)
        {
            Expense newExpense = new Expense
            {
               
                Amount = decimal.Parse(Amount),
                Currency = SelectedCurrency,
                Data = DateTime.Now,
                Category = SelectedCategory
            };

            Expenses.Add(newExpense);
            dbContext.Expense.Add(newExpense);
            dbContext.SaveChanges();
        }

        private bool CanDeleteExpense(object parameter)
        {
            return SelectedExpense != null;
        }

        private void DeleteExpense(object parameter)
        {
            if (parameter is Expense expenseToDelete)
            {
                Expenses.Remove(expenseToDelete);
                dbContext.Expense.Remove(expenseToDelete);
                dbContext.SaveChanges();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
