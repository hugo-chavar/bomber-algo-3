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
        public void BombaToleToleEstaExplotadaAlSerCreadaDebeDevolverFalse()
        {
            Bomba bomba = new BombaToleTole(posicion, 0);
            Assert.AreEqual(bomba.EstaExplotado(), false);
        }

        [Test]
        public void BombaToleToleEstaExplotadaAlPasar4SegundosDebeDevolverFalse()
        {
            Bomba bomba = new BombaToleTole(posicion, 0);
            System.Threading.Thread.Sleep(4000);//Pasan 4 segundos
            bomba.CuandoPasaElTiempo();
            Assert.AreEqual(bomba.EstaExplotado(), false);
        }

        [Test]
        public void BombaToleToleEstaExplotadaAlPasar5SegundosDebeDevolverTrue()
        {
            Bomba bomba = new BombaToleTole(posicion, 0);
            System.Threading.Thread.Sleep(5000);//Pasan 5 segundos
            bomba.CuandoPasaElTiempo();
            Assert.AreEqual(bomba.EstaExplotado(), true);
        }
    }
}
