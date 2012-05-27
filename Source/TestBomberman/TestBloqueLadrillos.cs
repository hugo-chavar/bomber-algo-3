using NUnit;
using NUnit.Framework;
using Bomberman;

namespace TestBomberman
{   [TestFixture]
    class TestBloqueLadrillos
    {
        [Test]
        public void TestDaniarConBombaMolotovBloqueLadrillosModificaUnidades()
        {
            Punto posicion = new Punto(3, 4);
            BloqueLadrillos UnObstaculo = new BloqueLadrillos(posicion);
            UnObstaculo.daniarConBombaMolotov(3);
            Assert.AreEqual(UnObstaculo.UnidadesDeResistencia, 2);
        }

        [Test]
        public void TestDaniarConBombaToleToleBloqueLadrillos()
        {
            Punto posicion = new Punto(3, 4);
            BloqueLadrillos UnObstaculo = new BloqueLadrillos(posicion);
            UnObstaculo.daniarConBombaToleTole();
            Assert.AreEqual(true, UnObstaculo.destruido());
        }

        [Test]
        public void TestDaniarConBombaMolotovBloqueLadrillosLoDestruye()
        {
            Punto posicion = new Punto(3, 4);
            BloqueLadrillos UnObstaculo = new BloqueLadrillos(posicion);
            UnObstaculo.daniarConBombaMolotov(5);
            Assert.AreEqual(UnObstaculo.destruido(),true);
        }
    }
}
