using BombermanModel.Articulo;
using BombermanModel.Personaje;
using NUnit.Framework;
using BombermanModel.Mapa.Casilla;
using BombermanModel;

namespace TestBombermanModel.TestArticulo
{
    [TestFixture]
    class TestTimer
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
        public void ComerTimerReduceElRetardoDeBombas()
        {
            Articulo unArticulo = new Timer();
            c.agregarArticulo(unArticulo);
            unBombita.Comer(unArticulo);

            //Assert.AreEqual(15, unBombita.ReduccionRetardoBombas); cambio debido al nuevo Lanzador
            Assert.AreEqual(15, unBombita.Lanzador.RetardoExplosion);
        }
    }
}
