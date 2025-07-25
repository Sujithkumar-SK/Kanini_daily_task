namespace CalculatorLib.Tests.NUnit;

public class Tests
{
    private Calculator? calc;
    [SetUp]
    public void Setup()
    {
        calc = new Calculator();
    }

    [Test]
    public void Add_ReturnCorrectSum()
    {
        var res = calc!.Add(3, 2);
        Assert.That(res, Is.EqualTo(5));
    }
    [Test]
    public void Subtract_Return_diff()
    {
        var res = calc!.Subtract(5, 2);
        Assert.That(res, Is.EqualTo(3));
    }
    [TestCase(2, 3, 6)]
    [TestCase(0, 5, 0)]
    [TestCase(-2, 4, -8)]
    [TestCase(-3, -3, 9)]
    [TestCase(100, 0, 0)]
    [TestCase(7, 1, 7)]
    public void Multiply_Result(int a, int b, int expected)
    {
        var result = calc!.Multiply(a, b);
        Assert.That(result, Is.EqualTo(expected));
    }
    [Test]
    public void Divide_Return_Ans()
    {
        var res = calc!.Divide(10, 2);
        Assert.That(res, Is.EqualTo(5));
    }
    [Test]
    public void Modulo_ReturnsCorrectRemainder()
    {
        Assert.That(calc?.Modulo(10, 3), Is.EqualTo(1));
    }
}
