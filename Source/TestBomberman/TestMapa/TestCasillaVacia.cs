using NUnit;
using NUnit.Framework;
using BombermanModel;
using BombermanModel.Mapa;
using BombermanModel.Mapa.Casilla;
using BombermanModel.Personaje;

namespace TestBombermanModel.TestMapa
{
    [TestFixture]
    class TestCasillaVacia
    {
        private Punto unaPosicion;

        private Casilla unaCasillaVacia;
        
        [SetUp]
        public void TestSetup()
        {
            unaPosicion = new Punto(2, 3);
            this.unaCasillaVacia = FabricaDeCasillas.FabricarPasillo(unaPosicion);
        }

        [Test]
        public void esTransitablePorUnPersonaje()
        {
            Personaje unPersonaje = new Bombita(unaPosicion);
            Assert.IsTrue(this.unaCasillaVacia.PermiteTransitarUn(unPersonaje));
        }
    }
}
