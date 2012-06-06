using NUnit;
using NUnit.Framework;
using Bomberman.Mapa.Casilla;
using Bomberman;
using Bomberman.Personaje;
using System.Collections.Generic;

namespace TestBomberman.TestPersonaje
{
    [TestFixture]
    class TestPersonaje
    {
        private FabricaDeCasillas unaFabricaDeCasillas;
        private Punto posicionOrigen;
        private Casilla origen;
        private Punto posicionDestino;
        private Casilla destino;
        private Personaje unPersonaje;

        [SetUp]
        public void TestSetup()
        {
            this.unaFabricaDeCasillas = new FabricaDeCasillas();
            this.posicionOrigen = new Punto(2, 3);
            this.origen = FabricaDeCasillas.FabricarPasillo(posicionOrigen);
            this.posicionDestino = new Punto(3, 3);
            this.destino = FabricaDeCasillas.FabricarPasillo(posicionDestino);
            this.unPersonaje = new Bombita(posicionOrigen);
        }

        [Test]
        public void MoverPersonajeloDejaEnLaCasillaDestinoSiAmbasSonTransitables()
        {
            destino.Transitar(unPersonaje);

            Assert.AreEqual(posicionDestino, unPersonaje.Posicion);
            destino.Dejar(unPersonaje); // Lo agrego porque sino no me sacaba los personajes de la lista de la casilla Destino (CHEQUEAR SI EL SETUP ESTA BIEN!!!)
        }

        [Test]
        public void MoverPersonajeloSacaDeLaCasillaOrigen()
        {
            origen.Dejar(unPersonaje);
            destino.Transitar(unPersonaje);
            List<IMovible> unaListaVacia = new List<IMovible>();
            
            Assert.AreEqual(unaListaVacia, origen.TransitandoEnCasilla);
            destino.Dejar(unPersonaje); // Lo agrego porque sino no me sacaba los personajes de la lista de la casilla Destino (CHEQUEAR SI EL SETUP ESTA BIEN!!!)
        }

        [Test]
        public void MoverPersonajeMeLoAgregaEnLaListaDeMovibles()
        {
            destino.Transitar(unPersonaje);
            List<IMovible> unaLista = new List<IMovible>();
            unaLista.Add(unPersonaje);

            Assert.AreEqual(unaLista[0], destino.TransitandoEnCasilla[0]);
            Assert.AreEqual((unaLista).Count, (destino.TransitandoEnCasilla).Count); //Hugo dice: Si hay dos Asserts Hacer 2 tests diferentes uno para cada Assert
            destino.Dejar(unPersonaje); // Lo agrego porque sino no me sacaba los personajes de la lista de la casilla Destino (CHEQUEAR SI EL SETUP ESTA BIEN!!!)
        }

        [Test]
        public void MePermiteMoverPersonaje()
        {

        }

        
    }
}
