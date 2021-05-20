using NUnit.Framework;

namespace NUnit_Produt_Tests
{
    // TestFixture indica que a classe deverá ser testada
    [TestFixture]
    public class Tests
    {
        // Setup indica configurações iniciais da classe de teste, é chamado antes de cada método de teste
        [SetUp]
        public void Setup()
        {
        }

        // Test indica que o método abaixo deverá ser testado
        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}