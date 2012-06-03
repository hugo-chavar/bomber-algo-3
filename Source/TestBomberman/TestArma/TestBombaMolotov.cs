using Bomberman.Arma;
using Bomberman.Mapa;
using NUnit.Framework;
using Bomberman.Mapa.Casilla;
using Bomberman;

namespace TestBomberman.TestArma
{
    [TestFixture]
    class TestBombaMolotov
    {
        //No logro que me funcione bien el setup
       /* private Bomba bomba;

        [TestFixtureSetUp]
        public void TestSetup()
        {
            bomba = new BombaMolotov(2, 3, 0);
        }
        */
        [Test]
        public void testBombaMolotovEstaExplotadaAlSerPlantadaDebeDevolverFalse()
        {
            Bomba bomba = new BombaMolotov(2, 3, 0);
            Assert.AreEqual(bomba.EstaExplotado(), false);
        }

        [Test]
        public void testBombaMolotovEstaExplotadaAlPasarUnTiempoDebeDevolverTrue()
        {
            Bomba bomba = new BombaMolotov(2, 3, 0);
            bomba.CuandoPasaElTiempo();
            Assert.AreEqual(bomba.EstaExplotado(), true);
        }
    }
}
