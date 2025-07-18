using System;
namespace multilevel
{
  class Marks : Person
  {
    public int marks;
    public string subject;

    public Marks(int id, string name, int age, int marks, string subject) : base(id, name, age)
    {
      this.marks = marks;
      this.subject = subject;
    }
    public override void Display()
    {
      base.Display();
      Console.WriteLine("marks: " + marks + " in " + subject);
    }
  }
}