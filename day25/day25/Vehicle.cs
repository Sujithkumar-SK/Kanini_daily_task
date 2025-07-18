using System;
namespace Program
{
  class Vehicle
  {
    private string make;
    private string model;
    private int year;

    public string Make
    {
      get => make;
      set => make = value;
    }
    public string Model
    {
      get => model;
      set => model = value;
    }
    public int Year
    {
      get => year;
      set => year = value;
    }
    public Vehicle(string make, string model, int year)
    {
      this.make = make;
      this.model = model;
      this.year = year;
    }
    public virtual void GetInfo()
    {
      Console.WriteLine($"{this.GetType().Name}: Make:{Make} Year: {Year}, Model: {Model}");
    }
  }

  class Car : Vehicle
  {
    public Car(string make, string model, int year) : base(make, model, year)
    { }
    public override void GetInfo()
    {
      Console.WriteLine("Vehicle Type: Car");
      base.GetInfo();
    }
  }
  class Motorcycle : Vehicle
  {
    public Motorcycle(string make, string model, int year) : base(make, model, year)
    { }
    public override void GetInfo()
    {
      Console.WriteLine("Vehicle Type: MotorCycle");
      base.GetInfo();
    }
  }

  class Maintain
  {
    public static void Main(string[] args)
    {
      Console.Write("Enter the car make: ");
      string cmake = Console.ReadLine();
      Console.Write("Enter the car model: ");
      string cmodel = Console.ReadLine();
      Console.Write("Enter the Car Year: ");
      int cyear = Convert.ToInt32(Console.ReadLine());
      Console.Write("Enter the Bike make: ");
      string bmake = Console.ReadLine();
      Console.Write("Enter the Bike model: ");
      string bmodel = Console.ReadLine();
      Console.Write("Enter the Bike Year: ");
      int byear = Convert.ToInt32(Console.ReadLine());
      Car c = new Car(cmake, cmodel, cyear);
      c.GetInfo();
      Motorcycle m = new Motorcycle(bmake, bmodel, byear);
      m.GetInfo();
    }
  }
}