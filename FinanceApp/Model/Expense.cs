using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Model
{
    public class Expense :  INotifyPropertyChanged
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }
        private decimal amount;
        public decimal Amount
        {
            get { return amount; }
            set { amount = value; OnPropertyChanged("Amount"); }
        }
        private DateTime date;
        public DateTime Date
        {
            get { return date; }
            set { date = value; OnPropertyChanged("Data"); }
        }
        private string category;
        public string Category
        {
            get { return category; }
            set { category = value; OnPropertyChanged(); }
        }

        private string currency;
        public string Currency
        {
            get { return currency; }
            set { currency = value; OnPropertyChanged(); }
        }
        private int userId;
        public int UserId
        {
            get { return userId; }
            set { userId = value; OnPropertyChanged("IdUser"); }
        }
        public string TransactionType { get; } = "Expense";

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public static event EventHandler<Expense> ExpenseAdded;

        public static void OnExpenseAdded(Expense expense)
        {
            ExpenseAdded?.Invoke(null, expense);
        }
    }
}
