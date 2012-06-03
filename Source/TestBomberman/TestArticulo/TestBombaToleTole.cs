using Bomberman;
using Bomberman.Arma;
using Bomberman.Articulo;
using Bomberman.Mapa.Casilla;
using Bomberman.Personaje;
using NUnit.Framework;

namespace TestBomberman.TestArticulo
{
    class TestBombaToleTole
    {
        private Punto pos;
        private Casilla c;
        private Bombita unBombita;


        [TestFixtureSetUp]
        public void TestSetup()
        {
            pos = new Punto(3, 4);
            c = new CasillaVacia(pos);
            unBombita = new Bombita(pos);
        }
        
        [Test]
        public void TestComerArticuloBombaToleToleModificaElLanzadorDeBombita()
        {
            Articulo unArticulo = new ArticuloBombaToleTole();
            ILanzador unLanzador = new LanzadorToleTole();
            c.agregarArticulo(unArticulo);
            unBombita.Comer(unArticulo);

            Assert.IsInstanceOf( typeof(LanzadorToleTole), unBombita.Lanzador);

        }
    }
}
