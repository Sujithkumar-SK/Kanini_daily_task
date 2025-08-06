namespace console_codefirst.Models;

public class Program
{
  public static void Main(String[] args)
  {
    var obj = new StudentsContext();
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
  static void ViewStudent(StudentsContext obj)
  {
    var data = obj.Students.ToList();
    foreach (var i in data)
    {
      Console.WriteLine($"ID: {i.Id}, Name: {i.Name}");
    }
  }
  static void AddStudents(StudentsContext obj)
  {
    Console.Write("Enter the Student ID: ");
    int id = Convert.ToInt32(Console.ReadLine());
    Console.Write("Enter the Student Name: ");
    string name = Console.ReadLine()!;
    var student = new Students
    {
      Id = id,
      Name = name,
    };
    obj.Students.Add(student);
    obj.SaveChanges();
    Console.WriteLine("Student added successfully.");
  }
  static void UpdateStudent(StudentsContext obj)
  {
    Console.Write("Enter the Student ID to Update: ");
    int id = Convert.ToInt32(Console.ReadLine());
    var check = obj.Students.Find(id);
    if (check != null)
    {
      Console.Write("Enther the New Name: ");
      string name = Console.ReadLine()!;
      check.Name = name;
      obj.SaveChanges();
      Console.WriteLine("Student updated successfully.");
    }
    else
    {
      Console.WriteLine("Student not found.");
    }
  }
  static void DeleteStudent(StudentsContext obj)
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