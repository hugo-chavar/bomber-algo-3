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
    
        [SetUp]
        public void TestSetup()
        {
            pos = new Punto(3, 4);
            c = FabricaDeCasillas.FabricarPasillo(pos);
            unBombita = new Bombita(pos);
        }
    
        [Test]
        public void ComerChalaDuplicaVelocidadDelComedor()
        {
            int velocidad = unBombita.Movimiento.Velocidad;
            Articulo unArticulo = new Chala();
            unBombita.Comer(unArticulo);

            Assert.AreEqual(2*velocidad, unBombita.Movimiento.Velocidad);
         }
    }
}
