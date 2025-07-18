using System;
using System.Collections.Generic;

class PlayerBO
{
    private List<int> oversList = new List<int>();
    public void AddOversDetails(int oversBowled)
    {
        oversList.Add(oversBowled);
    }
    public int GetNoOfBallsBowled()
    {
        int totalOvers = 0;
        foreach (int over in oversList)
        {
            totalOvers += over;
        }
        return totalOvers * 6;
    }
}

class Bowling
{
    static void Main()
    {
        PlayerBO player = new PlayerBO();

        Console.Write("Enter the number of overs: ");
        int overs = int.Parse(Console.ReadLine());

        player.AddOversDetails(overs);

        int balls = player.GetNoOfBallsBowled();
        Console.WriteLine("Balls Bowled : " + balls);
    }
}
