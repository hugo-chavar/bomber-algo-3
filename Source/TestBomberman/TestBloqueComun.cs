using NUnit;
using NUnit.Framework;
using Bomberman.Mapa.Casilla;
using Bomberman;
using Bomberman.Personaje;

namespace TestBomberman
{   [TestFixture]
    class TestBloqueComun
    {
        private BloqueComun obstaculoLadrillo;
        private BloqueComun obstaculoCemento;
        private Punto posicion;

        [TestFixtureSetUp]
        public void TestSetup()
        {
            posicion = new Punto(3, 4);
            obstaculoLadrillo = BloqueComun.CrearBloqueLadrillos(posicion);
            obstaculoCemento = BloqueComun.CrearBloqueCemento(posicion);
        }
            
        [Test]
        public void TestDaniarConBombaMolotovBloqueLadrillosModificaUnidades()
        {
            obstaculoLadrillo.DaniarConBombaMolotov();
            Assert.AreEqual(obstaculoLadrillo.UnidadesDeResistencia, 0);
        }

        [Test]
        public void TestDaniarConBombaToleToleBloqueLadrillos()
        {
            obstaculoLadrillo.DaniarConBombaToleTole();
            Assert.AreEqual(true, obstaculoLadrillo.Destruido());
        }

        [Test]
        public void TestDaniarConBombaMolotovBloqueLadrillosLoDestruye()
        {
            obstaculoLadrillo.DaniarConBombaMolotov();
            Assert.AreEqual(obstaculoLadrillo.Destruido(), true);
        }

        [Test]
        public void TestDaniarConBombaMolotovBloqueCementoModificaUnidades()
        {
            BloqueComun otroObstaculo = BloqueComun.CrearBloqueCemento(posicion);
            otroObstaculo.DaniarConBombaMolotov();
            Assert.AreEqual(otroObstaculo.UnidadesDeResistencia, 5);
        }

        [Test]
        public void TestDaniarConBombaToleToleBloqueCemento()
        {
            obstaculoCemento.DaniarConBombaToleTole();
            Assert.AreEqual(true, obstaculoCemento.Destruido());
        }

        [Test]
        public void TestDaniarConBombaMolotovBloqueCementoLoDestruye()
        {
            obstaculoCemento.DaniarConBombaMolotov();
            obstaculoCemento.DaniarConBombaMolotov();
            Assert.AreEqual(obstaculoCemento.Destruido(), true);
        }

        [Test]
        public void noEsTransitablePorUnPersonajeNoAlado()
        {
            Personaje unPersonaje = new Bombita();
            Assert.IsFalse(this.obstaculoCemento.TransitablePor(unPersonaje));
        }

    }
}
