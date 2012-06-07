using Bomberman.Arma;
using Bomberman.Mapa;
using NUnit.Framework;
using Bomberman.Mapa.Casilla;
using Bomberman;

namespace TestBomberman.TestArma
{
    [TestFixture]
    class TestManejadorProyectil
    {
        Proyectil unProyectil;
        ManejadorProyectil unManejador;
        Punto unPuntoInicial;
        Punto unPuntoFinal;
        Proyectil otroProyectil;

        [SetUp]
        public void TestSetup()
        {
            unPuntoInicial = new Punto(1,1);
            unProyectil = new Proyectil(unPuntoInicial);
            otroProyectil = new Proyectil(unPuntoInicial);
            unManejador = new ManejadorProyectil(unProyectil, 1);
            unPuntoFinal = new Punto(1, 4);
        }

        [Test]
        public void TestParaConfirmarQueElMisilSeLanza()
        {
            unManejador.LanzarMisil();
            Assert.AreEqual(unManejador.EstaLanzado(), true);
        }

        [Test]
        public void TestParaConfirmarQueElMisilNoAvanzaCuandoNoPasaElTiempo()
        {
            unPuntoInicial = new Punto(1, 1);
            unProyectil = new Proyectil(unPuntoInicial);
            unProyectil.LanzarMisil(2);

            Assert.AreEqual(unProyectil.Posicion.X, unPuntoInicial.X);
            Assert.AreEqual(unProyectil.Posicion.Y, unPuntoInicial.Y);
        }

        [Test]
        public void TestParaConfirmarQueElMisilAvanzaCuandoPasaElTiempo()
        {
            unProyectil.LanzarMisil(8);
            unProyectil.CuandoPasaElTiempo();
            unProyectil.CuandoPasaElTiempo();
            Punto unPuntoDestino = new Punto(1, 3);
            
            Assert.AreEqual(unProyectil.Posicion.Y, unPuntoDestino.Y);
            Assert.AreEqual(unProyectil.Posicion.X, unPuntoDestino.X);
        }

        [Test]
        public void TestParaConfirmarQueLaBombaNoExplotaCuandoNoPasaElTiempoSuficiente()
        {
            otroProyectil.LanzarMisil(2);
            otroProyectil.CuandoPasaElTiempo();

            Assert.AreEqual(otroProyectil.TiempoRestante(), 2);
            Assert.AreEqual(otroProyectil.EstaExplotado(), false);
        }

        [Test]
        public void TestParaConfirmarQueLaBombaExplotaCuandoPasaElTiempoSuficiente()
        {
            Punto unPuntoMas = new Punto(1, 2);
            unProyectil = new Proyectil(unPuntoMas);
            unProyectil.LanzarMisil(8);
            unProyectil.CuandoPasaElTiempo();
            unProyectil.CuandoPasaElTiempo();
            unProyectil.CuandoPasaElTiempo();

            /* VERIFICAR ACA! 
             * CUANDO CORRO LAS TEST LA PRIMER VEZ FUNCIONA TODO BIEN, 
             * LA CORRO DOS VECES Y NO CORRE!!!
             */

            Assert.AreEqual(unProyectil.TiempoRestante(), 0);
            Assert.AreEqual(unProyectil.Posicion.Y, 5);
            Assert.AreEqual(unProyectil.EstaExplotado(), true);
        }

        [Test]
        public void TestParaProbarNuevaPosicionAlBajarElMisilALaDerecha()
        {
            unPuntoInicial = new Punto(1, 1);
            unProyectil = new Proyectil(unPuntoInicial);
            unProyectil.LanzarMisil(6);
            unProyectil.CuandoPasaElTiempo();
            unProyectil.CuandoPasaElTiempo();
            Punto unPuntoDestino = new Punto(3, 1);
            
            Assert.AreEqual(unProyectil.Posicion.Y, unPuntoDestino.Y);
            Assert.AreEqual(unProyectil.Posicion.X, unPuntoDestino.X);
        }

        [Test]
        public void TestParaProbarNuevaPosicionAlBajarElMisilALaIzquierda()
        {
            unPuntoInicial = new Punto(2, 4);
            unProyectil = new Proyectil(unPuntoInicial);
            unProyectil.LanzarMisil(4);
            unProyectil.CuandoPasaElTiempo();
            unProyectil.CuandoPasaElTiempo();
            Punto unPuntoDestino = new Punto(2, 4);
            
            Assert.AreEqual(unProyectil.Posicion.Y, unPuntoDestino.Y);
            Assert.AreEqual(unProyectil.Posicion.X, unPuntoDestino.X);
        }

        [Test]
        
        public void TestParaProbarNuevaPosicionAlBajarElMisilAAbajo()
        {
            unPuntoInicial = new Punto(4, 4);
            unProyectil = new Proyectil(unPuntoInicial);
            unProyectil.LanzarMisil(2);
            unProyectil.CuandoPasaElTiempo();
            unProyectil.CuandoPasaElTiempo();
            Punto unPuntoDestino = new Punto(4, 2);
            
            Assert.AreEqual(unProyectil.Posicion.Y, unPuntoDestino.Y);
            Assert.AreEqual(unProyectil.Posicion.X, unPuntoDestino.X);
        }

        [Test]
        public void TestParaProbarQueElMisilNoSeActivaNiSeMueveSiElPuntoNoEsValido()
        {
            unPuntoInicial = new Punto(1, 2);
            unProyectil = new Proyectil(unPuntoInicial);
            unProyectil.LanzarMisil(2);
            unProyectil.CuandoPasaElTiempo();

            Assert.AreEqual(unProyectil.Posicion.X, unPuntoInicial.X);
            Assert.AreEqual(unProyectil.Posicion.Y, unPuntoInicial.Y);
        }
    }
}
