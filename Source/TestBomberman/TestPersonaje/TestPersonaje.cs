using NUnit;
using NUnit.Framework;
using Bomberman.Mapa.Casilla;
using Bomberman;
using Bomberman.Personaje;

namespace TestBomberman.TestPersonaje
{
    [TestFixture]
    class TestPersonaje
    {
        [Test]
        public void moverPersonajeloDejaEnLaCasillaDestinoSiAmbasSonTransitables()
        {
            FabricaDeCasillas unaFabricaDeCasillas = new FabricaDeCasillas();
            Punto posicionOrigen = new Punto(2, 3);
            Casilla origen = unaFabricaDeCasillas.FabricarPasillo(posicionOrigen);
            Punto posicionDestino = new Punto(3, 3);
            Casilla destino = unaFabricaDeCasillas.FabricarPasillo(posicionDestino);
            Personaje unPersonaje = new Bombita(posicionOrigen);
            
            destino.Transitar(unPersonaje);
            
            Assert.AreEqual(posicionDestino.X, unPersonaje.Posicion.X);
            Assert.AreEqual(posicionDestino.Y, unPersonaje.Posicion.Y);
        }

    }
}
