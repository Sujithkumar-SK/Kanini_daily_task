using Testdemokanini;

namespace testdemoxunit
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //3 A's
            string nm = "Anu";
            int age = 25;           
         
           Employee em = new Employee(nm,age);
            string expectedname = "Anu";
            int exage = 20;
            //Assert.Equal(expectedname, nm);
            // Assert.True(em.name == expectedname);
            Assert.Contains(expectedname, em.name);
           
        }
        [Theory]
        [InlineData("Anu",20)]
        [InlineData("Priya", 25)]
        public void Testwitharg(string nm,int age)
        {
            Employee em = new Employee(nm,age);
            Assert.Equal(nm, em.name);
        }
    }
}