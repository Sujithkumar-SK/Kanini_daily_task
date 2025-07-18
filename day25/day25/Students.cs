using System;
namespace Program
{
  class Student
  {
    private string name;
    private int age;
    private string grade;
    public string Name
    {
      get => name;

      set
      {
        if (string.IsNullOrWhiteSpace(value))
        {
          name = "Unknown";
        }
        else
        {
          name = value;
        }
      }
    }
    public int Age
    {
      get => age;
      set
      {
        if (value < 0)
        {
          age = 1;
        }
        else
        {
          age = value;
        }
      }
    }
    public string Grade
    {
      get => grade;
      set
      {
        if (string.IsNullOrWhiteSpace(value))
        {
          grade = "Unknown";
        }
        else
        {
          grade = value;
        }
      }
    }
    public Student(string name, int age, string grade)
    {
      Name = name;
      Age = age;
      Grade = grade;
    }
    public string Display()
    {
      return $"Student Name: {Name}\nStudent Age: {Age}\nStudent Grade: {Grade}";
    }
  }
  class StudentManage
  {
    public static void Main(string[] args)
    {
      Console.Write("Enter the Student's Name: ");
      string name = Console.ReadLine();
      Console.Write("Enter the student's age: ");
      int age = Convert.ToInt32(Console.ReadLine());
      Console.Write("Enter the Student;s grade: ");
      string grade = Console.ReadLine();

      Student std = new Student(name, age, grade);
      Console.WriteLine(std.Display());
    }
  }
}