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
        }

        [Test]
        public void MoverPersonajeloSacaDeLaCasillaOrigen()
        {
            origen.Dejar(unPersonaje);
            destino.Transitar(unPersonaje);
            List<IMovible> unaListaVacia = new List<IMovible>();
            
            Assert.AreEqual(unaListaVacia, origen.TransitandoEnCasilla);
        }

        [Test]
        public void MoverPersonajeMeLoAgregaEnLaListaDeMovibles()
        {
            destino.Transitar(unPersonaje);
            List<IMovible> unaLista = new List<IMovible>();
            unaLista.Add(unPersonaje);

            Assert.AreEqual(unaLista, destino.TransitandoEnCasilla);
        }

        [Test]
        public void MePermiteMoverPersonaje()
        {
            //Martin: PONGO ESTO PARA QUE EL QUE HIZO ESTA PRUEBA NO SE OLVIDE QUE NO HIZO NADA ACA ADENTRO!
            Assert.IsTrue(false);
        }

        
    }
}
