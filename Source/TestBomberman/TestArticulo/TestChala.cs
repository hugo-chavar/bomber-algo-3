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
            pos = new Punto(3, 4);
            FabricaDeCasillas f = new FabricaDeCasillas();
            c = f.FabricarPasillo(pos);
            unBombita = new Bombita(pos);
        }
    
        [Test]
        public void TestComerChalaDuplicaVelocidadDelComedor()
        {
            int velocidad = unBombita.Velocidad;
            Articulo unArticulo = new Chala();
            unBombita.Comer(unArticulo);

            Assert.AreEqual(2*velocidad, unBombita.Velocidad);
         }


    }
}
