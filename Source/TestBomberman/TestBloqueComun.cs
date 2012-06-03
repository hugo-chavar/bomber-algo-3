using NUnit;
using NUnit.Framework;
using Bomberman.Mapa.Casilla;
using Bomberman;
using Bomberman.Personaje;

namespace TestBomberman
{   [TestFixture]
    class TestBloqueComun
    {
        private BloqueComun obstaculoCemento;
        private Punto posicion;

        [TestFixtureSetUp]
        public void TestSetup()
        {
            posicion = new Punto(3, 4);
        }
            
        [Test]
        public void TestDaniarConBombaMolotovBloqueLadrillosModificaUnidades()
        {
            BloqueComun obstaculoLadrillo = BloqueComun.CrearBloqueLadrillos(posicion);
            obstaculoLadrillo.DaniarConBombaMolotov();
            Assert.AreEqual(obstaculoLadrillo.UnidadesDeResistencia, 0);
        }

        [Test]
        public void TestDaniarConBombaToleToleBloqueLadrillos()
        {
            BloqueComun obstaculoLadrillo = BloqueComun.CrearBloqueLadrillos(posicion);
            obstaculoLadrillo.DaniarConBombaToleTole();
            Assert.IsTrue(obstaculoLadrillo.Destruido());
        }

        [Test]
        public void TestDaniarConBombaMolotovBloqueLadrillosLoDestruye()
        {
            BloqueComun obstaculoLadrillo = BloqueComun.CrearBloqueLadrillos(posicion);
            obstaculoLadrillo.DaniarConBombaMolotov();
            Assert.IsTrue(obstaculoLadrillo.Destruido());
        }

        [Test]
        public void TestDaniarConBombaMolotovBloqueCementoModificaUnidades()
        {
            BloqueComun obstaculoCemento = BloqueComun.CrearBloqueCemento(posicion);
            obstaculoCemento.DaniarConBombaMolotov();
            Assert.AreEqual(obstaculoCemento.UnidadesDeResistencia, 5);
        }

        [Test]
        public void TestDaniarConBombaToleToleBloqueCemento()
        {
            BloqueComun obstaculoCemento = BloqueComun.CrearBloqueCemento(posicion);
            obstaculoCemento.DaniarConBombaToleTole();
            Assert.IsTrue(obstaculoCemento.Destruido());
        }

        [Test]
        public void TestDaniarConBombaMolotovBloqueCementoLoDestruye()
        {
            BloqueComun obstaculoCemento = BloqueComun.CrearBloqueCemento(posicion);
            obstaculoCemento.DaniarConBombaMolotov();
            obstaculoCemento.DaniarConBombaMolotov();
            Assert.IsTrue(obstaculoCemento.Destruido());
        }

        [Test]
        public void obstaculoDeCementoNoEsTransitablePorUnPersonajeNoAlado()
        {
            BloqueComun obstaculoCemento = BloqueComun.CrearBloqueCemento(posicion);
            Personaje unPersonaje = new Bombita();
            Assert.IsFalse(obstaculoCemento.TransitablePor(unPersonaje));
        }

        [Test]
        public void obstaculoDeLadrilloNoEsTransitablePorUnPersonajeNoAlado()
        {
            BloqueComun obstaculoLadrillo = BloqueComun.CrearBloqueLadrillos(posicion);
            Personaje unPersonaje = new Bombita();
            Assert.IsFalse(obstaculoLadrillo.TransitablePor(unPersonaje));
        }
    }
}
