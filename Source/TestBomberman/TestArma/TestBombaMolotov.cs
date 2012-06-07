using Bomberman.Arma;
using Bomberman.Mapa;
using Bomberman.Juego;
using NUnit.Framework;
using Bomberman.Mapa.Casilla;
using Bomberman;

namespace TestBomberman.TestArma
{
    [TestFixture]
    class TestBombaMolotov
    {
        private Punto posicion;
        private Juego juego;

        [TestFixtureSetUp]
        public void TestSetup()
        {
            juego = new Juego();
            posicion = new Punto(3, 4);
            Casilla unaCasilla = FabricaDeCasillas.FabricarPasillo(posicion);
            juego.Ambiente.AgregarCasilla(unaCasilla);
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
            juego.Ambiente.ObtenerCasilla(posicion).PlantarExplosivo(bomba);
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
