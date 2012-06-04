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
        private Punto posicion;

        [TestFixtureSetUp]
        public void TestSetup()
        {
            posicion = new Punto(3, 4);
        }

        [Test]
        public void TestBombaMolotovEstaExplotadaAlSerPlantadaDebeDevolverFalse()
        {
            Bomba bomba = new BombaMolotov(posicion, 0);
            Assert.AreEqual(bomba.EstaExplotado(), false);
        }

        [Test]
        public void TestBombaMolotovEstaExplotadaAlPasarUnTiempoDebeDevolverTrue()
        {
            Bomba bomba = new BombaMolotov(posicion, 0);
            bomba.CuandoPasaElTiempo();
            Assert.AreEqual(bomba.EstaExplotado(), true);
        }

        [Test]
        public void TestDaniarObstaculoConBombaMolotov()
        { 
           Bomba bomba = new BombaMolotov(posicion, 0);
           Obstaculo obstaculo= BloqueComun.CrearBloqueCemento();
           bomba.Daniar(obstaculo);
           Assert.AreEqual(obstaculo.UnidadesDeResistencia, 5);
        }
    }
}
