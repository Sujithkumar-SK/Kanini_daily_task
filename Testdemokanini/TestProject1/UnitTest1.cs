using Testdemokanini;

namespace TestProject1
{
    public class Tests
    {
        private MathOpe mth;
        [SetUp]
        public void Setup()
        {
           mth = new MathOpe();
        }

        [Test]
        public void TestAdd()
        {
          //  mth = new MathOpe();//Arrange
            int res = mth.Sum(10, 20);
            Assert.AreEqual(30, res);
            
        }
        [Test]
        public void TestSub()
        {
           // mth = new MathOpe();//Arrange
            int res = mth.Sub(60, 30);
            Assert.AreEqual(30, res);

        }
        [Test]
        public void TestProd()
        {
          //mth = new MathOpe();//Arrange
            int res = mth.Product(10,2);
            Assert.AreEqual(20, res);

        }
        [TestCase(100,5,20)]
        [TestCase(150, 0, 0)]
        [TestCase(50, 5, 25)]

        public void TestDiv(int a,int b,int expected)
        {
        //   mth = new MathOpe();//Arrange
         
          //  Assert.AreEqual(expected, res);
            if(b==0)
            {
                Assert.Throws<DivideByZeroException>(() => mth.Div(a, b));
            }
            else
            {
                Assert.AreEqual(expected, a/b);
            }
        }

    
    }
}