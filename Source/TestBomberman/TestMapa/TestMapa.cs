using NUnit;
using NUnit.Framework;
using Bomberman;
using Bomberman.Mapa;
using Bomberman.Mapa.Casilla;
using Bomberman.Excepciones;
using Bomberman.Personaje;

namespace TestBomberman.TestMapa
{
    [TestFixture]
    class TestMapa
    {
        private const int ANCHOMAPA = 5;
        private const int ALTOMAPA = 5;
        private Mapa unMapa;
        private Casilla unaCasilla;
        private Punto pos;
        private IMovible movil;

        [Test]
        public void AgregarUnaCasillaNulaLanzaNoExisteCasillaException()
        {
            unMapa = new Mapa(ANCHOMAPA, ALTOMAPA);
            Casilla otraCasilla = null;
            try
            {
                unMapa.agregarCasilla(otraCasilla);
                Assert.Fail();
            }
            catch (NoExisteCasillaException)
            {
            }
            catch (System.Exception)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void AgregarUnaCasillaConPosicionNulaLanzaPosicionNulaException()
        {
            unMapa = new Mapa(ANCHOMAPA, ALTOMAPA);
            pos = null;
            Casilla otraCasilla = new Casilla(pos);
            try
            {
                unMapa.agregarCasilla(otraCasilla);
                Assert.Fail();
            }
            catch (PosicionNulaException)
            {
            }
            catch (System.Exception)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void AgregarUnaCasillaConPosicionFueraDeRangoLanzaPuntoFueraDeRangoEnMapaException()
        {
            unMapa = new Mapa(ANCHOMAPA, ALTOMAPA);
            pos = new Punto(7, 2);
            Casilla otraCasilla = new Casilla(pos);
            try
            {
                unMapa.agregarCasilla(otraCasilla);
                Assert.Fail();
            }
            catch (PuntoFueraDeRangoEnMapaException)
            {
            }
            catch (System.Exception)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void UnMapaCreadoTieneElAnchoIndicadoEnElConstructor()
        { 
            unMapa = new Mapa(ANCHOMAPA, ALTOMAPA);
            Assert.AreEqual(unMapa.DimensionHorizontal, ANCHOMAPA);
        }

        [Test]
        public void UnMapaCreadoTieneElAltoIndicadoEnElConstructor()
        {
            unMapa = new Mapa(ANCHOMAPA, ALTOMAPA);
            Assert.AreEqual(unMapa.DimensionVertical, ALTOMAPA);
        }

        [Test]
        public void NuevoMapaContieneCasillaAgregada()
        {
            Mapa otroMapa = new Mapa(ANCHOMAPA, ALTOMAPA);
            pos = new Punto(4, 2);
            Casilla otraCasilla = FabricaDeCasillas.FabricarCasillaConBloqueCemento(pos);//new Casilla(pos);//
            otroMapa.agregarCasilla(otraCasilla);
            Assert.IsTrue(otroMapa.ExisteCasillaEnPosicion(pos));
        }

        [Test]
        public void NuevoMapaNoContieneCasillas()
        {
            unMapa = new Mapa(ANCHOMAPA, ALTOMAPA);
            Punto unaPos = new Punto(0, 0);
            Assert.IsFalse(unMapa.ExisteCasillaEnPosicion(unaPos));
            
        }

        [Test]
        public void AgregarCasillaDejaLaCasillaEnLaPosicionCorrecta()
        {
            this.pos = new Punto(2, 3);
            this.unMapa = new Mapa(ANCHOMAPA, ALTOMAPA);
            this.unaCasilla = FabricaDeCasillas.FabricarPasillo(pos);
            this.unMapa.agregarCasilla(unaCasilla);
            Assert.AreSame(unMapa.ObtenerCasilla(pos) , unaCasilla);
        }
        
        [TestFixtureSetUp]
        public void TestSetup()
        {
            //creo un mapa 5x5 con esta distribucion (P = Pasillo, * = BloqueAcero):
             //      P P P P P
             //      P * P * P
             //      P P P P P
             //      P * P * P
             //      P P P P P
            
            Punto unaPosicion;
            Casilla unaCasilla;
            this.unMapa = new Mapa(5,5);
            int i,j;
            for ( i = 0 ; i < 5 ; i++)
                for ( j = 0; j < 5; j++)
                {
                    unaPosicion = new Punto(i, j);
                    if ((i & 1) == 0 && (j & 1) == 0)
                    {
                        //ambos son numeros pares
                        unaCasilla = FabricaDeCasillas.FabricarCasillaConBloqueAcero(unaPosicion);
                    }
                    else
                    { 
                        //uno de los dos es impar
                        unaCasilla = FabricaDeCasillas.FabricarPasillo(unaPosicion);
                    }
                    this.unMapa.agregarCasilla(unaCasilla);
                }

        }

        [Test]
        public void ExisteCasillaEnPosicionDevuelveTrueSiVoyAArribaYEsaPosicionExisteEnMapa()
        {
            this.pos = new Punto(0, 0);
            Casilla unaCasilla = FabricaDeCasillas.FabricarPasillo(this.pos);
            this.unMapa.agregarCasilla(unaCasilla);
            Assert.IsTrue(this.unMapa.ExisteCasillaEnPosicion(this.pos));
            //Punto otra = this.pos.PosicionSuperior();
            //Assert.IsTrue(unMapa.ExisteCasillaEnPosicion(otra));
        }

        /*[Test]
        public void ExisteCasillaEnPosicionDevuelveTrueSiVoyAAbajoYEsaPosicionExisteEnMapa()
        {
            this.pos = new Punto(0, 1);
            Punto otra = this.pos.PosicionInferior();
            Assert.IsTrue(unMapa.ExisteCasillaEnPosicion(otra));
        }
        */
        /*[Test]
        public void PosicionDeArribaTienePasilloBombitaPuedeMoverseHaciaArriba()
        {
            movil = new Bombita(this.pos);
            this.pos = new Punto(0, 0);
            this.unaCasilla = unMapa.ObtenerCasilla(this.pos);
            this.unaCasilla.Transitar(movil);
            this.unMapa.agregarCasilla(unaCasilla);
            Assert.IsTrue(unMapa.PermitidoMoverHaciaArribaA(movil));
        }*/

    }
}
