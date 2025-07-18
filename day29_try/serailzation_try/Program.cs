using System;
using System.Diagnostics;
using Newtonsoft.Json;
class Program
{
  public static void Main(string[] args)
  {
    Account Acc = new Account()
    {
      AccId = 101,
      Name = "Sujith"
    };
    string json = JsonConvert.SerializeObject(Acc);
    Console.WriteLine(json);

    FileStream fs = new FileStream("sample.txt", FileMode.OpenOrCreate);
    //StreamWriter sw = new StreamWriter(fs);
    //sw.WriteLine(json);
    //sw.Close();
    StreamReader sr = new StreamReader(fs);
    Console.WriteLine(sr.ReadToEnd());
    fs.Close();
  }
}
class Account
{
  public int AccId;
  public String Name;
}