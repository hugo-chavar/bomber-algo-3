using NUnit;
using NUnit.Framework;
using Bomberman;

namespace TestBomberman
{
    [TestFixture]
    public class TestBloqueAcero
    {
        [Test]
        public void TestDaniarConBombaMolotovBloqueDeAceroNoModificaSuEstado()
            {
                Punto posicion = new Punto(3, 4);
                BloqueAcero UnObstaculo = new BloqueAcero(posicion);
                UnObstaculo.daniarConBombaMolotov(3);
                Assert.AreEqual(UnObstaculo.UnidadesDeResistencia, 1); 
            }

        [Test]
        public void TestDaniarConBombaToleToleBloqueDeAcero()
        {
            Punto posicion = new Punto(3, 4);
            BloqueAcero UnObstaculo = new BloqueAcero(posicion);
            UnObstaculo.daniarConBombaToleTole();
            Assert.AreEqual(true, UnObstaculo.destruido());
        }
    }
}
