using FinanceApp.Model;
using FinanceApp.View;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using FinanceApp.ViewModel;

namespace FinanceApp.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private DataBaseContext dbContext;
     

        private IncomePageViewModel incomePageViewModel;
        private ExpensePageViewModel expensePageViewModel;
        private DepositCalculatorViewModel depositCalculatorViewModel;
        private BalancePageViewModel balancePageViewModel;
        private AllTransactionsPageViewModel allTransactionsPageViewModel;
        private ReportPageViewModel reportPageViewModel;
        private Page reportPage;
       

        private Page incomePage;
        private Page expensePage;
        private Page depositCalculatorPage;
        private Page balancePage;
        private Page allTransactionsPage;

        private Page currentPage;

        public Page CurrentPage
        {
            get { return currentPage; }
            set { currentPage = value; OnPropertyChanged("CurrentPage"); }
        }
        public void OpenReportPage()
        {
            CurrentPage = reportPage;
        }
        public void AllTransactionsPage()
        {
            CurrentPage = allTransactionsPage;
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
        private RelayCommand reportPageCommand;
public RelayCommand ReportPageCommand
        {
            get
            {
                return reportPageCommand ??
                    (reportPageCommand = new RelayCommand(obj =>
                    {
                        OpenReportPage();
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
        private RelayCommand allTransactionsPageCommand;
        public RelayCommand AllTransactionsPageCommand
        {
            get
            {
                return allTransactionsPageCommand ??
                    (allTransactionsPageCommand = new RelayCommand(obj =>
                    {
                        AllTransactionsPage();
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

            // Initialize the balance
            balancePageViewModel = new BalancePageViewModel();
            balancePage = new BalancePage() { DataContext = balancePageViewModel };
           

            reportPageViewModel = new ReportPageViewModel();
            reportPage = new ReportPage() { DataContext = reportPageViewModel };
            allTransactionsPageViewModel = new AllTransactionsPageViewModel();
            allTransactionsPage = new AllTransactionsPage() { DataContext = allTransactionsPageViewModel };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
