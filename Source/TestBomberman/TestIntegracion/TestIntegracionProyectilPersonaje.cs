using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BombermanModel.Juego;
using BombermanModel.Mapa;
using NUnit.Framework;
using BombermanModel.Personaje;
using BombermanModel;
using BombermanModel.Mapa.Casilla;
using BombermanModel.Arma;

namespace TestBombermanModel.TestIntegracion
{
    class TestIntegracionProyectilPersonaje
    {
        private Juego unJuego;
        private Tablero unMapa;

        [SetUp]
        public void TestSetup()
        {
            this.unJuego = Juego.Instancia();
            this.unJuego.ComenzarDesdeElPrincipio();
            this.unJuego.SeleccionarMapa();
            this.unJuego.CargarMapa();
            this.unMapa = this.unJuego.Ambiente;

        }

        [TearDown]
        public void TearDown()
        {
            this.unJuego.ComenzarDesdeElPrincipio();
            this.unJuego.CargarMapa();
        }

        [Test]
        public void TiroUnProyectilATresCasillasDeDistanciaYLosObjetosGolpeadosBajanSuResistenciaEnUnaUnidad()
        {
            Personaje unTipoQueTiraProyectiles = new LosLopezReggae(new Punto(13, 6));

            unTipoQueTiraProyectiles.Movimiento.CambiarADerecha();
            unTipoQueTiraProyectiles.LanzarExplosivo();
            int resistenciaLadrillo1 = Juego.Instancia().Ambiente.ObtenerCasilla(new Punto(16, 5)).Estado.UnidadesDeResistencia;
            int resistenciaLadrillo2 = Juego.Instancia().Ambiente.ObtenerCasilla(new Punto(16, 7)).Estado.UnidadesDeResistencia;

            System.Threading.Thread.Sleep(300);
            this.unJuego.AvanzarElTiempo();
            System.Threading.Thread.Sleep(300);
            this.unJuego.AvanzarElTiempo();
            System.Threading.Thread.Sleep(300);
            this.unJuego.AvanzarElTiempo();

            Assert.AreEqual(resistenciaLadrillo1 - 1, Juego.Instancia().Ambiente.ObtenerCasilla(new Punto(16, 5)).Estado.UnidadesDeResistencia);
            Assert.AreEqual(resistenciaLadrillo2 - 1, Juego.Instancia().Ambiente.ObtenerCasilla(new Punto(16, 7)).Estado.UnidadesDeResistencia);
        }

        [Test]
        public void TiroUnProyectilADosCasillasDeDistanciaYLosObjetosGolpeadosBajanSuResistenciaEnUnaUnidad()
        {
            Personaje unTipoQueTiraProyectiles = new LosLopezReggae(new Punto(14, 6));

            unTipoQueTiraProyectiles.Movimiento.CambiarADerecha();
            unTipoQueTiraProyectiles.LanzarExplosivo();
            int resistenciaLadrillo1 = Juego.Instancia().Ambiente.ObtenerCasilla(new Punto(16, 5)).Estado.UnidadesDeResistencia;
            int resistenciaLadrillo2 = Juego.Instancia().Ambiente.ObtenerCasilla(new Punto(16, 7)).Estado.UnidadesDeResistencia;

            System.Threading.Thread.Sleep(300);
            this.unJuego.AvanzarElTiempo();
            System.Threading.Thread.Sleep(300);
            this.unJuego.AvanzarElTiempo();
            System.Threading.Thread.Sleep(300);
            this.unJuego.AvanzarElTiempo();

            Assert.AreEqual(resistenciaLadrillo1 - 1, Juego.Instancia().Ambiente.ObtenerCasilla(new Punto(16, 5)).Estado.UnidadesDeResistencia);
            Assert.AreEqual(resistenciaLadrillo2 - 1, Juego.Instancia().Ambiente.ObtenerCasilla(new Punto(16, 7)).Estado.UnidadesDeResistencia);
        }

        [Test]
        public void TiroUnProyectilAUnaCasillasDeDistanciaYLosObjetosGolpeadosBajanSuResistenciaEnUnaUnidad()
        {
            Personaje unTipoQueTiraProyectiles = new LosLopezReggae(new Punto(15, 6));

            unTipoQueTiraProyectiles.Movimiento.CambiarADerecha();
            unTipoQueTiraProyectiles.LanzarExplosivo();
            int resistenciaLadrillo1 = Juego.Instancia().Ambiente.ObtenerCasilla(new Punto(16, 5)).Estado.UnidadesDeResistencia;
            int resistenciaLadrillo2 = Juego.Instancia().Ambiente.ObtenerCasilla(new Punto(16, 7)).Estado.UnidadesDeResistencia;

            System.Threading.Thread.Sleep(300);
            this.unJuego.AvanzarElTiempo();
            System.Threading.Thread.Sleep(300);
            this.unJuego.AvanzarElTiempo();

            Assert.AreEqual(resistenciaLadrillo1 - 1, Juego.Instancia().Ambiente.ObtenerCasilla(new Punto(16, 5)).Estado.UnidadesDeResistencia);
            Assert.AreEqual(resistenciaLadrillo2 - 1, Juego.Instancia().Ambiente.ObtenerCasilla(new Punto(16, 7)).Estado.UnidadesDeResistencia);
        }

        [Test]
        public void TiroCincoProyectilesAUnLadrilloYElMismoSeDestruye()
        {
            Personaje unTipoQueTiraProyectiles = new LosLopezReggae(new Punto(11, 12));

            unTipoQueTiraProyectiles.Movimiento.CambiarADerecha();
            unTipoQueTiraProyectiles.LanzarExplosivo();
            unTipoQueTiraProyectiles.LanzarExplosivo();
            unTipoQueTiraProyectiles.LanzarExplosivo();
            unTipoQueTiraProyectiles.LanzarExplosivo();
            unTipoQueTiraProyectiles.LanzarExplosivo();

            System.Threading.Thread.Sleep(300);
            this.unJuego.AvanzarElTiempo();
            System.Threading.Thread.Sleep(300);
            this.unJuego.AvanzarElTiempo();
            System.Threading.Thread.Sleep(300);
            this.unJuego.AvanzarElTiempo();

            Assert.IsInstanceOf(typeof(Pasillo), Juego.Instancia().Ambiente.ObtenerCasilla(new Punto(14, 12)).Estado);
        }

        [Test]
        public void TiroUnProyectilPorUnCaminoVacioYComoTieneAlcanceTresNoDaniaAlLanzador()
        {
            Personaje unTipoQueTiraProyectiles = new LosLopezReggae(new Punto(13, 6));

            unTipoQueTiraProyectiles.Movimiento.CambiarADerecha();
            int vida = unTipoQueTiraProyectiles.UnidadesDeResistencia;
            unTipoQueTiraProyectiles.LanzarExplosivo();

            System.Threading.Thread.Sleep(300);
            this.unJuego.AvanzarElTiempo();
            System.Threading.Thread.Sleep(300);
            this.unJuego.AvanzarElTiempo();
            System.Threading.Thread.Sleep(300);
            this.unJuego.AvanzarElTiempo();

            Assert.AreEqual(vida, unTipoQueTiraProyectiles.UnidadesDeResistencia);
        }

        [Test]
        public void TiroUnProyectilADosPosicionesDeDistanciaDelBordeDelMapaYElLanzadorNoRecibeDanios()
        {
            Personaje unTipoQueTiraProyectiles = new LosLopezReggae(new Punto(14, 6));

            unTipoQueTiraProyectiles.Movimiento.CambiarADerecha();
            int vida = unTipoQueTiraProyectiles.UnidadesDeResistencia;
            unTipoQueTiraProyectiles.LanzarExplosivo();

            System.Threading.Thread.Sleep(300);
            this.unJuego.AvanzarElTiempo();
            System.Threading.Thread.Sleep(300);
            this.unJuego.AvanzarElTiempo();
            System.Threading.Thread.Sleep(300);
            this.unJuego.AvanzarElTiempo();

            Assert.AreEqual(vida, unTipoQueTiraProyectiles.UnidadesDeResistencia);
        }

        [Test]
        public void TiroUnProyectilAUnaPosicionDeDistanciaDelBordeDelMapaYElLanzadorRecibeDanios()
        {
            Personaje unTipoQueTiraProyectiles = new LosLopezReggae(new Punto(15, 6));

            Juego.Instancia().Ambiente.AgregarPersonaje(unTipoQueTiraProyectiles);
            unTipoQueTiraProyectiles.Movimiento.CambiarADerecha();
            int vida = unTipoQueTiraProyectiles.UnidadesDeResistencia;
            unTipoQueTiraProyectiles.LanzarExplosivo();

            System.Threading.Thread.Sleep(300);
            this.unJuego.AvanzarElTiempo();
            System.Threading.Thread.Sleep(300);
            this.unJuego.AvanzarElTiempo();
            System.Threading.Thread.Sleep(300);
            this.unJuego.AvanzarElTiempo();

            Assert.AreEqual(vida - 1, unTipoQueTiraProyectiles.UnidadesDeResistencia);
        }

        [Test]
        public void ProyectilNoDebeAtravesarBloqueDeCemento()
        {
            Personaje personaje = new LosLopezReggae(new Punto(7, 12));
            Juego.Instancia().Ambiente.AgregarPersonaje(personaje);
            personaje.Movimiento.CambiarADerecha();
            personaje.LanzarExplosivo();
            System.Threading.Thread.Sleep(300);
            this.unJuego.AvanzarElTiempo();
            System.Threading.Thread.Sleep(300);
            this.unJuego.AvanzarElTiempo();
            Assert.AreEqual(Juego.Instancia().Ambiente.ObtenerCasilla(new Punto(9, 12)).TransitandoEnCasilla.Count, 0);
        }

        [Test]
        public void ProyectilChocaConBloqueLadrillosYAlPasarUnTiempoDaniaEn1ABloqueDeLadrillosYDeCemento()
        {
            Personaje personaje = new LosLopezReggae(new Punto(8, 8));
            Juego.Instancia().Ambiente.AgregarPersonaje(personaje);
            personaje.Movimiento.CambiarADerecha();
            personaje.LanzarExplosivo();
            System.Threading.Thread.Sleep(300);
            this.unJuego.AvanzarElTiempo();
            Assert.AreEqual(Juego.Instancia().Ambiente.ObtenerCasilla(new Punto(9, 8)).Estado.UnidadesDeResistencia, 4);
            Assert.AreEqual(Juego.Instancia().Ambiente.ObtenerCasilla(new Punto(10, 8)).Estado.UnidadesDeResistencia, 9);
        }

        [Test]
        public void ProyectilChocaConBloqueDeAceroYAlPasarUnTiempoExplotaYNoDaniaBloquesAcero()
        {
            //Juego.Reiniciar();
            Personaje personaje = new LosLopezReggae(new Punto(2, 7));
            Juego.Instancia().Ambiente.AgregarPersonaje(personaje);
            personaje.Movimiento.CambiarADerecha();
            personaje.LanzarExplosivo();
            System.Threading.Thread.Sleep(300);
            this.unJuego.AvanzarElTiempo();
            Assert.AreEqual(Juego.Instancia().Ambiente.ObtenerCasilla(new Punto(1, 7)).Estado.UnidadesDeResistencia, 10);
            Assert.AreEqual(Juego.Instancia().Ambiente.ObtenerCasilla(new Punto(3, 7)).Estado.UnidadesDeResistencia, 10);
        }

        [Test]
        public void ProyectilChocaConBloqueDeAceroYAlPasarUnTiempoExplotaYDaniaPersonaje()
        {
            Personaje personaje = new LosLopezReggae(new Punto(2, 7));
            Juego.Instancia().Ambiente.AgregarPersonaje(personaje);
            personaje.Movimiento.CambiarADerecha();
            personaje.LanzarExplosivo();
            System.Threading.Thread.Sleep(300);
            this.unJuego.AvanzarElTiempo();
            Assert.AreEqual(personaje.UnidadesDeResistencia, 9);
        }

        [Test]
        public void ProyectilNoImpactaACecilioAlPasarDosTiemposPorLoQueNoExplotaYNoLoDania()
        {
            Personaje personaje = new LosLopezReggae(new Punto(10, 6));
            Personaje cecilio = new Cecilio(new Punto(9, 6));
            Juego.Instancia().Ambiente.AgregarPersonaje(personaje);
            Juego.Instancia().Ambiente.AgregarPersonaje(cecilio);
            int vida = cecilio.UnidadesDeResistencia;
            personaje.Movimiento.CambiarAIzquierda();
            personaje.LanzarExplosivo();
            System.Threading.Thread.Sleep(300);
            this.unJuego.AvanzarElTiempo();
            System.Threading.Thread.Sleep(300);
            this.unJuego.AvanzarElTiempo();
            Assert.AreEqual(cecilio.UnidadesDeResistencia, vida);
        }

        [Test]
        public void ProyectilNoImpactaACecilioYAlPasarTresTiemposExplotaYLoDania()
        {
            Personaje personaje = new LosLopezReggae(new Punto(6, 0));
            Personaje cecilio = new Cecilio(new Punto(4, 0));
            Juego.Instancia().Ambiente.AgregarPersonaje(personaje);
            Juego.Instancia().Ambiente.AgregarPersonaje(cecilio);
            int vida = cecilio.UnidadesDeResistencia;
            personaje.Movimiento.CambiarAIzquierda();
            personaje.LanzarExplosivo();
            System.Threading.Thread.Sleep(300);
            this.unJuego.AvanzarElTiempo();
            System.Threading.Thread.Sleep(300);
            this.unJuego.AvanzarElTiempo();
            System.Threading.Thread.Sleep(300);
            this.unJuego.AvanzarElTiempo();
            Assert.AreEqual(cecilio.UnidadesDeResistencia, vida - 1);
        }

        [Test]
        public void LanzadorDisparaProyectilHaciaUnBloqueDeAceroVariasVecesHastaQueMuereYElBloqueNoSeDania()
        {
            Personaje personaje = new LosLopezReggae(new Punto(10, 11));
            Juego.Instancia().Ambiente.AgregarPersonaje(personaje);
            int iteracion;
            int vida = personaje.UnidadesDeResistencia;
            personaje.Movimiento.CambiarAIzquierda();
            int vidaBloque = Juego.Instancia().Ambiente.ObtenerCasilla(new Punto(9, 11)).Estado.UnidadesDeResistencia;
            for (iteracion = 0; iteracion < 10; iteracion++)
            {
                personaje.LanzarExplosivo();
                System.Threading.Thread.Sleep(300);
                this.unJuego.AvanzarElTiempo();
            }
            Assert.AreEqual(personaje.UnidadesDeResistencia, 0);
            Assert.AreEqual(vidaBloque, Juego.Instancia().Ambiente.ObtenerCasilla(new Punto(9, 11)).Estado.UnidadesDeResistencia);
        }
    }
}

