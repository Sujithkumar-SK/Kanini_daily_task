namespace CalculatorLib.Tests.XUnit;

public class UnitTest1
{
    private readonly Calculator calc = new();
    [Fact]
    public void Add_ReturnsCorrectSum()
    {
        Assert.Equal(5, calc.Add(3, 2));
    }

    [Fact]
    public void Subtract_ReturnsCorrectDifference()
    {
        Assert.Equal(6, calc.Subtract(10, 4));
    }
    [Theory]
    [InlineData(2, 3, 6)]
    [InlineData(0, 5, 0)]
    [InlineData(-2, 4, -8)]
    [InlineData(-3, -3, 9)]
    [InlineData(100, 0, 0)]
    [InlineData(7, 1, 7)]
    public void Multiply_MultipleCases_ReturnsExpectedResult(int a, int b, int expected)
    {
        var result = calc.Multiply(a, b);
        Assert.Equal(expected, result);
    }
    [Fact]
    public void Divide_ReturnsCorrectQuotient()
    {
        Assert.Equal(5, calc.Divide(10, 2));
    }
    [Fact]
    public void Modulo_ReturnsCorrectRemainder()
    {
        Assert.Equal(1, calc.Modulo(10, 3));
    }
}
