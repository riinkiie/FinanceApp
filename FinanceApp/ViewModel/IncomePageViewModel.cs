using FinanceApp.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FinanceApp.ViewModel
{
    public class IncomePageViewModel : INotifyPropertyChanged
    {
        private MainWindowViewModel mainWindowViewModel;
        private DataBaseContext dbContext;

        private string id;
        private string amount;
        private DateTime selectedDate = DateTime.Today;
        private Income selectedIncome;
        private string selectedCategory;
        private string selectedCurrency;

        public ObservableCollection<Income> Incomes { get; set; }

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
        public string SelectedCurrency
        {
            get { return selectedCurrency; }
            set
            {
                selectedCurrency = value;
                OnPropertyChanged(nameof(SelectedCurrency));
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

        public Income SelectedIncome
        {
            get { return selectedIncome; }
            set
            {
                selectedIncome = value;
                OnPropertyChanged(nameof(SelectedIncome));
            }
        }

        public RelayCommand SaveCommand { get; }
        public RelayCommand DeleteCommand { get; }

        public IncomePageViewModel(MainWindowViewModel mainWindowViewModel)
        {
            dbContext = new DataBaseContext();
            this.mainWindowViewModel = mainWindowViewModel;

            Incomes = new ObservableCollection<Income>();
            Currencies = new ObservableCollection<string> { "USD", "EUR", "GBP" };
            Categories = new ObservableCollection<string> { "Salary", "Investment", "Gift" };

            LoadIncomes();

            SaveCommand = new RelayCommand(SaveIncome, CanSaveIncome);
            DeleteCommand = new RelayCommand(DeleteIncome, CanDeleteIncome);
        }

        private void LoadIncomes()
        {
            foreach (var income in dbContext.Income)
            {
                Incomes.Add(income);
            }
        }

        private bool CanSaveIncome(object parameter)
        {
            return true;
        }

        private void SaveIncome(object parameter)
        {
            Income newIncome = new Income
            {
                
                Amount = decimal.Parse(Amount),
                Currency = SelectedCurrency,
                Date = DateTime.Now,
                Category = SelectedCategory
            };

            Incomes.Add(newIncome);
            dbContext.Income.Add(newIncome);
            dbContext.SaveChanges();
        }

        private bool CanDeleteIncome(object parameter)
        {
            return SelectedIncome != null;
        }

        private void DeleteIncome(object parameter)
        {
            if (parameter is Income incomeToDelete)
            {
                Incomes.Remove(incomeToDelete);
                dbContext.Income.Remove(incomeToDelete);
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
