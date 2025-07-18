using System;

namespace day23
{
  class Maintain
  {
    public static void Main(string[] args)
    {
      try
      {
        Console.Write("Enter your name: ");
        string name = Console.ReadLine();
        Console.Write("Enther your age: ");
        int age = Convert.ToInt32(Console.ReadLine());
        Console.Write("Entter your country: ");
        string country = Console.ReadLine();

        Form obj = new Form(name, age, country);
        obj.Display();
      }
      catch (Exception e)
      {
        Console.WriteLine($"Error: {e.Message}");
      }
    }
  }
}