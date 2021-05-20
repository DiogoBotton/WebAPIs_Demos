using NUnit.Framework;

namespace NUnit_Produt_Tests
{
    // TestFixture indica que a classe dever� ser testada
    [TestFixture]
    public class Tests
    {
        // Setup indica configura��es iniciais da classe de teste, � chamado antes de cada m�todo de teste
        [SetUp]
        public void Setup()
        {
        }

        // Test indica que o m�todo abaixo dever� ser testado
        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}