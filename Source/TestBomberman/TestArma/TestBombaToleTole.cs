using Bomberman.Arma;
using Bomberman.Mapa;
using NUnit.Framework;
using Bomberman.Mapa.Casilla;
using Bomberman;

namespace TestBomberman.TestArma
{
    [TestFixture]
    class TestBombaToleTole
    {
        private Punto posicion;

        [SetUp]
        public void TestSetup()
        {
            posicion = new Punto(3, 4);
        }

            [Test]
            public void TestBombaToleToleEstaExplotadaAlSerCreadaDebeDevolverFalse()
            {
                Bomba bomba = new BombaToleTole(posicion, 0);
                Assert.AreEqual(bomba.EstaExplotado(), false);
            }

            [Test]
            public void TestBombaToleToleEstaExplotadaAlPasar4SegundosDebeDevolverFalse()
            {
                Bomba bomba = new BombaToleTole(posicion, 0);
                bomba.CuandoPasaElTiempo();
                bomba.CuandoPasaElTiempo();
                bomba.CuandoPasaElTiempo();
                bomba.CuandoPasaElTiempo();
                Assert.AreEqual(bomba.EstaExplotado(), false);
            }

            [Test]
            public void TestBombaToleToleEstaExplotadaAl5SegundosDebeDevolverTrue()
            {
                Bomba bomba = new BombaToleTole(posicion, 0);
                bomba.CuandoPasaElTiempo();
                bomba.CuandoPasaElTiempo();
                bomba.CuandoPasaElTiempo();
                bomba.CuandoPasaElTiempo();
                bomba.CuandoPasaElTiempo();
                Assert.AreEqual(bomba.EstaExplotado(), true);
            }


        
    }
}
