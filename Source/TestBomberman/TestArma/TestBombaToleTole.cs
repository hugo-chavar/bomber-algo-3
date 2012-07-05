using BombermanModel.Arma;
using BombermanModel.Mapa;
using NUnit.Framework;
using BombermanModel.Mapa.Casilla;
using BombermanModel;
using BombermanModel.Juego;

namespace TestBombermanModel.TestArma
{
    [TestFixture]
    class TestBombaToleTole
    {
        private Punto posicion;
        private Juego unJuego;

        [SetUp]
        public void TestSetup()
        {
            this.unJuego = Juego.Instancia();
            this.unJuego.ComenzarDesdeElPrincipio();
            this.unJuego.SeleccionarMapa();
            this.unJuego.CargarMapa();
            posicion = new Punto(3, 4);
        }


        [TearDown]
        public void TearDown()
        {
            Juego.Reiniciar();
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
