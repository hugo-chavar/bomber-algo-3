using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Juego;
using Bomberman.Mapa;
using NUnit.Framework;
using Bomberman.Personaje;
using Bomberman;
using Bomberman.Mapa.Casilla;

namespace TestBomberman.TestIntegracion
{
    class TestIntegracionProyectilPersonaje
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
        public void TiroUnProyectilATresCasillasDeDistanciaYLosObjetosGolpeadosBajanSuResistenciaEnUnaUnidad()
        {
            Personaje unTipoQueTiraProyectiles = new LosLopezReggae(new Punto(13,6));

            unTipoQueTiraProyectiles.Movimiento.CambiarADerecha();
            unTipoQueTiraProyectiles.LanzarExplosivo();
            int resistenciaLadrillo1 = Juego.Instancia().Ambiente.ObtenerCasilla(new Punto(16, 5)).Estado.UnidadesDeResistencia;
            int resistenciaLadrillo2 = Juego.Instancia().Ambiente.ObtenerCasilla(new Punto(16, 7)).Estado.UnidadesDeResistencia;

            Juego.Instancia().AvanzarElTiempo();
            Juego.Instancia().AvanzarElTiempo();
            Juego.Instancia().AvanzarElTiempo();

            Assert.AreEqual(resistenciaLadrillo1-1,Juego.Instancia().Ambiente.ObtenerCasilla(new Punto(16, 5)).Estado.UnidadesDeResistencia);
            Assert.AreEqual(resistenciaLadrillo2-1,Juego.Instancia().Ambiente.ObtenerCasilla(new Punto(16, 7)).Estado.UnidadesDeResistencia);
        }

        [Test]
        public void TiroUnProyectilADosCasillasDeDistanciaYLosObjetosGolpeadosBajanSuResistenciaEnUnaUnidad()
        {
            Personaje unTipoQueTiraProyectiles = new LosLopezReggae(new Punto(14, 6));

            unTipoQueTiraProyectiles.Movimiento.CambiarADerecha();
            unTipoQueTiraProyectiles.LanzarExplosivo();
            int resistenciaLadrillo1 = Juego.Instancia().Ambiente.ObtenerCasilla(new Punto(16, 5)).Estado.UnidadesDeResistencia;
            int resistenciaLadrillo2 = Juego.Instancia().Ambiente.ObtenerCasilla(new Punto(16, 7)).Estado.UnidadesDeResistencia;

            Juego.Instancia().AvanzarElTiempo();
            Juego.Instancia().AvanzarElTiempo();
            Juego.Instancia().AvanzarElTiempo();
            
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

            Juego.Instancia().AvanzarElTiempo();
            Juego.Instancia().AvanzarElTiempo();
            
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

            Juego.Instancia().AvanzarElTiempo();
            Juego.Instancia().AvanzarElTiempo();
            Juego.Instancia().AvanzarElTiempo();

            Assert.IsInstanceOf(typeof(Pasillo), Juego.Instancia().Ambiente.ObtenerCasilla(new Punto(14, 12)).Estado);
        }
    }
}
