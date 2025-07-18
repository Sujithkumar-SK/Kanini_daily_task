using System;
public interface IOpenable
{
  string OpenSesame();
}
public class TreasureBox : IOpenable
{
  public string OpenSesame()
  {
    return "Congratulations, Here is your lucky win";
  }
}
public class Parachute : IOpenable
{
  public string OpenSesame()
  {
    return "Have a thrilling experience flying in air";
  }
}
public class LuckyMain
{
  public static void Main()
  {
    Console.WriteLine("Enter the letter found in the paper");
    string input = Console.ReadLine();

    if (input == "T")
    {
      IOpenable treasure = new TreasureBox();
      Console.WriteLine(treasure.OpenSesame());
    }
    else if (input == "P")
    {
      IOpenable parachute = new Parachute();
      Console.WriteLine(parachute.OpenSesame());
    }
  }
}
