


//Generics - Code flexibility and reusability
//Generics - class,method,parameter,interface,baseclass

using System.Data.Common;

internal class Program
{
    private static void Main(string[] args)
    {
        TypeChecker(12);
        TypeChecker("Anu");      

    }
    //Generic Method with Generic parameter
    static void TypeChecker<T>(T value)
    {
        Console.WriteLine(typeof(T));
        Console.WriteLine(value);
    }
}