using BombermanModel;
using BombermanModel.Arma;
using BombermanModel.Articulo;
using BombermanModel.Mapa.Casilla;
using BombermanModel.Personaje;
using NUnit.Framework;

namespace TestBombermanModel.TestArticulo
{
    class TestArticuloBombaToleTole
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
        public void ComerArticuloBombaToleToleModificaElLanzadorDeBombita()
        {
            Articulo unArticulo = new ArticuloBombaToleTole();
            Lanzador unLanzador = new LanzadorToleTole();
            c.agregarArticulo(unArticulo);
            unBombita.Comer(unArticulo);

            Assert.IsInstanceOf( typeof(LanzadorToleTole), unBombita.Lanzador);
        }
    }
}
