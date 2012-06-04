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
            pos = new Punto(3, 4);
            //FabricaDeCasillas f = new FabricaDeCasillas();
            c = FabricaDeCasillas.FabricarPasillo(pos);
            unBombita = new Bombita(pos);
        }

        [Test]
        public void TestComerTimerReduceElRetardoDeBombas()
        {
            Articulo unArticulo = new Timer();
            c.agregarArticulo(unArticulo);
            unBombita.Comer(unArticulo);

            Assert.AreEqual(15, unBombita.ReduccionRetardoBombas);
        }
    }

}
