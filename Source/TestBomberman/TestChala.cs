using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Bomberman.Personaje;
using Bomberman.Articulo;

namespace TestBomberman
{   [TestFixture]
    class TestChala
    {
        [Test]
        public void TestComerChalaDuplicaVelocidadDelComedor()
        {
            int velocidad;
            Bombita unBombita = new Bombita();
            Chala unArticulo = new Chala();
            velocidad = unBombita.Velocidad;

            unBombita.comer(unArticulo);

            Assert.AreEqual(2*velocidad, unBombita.Velocidad);

         }
    }
}
