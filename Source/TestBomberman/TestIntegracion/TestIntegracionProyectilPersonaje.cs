using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Juego;
using Bomberman.Mapa;
using NUnit.Framework;

namespace TestBomberman.TestIntegracion
{
    class TestIntegracionProyectilPersonaje
    {
        private Juego unJuego;
        private Mapa unMapa;

        [SetUp]
        public void TestSetup()
        {
            this.unJuego = Juego.Instancia();
            this.unMapa = this.unJuego.Ambiente;

        }

        [TearDown]
        public void TearDown()
        {
            Juego.Reiniciar();
        }

    }
}
