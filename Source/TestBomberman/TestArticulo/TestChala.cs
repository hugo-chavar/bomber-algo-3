using Bomberman.Articulo;
using Bomberman.Personaje;
using NUnit.Framework;
using Bomberman;
using Bomberman.Mapa.Casilla;


namespace TestBomberman.TestArticulo
{   [TestFixture]
    class TestChala
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
            int velocidad;
            Articulo unArticulo = new Chala();
            velocidad = unBombita.Velocidad;
            unBombita.Comer(unArticulo);

            Assert.AreEqual(2*velocidad, unBombita.Velocidad);
         }


    }
}
