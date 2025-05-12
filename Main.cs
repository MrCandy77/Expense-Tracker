using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker
{
    public class Expense
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; }

        public Expense(string description, decimal amount, string category)
        {
            Description = description;
            Amount = amount;
            Date = DateTime.Now;
            Category = category;
        }
    }

    public class ExpenseManager
    {
        private List<Expense> expenses;

        public ExpenseManager()
        {
            expenses = new List<Expense>();
        }

        public void AddExpense(Expense expense)
        {
            expense.Id = expenses.Count + 1;
            expenses.Add(expense);
        }

        public List<Expense> GetAllExpenses()
        {
            return expenses;
        }

        public decimal GetTotalExpenses()
        {
            return expenses.Sum(e => e.Amount);
        }

        public List<Expense> GetExpensesByCategory(string category)
        {
            return expenses.Where(e => e.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ExpenseManager manager = new ExpenseManager();
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n=== Expense Tracker ===");
                Console.WriteLine("1. Add Expense");
                Console.WriteLine("2. View All Expenses");
                Console.WriteLine("3. View Total Expenses");
                Console.WriteLine("4. View Expenses by Category");
                Console.WriteLine("5. Exit");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddExpense(manager);
                        break;
                    case "2":
                        DisplayAllExpenses(manager);
                        break;
                    case "3":
                        DisplayTotalExpenses(manager);
                        break;
                    case "4":
                        DisplayExpensesByCategory(manager);
                        break;
                    case "5":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void AddExpense(ExpenseManager manager)
        {
            Console.Write("Enter description: ");
            string description = Console.ReadLine();

            Console.Write("Enter amount: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                Console.Write("Enter category: ");
                string category = Console.ReadLine();

                Expense expense = new Expense(description, amount, category);
                manager.AddExpense(expense);
                Console.WriteLine("Expense added successfully!");
            }
            else
            {
                Console.WriteLine("Invalid amount format.");
            }
        }

        static void DisplayAllExpenses(ExpenseManager manager)
        {
            var expenses = manager.GetAllExpenses();
            if (expenses.Count == 0)
            {
                Console.WriteLine("No expenses recorded.");
                return;
            }

            Console.WriteLine("\nAll Expenses:");
            foreach (var expense in expenses)
            {
                Console.WriteLine($"ID: {expense.Id}, Description: {expense.Description}, Amount: ${expense.Amount}, Category: {expense.Category}, Date: {expense.Date}");
            }
        }

        static void DisplayTotalExpenses(ExpenseManager manager)
        {
            decimal total = manager.GetTotalExpenses();
            Console.WriteLine($"\nTotal Expenses: ${total}");
        }

        static void DisplayExpensesByCategory(ExpenseManager manager)
        {
            Console.Write("Enter category to filter: ");
            string category = Console.ReadLine();

            var expenses = manager.GetExpensesByCategory(category);
            if (expenses.Count == 0)
            {
                Console.WriteLine($"No expenses found in category: {category}");
                return;
            }

            Console.WriteLine($"\nExpenses in category '{category}':");
            foreach (var expense in expenses)
            {
                Console.WriteLine($"ID: {expense.Id}, Description: {expense.Description}, Amount: ${expense.Amount}, Date: {expense.Date}");
            }
        }
    }
}