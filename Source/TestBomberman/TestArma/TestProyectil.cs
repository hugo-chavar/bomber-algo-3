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

        [TestFixtureSetUp]
        public void testSetUp()
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
            unProyectil.LanzarMisil(1);

            Assert.AreEqual(unProyectil.Posicion.X, unPuntoInicial.X);
            Assert.AreEqual(unProyectil.Posicion.Y, unPuntoInicial.Y);
        }

        [Test]
        public void TestParaConfirmarQueElMisilAvanzaCuandoPasaElTiempo()
        {
            unProyectil.LanzarMisil(1);
            unProyectil.CuandoPasaElTiempo();
            unProyectil.CuandoPasaElTiempo();
            Punto unPuntoDestino = new Punto(1, 3);


            Assert.AreEqual(unProyectil.Posicion.Y, unPuntoDestino.Y);
            Assert.AreEqual(unProyectil.Posicion.X, unPuntoDestino.X);

        }

        [Test]
        public void TestParaConfirmarQueLaBombaNoExplotaCuandoNoPasaElTiempoSuficiente()
        {
            otroProyectil.LanzarMisil(1);
            otroProyectil.CuandoPasaElTiempo();

            Assert.AreEqual(otroProyectil.TiempoRestante(), 2);

            Assert.AreEqual(otroProyectil.EstaExplotado(), false);
        }

        [Test]
        public void TestParaConfirmarQueLaBombaExplotaCuandoPasaElTiempoSuficiente()
        {
            unProyectil.LanzarMisil(1);
            unProyectil.CuandoPasaElTiempo();
            unProyectil.CuandoPasaElTiempo();
            unProyectil.CuandoPasaElTiempo();
 

            Assert.AreEqual(unProyectil.TiempoRestante(), 0);
            Assert.AreEqual(unProyectil.Posicion.Y, 4);

        }



    }

}
