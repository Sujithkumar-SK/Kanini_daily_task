using System;
namespace multilevel
{
  class Student : Person
  {
    public int rollno;

    public Student(int id, string name, int age, int rollno) : base(id, name, age)
    {
      this.rollno = rollno;
    }
    public override void Display()
    {
      base.Display();
      Console.WriteLine("Roll.no: " + rollno);
    }
  }
}