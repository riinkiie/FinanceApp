using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Model
{
    public class User : INotifyPropertyChanged
    {
        private int userId;
        public int UserId
        {
            get { return userId; }
            set { userId = value; OnPropertyChanged("IdUser"); }
        }
        private string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value; OnPropertyChanged("Name"); }
        }
        private string surname;
        public string Surname
        {
            get { return Surname; }
            set { Surname = value; OnPropertyChanged("Surname"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public static event EventHandler<Income> IncomeAdded;

        public static void OnIncomeAdded(Income income)
        {
            IncomeAdded?.Invoke(null, income);
        }

    }
}
