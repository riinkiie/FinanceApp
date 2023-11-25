using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FinanceApp.Model;
using FinanceApp.View;
using System.Windows.Controls;


namespace FinanceApp.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private DataBaseContext dbContext;

        private IncomePageViewModel incomePageViewModel;
        private ExpensePageViewModel expensePageViewModel;

        private Page incomePage;
        private Page expensePage;

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
        public MainWindowViewModel()
        {
            dbContext = new DataBaseContext();
            LoadPages();
        }

        private RelayCommand incomePageCommand;
        public RelayCommand IncomePageCommand
        {
            get {
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
        public void LoadPages()
        {
            incomePageViewModel=new IncomePageViewModel(this);
            incomePage = new IncomePage() { DataContext = incomePageViewModel };
            expensePageViewModel = new ExpensePageViewModel(this);
            expensePage = new ExpensePage() { DataContext = expensePageViewModel };
        }

        private MainWindowViewModel mainWindowViewModel;
        public MainWindowViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
