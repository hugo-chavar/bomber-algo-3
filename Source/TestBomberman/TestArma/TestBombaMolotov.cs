using BombermanModel.Arma;
using BombermanModel.Mapa;
using BombermanModel.Juego;
using NUnit.Framework;
using BombermanModel.Mapa.Casilla;
using BombermanModel;

namespace TestBombermanModel.TestArma
{
    [TestFixture]
    class TestBombaMolotov
    {
        private Juego unJuego;
        private Tablero unMapa;
        
        [SetUp]
        public void TestSetup()
        {

            this.unJuego = Juego.Instancia();
            this.unJuego.CargarMapa();
            this.unMapa = Juego.Instancia().Ambiente;
            //this.unJuego.Ambiente = this.unMapa;

        }

        //[TearDown]
        //public void Restart()
        //{
        //    this.juego = null;
        //    this.unMapa = null;
        //    this.unaCasilla = null;
        //}

        [Test]
        public void BombaMolotovEstaExplotadaAlSerPlantadaDebeDevolverFalse()
        {
            Punto unPto = new Punto(3, 4);
            Bomba bomba = new BombaMolotov(unPto, 0);
            Assert.IsFalse(bomba.EstaExplotado());
        }

        [Test]
        public void EstaExplotadaAlPasarUnTiempoDevuelveTrue()
        {
            Punto unPto = new Punto(2, 2);
            Bomba bomba = new BombaMolotov(unPto, 0);
            Juego.Instancia().Ambiente.ObtenerCasilla(unPto).PlantarExplosivo(bomba);
            System.Threading.Thread.Sleep(1000);//Dejo Pasar un segundo y explota
            bomba.CuandoPasaElTiempo();
            System.Threading.Thread.Sleep(1000);
            bomba.CuandoPasaElTiempo();
            System.Threading.Thread.Sleep(1000);
            bomba.CuandoPasaElTiempo();
            Assert.IsTrue(bomba.EstaExplotado());
        }

        [Test]
        public void DaniarBloqueDeCementoConBombaMolotovDisminuyeUnidadesDeResistenciaEn5Unidades() //corregir la X aca!!!!!!
        {
            Punto unPto = new Punto(3, 4);
            Bomba bomba = new BombaMolotov(unPto, 0);
            Obstaculo obstaculo = BloqueComun.CrearBloqueCemento();
            bomba.Daniar(obstaculo);
            Assert.AreEqual(obstaculo.UnidadesDeResistencia, 5);
        }

        [Test]
        public void ExplotaUnaBombaMolotovYTieneUnCasilleroASuIzquierdaConBloqueLadrilloLoDestruyeLuegoLaCasillaEsUnPasillo()
        {
            Punto posicion = new Punto(3, 0);
            Punto puntoCasillaDaniada = new Punto(2, 0);
            Bomba unaBomba = new BombaMolotov(posicion, 0);

            Casilla unaCasillaBomba = unMapa.ObtenerCasilla(posicion);
            unaCasillaBomba.PlantarExplosivo(unaBomba);

            unMapa.ManejarExplosion(unaBomba);

            Casilla unaCasillaDaniada = unMapa.ObtenerCasilla(puntoCasillaDaniada);

            Assert.IsInstanceOf(typeof(Pasillo), unaCasillaDaniada.Estado);
        }

        [Test]
        public void ExplotaUnaBombaMolotovYTieneUnCasilleroASuDerechaConBloqueLadrilloYLoDestruyeLuegoLaCasillaEsUnPasillo()
        {
            Punto posicion = new Punto(1, 0);
            Punto puntoCasillaDaniada = new Punto(2, 0);
            Bomba unaBomba = new BombaMolotov(posicion, 0);

            Casilla unaCasillaBomba = unMapa.ObtenerCasilla(posicion);
            unaCasillaBomba.PlantarExplosivo(unaBomba);

            unMapa.ManejarExplosion(unaBomba);

            Casilla unaCasillaDaniada = unMapa.ObtenerCasilla(puntoCasillaDaniada);

            Assert.IsInstanceOf(typeof(Pasillo), unaCasillaDaniada.Estado);
        }

        [Test]
        public void ExplotaUnaBombaMolotovYTieneUnCasilleroArribaConBloqueLadrilloLoDestruyeYLuegoLaCasillaEsUnPasillo()
        {
            Punto posicion = new Punto(0, 1);
            Punto puntoCasillaDaniada = new Punto(0, 2);
            Bomba unaBomba = new BombaMolotov(posicion, 0);

            Casilla unaCasillaBomba = unMapa.ObtenerCasilla(posicion);
            unaCasillaBomba.PlantarExplosivo(unaBomba);

            unMapa.ManejarExplosion(unaBomba);

            Casilla unaCasillaDaniada = unMapa.ObtenerCasilla(puntoCasillaDaniada);

            Assert.IsInstanceOf(typeof(Pasillo), unaCasillaDaniada.Estado);
        }

        [Test]
        public void ExplotaUnaBombaMolotovYTieneUnCasilleroAbajoConBloqueLadrilloLoDestruyeYLuegoLaCasillaEsUnPasillo()
        {
            Punto posicion = new Punto(0, 3);
            Punto puntoCasillaDaniada = new Punto(0, 2);
            Bomba unaBomba = new BombaMolotov(posicion, 0);

            Casilla unaCasillaBomba = unMapa.ObtenerCasilla(posicion);
            unaCasillaBomba.PlantarExplosivo(unaBomba);

            unMapa.ManejarExplosion(unaBomba);

            Casilla unaCasillaDaniada = unMapa.ObtenerCasilla(puntoCasillaDaniada);

            Assert.IsInstanceOf(typeof(Pasillo), unaCasillaDaniada.Estado);
        }
    }
}
