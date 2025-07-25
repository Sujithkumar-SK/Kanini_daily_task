using NUnit.Framework;
using MyApp;
namespace MyApp.Tests;

public class Tests
{
    private Class1? obj;

    [SetUp]
    public void Setup()
    {
        obj = new Class1();
    }

    [Test]
    public void Test1()
    {
        int res = obj!.Add(3, 4);
        Assert.That(res, Is.EqualTo(7));
    }
}
