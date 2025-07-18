using System;
namespace day21
{
  class NewThing
  {
    public static void Main(string[] args)
    {
      /*Console.WriteLine("Enter a Number: ");
      int num = Convert.ToInt32(Console.ReadLine());

      int l_num = num % 10;

      int f_num = num;
      while (10 <= f_num)
      {
        f_num /= 10;
      }
      Console.WriteLine("First Number: " + f_num);
      Console.WriteLine("Last Number: " + l_num);

      Console.Write("Enter Number: ");
      string num = Console.ReadLine();
      Console.WriteLine("First Digit: " + num[0]);
      Console.WriteLine("Last Digit: " + num[num.Length - 1]);

      Console.Write("Enter a Number: ");
      int n = Convert.ToInt32(Console.ReadLine());
      if (n == 0)
      {
        Console.WriteLine("The Entered Number is Zero.");
      }
      else if (n < 1)
      {
        Console.WriteLine("The Entered Number is Negative.");
      }
      else if (n > 1)
      {
        Console.WriteLine("The Entered Number is Positive.");
      }
      else
      {
        Console.WriteLine("Invalid Input....");
      }
      Console.Write("Enter a Number: ");
      int n = Convert.ToInt32(Console.ReadLine());
      Console.WriteLine("********************");
      for (int i = 1; i <= 10; i++)
      {
        Console.WriteLine(n + " * " + i + " = " + (n * i));
      }
      Console.WriteLine("********************");

      Console.Write("Input Basic Salary Of An Employee: ");
      int bs = Convert.ToInt32(Console.ReadLine());
      double da, hra, grss;
      if (bs <= 10000)
      {
        double HRA = 0.2;
        double DA = 0.8;
        da = bs * DA;
        hra = bs * HRA;
        grss = bs + da + hra;
        Console.WriteLine("Gross Salarry: " + grss);
      }
      else if (bs >= 10001 && bs <= 20000)
      {
        double HRA = 0.25;
        double DA = 0.9;
        da = bs * DA;
        hra = bs * HRA;
        grss = bs + da + hra;
        Console.WriteLine("Gross Salarry: " + grss);
      }
      else if (bs >= 20001)
      {
        double HRA = 0.3;
        double DA = 0.95;
        da = bs * DA;
        hra = bs * HRA;
        grss = bs + da + hra;
        Console.WriteLine("Gross Salarry: " + grss);
      }
      else
      {
        Console.WriteLine("Invalid Input...");
      }*/
      Console.Write("Enter the PersonID: ");
      int id = Convert.ToInt32(Console.ReadLine());
      Console.Write("Enter the Name: ");
      string name = Console.ReadLine();
      Console.Write("Enter the Age: ");
      int age = Convert.ToInt32(Console.ReadLine());

      Person p = new Person();
      p.id = id;
      p.name = name;
      p.age = age;

      p.checkEligiblity();
    }
  }
  class Person
  {
    public int id;
    public string name;
    public int age;
    public void checkEligiblity()
    {
      if (age < 18)
      {
        Console.WriteLine($"{name} with ID {id} is not eligible to vote.");
      }
      else
      {
        Console.WriteLine($"{name} with ID {id} is eligible to vote.");
      }
    }
  }
}