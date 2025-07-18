using System;

public class Game
{
  public string Name;
  public int MaxNumPlayers;
  public override string ToString()
  {
    return $"Maximum number of players for {Name} is {MaxNumPlayers}";
  }
}
public class GameWithTimeLimit : Game
{
  public int TimeLimit;
  public override string ToString()
  {
    return base.ToString() + $"\nTime Limit for {Name} is {TimeLimit} minutes";
  }
}
public class GameMain
{
  public static void Main(string[] args)
  {
    Console.WriteLine("Enter a game");
    string name1 = Console.ReadLine();

    Console.WriteLine("Enter the maximum number of players");
    int players1 = Convert.ToInt32(Console.ReadLine());

    Game gam1 = new Game { Name = name1, MaxNumPlayers = players1 };

    Console.WriteLine("Enter a game that has time limit");
    string name2 = Console.ReadLine();

    Console.WriteLine("Enter the maximum number of players");
    int players2 = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine("Enter the time limit in minutes");
    int time = Convert.ToInt32(Console.ReadLine());

    GameWithTimeLimit gam2 = new GameWithTimeLimit
    {
      Name = name2,
      MaxNumPlayers = players2,
      TimeLimit = time
    };
    Console.WriteLine(gam1.ToString());
    Console.WriteLine(gam2.ToString());
  }
}