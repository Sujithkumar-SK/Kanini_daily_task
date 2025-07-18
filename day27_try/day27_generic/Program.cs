using System;
namespace trying
{
  class Program
  {
    public static void Main(string[] args)
    {
      Storage<User> usr = new Storage<User>();
      usr.gdisplay(new User { Id = 1 });
      Console.WriteLine(usr.pdisplay().Id);

      Storage<Bus> bus = new Storage<Bus>();
      bus.gdisplay(new Bus { BusName = "KPR" });
      Console.WriteLine(bus.pdisplay().BusName);
    }
  }
}