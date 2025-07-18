using System;
namespace multilevel
{
  class Person
  {
    public int id;
    public string name;

    public Person(int id, string name, int age)
    {
      this.id = id;
      this.name = name;
      this.age = age;
    }
    public int age;
    public virtual void Display()
    {
      Console.WriteLine("Person Id "+id+" with name "+name+" and his ages is "+age);
    }
  }
}