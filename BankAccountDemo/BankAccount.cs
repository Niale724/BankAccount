using System;
using System.Collections.Generic;

namespace BankAccount 
{
    public class BankAccount
    {
        private string accountHolder;
        private double balance;
        private List<string> transactions;
        
        public BankAccount(string accountHolder, double initialBalance)
        {
            if (string.IsNullOrEmpty(accountHolder))
                throw new ArgumentException("Account holder name cannot be empty");
                
            this.accountHolder = accountHolder;
            this.balance = initialBalance;
            this.transactions = new List<string>();
            
            if (initialBalance > 0)
            {
                this.transactions.Add($"Initial deposit: ${initialBalance:F2}");
            }
        }
        
        public void Deposit(double amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Deposit amount must be positive");
                
            this.balance += amount;
            this.transactions.Add($"Deposit: +${amount} (Balance: ${this.balance})");
        }
        
        public void Withdraw(double amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Withdrawal amount must be positive");
                
            if (amount > this.balance)
                throw new InvalidOperationException("Insufficient funds");
                
            this.balance -= amount;
            this.transactions.Add($"Withdrawal: -${amount} (Balance: ${this.balance})");
        }
        
        public double Balance 
        { 
            get { return this.balance; } 
        }
        
        public string AccountHolder 
        { 
            get { return this.accountHolder; } 
        }
        
        public List<string> Transactions 
        { 
            get { return this.transactions; } 
        }
    }
}