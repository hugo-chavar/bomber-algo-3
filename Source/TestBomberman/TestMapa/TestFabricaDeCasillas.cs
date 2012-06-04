using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Bomberman.Mapa.Casilla;
using Bomberman;

namespace TestBomberman.TestMapa
{
    [TestFixture]
    class TestFabricaDeCasillas
    {
        private const int RESISTENCIALADRILLO = 5;
        private const int RESISTENCIACEMENTO = 10;
        
        private Casilla unaCasilla;
        private Punto unPunto;
        private FabricaDeCasillas unaFabricaDeCasillas;
        
        [TestFixtureSetUp]
        public void TestSetUp()
        {
            unPunto = new Punto (2,3);
            unaCasilla = new Casilla(unPunto);
            unaFabricaDeCasillas = new FabricaDeCasillas();
        }

        [Test]
        public void TestFabricarPasilloDevuelveCasillaEnEstadoPasillo()
        {
            unaCasilla = unaFabricaDeCasillas.FabricarPasillo(unPunto);
            Assert.IsInstanceOf(typeof (Pasillo) , unaCasilla.Estado);
        }

        [Test]
        public void TestFabricarCasillaConBloqueDeAceroDevuelveCasillaEnEstadoBloqueAcero()
        {
            unaCasilla = unaFabricaDeCasillas.FabricarCasillaConBloqueAcero(unPunto);
            Assert.IsInstanceOf(typeof(BloqueAcero), unaCasilla.Estado);
        }
        
        [Test]
        public void TestFabricarCasillaConBloqueDeLadrillosDevuelveCasillaEnEstadoBloqueLadrillos()
        {
            unaCasilla = unaFabricaDeCasillas.FabricarCasillaConBloqueLadrillos(unPunto);
            Assert.IsInstanceOf(typeof(BloqueComun), unaCasilla.Estado);
            Assert.AreEqual(unaCasilla.Estado.UnidadesDeResistencia, RESISTENCIALADRILLO);
        }
        
        [Test]
        public void TestFabricarCasillaConBloqueDeCemento()
        {
            unaCasilla = unaFabricaDeCasillas.FabricarCasillaConBloqueCemento(unPunto);
            Assert.IsInstanceOf(typeof(BloqueComun), unaCasilla.Estado);
            Assert.AreEqual(unaCasilla.Estado.UnidadesDeResistencia, RESISTENCIACEMENTO);
        }      
    }
}
