using System;
namespace multilevel
{
  class Maintain
  {
    public static void Main(string[] args)
    {
      
      Console.Write("Enter the person Id: ");
      int id = Convert.ToInt32(Console.ReadLine());

      Console.Write("Enter the person name: ");
      string name = Console.ReadLine();

      Console.Write("Enter the age: ");
      int age = Convert.ToInt32(Console.ReadLine());

      Console.Write("Do you want to enter the RollNo[1] or Marks[2]: ");
      int choice = Convert.ToInt32(Console.ReadLine());
      if (choice == 1)
      {
        Console.Write("Enter the RollNo: ");
        int rollno = Convert.ToInt32(Console.ReadLine());
        Student mark = new Student(id, name, age, rollno);
        mark.Display();
      }
      else if (choice == 2)
      {
        Console.Write("Enter the mark: ");
        int marks = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter the Subject: ");
        string subject = Console.ReadLine();
        Marks obj = new Marks(id, name, age, marks,subject);
        obj.Display();
      }
    }
  }
}
