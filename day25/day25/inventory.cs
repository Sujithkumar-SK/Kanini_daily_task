using System;
namespace Program
{
  class Product
  {
    public string Name;
    public decimal Price;
    public int Quantity;
  }

  class Inventory
  {
    public static void Main(string[] args)
    {
      int maxsize = 100;
      Product[] inv = new Product[maxsize];
      int count = 0;
      while (true)
      {
        Console.WriteLine("\n----options----");
        Console.WriteLine("1. Add Product");
        Console.WriteLine("2. View Products");
        Console.WriteLine("3. Update Product");
        Console.WriteLine("4. Exit");
        Console.Write("Select an Option: ");
        int choice = Convert.ToInt32(Console.ReadLine());
        switch (choice)
        {
          case 1:
            if (count >= maxsize)
            {
              Console.WriteLine("Inventory is full...");
            }
            else
            {
              adding_cart(inv, ref count);
            }
            break;
          case 2:
            ViewProduct(inv, count);
            break;
          case 3:
            UpdateProduct(inv, count);
            break;
          case 4:
            Console.WriteLine("byee...");
            return;
          default:
            Console.WriteLine("Invalid option. Please try again..");
            break;
        }
      }

    }
    public static void adding_cart(Product[] inv, ref int count)
    {
      Product obj = new Product();
      Console.WriteLine("Enter the Product Name: ");
      obj.Name = Console.ReadLine();
      Console.WriteLine("Enther the Product Pice:");
      obj.Price = Convert.ToDecimal(Console.ReadLine());
      Console.WriteLine("Enter the Product Quantity: ");
      obj.Quantity = Convert.ToInt32(Console.ReadLine());

      inv[count] = obj;
      count++;
      Console.WriteLine("Product added Successfully.");
    }

    public static void ViewProduct(Product[] inv, int count)
    {
      if (count == 0)
      {
        Console.WriteLine("No Products in inventory..");
        return;
      }
      Console.WriteLine("\n---Prodcut List---");
      for (int i = 0; i < count; i++)
      {
        Console.WriteLine($"{i + 1}. Name: {inv[i].Name}, Price: {inv[i].Price}, Qunatity: {inv[i].Quantity}");
      }
    }

    public static void UpdateProduct(Product[] inv, int count)
    {
      if (count == 0)
      {
        Console.WriteLine("No products to update...");
        return;
      }
      Console.WriteLine("Enter the product number to update: ");
      int no = Convert.ToInt32(Console.ReadLine());
      no -= 1;
      if (no < 0 || no >= count)
      {
        Console.WriteLine("Invalid product Number...");
        return;
      }
      Console.WriteLine("Enter the new price: ");
      inv[no].Price = Convert.ToDecimal(Console.ReadLine());
      Console.WriteLine("Enter the New qunatity: ");
      inv[no].Quantity = Convert.ToInt32(Console.ReadLine());

      Console.WriteLine("Product updated Sucessfully..");
    }
  }
}