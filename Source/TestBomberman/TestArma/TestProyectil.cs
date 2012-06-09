using NUnit.Framework;
using Bomberman.Arma;
using Bomberman;
using System.Collections.Generic;

namespace TestBomberman.TestArma
{
    [TestFixture]
    class TestProyectil
    {
        private Proyectil unProyectil;

        [SetUp]
        public void TestSetup()
        {
            Queue<Punto> recorridoProyectil = new Queue<Punto>();
            recorridoProyectil.Enqueue(new Punto(0, 1));
            recorridoProyectil.Enqueue(new Punto(0, 2));
            recorridoProyectil.Enqueue(new Punto(0, 3));
            this.unProyectil = new Proyectil(new Punto(0, 0)); //ElProyectil deberia Guardar en la posicion actual la inicial
            unProyectil.Trayectoria = recorridoProyectil;
        
        }

        [Test]
        public void TestProyectilDebeAvanzarUnaPosicionAlPasar1Tiempo()
        {

            unProyectil.CuandoPasaElTiempo();
            Assert.AreEqual(unProyectil.PosicionActual, new Punto(0, 1));
        }

        [Test]
        public void TestProyectilDebeAvanzarDosPosicionAlPasar2Tiempos()
        {
            this.unProyectil.CuandoPasaElTiempo();
            this.unProyectil.CuandoPasaElTiempo();
            Assert.AreEqual(this.unProyectil.PosicionActual, new Punto(0, 2));
        }

        [Test]
        public void TestProyectilDebeAvanzarTresPosicionesYExplotarAlPasar3Tiempos()
        {
            unProyectil.CuandoPasaElTiempo();
            unProyectil.CuandoPasaElTiempo();
            unProyectil.CuandoPasaElTiempo();
            Assert.IsTrue(unProyectil.EstaExplotado());//No esta explotando,deberia hacerlo.Al dejar pasar un tiempo mas si lo hace pero Salta excepcion de casillas no creadas
        }

    }
}
