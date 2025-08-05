namespace efcore_solution_dbfirst.Models;

public class Program
{
  public static void Main(String[] args)
  {
    var obj = new SchoolContext();
    bool run = true;
    while (run)
    {
      Console.WriteLine("1. View Students");
      Console.WriteLine("2. Add Student");
      Console.WriteLine("3. Update Student");
      Console.WriteLine("4. Delete Student");
      Console.WriteLine("5. Exit");
      Console.Write("Choose an option: ");
      var choice = Console.ReadLine();

      switch (choice)
      {
        case "1":
          ViewStudent(obj);
          break;
        case "2":
          AddStudents(obj);
          break;
        case "3":
          UpdateStudent(obj);
          break;
        case "4":
          DeleteStudent(obj);
          break;
        case "5":
          run = false;
          break;
        default:
          Console.WriteLine("Invalid choice, please try again.");
          break;
      }
    }
  }
  static void ViewStudent(SchoolContext obj)
  {
    var data = obj.Students.ToList();
    foreach (var i in data)
    {
      Console.WriteLine($"ID: {i.StdId}, Name: {i.Name}, Roll No: {i.RollNo}, Age: {i.Age}, Place: {i.Place}");
    }
  }
  static void AddStudents(SchoolContext obj)
  {
    Console.Write("Enter the Student ID: ");
    int id = Convert.ToInt32(Console.ReadLine());
    Console.Write("Enter the Student Name: ");
    string name = Console.ReadLine()!;
    Console.Write("Enter the Student Roll No: ");
    int rollno = Convert.ToInt32(Console.ReadLine());
    Console.Write("Enter the Student Age: ");
    int age = Convert.ToInt32(Console.ReadLine());
    Console.Write("Enter the Student Place: ");
    string place = Console.ReadLine()!;
    var student = new Student
    {
      StdId = id,
      RollNo = rollno,
      Name = name,
      Age = age,
      Place = place
    };
    obj.Students.Add(student);
    obj.SaveChanges();
    Console.WriteLine("Student added successfully.");
  }
  static void UpdateStudent(SchoolContext obj)
  {
    Console.Write("Enter the Student ID to Update: ");
    int id = Convert.ToInt32(Console.ReadLine());
    var check = obj.Students.Find(id);
    if (check != null)
    {
      Console.Write("Enther the New Name: ");
      string name = Console.ReadLine()!;
      Console.Write("Enter the New Age: ");
      int age = Convert.ToInt32(Console.ReadLine());
      Console.Write("Enter the New Place: ");
      string place = Console.ReadLine()!;
      check.Name = name;
      check.Age = age;
      check.Place = place;
      obj.SaveChanges();
      Console.WriteLine("Student updated successfully.");
    }
    else
    {
      Console.WriteLine("Student not found.");
    }
  }
  static void DeleteStudent(SchoolContext obj)
  {
    Console.Write("Enter the Student ID to Delete: ");
    int id = Convert.ToInt32(Console.ReadLine());
    var check = obj.Students.Find(id);
    if (check != null)
    {
      obj.Students.Remove(check);
      obj.SaveChanges();
      Console.WriteLine("Student deleted successfully.");
    }
    else
    {
      Console.WriteLine("Student not found.");
    }
  }
}