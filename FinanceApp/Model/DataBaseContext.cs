using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Collections.ObjectModel;

namespace FinanceApp.Model
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext() : base("DBConnection")
        { }
        public DbSet<Income> Income { get; set; }

        public DbSet<Expense> Expense { get; set; }
       public DbSet<User>Users { get; set; }
    }}
