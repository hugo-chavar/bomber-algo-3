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
            BloqueComun UnObstaculo = BloqueComun.crearBloqueLadrillos(posicion);
            UnObstaculo.daniarConBombaMolotov();
            Assert.AreEqual(UnObstaculo.UnidadesDeResistencia, 0);
        }

        [Test]
        public void TestDaniarConBombaToleToleBloqueLadrillos()
        {
            Punto posicion = new Punto(3, 4);
            BloqueComun UnObstaculo = BloqueComun.crearBloqueLadrillos(posicion);
            UnObstaculo.daniarConBombaToleTole();
            Assert.AreEqual(true, UnObstaculo.destruido());
        }

        [Test]
        public void TestDaniarConBombaMolotovBloqueLadrillosLoDestruye()
        {
            Punto posicion = new Punto(3, 4);
            BloqueComun UnObstaculo = BloqueComun.crearBloqueLadrillos(posicion);
            UnObstaculo.daniarConBombaMolotov();
            Assert.AreEqual(UnObstaculo.destruido(),true);
        }

        [Test]
        public void TestDaniarConBombaMolotovBloqueCementoModificaUnidades()
        {
            Punto posicion = new Punto(3, 4);
            BloqueComun UnObstaculo = BloqueComun.crearBloqueCemento(posicion);
            UnObstaculo.daniarConBombaMolotov();
            Assert.AreEqual(UnObstaculo.UnidadesDeResistencia, 5);
        }

        [Test]
        public void TestDaniarConBombaToleToleBloqueCemento()
        {
            Punto posicion = new Punto(3, 4);
            BloqueComun UnObstaculo = BloqueComun.crearBloqueCemento(posicion);
            UnObstaculo.daniarConBombaToleTole();
            Assert.AreEqual(true, UnObstaculo.destruido());
        }

        [Test]
        public void TestDaniarConBombaMolotovBloqueCementoLoDestruye()
        {
            Punto posicion = new Punto(3, 4);
            BloqueComun UnObstaculo = BloqueComun.crearBloqueCemento(posicion);
            UnObstaculo.daniarConBombaMolotov();
            UnObstaculo.daniarConBombaMolotov();
            Assert.AreEqual(UnObstaculo.destruido(), true);
        }


    }
}
