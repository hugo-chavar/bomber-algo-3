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

        [Test]
        public void PausarJuegoFuncionaOK()
        {
            unJuego.PausarJuego();
            Assert.AreEqual(unJuego.JuegoPausado, true);
        }

        [Test]
        public void DesPausarJuegoFuncionaOK()
        {
            unJuego.PausarJuego();
            unJuego.DesPausarJuego();
            Assert.AreEqual(unJuego.JuegoPausado, false);
        }

        [Test]
        public void PerderVidaFuncionaOK()
        {
            unJuego.PerderVida();
            Assert.AreEqual(2, unJuego.CantDeVidas);
        }
    }
}
