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
            [Test]
            public void testBombaToleToleEstaExplotadaAlSerCreadaDebeDevolverFalse()
            {
                Bomba bomba = new BombaToleTole(2, 3, 0);
                Assert.AreEqual(bomba.EstaExplotado(), false);
            }

            [Test]
            public void testBombaToleToleEstaExplotadaAlPasar4SegundosDebeDevolverFalse()
            {
                Bomba bomba = new BombaToleTole(2, 3, 0);
                bomba.CuandoPasaElTiempo();
                bomba.CuandoPasaElTiempo();
                bomba.CuandoPasaElTiempo();
                bomba.CuandoPasaElTiempo();
                Assert.AreEqual(bomba.EstaExplotado(), false);
            }

            [Test]
            public void testBombaMolotovEstaExplotadaAl5SegundosDebeDevolverTrue()
            {
                Bomba bomba = new BombaToleTole(2, 3, 0);
                bomba.CuandoPasaElTiempo();
                bomba.CuandoPasaElTiempo();
                bomba.CuandoPasaElTiempo();
                bomba.CuandoPasaElTiempo();
                bomba.CuandoPasaElTiempo();
                Assert.AreEqual(bomba.EstaExplotado(), true);
            }
        
    }
}
