using System;
using System.Collections.Generic;

class Members
{
    static void Main(string[] args)
    {
        Dictionary<string, List<string>> groups = new Dictionary<string, List<string>>()
        {
            { "Gold", new List<string> { "Tom", "Harry" } },
            { "Silver", new List<string> { "Sam", "Peter" } },
            { "Platinum", new List<string> { "John", "David" } }
        };
        Console.Write("Group Name: ");
        string groupName = Console.ReadLine();
        Console.Write("Member Name: ");
        string memberName = Console.ReadLine();
        groupName = char.ToUpper(groupName[0]) + groupName.Substring(1).ToLower();
        if (groups.ContainsKey(groupName))
        {
            groups[groupName].Add(memberName);
            Console.WriteLine("\nGroup Members:");
            foreach (string member in groups[groupName])
            {
                Console.WriteLine(member);
            }
        }
        else
        {
            Console.WriteLine("Invalid group name.");
        }
    }
}
