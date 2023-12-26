using System;
using System.Collections.Generic;
using System.Linq;

namespace FinanceApp.Model
{
    public class Transaction
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; } 


        // Метод для объединения данных из таблиц Income и Expense
        public static List<Transaction> CombineIncomesAndExpenses(List<Income> incomes, List<Expense> expenses)
        {
            // Преобразование доходов и расходов в список транзакций
            List<Transaction> transactions = incomes.Select(income => new Transaction
            {
                Id = income.Id,
                Amount = income.Amount,
                Currency = income.Currency,
                Date = income.Date,
                Category = income.Category, 
        
            }).ToList();

            // Добавление расходов в список транзакций
            transactions.AddRange(expenses.Select(expense => new Transaction
            {
                Id = expense.Id,
                Amount = -expense.Amount, // Расходы представлены отрицательными значениями
                Currency = expense.Currency,
                Date = expense.Date,
                Category = expense.Category, // 
              
            }));

            // Сортировка транзакций по дате
            transactions = transactions.OrderByDescending(t => t.Date).ToList();

            return transactions;
        }
    }
}
