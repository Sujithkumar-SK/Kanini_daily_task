namespace CalculatorLib;

public class Calculator
{
  public int Add(int a, int b) => a + b;
  public int Subtract(int a, int b) => a - b;
  public int Multiply(int a, int b) => a * b;
  public double Divide(int a, int b)
  {
    if (b == 0)
      throw new DivideByZeroException("Cannot divide by Zero.");
    return (double)a / b;
  }
  public int Modulo(int a, int b)
  {
    if (b == 0)
      throw new DivideByZeroException("Cannot find modulo with zero..");
    return a % b;
  }

}
