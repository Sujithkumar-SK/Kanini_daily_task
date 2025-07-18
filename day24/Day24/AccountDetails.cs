class Program
{
  public static void Main(String[] args)
  {
    try
    {
      Console.Write("Enter the Account id: ");
      int id = Convert.ToInt32(Console.ReadLine());
      Console.Write("Enter Account Type(Savings or Current): ");
      string accountType = Console.ReadLine();
      Console.Write("Enter amount Balance: ");
      double balance = Convert.ToDouble(Console.ReadLine());
      Account acc = new Account(id, accountType, balance);
      

      Console.Write("\nEnter amount to withdraw: ");
      double amount = Convert.ToDouble(Console.ReadLine());
      Console.WriteLine(acc.GetDetails());
      bool st = acc.Withdraw(amount);
      if (st)
      {
        Console.WriteLine("New Balance:" + acc.Balance);
      }
      else
      {
        Console.WriteLine("Withdraw Failed....");
      }
    }
    catch (ArgumentException ex)
    {
      Console.WriteLine("Validation Error: " + ex.Message);
    }
  }

  public class Account
  {
    private int id;
    private string accountType;
    private double balance;

    public int Id
    {
      get => id;
      set
      {
        if (value <= 0)
        {
          throw new ArgumentException("Id must be Positive number.");
        }
        id = value;
      }
    }
    public string AccountType
    {
      get => accountType;
      set
      {
        if (string.IsNullOrWhiteSpace(value))
        {
          throw new ArgumentException("Account type cannot be Empty..");
        }

        string dummy = value.ToLower();
        if (dummy != "savings" && dummy != "current")
        {
          throw new ArgumentException("Account type must be either 'Saving' or 'Current'");
        }
        accountType = value;
      }
    }
    public double Balance
    {
      get => balance;
      set
      {
        if (value < 0)
        {
          throw new ArgumentException("Balance cannot be Negative.");
        }
        balance = value;
      }
    }

    public Account()
    {

    }
    public Account(int id, string accountType, double balance)
    {
      Id = id;
      AccountType = accountType;
      Balance = balance;
    }

    public bool Withdraw(double amount)
    {
      if (amount <= balance)
      {
        balance -= amount;
        return true;
      }
      return false;
    }
    public string GetDetails()
    {
      return
      $"\nAccount Details:\nAccount Id: {id}\nAccount Type: {accountType}\nBalance: {balance}";
    }
  }
}