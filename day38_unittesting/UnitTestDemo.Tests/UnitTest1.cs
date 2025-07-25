namespace UnitTestDemo.Tests;
using NUnit.Framework;
using UnitTestDemo;


public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        var calc = new Calculator();
        int res = calc.Add(2, 3);
        Assert.That(res, Is.EqualTo(5));
    }
}
