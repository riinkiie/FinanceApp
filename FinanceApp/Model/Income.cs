﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;

namespace FinanceApp.Model
{
    public class Income : INotifyPropertyChanged
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
            set { date = value; OnPropertyChanged("Date"); }
        }

        private string category;
        public string Category
        {
            get { return category; }
            set { category = value; OnPropertyChanged(); }
        }
        private int userId;
        public int UserId
        {
            get { return userId; }
            set { userId = value; OnPropertyChanged("IdUser"); }
        }
        private string currency;
        public string Currency
        {
            get { return currency; }
            set { currency = value; OnPropertyChanged(); }
        }

        public string TransactionType { get; } = "Income";

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
