using Bomberman.Articulo;
using Bomberman.Personaje;
using NUnit.Framework;

namespace TestBomberman.TestArticulo
{
    [TestFixture]
    class TestTimer
    {
        [Test]
        public void TestComerChalaDuplicaVelocidadDelComedor()
        {
            Bombita unBombita = new Bombita();
            Articulo unArticulo = new Timer();
            
            unBombita.Comer(unArticulo);

            Assert.AreEqual(15, unBombita.ReduccionRetardoBombas);
        }
    }

}
