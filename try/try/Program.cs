using System;
using System.Globalization;
class Try
{
  public static void Main(string[] args)
  {
    int s;
    Console.Write("Enter the number of teams: ");
    int team = Convert.ToInt32(Console.ReadLine());
    int[] sum = new int[team];
    int[][] arr = new int[team][];
    for (int i = 0; i < team; i++)
    {
      s = 0;
      Console.Write($"Enter the number of rounds that team{i + 1} has: ");
      int round = Convert.ToInt32(Console.ReadLine());
      arr[i] = new int[round];

      Console.Write($"Enter the Scores for the team{i + 1}: ");
      for (int j = 0; j < round; j++)
      {
        arr[i][j] = Convert.ToInt32(Console.ReadLine());
        s += arr[i][j];
      }
      sum[i] = s;
    }
    Console.WriteLine("Sum of scores:");
    foreach (int i in sum)
    {
      int j = 1;
      Console.WriteLine($"The total Scores of team{j} is {i}");
    }
  }
}