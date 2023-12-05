using FinanceApp.Model;
using FinanceApp.View;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace FinanceApp.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private DataBaseContext dbContext;

        private IncomePageViewModel incomePageViewModel;
        private ExpensePageViewModel expensePageViewModel;
        private DepositCalculatorViewModel depositCalculatorViewModel;
        private BalancePageViewModel balancePageViewModel; // Добавлено для баланса

        private Page incomePage;
        private Page expensePage;
        private Page depositCalculatorPage;
        private Page balancePage; // Добавлено для баланса

        private Page currentPage;

        public Page CurrentPage
        {
            get { return currentPage; }
            set { currentPage = value; OnPropertyChanged("CurrentPage"); }
        }

        public void IncomePage()
        {
            CurrentPage = incomePage;
        }

        public void ExpensePage()
        {
            CurrentPage = expensePage;
        }

        public void OpenDepositCalculatorPage()
        {
            CurrentPage = depositCalculatorPage;
        }

        // Новый метод для открытия страницы баланса
        public void OpenBalancePage()
        {
            CurrentPage = balancePage;
        }

        public MainWindowViewModel()
        {
            dbContext = new DataBaseContext();
            LoadPages();
        }

        private RelayCommand incomePageCommand;
        public RelayCommand IncomePageCommand
        {
            get
            {
                return incomePageCommand ??
                    (incomePageCommand = new RelayCommand(obj =>
                    {
                        IncomePage();
                    }));
            }
        }

        private RelayCommand expensePageCommand;
        public RelayCommand ExpensePageCommand
        {
            get
            {
                return expensePageCommand ??
                    (expensePageCommand = new RelayCommand(obj =>
                    {
                        ExpensePage();
                    }));
            }
        }

        private RelayCommand depositCalculatorPageCommand;
        public RelayCommand DepositCalculatorPageCommand
        {
            get
            {
                return depositCalculatorPageCommand ??
                    (depositCalculatorPageCommand = new RelayCommand(obj =>
                    {
                        OpenDepositCalculatorPage();
                    }));
            }
        }

        // Новая команда для открытия страницы баланса
        private RelayCommand balancePageCommand;
        public RelayCommand BalancePageCommand
        {
            get
            {
                return balancePageCommand ??
                    (balancePageCommand = new RelayCommand(obj =>
                    {
                        OpenBalancePage();
                    }));
            }
        }

        public void LoadPages()
        {
            incomePageViewModel = new IncomePageViewModel(this);
            incomePage = new IncomePage() { DataContext = incomePageViewModel };

            expensePageViewModel = new ExpensePageViewModel(this);
            expensePage = new ExpensePage() { DataContext = expensePageViewModel };

            depositCalculatorViewModel = new DepositCalculatorViewModel();
            depositCalculatorPage = new DepositCalculatorPage() { DataContext = depositCalculatorViewModel };

            // Инициализируем баланс
            balancePageViewModel = new BalancePageViewModel();
            balancePage = new BalancePage() { DataContext = balancePageViewModel };
        }
        public void OpenAllTransactionsPage()
        {
            // Создаем новый экземпляр AllTransactionsViewModel
            AllTransactionsViewModel allTransactionsViewModel = new AllTransactionsViewModel();

            // Создаем новую страницу AllTransactionsPage и передаем ей DataContext
            Page allTransactionsPage = new AllTransactionsPage() { DataContext = allTransactionsViewModel };

            // Устанавливаем текущую страницу
            CurrentPage = allTransactionsPage;
        }

        private RelayCommand allTransactionsPageCommand;
        public RelayCommand AllTransactionsPageCommand
        {
            get
            {
                return allTransactionsPageCommand ??
                    (allTransactionsPageCommand = new RelayCommand(obj =>
                    {
                        OpenAllTransactionsPage();
                    }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
