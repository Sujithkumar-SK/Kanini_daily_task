using System;
using System.Text.RegularExpressions;
namespace day23
{
  class Form
  {
    private string name;
    private int age;
    private string country;

    public string Name
    {
      get => name;
      set
      {
        if (string.IsNullOrWhiteSpace(value))
        {
          throw new ArgumentException("Name cannot be empty....");
        }
        if (!Regex.IsMatch(value, @"^[a-zA-Z\s]+$"))
        {
          throw new ArgumentException("Name can only contain Letters and space");
        }
        name = value;
      }
    }

    public int Age
    {
      get => age;
      set
      {
        if (value <= 0 && value >= 90)
        {
          throw new ArgumentException("Age must be between 0 and 90");
        }
        age = value;
      }
    }
    public string Country
    {
      get => country;
      set
      {
        if (string.IsNullOrWhiteSpace(value))
        {
          throw new ArgumentException("Country cannot be empty...");
        }
        country = value;
      }
    }

    public Form(string name, int age, string country)
    {
      Name = name;
      Age = age;
      Country = country;
    }

    public void Display()
    {
      Console.Write($"Welcome {name}. Your age is {age} and you are from {country}");
    }
  }
}