using NUnit.Framework;
using Bomberman.Arma;
using Bomberman;
using Bomberman.Excepciones;
using System.Collections.Generic;

namespace TestBomberman.TestArma
{
    [TestFixture]
    class TestProyectil
    {
        [Test]
        public void ProyectilAvanzaUnaPosicionAlPasar1Tiempo()
        {
            Proyectil unProyectil = new Proyectil(new Punto(0, 3));//ElProyectil se crea con la posicion destino
            Queue<Punto> recorridoProyectil = new Queue<Punto>();
            recorridoProyectil.Enqueue(new Punto(0, 1));
            unProyectil.Trayectoria = recorridoProyectil;
            unProyectil.Posicion = new Punto(0, 0);
            unProyectil.CuandoPasaElTiempo();
            Assert.AreEqual(unProyectil.Posicion, new Punto(0, 1));
        }

        [Test]
        public void ProyectilAvanzaDosPosicionesAlPasar2Tiempos()
        {
            Proyectil unProyectil = new Proyectil(new Punto(0, 3));//ElProyectil se crea con la posicion destino
            Queue<Punto> recorridoProyectil = new Queue<Punto>();
            recorridoProyectil.Enqueue(new Punto(0, 1));
            recorridoProyectil.Enqueue(new Punto(0, 2));
            unProyectil.Trayectoria = recorridoProyectil;
            unProyectil.Posicion = new Punto(0, 0);
            unProyectil.CuandoPasaElTiempo();
            unProyectil.CuandoPasaElTiempo();
            Assert.AreEqual(unProyectil.Posicion, new Punto(0, 2));
        }

        [Test]
        public void TestProyectilDebeAvanzarDosPosicionAlPasar2Tiempos()
        {
            Proyectil unProyectil = new Proyectil(new Punto(0, 3));//ElProyectil se crea con la posicion destino
            Queue<Punto> recorridoProyectil = new Queue<Punto>();

            unProyectil.Trayectoria = recorridoProyectil;
            unProyectil.Posicion = new Punto(0, 0);
            
            try
            {
                unProyectil.CuandoPasaElTiempo();
                Assert.Fail();
            }
            catch (AvanzarProyectilNoValidoException)
            {
                Assert.Pass("Exception correcta fue lanzada.");
            }
            catch (System.Exception)
            {
                Assert.Fail();
            }
        }

        //[Test] Este test va a la integracion con el mapa.. ya que necesita la casilla donde explotar
        //public void TestProyectilDebeAvanzarTresPosicionesYExplotarAlPasar3Tiempos()
        //{
        //    unProyectil.CuandoPasaElTiempo();
        //    unProyectil.CuandoPasaElTiempo();
        //    unProyectil.CuandoPasaElTiempo();
            
        //    Assert.IsTrue(unProyectil.EstaExplotado());//No esta explotando,deberia hacerlo.Al dejar pasar un tiempo mas si lo hace pero Salta excepcion de casillas no creadas
        //}

    }
}
