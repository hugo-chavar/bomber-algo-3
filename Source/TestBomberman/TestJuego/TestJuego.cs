using NUnit;
using NUnit.Framework;
using Bomberman.Juego;

namespace TestBomberman.TestJuego
{
    [TestFixture]
    public class TestJuego
    {
        private Juego unJuego;

        [TestFixtureSetUp]
        public void TestSetup()
        {
            this.unJuego = Juego.Instancia();
        }

        [Test]
        public void DosReferenciasAJuegoDebenSerElMismoObjeto()
        {
            //verifico que funcione el Singleton
            Juego otroJuego = Juego.Instancia();
            Assert.AreSame(otroJuego, this.unJuego);
        }
    }
}
