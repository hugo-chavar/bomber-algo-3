using NUnit;
using NUnit.Framework;
using Bomberman;
using Bomberman.Mapa;
using Bomberman.Mapa.Casilla;

namespace TestBomberman.TestMapa
{
    [TestFixture]
    class TestMapa
    {
        private Mapa unMapa;

        [TestFixtureSetUp]
        public void TestSetup()
        {
            this.unMapa = new Mapa(5,5);
            Punto unaPosicion = new Punto(2, 3);


        }

    }
}
