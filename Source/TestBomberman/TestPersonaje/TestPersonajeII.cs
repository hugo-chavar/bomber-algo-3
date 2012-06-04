using NUnit;
using NUnit.Framework;
using Bomberman.Mapa.Casilla;
using Bomberman;
using Bomberman.Personaje;
using System.Collections.Generic;
using Bomberman.Mapa;
namespace TestBomberman.TestPersonaje
{
    [TestFixture]
    class TestPersonajeII
    {
        private Punto posicionOrigen;
        private Casilla origen;
        private Punto posicionDestino;
        private Casilla destino;
        private Personaje unPersonaje;
        private Mapa unMapa;
        private ManejadorDeMovimiento manejador;

        [TestFixtureSetUp]
        public void TestSetup()
        {
      
            this.posicionOrigen = new Punto(1, 1);
            this.origen = FabricaDeCasillas.FabricarPasillo(posicionOrigen);
                        this.posicionDestino = new Punto(2, 1);
            this.destino = FabricaDeCasillas.FabricarPasillo(posicionDestino);
            this.unPersonaje = new Bombita(posicionOrigen);
            this.unMapa = new Mapa(3, 3);
            this.unMapa.agregarCasilla(origen);
            this.unMapa.agregarCasilla(destino);
            this.manejador = new ManejadorDeMovimiento(unPersonaje, unMapa);
                        
        }

        [Test]
        public void CuandoManejadorMueveAPersonajeUnoYLoConsigue()
        {
            manejador.MoverPersonaje(2);
            Assert.AreEqual(unPersonaje.Posicion.X, posicionDestino.X);
        }
    }

}