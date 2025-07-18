using System;
using System.Collections.Generic;

namespace Institute
{
  class Program
  {
    public static Dictionary<string, int> CourseDetails = new Dictionary<string, int>();

    public static void Main(string[] args)
    {
      Course obj = new Course();
      int choice;

      while (true)
      {
        Console.WriteLine("\n");
        Console.WriteLine("1. Add Course Details");
        Console.WriteLine("2. Remove Course Details");
        Console.WriteLine("3. Sort Course By Fee");
        Console.WriteLine("4. Exit");
        Console.Write("Enter your choice: ");
        choice = Convert.ToInt32(Console.ReadLine());
        switch (choice)
        {
          case 1:
            Console.Write("Enter the Course Name: ");
            string name = Console.ReadLine()!;
            Console.Write("Enter the Course fee: ");
            int fee = Convert.ToInt32(Console.ReadLine());
            obj.AddCourseDetails(name, fee);
            break;
          case 2:
            Console.Write("Enter the course name to remove:");
            string removeName = Console.ReadLine()!;
            obj.RemoveCourseDetails(removeName);
            break;
          case 3:
            Dictionary<string, int> SortedFees = obj.SortCourseByFee();
            Console.WriteLine("Course Details (sorted by Fee):");
            foreach (var temp in SortedFees)
            {
              Console.WriteLine($"{temp.Key} -- {temp.Value}");
            }
            break;
          case 4:
            Console.WriteLine("Thank You byee....");
            return;
          default:
            Console.WriteLine("Invaild Choice... Please try again...");
            break;
        }
      }
    }
  }
}