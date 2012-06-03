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

        private CasillaVacia unaCasillaVacia;
        
        [TestFixtureSetUp]
        public void TestSetup()
        {
            unaPosicion = new Punto(2, 3);
            this.unaCasillaVacia = new CasillaVacia(unaPosicion);
        }

        [Test]
        public void esTransitablePorUnPersonaje()
        {
            Personaje unPersonaje = new Bombita(unaPosicion);
            Assert.IsTrue(this.unaCasillaVacia.TransitablePor(unPersonaje));
        }
    }
}
