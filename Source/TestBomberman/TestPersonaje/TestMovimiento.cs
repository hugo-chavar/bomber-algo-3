using NUnit;
using NUnit.Framework;
using BombermanModel.Mapa;
using BombermanModel.Personaje;
using System.Collections.Generic;

namespace TestBombermanModel.TestPersonaje
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
            List<int> direcciones = new List<int> { Tablero.ABAJO, Tablero.ARRIBA, Tablero.IZQUIERDA, Tablero.DERECHA };
            Assert.Contains(unMovimiento.Direccion,direcciones);
        }

        [Test]
        public void MultiplicarVelocidadPorHaceProductoDeLaVelocidadDelMovimientoPorElNumeroPasadoPorParametro()
        {
            this.unMovimiento.MultiplicarVelocidadPor(3);
            //la velocidad inicial es 1
            Assert.AreEqual(unMovimiento.Velocidad, 3f);
        }

        [Test]
        public void CambiarAIzquierdaModificaLaDireccionALaIzquierdaDelMapa()
        {
            this.unMovimiento.CambiarADerecha();
            this.unMovimiento.CambiarAIzquierda();
            Assert.AreEqual(unMovimiento.Direccion, Tablero.IZQUIERDA);
        }

        [Test]
        public void CambiarAIzquierdaModificaLaDireccionALaDerechaDelMapa()
        {
            this.unMovimiento.CambiarAIzquierda();
            this.unMovimiento.CambiarADerecha();
            Assert.AreEqual(unMovimiento.Direccion, Tablero.DERECHA);
        }

        [Test]
        public void CambiarAIzquierdaModificaLaDireccionHaciaArribaDelMapa()
        {
            this.unMovimiento.CambiarADerecha();
            this.unMovimiento.CambiarAArriba();
            Assert.AreEqual(unMovimiento.Direccion, Tablero.ARRIBA);
        }

        [Test]
        public void CambiarAIzquierdaModificaLaDireccionHaciaAbajoDelMapa()
        {
            this.unMovimiento.CambiarADerecha();
            this.unMovimiento.CambiarAAbajo();
            Assert.AreEqual(unMovimiento.Direccion, Tablero.ABAJO);
        }
    }
}
