using BankAccount;

namespace BankAccountDemo;

[TestClass]
public sealed class Test1
{
    //Example unit test from lab
    [TestMethod]
    public void TestMethod1()
    {
        //Set up test data - Arrange
        double beginningBalance = 11.99;
        double debitAmount = 4.55;
        double expected = 7.44; // 11.99 - 4.55 = 7.44

        //Create account and withdrawal - Act
        BankAccount.BankAccount account = new BankAccount.BankAccount("Mr. Bryan Walton", beginningBalance);
        account.Withdraw(debitAmount);

        //Verify the balance was correctly updated - Assert
        double actual = account.Balance;
        Assert.AreEqual(expected, actual, 0.01, "Account not debited correctly");
    }

    [TestMethod]
    public void ConstructorTest()
    {
        BankAccount.BankAccount account1 = new BankAccount.BankAccount("Zero Balance", 0.00);
        Assert.AreEqual(0.00, account1.Balance, 0.01);
        Assert.AreEqual("Zero Balance", account1.AccountHolder);
        Assert.IsNotNull(account1.Transactions);
        Assert.AreEqual(0, account1.Transactions.Count);
        
        BankAccount.BankAccount account2 = new BankAccount.BankAccount("Initial Balance", 100.00);
        Assert.AreEqual(100.00, account2.Balance, 0.01);
        Assert.AreEqual("Initial Balance", account2.AccountHolder);
        Assert.AreEqual(1, account2.Transactions.Count);
        Assert.IsTrue(account2.Transactions[0].Contains("Initial deposit: $100"));
    }

    [TestMethod]
    public void DepositMethodTest()
    {
        BankAccount.BankAccount account = new BankAccount.BankAccount("Deposit Tester", 50.00);
        
        account.Deposit(25.00);
        Assert.AreEqual(75.00, account.Balance, 0.01);
        
        Assert.AreEqual(2, account.Transactions.Count);
        Assert.IsTrue(account.Transactions[1].Contains("Deposit: +$25"));
        
        Assert.ThrowsException<ArgumentException>(() => account.Deposit(-10.00));
        Assert.ThrowsException<ArgumentException>(() => account.Deposit(0.00));
        
        Assert.AreEqual(75.00, account.Balance, 0.01);
    }

    [TestMethod]
    public void WithdrawMethodTest()
    {
        BankAccount.BankAccount account = new BankAccount.BankAccount("Withdraw Tester", 100.00);
        
        account.Withdraw(30.00);
        Assert.AreEqual(70.00, account.Balance, 0.01);
        
        Assert.AreEqual(2, account.Transactions.Count);
        Assert.IsTrue(account.Transactions[1].Contains("Withdrawal: -$30"));
        
        Assert.ThrowsException<InvalidOperationException>(() => account.Withdraw(200.00));
        Assert.ThrowsException<ArgumentException>(() => account.Withdraw(-5.00));
        Assert.ThrowsException<ArgumentException>(() => account.Withdraw(0.00));
        
        Assert.AreEqual(70.00, account.Balance, 0.01);
    }

    [TestMethod]
    public void TransactionRecordingTest()
    {
        BankAccount.BankAccount account = new BankAccount.BankAccount("Transaction Tester", 100.00);
        
        account.Deposit(50.00);
        account.Withdraw(25.00);
        account.Deposit(75.00);
        account.Withdraw(30.00);
        
        Assert.AreEqual(5, account.Transactions.Count);
        
        Assert.IsTrue(account.Transactions[0].Contains("Initial deposit: $100"));
        Assert.IsTrue(account.Transactions[1].Contains("Deposit: +$50"));
        Assert.IsTrue(account.Transactions[2].Contains("Withdrawal: -$25"));
        Assert.IsTrue(account.Transactions[3].Contains("Deposit: +$75"));
        Assert.IsTrue(account.Transactions[4].Contains("Withdrawal: -$30"));
        
        Assert.AreEqual(170.00, account.Balance, 0.01);
    }

    [TestMethod]
    public void AccountBalanceTest()
    {
        BankAccount.BankAccount account1 = new BankAccount.BankAccount("Zero Start", 0.00);
        Assert.AreEqual(0.00, account1.Balance, 0.01);
        
        BankAccount.BankAccount account2 = new BankAccount.BankAccount("Balance Tester", 1000.00);
        Assert.AreEqual(1000.00, account2.Balance, 0.01);
        
        account2.Deposit(250.50);
        account2.Withdraw(175.25);
        account2.Deposit(100.00);
        account2.Withdraw(50.75);
        
        double expectedBalance = 1000.00 + 250.50 - 175.25 + 100.00 - 50.75;
        Assert.AreEqual(expectedBalance, account2.Balance, 0.01);
        Assert.AreEqual(1124.50, account2.Balance, 0.01);
    }


}
