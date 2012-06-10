using NUnit;
using NUnit.Framework;
using Bomberman.Mapa.Casilla;
using Bomberman;
using Bomberman.Personaje;

namespace TestBomberman
{
    [TestFixture]
    public class TestBloqueAcero
    {
        private BloqueAcero unObstaculo;

        [SetUp]
        public void TestSetup()
        {
            unObstaculo = new BloqueAcero();
        }
        
        [Test]
        public void DaniarConBombaMolotovBloqueDeAceroNoModificaSuEstado()
            {
                unObstaculo.DaniarConBombaMolotov(5);
                Assert.AreEqual(unObstaculo.UnidadesDeResistencia,10); 
            }

        [Test]
        public void DaniarConBombaToleToleBloqueDeAcero()
        {

            unObstaculo.DaniarConBombaToleTole();
            Assert.IsTrue(unObstaculo.Destruido());
        }

        [Test]
        public void noEsTransitablePorUnPersonajeNoAlado()
        {
            Punto unPunto = new Punto(0, 0);
            Personaje unPersonaje = new Bombita(unPunto);
            Assert.IsFalse(this.unObstaculo.TransitablePor(unPersonaje));
        }
    }
}
