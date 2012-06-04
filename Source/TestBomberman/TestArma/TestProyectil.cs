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

        [TestFixtureSetUp]
        public void testSetUp()
        {
            Punto unPuntoInicial = new Punto(1,1);
            unProyectil = new Proyectil(unPuntoInicial);
            unManejador = new ManejadorProyectil(unProyectil, 1);


        }

        [Test]
        public void TestParaConfirmarExplosionDeProyectil()
        {
            unManejador.LanzarMisil();
            Assert.AreEqual(true, unProyectil.EstaExplotado());

        }


    }

}
