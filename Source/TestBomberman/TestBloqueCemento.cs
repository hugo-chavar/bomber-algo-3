using NUnit;
using NUnit.Framework;
using Bomberman;

namespace TestBomberman
{
    [TestFixture]
    class TestBloqueCemento
    {
        [Test]
        public void TestDaniarConBombaMolotovBloqueCementoModificaUnidades()
        {
            Punto posicion = new Punto(3, 4);
            BloqueCemento UnObstaculo = new BloqueCemento(posicion);
            UnObstaculo.daniarConBombaMolotov(5);
            Assert.AreEqual(UnObstaculo.UnidadesDeResistencia, 5);
        }

        [Test]
        public void TestDaniarConBombaToleToleBloqueDeCemento()
        {
            Punto posicion = new Punto(3, 4);
            BloqueCemento UnObstaculo = new BloqueCemento(posicion);
            UnObstaculo.daniarConBombaToleTole();
            Assert.AreEqual(true, UnObstaculo.destruido());
        }

        [Test]
        public void TestDaniarConBombaMolotovBloqueCementoLoDestruye()
        {
            Punto posicion = new Punto(3, 4);
            BloqueCemento UnObstaculo = new BloqueCemento(posicion);
            UnObstaculo.daniarConBombaMolotov(10);
            Assert.AreEqual(UnObstaculo.destruido(),true);
        }

    }
}
