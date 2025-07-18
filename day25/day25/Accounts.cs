using System;
namespace Program
{
  class Account
  {
    private String AccountNumber;
    private decimal Balance;
    private string OwnerName;
    public Account(string AccountNumber, string OwnerName)
    {
      this.AccountNumber = AccountNumber;
      this.OwnerName = OwnerName;
      this.Balance = 0;
    }
    public void depositingfunds(int amount)
    {
      if (amount > 0)
      {
        Balance += amount;
        Console.WriteLine($"Successfully deposited of ${amount}.\nNew Balance: ${Balance}");
      }
      else
      {
        Console.WriteLine("Deposit amount must not be in negative.");
      }
    }
    public void withdrawfunds(int amount)
    {
      if (amount < 0)
      {
        Console.WriteLine("Fool withdraw must be in positive..");
      }
      else if (amount > Balance)
      {
        Console.WriteLine("You dont Money bro...");
      }
      else
      {
        Balance -= amount;
        Console.WriteLine($"Withdraw Sucessfully &{amount}.\nNew Balance: {Balance}");
      }
    }
    public void checkingbalance()
    {
      Console.WriteLine($"Account Owner: {OwnerName}");
      Console.WriteLine($"Account Number: {AccountNumber}");
      Console.WriteLine($"Current Balance: ${Balance}");
    }
  }

  class AccountManage
  {
    public static void Main(string[] args)
    {
      Console.WriteLine("----Welcome to bank----");
      Console.Write("Enter the Account Number: ");
      string accno = Console.ReadLine();
      Console.Write("Enter the  Account Holder Name: ");
      string accHolder = Console.ReadLine();

      Account acc = new Account(accno, accHolder);
      while (true)
      {
        Console.WriteLine("\n--- Menu ---");
        Console.WriteLine("1. Deposit");
        Console.WriteLine("2. Withdraw");
        Console.WriteLine("3. Check Balance");
        Console.WriteLine("4. Exit");
        Console.Write("Choose an option: ");
        int choice = Convert.ToInt32(Console.ReadLine());

        switch (choice)
        {
          case 1:
            Console.Write("Enter the deposit amount: ");
            int depoamount = Convert.ToInt32(Console.ReadLine());
            acc.depositingfunds(depoamount);
            break;
          case 2:
            Console.Write("Enter the withdraw amount: ");
            int withamount = Convert.ToInt32(Console.ReadLine());
            acc.withdrawfunds(withamount);
            break;
          case 3:
            acc.checkingbalance();
            break;
          case 4:
            Console.Write("Thanks fro spending Time...");
            return;
          default:
            Console.WriteLine("Invalid option...");
            break;
        }
      }
    }
  }
}