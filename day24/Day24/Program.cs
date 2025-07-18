using System;
namespace Maintain
{
  class Join
  {
    public static void Main(string[] args)
    {
      Console.Write("Enter the First Name: ");
      string fname = Console.ReadLine();

      Console.Write("Enter the Last Name: ");
      string lname = Console.ReadLine();

      string name = string.Concat(fname, " ", lname);
      //string name = string.Join(" ", fname, lname);

      Console.WriteLine($"Full Name: {name}");
    }

  }
}