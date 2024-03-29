﻿using NUnit;
using NUnit.Framework;
using BombermanModel.Mapa.Casilla;
using BombermanModel;
using BombermanModel.Personaje;

namespace TestBomberman
{   
    [TestFixture]
    class TestBloqueComun
    {
        private Obstaculo obstaculoLadrillo;
        private Obstaculo obstaculoCemento;

        [SetUp]
        public void TestSetup()
        {
            obstaculoLadrillo = BloqueComun.CrearBloqueLadrillos();
            obstaculoCemento = BloqueComun.CrearBloqueCemento();
        }

        [Test]
        public void DaniarConBombaMolotovBloqueLadrillosModificaUnidades()
        {
            obstaculoLadrillo.DaniarConBombaMolotov(5);
            Assert.AreEqual(obstaculoLadrillo.UnidadesDeResistencia, 0);
        }

        [Test]
        public void DaniarConBombaToleToleBloqueLadrillos()
        {
            obstaculoLadrillo.DaniarConBombaToleTole();
            Assert.IsTrue(obstaculoLadrillo.Destruido());
        }

        [Test]
        public void DaniarConBombaMolotovBloqueLadrillosLoDestruye()
        {
            obstaculoLadrillo.DaniarConBombaMolotov(5);
            Assert.IsTrue(obstaculoLadrillo.Destruido());
        }

        [Test]
        public void DaniarConBombaMolotovBloqueCementoModificaUnidades()
        {
            obstaculoCemento.DaniarConBombaMolotov(5);
            Assert.AreEqual(obstaculoCemento.UnidadesDeResistencia, 5);
        }

        [Test]
        public void DaniarConBombaToleToleBloqueCemento()
        {
            obstaculoCemento.DaniarConBombaToleTole();
            Assert.IsTrue(obstaculoCemento.Destruido());
        }

        [Test]
        public void TestDaniarConBombaMolotovBloqueCementoLoDestruye()
        {
            obstaculoCemento.DaniarConBombaMolotov(5);
            obstaculoCemento.DaniarConBombaMolotov(5);
            Assert.IsTrue(obstaculoCemento.Destruido());
        }

        [Test]
        public void obstaculoDeCementoNoEsTransitablePorUnPersonajeNoAlado()
        {
            Punto unPunto = new Punto(0, 0);
            Personaje unPersonaje = new Bombita(unPunto);
            Assert.IsFalse(obstaculoCemento.TransitablePor(unPersonaje));
        }

        [Test]
        public void obstaculoDeLadrilloNoEsTransitablePorUnPersonajeNoAlado()
        {
            Punto unPunto = new Punto(0, 0);
            Personaje unPersonaje = new Bombita(unPunto);
            Assert.IsFalse(obstaculoLadrillo.TransitablePor(unPersonaje));
        }
    }
}
