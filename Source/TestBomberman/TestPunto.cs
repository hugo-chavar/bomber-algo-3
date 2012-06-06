using NUnit;
using NUnit.Framework;
using Bomberman;

namespace TestBomberman
{
    [TestFixture]
    class TestPunto
    {
        private Punto posicion;
        
        [TestFixtureSetUp]
        public void TestSetup()
        {
            posicion = new Punto(3, 4);
        }

        [Test]
        public void EqualsDevuelveTrueSiElPuntoEsComparadoConOtroQuePoseeLasMismasCoordenadas()
        {
            Punto otroPunto = new Punto(3, 4);

            Assert.IsTrue(this.posicion.Equals(otroPunto));
        }

        [Test]
        public void EqualsDevuelveFalseSiElPuntoEsComparadoConOtroQuePoseeDifiereEnAlMenosUnaCoordenada()
        {
            Punto otroPunto = new Punto(3, 5);

            Assert.IsFalse(this.posicion.Equals(otroPunto));
        }

        [Test]
        public void ClonarDevuelveUnNuevoPuntoConCordenadasIgualesAlPuntoClonado()
        {
            Punto otroPunto = this.posicion.Clonar();

            Assert.IsTrue(this.posicion.Equals(otroPunto));
        }

        [Test]
        public void ClonarDevuelveUnNuevoPuntoYLasReferenciasNoSonIgules()
        {
            Punto otroPunto = this.posicion.Clonar();

            Assert.AreNotSame(this.posicion, otroPunto);
        }

        [Test]
        public void PosicionSuperiorDevuelveUnaNuevaPosicionConCoordenadaYAumentadaEn1()
        {
            Punto posicionSuperior = this.posicion.PosicionSuperior();
            
            Assert.IsTrue(posicionSuperior.Equals(new Punto(3,5)));
        }

        [Test]
        public void PosicionSuperiorNoDevuelveUnaNuevaPosicionIdentica()
        {
            Punto posicionSuperior = this.posicion.PosicionSuperior();

            Assert.IsFalse(posicionSuperior.Equals(new Punto(3, 4)));
        }

        [Test]
        public void PosicionInferriorDevuelveUnaNuevaPosicionConCoordenadaYDisminuidaEn1()
        {
            Punto posicionInferior = this.posicion.PosicionInferior();

            Assert.IsTrue(posicionInferior.Equals(new Punto(3, 3)));
        }

        [Test]
        public void PosicionInferiorNoDevuelveUnaNuevaPosicionIdentica()
        {
            Punto posicionInferior = this.posicion.PosicionInferior();

            Assert.IsFalse(posicionInferior.Equals(new Punto(3, 4)));
        }

        [Test]
        public void PosicionIzquierdaDevuelveUnaNuevaPosicionConCoordenadaXDisminuidaEn1()
        {
            Punto posicionIzquierda = this.posicion.PosicionIzquierda();

            Assert.IsTrue(posicionIzquierda.Equals(new Punto(2, 4)));
        }

        [Test]
        public void PosicionIzquierdaNoDevuelveUnaNuevaPosicionIdentica()
        {
            Punto posicionIzquierda = this.posicion.PosicionIzquierda();

            Assert.IsFalse(posicionIzquierda.Equals(new Punto(3, 4)));
        }
        [Test]
        public void PosicionDerechaDevuelveUnaNuevaPosicionConCoordenadaXAumentadaEn1()
        {
            Punto posicionDerecha = this.posicion.PosicionDerecha();

            Assert.IsTrue(posicionDerecha.Equals(new Punto(4, 4)));
        }

        [Test]
        public void PosicionDerechaNoDevuelveUnaNuevaPosicionIdentica()
        {
            Punto posicionDerecha = this.posicion.PosicionDerecha();

            Assert.IsFalse(posicionDerecha.Equals(new Punto(3, 4)));
        }
    }
}
