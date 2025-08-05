using tryingsomething.Models;

public class Program
{
  public static void Main(string[] args)
  {
    SchoolContext obj = new SchoolContext();
    foreach (var i in obj.Students)
    {
      Console.WriteLine($"Id: {i.StdId}, Name: {i.Name}, Roll No: {i.RollNo}, Age: {i.Age}, Place: {i.Place}");
    }   
  }
}