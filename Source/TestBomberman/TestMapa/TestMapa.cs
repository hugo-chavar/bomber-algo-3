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
            int i,j;
            Punto unaPosicion;
            this.unMapa = new Mapa(5,5);
            for (i=0;i<5;i++)
                for (j = 0; j < 5; j++)
                {
                    unaPosicion = new Punto(i, j);
                }

        }

    }
}
