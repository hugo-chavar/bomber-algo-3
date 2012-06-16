using NUnit.Framework;
using Bomberman.Juego;
using Bomberman.Mapa;
using Bomberman.Mapa.Casilla;
using Bomberman.Personaje;
using Bomberman.Articulo;
using Bomberman.Arma;
using Bomberman;
namespace TestBomberman.TestIntegracion
{
    class TestIntegracionExplosivoYPersonaje
    {
        private Juego unJuego;
        private Mapa unMapa;


        [SetUp]
        public void TestSetup()
        {
            this.unJuego = Juego.Instancia();
            this.unMapa = this.unJuego.Ambiente;

        }

        [TearDown]
        public void TearDown()
        {
            Juego.Reiniciar();
        }

        [Test]
        public void BombitaPlantaUnaMolotovSeMueveFueraDeSuAlcanceYNoEsDaniadoPorLaBomba()
        {
            Punto PosicionDePlantado = new Punto(1,0);
            Bombita bombita = new Bombita(PosicionDePlantado);
            this.unJuego.Ambiente.ObtenerCasilla(PosicionDePlantado).Transitar(bombita);
            bombita.LanzarExplosivo();
            bombita.Movimiento.CambiarAIzquierda();
            bombita.Mover();
            bombita.Movimiento.CambiarAArriba();
            bombita.Mover();
            this.unJuego.AvanzarElTiempo();
            Assert.IsFalse(bombita.Destruido());
        }


        [Test]
        public void BombitaPlantaUnaMolotovSeMueveFueraDeSuAlcanceYLuegoDeQueLaBombaExplotaVuelveYPlantaOtraMolotov()
        {
            Punto PosicionDePlantado=new Punto(7,0);
            Bombita bombita = new Bombita(PosicionDePlantado);
            this.unJuego.Ambiente.ObtenerCasilla(PosicionDePlantado).Transitar(bombita);
            bombita.LanzarExplosivo();
            bombita.Movimiento.CambiarADerecha();
            bombita.Mover();
            bombita.Movimiento.CambiarAArriba();
            bombita.Mover();
            this.unJuego.AvanzarElTiempo();
            bombita.Movimiento.CambiarAAbajo();
            bombita.Mover();
            bombita.Movimiento.CambiarAIzquierda();
            bombita.Mover();
            bombita.LanzarExplosivo();
            Assert.IsInstanceOf( typeof(BombaMolotov),Juego.Instancia().Ambiente.ObtenerCasilla(PosicionDePlantado).Explosivo);
        }

        [Test]
        public void BombitaPlantaUnaMolotovSeMueveFueraDeSuAlcanceAgarraArticuloYLuegoDeQueLaBombaExplotaVuelveYPlantaUnaToleTole()
        {
            Punto PosicionDePlantado = new Punto(8, 2);
            Bombita bombita = new Bombita(PosicionDePlantado);
            Casilla unaCasilla = this.unJuego.Ambiente.ObtenerCasilla(PosicionDePlantado);
            Casilla casillaBloqueConArticulo = this.unJuego.Ambiente.ObtenerCasilla(new Punto(7, 2));
            unaCasilla.Transitar(bombita);
            bombita.LanzarExplosivo();
            bombita.Movimiento.CambiarADerecha();
            bombita.Mover();
            bombita.Mover();
            bombita.Movimiento.CambiarAArriba();
            bombita.Mover();
            unJuego.AvanzarElTiempo();
            bombita.Movimiento.CambiarAAbajo();
            bombita.Mover();
            bombita.Movimiento.CambiarAIzquierda();
            bombita.Mover();
            bombita.Mover();
            bombita.Mover(); //come articulo BombaToleToe
            bombita.LanzarExplosivo();
            Assert.IsInstanceOf(typeof(BombaToleTole), this.unJuego.Ambiente.ObtenerCasilla(new Punto(7, 2)).Explosivo);
            Assert.IsFalse(bombita.Destruido());
        }

        [Test]
        public void LopezReggaeAladoPlantaUnaMolotovSeMueveFueraDeSuAlcanceYLuegoDeQueLaBombaExplotaVuelveYPlantaOtra()
        {
            Punto PosicionDePlantado = new Punto(0, 1);
            LosLopezReggaeAlado personaje = new LosLopezReggaeAlado(PosicionDePlantado);
            Juego.Instancia().Ambiente.ObtenerCasilla(PosicionDePlantado).Transitar(personaje);
            personaje.LanzarExplosivo();
            personaje.Movimiento.CambiarADerecha();
            personaje.Mover();
            personaje.Mover();
            personaje.Mover();
            personaje.Mover();
            Juego.Instancia().AvanzarElTiempo();
            personaje.Movimiento.CambiarAIzquierda();
            personaje.Mover();
            personaje.Mover();
            personaje.Mover();
            personaje.Mover();
            personaje.LanzarExplosivo();
            Assert.IsInstanceOf(typeof(BombaMolotov), Juego.Instancia().Ambiente.ObtenerCasilla(PosicionDePlantado).Explosivo);
            Assert.IsFalse(personaje.Destruido());
        }

        [Test]
        public void LopezReggaeDisparaProyectilHaciaBloqueDeLadrilloAlImpactarDisminuyeSuResistenciaEnUnaUnidad()
        {
            Punto PosicionDePartida = new Punto(5, 0);
            LosLopezReggae personaje = new LosLopezReggae(PosicionDePartida);
            Casilla casillaConBloqueDeLadrillo = this.unJuego.Ambiente.ObtenerCasilla(new Punto(2, 0));
            int resistenciaBloque = casillaConBloqueDeLadrillo.Estado.UnidadesDeResistencia;
            //casillaAux.Estado = BloqueComun.CrearBloqueLadrillos();
            this.unJuego.AgregarEnemigo(personaje);
            personaje.Movimiento.CambiarAIzquierda();
            personaje.LanzarExplosivo();
            this.unJuego.AvanzarElTiempo();
            this.unJuego.AvanzarElTiempo();
            this.unJuego.AvanzarElTiempo();
            Assert.AreEqual(resistenciaBloque - 1, casillaConBloqueDeLadrillo.Estado.UnidadesDeResistencia);
        }
        




    }
}
