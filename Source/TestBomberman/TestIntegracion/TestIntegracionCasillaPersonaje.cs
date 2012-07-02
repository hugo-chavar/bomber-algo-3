using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BombermanModel.Juego;
using BombermanModel.Mapa;
using BombermanModel.Mapa.Casilla;
using NUnit.Framework;
using BombermanModel.Personaje;
using BombermanModel.Articulo;
using BombermanModel;
using BombermanModel.Arma;

namespace TestBomberman.TestIntegracion
{
    class TestIntegracionCasillaPersonaje
    {
        private Juego unJuego;
        private Tablero unMapa;

        [SetUp]
        public void TestSetup()
        {
            this.unJuego = Juego.Instancia();
            this.unJuego.CargarMapa();
            this.unMapa = this.unJuego.Ambiente;

        }

        [TearDown]
        public void TearDown()
        {
            Juego.Reiniciar();
        }

        [Test]
        public void TransitarCasillaHaceQueUnPersonajeDeLaMismaClaseTransiteEsaCasilla()
        {
            Personaje unBombita = new Bombita(new Punto(0, 0));

            Casilla unaCas = Juego.Instancia().Ambiente.ObtenerCasilla(new Punto(0, 0));
            unaCas.Transitar(unBombita);

            Assert.IsTrue(unaCas.TransitaEnCasillaUn(new Bombita(new Punto(0, 0))));
        }

        [Test]
        public void TransitarCasillaNoHaceQueUnPersonajeDeOtraClaseTransiteEsaCasilla()
        {
            Cecilio unCeci = new Cecilio(new Punto(7, 2));

            Casilla unaCas = Juego.Instancia().Ambiente.ObtenerCasilla(new Punto(7, 2));
            unaCas.Transitar(unCeci);
            Assert.IsFalse(unaCas.TransitaEnCasillaUn(new Bombita(new Punto(7, 2))));
        }
    }
}
