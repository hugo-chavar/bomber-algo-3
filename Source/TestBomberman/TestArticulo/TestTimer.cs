using Bomberman.Articulo;
using Bomberman.Personaje;
using NUnit.Framework;
using Bomberman.Mapa.Casilla;
using Bomberman;

namespace TestBomberman.TestArticulo
{
    [TestFixture]
    class TestTimer
    {
        private Punto pos;
        private Casilla c;
        private Bombita unBombita;

        
        [TestFixtureSetUp]
        public void TestSetup()
        {
            Punto pos = new Punto(3, 4);
            Casilla c = new CasillaVacia(pos);
            Bombita unBombita = new Bombita(pos);
        }

        [Test]
        public void TestComerChalaDuplicaVelocidadDelComedor()
        {
            Articulo unArticulo = new Timer();
            c.agregarArticulo(unArticulo);
            unBombita.Comer(unArticulo);

            Assert.AreEqual(15, unBombita.ReduccionRetardoBombas);
        }
    }

}
