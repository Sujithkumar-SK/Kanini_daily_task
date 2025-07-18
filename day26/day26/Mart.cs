using System;
using System.Collections;
using System.Globalization;

class Product
{
  public string _productName;
  public string _serialNumber;
  public DateTime _purchaseDate;
  public double _cost;

  public Product(string productName, string serialNumber, DateTime purchaseDate, double cost)
  {
    _productName = productName;
    _serialNumber = serialNumber;
    _purchaseDate = purchaseDate;
    _cost = cost;
  }

  public override string ToString()
  {
    return string.Format("{0,-15}{1,-15}{2,-15}{3,-15}", _productName, _serialNumber, _purchaseDate.ToString("dd-MM-yyyy"), _cost.ToString("F0"));
  }
}

class Program
{
    static void Main()
    {
        ArrayList productList = new ArrayList();

        Console.Write("Enter the number of products: ");
        int n = int.Parse(Console.ReadLine());

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"\nEnter details for Product {i + 1}:");

            Console.Write("Product Name: ");
            string name = Console.ReadLine();

            Console.Write("Serial Number: ");
            string serial = Console.ReadLine();

            Console.Write("Purchase Date (dd-MM-yyyy): ");
            string dateInput = Console.ReadLine();
            DateTime purchaseDate = DateTime.ParseExact(dateInput, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            Console.Write("Cost: ");
            double cost = double.Parse(Console.ReadLine());

            Product product = new Product(name, serial, purchaseDate, cost);
            productList.Add(product);
        }
        Console.WriteLine("\n" + String.Format("{0,-15}{1,-15}{2,-15}{3,15}",
            "Product Name", "Serial Number", "Purchase Date", "Purchase Cost"));
        foreach (Product p in productList)
        {
            Console.WriteLine(p);
        }
    }
}