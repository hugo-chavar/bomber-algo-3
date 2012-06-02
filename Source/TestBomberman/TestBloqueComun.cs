using NUnit;
using NUnit.Framework;
using Bomberman.Mapa.Casilla;
using Bomberman;

namespace TestBomberman
{   [TestFixture]
    class TestBloqueComun
    {
        [Test]
        public void TestDaniarConBombaMolotovBloqueLadrillosModificaUnidades()
        {
            Punto posicion = new Punto(3, 4);
            BloqueComun UnObstaculo = BloqueComun.CrearBloqueLadrillos(posicion);
            UnObstaculo.DaniarConBombaMolotov();
            Assert.AreEqual(UnObstaculo.UnidadesDeResistencia, 0);
        }

        [Test]
        public void TestDaniarConBombaToleToleBloqueLadrillos()
        {
            Punto posicion = new Punto(3, 4);
            BloqueComun UnObstaculo = BloqueComun.CrearBloqueLadrillos(posicion);
            UnObstaculo.DaniarConBombaToleTole();
            Assert.AreEqual(true, UnObstaculo.Destruido());
        }

        [Test]
        public void TestDaniarConBombaMolotovBloqueLadrillosLoDestruye()
        {
            Punto posicion = new Punto(3, 4);
            BloqueComun UnObstaculo = BloqueComun.CrearBloqueLadrillos(posicion);
            UnObstaculo.DaniarConBombaMolotov();
            Assert.AreEqual(UnObstaculo.Destruido(),true);
        }

        [Test]
        public void TestDaniarConBombaMolotovBloqueCementoModificaUnidades()
        {
            Punto posicion = new Punto(3, 4);
            BloqueComun UnObstaculo = BloqueComun.CrearBloqueCemento(posicion);
            UnObstaculo.DaniarConBombaMolotov();
            Assert.AreEqual(UnObstaculo.UnidadesDeResistencia, 5);
        }

        [Test]
        public void TestDaniarConBombaToleToleBloqueCemento()
        {
            Punto posicion = new Punto(3, 4);
            BloqueComun UnObstaculo = BloqueComun.CrearBloqueCemento(posicion);
            UnObstaculo.DaniarConBombaToleTole();
            Assert.AreEqual(true, UnObstaculo.Destruido());
        }

        [Test]
        public void TestDaniarConBombaMolotovBloqueCementoLoDestruye()
        {
            Punto posicion = new Punto(3, 4);
            BloqueComun UnObstaculo = BloqueComun.CrearBloqueCemento(posicion);
            UnObstaculo.DaniarConBombaMolotov();
            UnObstaculo.DaniarConBombaMolotov();
            Assert.AreEqual(UnObstaculo.Destruido(), true);
        }


    }
}
