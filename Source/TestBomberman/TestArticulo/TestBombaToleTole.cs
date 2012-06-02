using Bomberman.Arma;
using Bomberman.Articulo;
using Bomberman.Personaje;
using NUnit.Framework;

namespace TestBomberman.TestArticulo
{
    class TestBombaToleTole
    {

        [Test]
        public void TestComerArticuloBombaToleToleModificaElLanzadorDeBombita()
        {
            Bombita unBombita = new Bombita();
            Articulo unArticulo = new BombaToleTole();
            Lanzador unLanzador = new LanzadorToleTole();

            unBombita.comer(unArticulo);

            Assert.IsInstanceOf( typeof(LanzadorToleTole), unBombita.Lanzador);

        }
    }
}
