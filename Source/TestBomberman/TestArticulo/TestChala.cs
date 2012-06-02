using Bomberman.Articulo;
using Bomberman.Personaje;
using NUnit.Framework;
using Bomberman;


namespace TestBomberman.TestArticulo
{   [TestFixture]
    class TestChala
    {
        [Test]
        public void TestComerChalaDuplicaVelocidadDelComedor()
        {
            int velocidad;
            Bombita unBombita = new Bombita();
            Articulo unArticulo = new Chala();
            velocidad = unBombita.Velocidad;
            unBombita.comer(unArticulo);

            Assert.AreEqual(2*velocidad, unBombita.Velocidad);
         }


    }
}
