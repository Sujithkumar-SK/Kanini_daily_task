using System;
namespace day23
{
  class Bill
  {
    public static void Main(string[] args)
    {
      Console.Write("Enter the number of pizzas bught: ");
      int pisszas = Convert.ToInt32(Console.ReadLine());

      Console.Write("Enter the number of puff's bought: ");
      int puffs = Convert.ToInt32(Console.ReadLine());

      Console.Write("Enter the number of pepsi bought: ");
      int pepsis = Convert.ToInt32(Console.ReadLine());

      Process cal = new Process(pisszas, puffs, pepsis);
      cal.generate();
    }
  }
  class Process : Final
  {
    public Process(int pisszas, int puffs, int pepsis) : base(pisszas, puffs, pepsis)
    {
      Console.WriteLine("Bill is generating please wait...");
    }
    public override void generate()
    {
      base.generate();
      Console.WriteLine("\n------Bill Summary------");
      Console.WriteLine($"Pizza Amount  : ₹{PI}");
      Console.WriteLine($"Puff Amount   : ₹{PU}");
      Console.WriteLine($"Pepsi Amount  : ₹{PE}");
      Console.WriteLine($"GST (12%)     : ₹{GST}");
      Console.WriteLine($"CESS (5%)     : ₹{CESS}");
      Console.WriteLine("               ------------");
      Console.WriteLine($"Total Amount  : ₹{total}");
    }
  }
  class Final
  {
    protected int pisszas;
    protected int puffs;
    protected int pepsis;
    protected int PI, PU, PE;
    protected double gst = 0.12, cess = 0.05, GST, CESS, price, total;
    public Final(int pisszas, int puffs, int pepsis)
    {
      this.pisszas = pisszas;
      this.puffs = puffs;
      this.pepsis = pepsis;
    }
    public virtual void generate()
    {
      PI = pisszas * 200;
      PU = puffs * 40;
      PE = pepsis * 120;
      price = PI + PU + PE;
      GST = price * gst;
      CESS = price * cess;
      total = price + GST + CESS;
    }
  }
}