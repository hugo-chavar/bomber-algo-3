using Bomberman.Arma;
using Bomberman.Articulo;
using Bomberman.Personaje;
using NUnit.Framework;

namespace TestBomberman.TestArticulo
{
    class TestBombaToleTole
    {
        [Test]
        public void TestComerArticuloBombaToleToleCambiaElLanzadorDelComedor()
        {
            Bombita unBombita = new Bombita();
            Articulo unArticulo = new BombaToleTole();
            LanzadorToleTole unLanzador = new LanzadorToleTole();
            unBombita.comer(unArticulo);

            Assert.IsInstanceOf(unLanzador.GetType(), unBombita.Lanzador);

         }
    }
}
