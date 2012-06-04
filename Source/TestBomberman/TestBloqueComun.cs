using NUnit;
using NUnit.Framework;
using Bomberman.Mapa.Casilla;
using Bomberman;
using Bomberman.Personaje;

namespace TestBomberman
{   
    [TestFixture]
    class TestBloqueComun
    {
        [Test]
        public void TestDaniarConBombaMolotovBloqueLadrillosModificaUnidades()
        {
            BloqueComun obstaculoLadrillo = BloqueComun.CrearBloqueLadrillos();
            obstaculoLadrillo.DaniarConBombaMolotov(5);
            Assert.AreEqual(obstaculoLadrillo.UnidadesDeResistencia, 0);
        }

        [Test]
        public void TestDaniarConBombaToleToleBloqueLadrillos()
        {
            BloqueComun obstaculoLadrillo = BloqueComun.CrearBloqueLadrillos();
            obstaculoLadrillo.DaniarConBombaToleTole();
            Assert.IsTrue(obstaculoLadrillo.Destruido());
        }

        [Test]
        public void TestDaniarConBombaMolotovBloqueLadrillosLoDestruye()
        {
            BloqueComun obstaculoLadrillo = BloqueComun.CrearBloqueLadrillos();
            obstaculoLadrillo.DaniarConBombaMolotov(5);
            Assert.IsTrue(obstaculoLadrillo.Destruido());
        }

        [Test]
        public void TestDaniarConBombaMolotovBloqueCementoModificaUnidades()
        {
            BloqueComun obstaculoCemento = BloqueComun.CrearBloqueCemento();
            obstaculoCemento.DaniarConBombaMolotov(5);
            Assert.AreEqual(obstaculoCemento.UnidadesDeResistencia, 5);
        }

        [Test]
        public void TestDaniarConBombaToleToleBloqueCemento()
        {
            BloqueComun obstaculoCemento = BloqueComun.CrearBloqueCemento();
            obstaculoCemento.DaniarConBombaToleTole();
            Assert.IsTrue(obstaculoCemento.Destruido());
        }

        [Test]
        public void TestDaniarConBombaMolotovBloqueCementoLoDestruye()
        {
            BloqueComun obstaculoCemento = BloqueComun.CrearBloqueCemento();
            obstaculoCemento.DaniarConBombaMolotov(5);
            obstaculoCemento.DaniarConBombaMolotov(5);
            Assert.IsTrue(obstaculoCemento.Destruido());
        }

        [Test]
        public void obstaculoDeCementoNoEsTransitablePorUnPersonajeNoAlado()
        {
            BloqueComun obstaculoCemento = BloqueComun.CrearBloqueCemento();
            Punto unPunto = new Punto(0, 0);
            Personaje unPersonaje = new Bombita(unPunto);
            Assert.IsFalse(obstaculoCemento.TransitablePor(unPersonaje));
        }

        [Test]
        public void obstaculoDeLadrilloNoEsTransitablePorUnPersonajeNoAlado()
        {
            BloqueComun obstaculoLadrillo = BloqueComun.CrearBloqueLadrillos();
            Punto unPunto = new Punto(0, 0);
            Personaje unPersonaje = new Bombita(unPunto);
            Assert.IsFalse(obstaculoLadrillo.TransitablePor(unPersonaje));
        }
    }
}
