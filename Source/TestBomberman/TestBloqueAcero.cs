﻿using NUnit;
using NUnit.Framework;
using Bomberman.Mapa.Casilla;
using Bomberman;

namespace TestBomberman
{
    [TestFixture]
    public class TestBloqueAcero
    {
        private BloqueAcero unObstaculo;
        private Punto posicion;

        [TestFixtureSetUp]
        public void TestSetup()
        {
            posicion = new Punto(3, 4);
            unObstaculo = new BloqueAcero(posicion);
        }

        
        [Test]
        public void TestDaniarConBombaMolotovBloqueDeAceroNoModificaSuEstado()
            {

                unObstaculo.DaniarConBombaMolotov();
                Assert.AreEqual(unObstaculo.UnidadesDeResistencia, 1); 
            }

        [Test]
        public void TestDaniarConBombaToleToleBloqueDeAcero()
        {

            unObstaculo.DaniarConBombaToleTole();
            Assert.AreEqual(true, unObstaculo.Destruido());
        }
    }
}
