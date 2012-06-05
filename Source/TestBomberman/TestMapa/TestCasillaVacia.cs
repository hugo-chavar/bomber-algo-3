using NUnit;
using NUnit.Framework;
using Bomberman;
using Bomberman.Mapa;
using Bomberman.Mapa.Casilla;
using Bomberman.Personaje;

namespace TestBomberman.TestMapa
{
    [TestFixture]
    class TestCasillaVacia
    {
        private Punto unaPosicion;

        private Casilla unaCasillaVacia;
        
        [TestFixtureSetUp]
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
