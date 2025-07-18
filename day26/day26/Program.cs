using System;
using System.Collections.Generic;

class Flight
{
  static Dictionary<string, DateTime> flightTiming = new Dictionary<string, DateTime>()
  {
    {"ZW346", DateTime.Today.AddHours(18).AddMinutes(30).AddSeconds(15)},
    {"BR267", DateTime.Today.AddHours(12).AddMinutes(00).AddSeconds(00)},
    {"AR456", DateTime.Today.AddHours(13).AddMinutes(15).AddSeconds(00)},
    {"PQ264", DateTime.Today.AddHours(09).AddMinutes(45).AddSeconds(00)}
  };
  public static void Main(string[] args)
  {
    Console.Write("Enter the Flight Number No: ");
    string flightNo = Console.ReadLine();

    string output = flightStatus(flightNo);
    Console.WriteLine(output);
  }
  public static string flightStatus(string flightNo)
  {
    DateTime current = DateTime.Now;
    if (flightTiming.TryGetValue(flightNo, out DateTime depTime))
    {
      if (current < depTime)
      {
        TimeSpan timeToFlight = depTime - current;
        return $"Time to Flight is {timeToFlight}";
      }
      else
      {
        return $"Your flight {flightNo} is left you...";
      }
    }
    else
    {
      return $"Flight.No is not found";
    }
  }
}