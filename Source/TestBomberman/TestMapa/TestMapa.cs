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
        private Mapa otroMapa;
        private Casilla unaCasilla;
        private Punto pos;
        private IMovible movil;

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
            this.unMapa = new Mapa(ANCHOMAPA, ANCHOMAPA);
            this.otroMapa = new Mapa(ANCHOMAPA, ANCHOMAPA);
            int i, j;
            for (i = 0; i < ANCHOMAPA; i++)
                for (j = 0; j < ANCHOMAPA; j++)
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
        public void AgregarUnaCasillaNulaLanzaNoExisteCasillaException()
        {
            //unMapa = new Mapa(ANCHOMAPA, ALTOMAPA);
            Casilla otraCasilla = null;
            try
            {
                otroMapa.agregarCasilla(otraCasilla);
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
            //unMapa = new Mapa(ANCHOMAPA, ALTOMAPA);
            pos = null;
            Casilla otraCasilla = new Casilla(pos);
            try
            {
                otroMapa.agregarCasilla(otraCasilla);
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
            //unMapa = new Mapa(ANCHOMAPA, ALTOMAPA);
            pos = new Punto(ANCHOMAPA + 1, ALTOMAPA - 1);
            Casilla otraCasilla = new Casilla(pos);
            try
            {
                otroMapa.agregarCasilla(otraCasilla);
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
            //unMapa = new Mapa(ANCHOMAPA, ALTOMAPA);
            Assert.AreEqual(unMapa.DimensionHorizontal, ANCHOMAPA);
        }

        [Test]
        public void UnMapaCreadoTieneElAltoIndicadoEnElConstructor()
        {
            //Mapa otroMapa = new Mapa(ANCHOMAPA, ALTOMAPA);
            Assert.AreEqual(unMapa.DimensionVertical, ALTOMAPA);
        }

        [Test]
        public void NuevoMapaContieneCasillaAgregada()
        {
            //Mapa otroMapa = new Mapa(ANCHOMAPA, ALTOMAPA);
            Punto unaPos = new Punto(4, 2);
            Casilla otraCasilla = FabricaDeCasillas.FabricarCasillaConBloqueCemento(unaPos);//new Casilla(pos);//
            otroMapa.agregarCasilla(otraCasilla);
            Assert.IsTrue(otroMapa.ExisteCasillaEnPosicion(unaPos));
        }

        [Test]
        public void NuevoMapaNoContieneCasillas()
        {
            //Mapa otroMapa = new Mapa(ANCHOMAPA, ALTOMAPA);
            Punto unaPos = new Punto(0, 0);
            Assert.IsFalse(otroMapa.ExisteCasillaEnPosicion(unaPos));
            
        }

        [Test]
        public void AgregarCasillaDejaLaCasillaEnLaPosicionCorrecta()
        {
            Punto unaPos = new Punto(2, 2);
            Casilla unaCasilla = FabricaDeCasillas.FabricarPasillo(unaPos);
            otroMapa.agregarCasilla(unaCasilla);
            Assert.AreSame(otroMapa.ObtenerCasilla(unaPos), unaCasilla);
        }



        [Test]
        public void ExisteCasillaEnPosicionDevuelveTrueSiPreguntoPorLaPosDeArribaYEsaPosicionExisteEnMapa()
        {
            //La posicion existe porque en fixtureSetup unMapa es creado de 5x5 con casillas en todas sus posiciones [0..4][0..4]
            this.pos = new Punto(0, 0);
            //Casilla unaCasilla = FabricaDeCasillas.FabricarPasillo(this.pos);
            //this.unMapa.agregarCasilla(unaCasilla);
            //Assert.IsTrue(this.unMapa.ExisteCasillaEnPosicion(this.pos));
            Punto posSup = this.pos.PosicionSuperior();
            Assert.IsTrue(unMapa.ExisteCasillaEnPosicion(posSup));
        }

        [Test]
        public void ExisteCasillaEnPosicionDevuelveTrueSiPreguntoPorLaPosDeAbajoYEsaPosicionExisteEnMapa()
        {
            //La posicion existe porque en fixtureSetup unMapa es creado de 5x5 con casillas en todas sus posiciones [0..4][0..4]
            this.pos = new Punto(0, 1);
            Punto posInf = this.pos.PosicionInferior();
            Assert.IsTrue(unMapa.ExisteCasillaEnPosicion(posInf));
        }

        [Test]
        public void ExisteCasillaEnPosicionDevuelveTrueSiPreguntoPorLaPosDeLaDerechaYEsaPosicionExisteEnMapa()
        {
            //La posicion existe porque en fixtureSetup unMapa es creado de 5x5 con casillas en todas sus posiciones [0..4][0..4]
            this.pos = new Punto(0, 1);
            Punto posDer = this.pos.PosicionDerecha();
            Assert.IsTrue(unMapa.ExisteCasillaEnPosicion(posDer));
        }

        [Test]
        public void ExisteCasillaEnPosicionDevuelveTrueSiPreguntoPorLaPosDeLaIzquierdaYEsaPosicionExisteEnMapa()
        {
            //La posicion existe porque en fixtureSetup unMapa es creado de 5x5 con casillas en todas sus posiciones [0..4][0..4]
            this.pos = new Punto(1, 1);
            Punto posIzq = this.pos.PosicionIzquierda();
            Assert.IsTrue(unMapa.ExisteCasillaEnPosicion(posIzq));
        }

        [Test]
        public void ExisteCasillaEnPosicionDevuelveFalseSiPreguntoPorLaPosDeArribaYEsaPosicionNoExisteEnMapa()
        {
            //La posicion existe porque en fixtureSetup unMapa es creado de 5x5 con casillas en todas sus posiciones [0..4][0..4]
            this.pos = new Punto(4, 4);
            //Casilla unaCasilla = FabricaDeCasillas.FabricarPasillo(this.pos);
            //this.unMapa.agregarCasilla(unaCasilla);
            //Assert.IsTrue(this.unMapa.ExisteCasillaEnPosicion(this.pos));
            Punto posSup = this.pos.PosicionSuperior();
            Assert.IsFalse(unMapa.ExisteCasillaEnPosicion(posSup));
        }

        [Test]
        public void ExisteCasillaEnPosicionDevuelveFalseSiPreguntoPorLaPosDeAbajoYEsaPosicionExisteEnMapa()
        {
            //La posicion existe porque en fixtureSetup unMapa es creado de 5x5 con casillas en todas sus posiciones [0..4][0..4]
            this.pos = new Punto(3, 0);
            Punto posInf = this.pos.PosicionInferior();
            Assert.IsFalse(unMapa.ExisteCasillaEnPosicion(posInf));
        }

        [Test]
        public void ExisteCasillaEnPosicionDevuelveFalseSiPreguntoPorLaPosDeLaDerechaYEsaPosicionNoExisteEnMapa()
        {
            //La posicion existe porque en fixtureSetup unMapa es creado de 5x5 con casillas en todas sus posiciones [0..4][0..4]
            this.pos = new Punto(4, 2);
            Punto posDer = this.pos.PosicionDerecha();
            Assert.IsFalse(unMapa.ExisteCasillaEnPosicion(posDer));
        }

        [Test]
        public void ExisteCasillaEnPosicionDevuelveFalseSiPreguntoPorLaPosDeLaIzquierdaYEsaPosicionNoExisteEnMapa()
        {
            //La posicion existe porque en fixtureSetup unMapa es creado de 5x5 con casillas en todas sus posiciones [0..4][0..4]
            this.pos = new Punto(0, 3);
            Punto posIzq = this.pos.PosicionIzquierda();
            Assert.IsFalse(unMapa.ExisteCasillaEnPosicion(posIzq));
        }
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
