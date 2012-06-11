using NUnit;
using NUnit.Framework;
using Bomberman.Mapa;
using Bomberman.Personaje;
using System.Collections.Generic;

namespace TestBomberman.TestPersonaje
{
    [TestFixture]
    class TestMovimiento
    {
        private Movimiento unMovimiento;
        
        [SetUp]
        public void TestSetup()
        {
            this.unMovimiento = new Movimiento();
        }

        [Test]
        public void DireccionInicialDelMovimientoEsUnaDeLasDireccionesPermitidas()
        {
            List<int> direcciones = new List<int> { Mapa.ABAJO, Mapa.ARRIBA, Mapa.IZQUIERDA, Mapa.DERECHA };
            Assert.Contains(unMovimiento.Direccion,direcciones);
        }

        [Test]
        public void MultiplicarVelocidadPorHaceProductoDeLaVelocidadDelMovimientoPorElNumeroPasadoPorParametro()
        {
            this.unMovimiento.MultiplicarVelocidadPor(7);
            //la velocidad inicial es 1
            Assert.AreEqual(unMovimiento.Velocidad, 7);
        }

        [Test]
        public void CambiarAIzquierdaModificaLaDireccionALaIzquierdaDelMapa()
        {
            this.unMovimiento.CambiarADerecha();
            this.unMovimiento.CambiarAIzquierda();
            Assert.AreEqual(unMovimiento.Direccion, Mapa.IZQUIERDA);
        }

        [Test]
        public void CambiarAIzquierdaModificaLaDireccionALaDerechaDelMapa()
        {
            this.unMovimiento.CambiarAIzquierda();
            this.unMovimiento.CambiarADerecha();
            Assert.AreEqual(unMovimiento.Direccion, Mapa.DERECHA);
        }

        [Test]
        public void CambiarAIzquierdaModificaLaDireccionHaciaArribaDelMapa()
        {
            this.unMovimiento.CambiarADerecha();
            this.unMovimiento.CambiarAArriba();
            Assert.AreEqual(unMovimiento.Direccion, Mapa.ARRIBA);
        }

        [Test]
        public void CambiarAIzquierdaModificaLaDireccionHaciaAbajoDelMapa()
        {
            this.unMovimiento.CambiarADerecha();
            this.unMovimiento.CambiarAAbajo();
            Assert.AreEqual(unMovimiento.Direccion, Mapa.ABAJO);
        }
    }
}
