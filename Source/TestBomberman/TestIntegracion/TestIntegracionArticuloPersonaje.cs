using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BombermanModel.Juego;
using BombermanModel.Mapa;
using NUnit.Framework;
using BombermanModel.Personaje;
using BombermanModel.Articulo;
using BombermanModel;
using BombermanModel.Arma;

namespace TestBombermanModel.TestIntegracion
{
    class TestIntegracionArticuloPersonaje
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
        public void CuandoBombitaComeDosChalasLaVelocidadEsElCuadruple()
        {
            Personaje unBombita = new Bombita(new Punto (0,0));
            Articulo unaChala = new Chala();
            Articulo otraChala = new Chala();

            Juego.Instancia().Ambiente.ObtenerCasilla(new Punto(0, 1)).ArticuloContenido = unaChala; // hardcodeo la chala en el pasillo, arriba de bombita.
            Juego.Instancia().Ambiente.ObtenerCasilla(new Punto(1, 0)).ArticuloContenido = otraChala; // hardcodeo la chala en el pasillo, a la derecha de bombita.

            unBombita.Movimiento.CambiarAArriba();
            unBombita.Mover(); // 0,1 como la primer chala.
            unBombita.Movimiento.CambiarAAbajo();
            unBombita.Mover(); // 0,0
            unBombita.Movimiento.CambiarADerecha();
            unBombita.Mover(); // 0,1 como la segunda chala.

            Assert.AreEqual(4, unBombita.Movimiento.Velocidad);
        }

        [Test]
        public void CuandoBombitaComeDosArticulosBombaToleToleElLanzadorEsLanzadorBombaToleTole()
        {
            Personaje unBombita = new Bombita(new Punto(0, 0));
            Articulo unArtBombaToleTole = new ArticuloBombaToleTole();
            Articulo otroArtBombaToleTole = new ArticuloBombaToleTole();

            Juego.Instancia().Ambiente.ObtenerCasilla(new Punto(0, 1)).ArticuloContenido = unArtBombaToleTole; // hardcodeo el art bomba tole tole en el pasillo, arriba de bombita.
            Juego.Instancia().Ambiente.ObtenerCasilla(new Punto(1, 0)).ArticuloContenido = otroArtBombaToleTole; // hardcodeo el art bomba tole tole en el pasillo, a la derecha de bombita.

            unBombita.Movimiento.CambiarAArriba();
            unBombita.Mover(); // 0,1 como el primer art.
            unBombita.Movimiento.CambiarAAbajo();
            unBombita.Mover(); // 0,0
            unBombita.Movimiento.CambiarADerecha();
            unBombita.Mover(); // 0,1 como el segundo art.

            Assert.IsInstanceOf(typeof(LanzadorToleTole), unBombita.Lanzador);
        }

        [Test]
        public void CuandoBombitaComeDosTimersElRetardoEsAhora30()
        {
            Personaje unBombita = new Bombita(new Punto(0, 0));
            Articulo unTimer = new Timer();
            Articulo otroTimer = new Timer();

            Juego.Instancia().Ambiente.ObtenerCasilla(new Punto(0, 1)).ArticuloContenido = unTimer; // hardcodeo el timer en el pasillo, arriba de bombita.
            Juego.Instancia().Ambiente.ObtenerCasilla(new Punto(1, 0)).ArticuloContenido = otroTimer; // hardcodeo el timer en el pasillo, a la derecha de bombita.

            unBombita.Movimiento.CambiarAArriba();
            unBombita.Mover(); // 0,1 como el primer art.
            unBombita.Movimiento.CambiarAAbajo();
            unBombita.Mover(); // 0,0
            unBombita.Movimiento.CambiarADerecha();
            unBombita.Mover(); // 0,1 como el segundo art.

            Assert.AreEqual(30, unBombita.Lanzador.RetardoExplosion);
        }
    }
}
