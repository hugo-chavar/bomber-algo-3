using BombermanModel.Articulo;
using BombermanModel.Personaje;
using NUnit.Framework;
using BombermanModel;
using BombermanModel.Mapa.Casilla;


namespace TestBombermanModel.TestArticulo
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
            float velocidad = unBombita.Movimiento.Velocidad;
            Articulo unArticulo = new Chala();
            unBombita.Comer(unArticulo);

            Assert.AreEqual(2*velocidad, unBombita.Movimiento.Velocidad);
         }
    }
}
