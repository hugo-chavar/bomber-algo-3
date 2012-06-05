using NUnit;
using NUnit.Framework;
using Bomberman;
using Bomberman.Mapa;
using Bomberman.Mapa.Casilla;
using Bomberman.Excepciones;
using Bomberman.Personaje;
using Bomberman.Arma;

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
                    if ((i & 1) == 1 && (j & 1) == 1)
                    {
                        //ambos son numeros impares
                        unaCasilla = FabricaDeCasillas.FabricarCasillaConBloqueLadrillos(unaPosicion);//.FabricarCasillaConBloqueAcero(unaPosicion);
                    }
                    else
                    {
                        //uno de los dos es par
                        unaCasilla = FabricaDeCasillas.FabricarPasillo(unaPosicion);
                    }
                    this.unMapa.AgregarCasilla(unaCasilla);
                }

        }

        [Test]
        public void AgregarUnaCasillaNulaLanzaNoExisteCasillaException()
        {
            Casilla otraCasilla = null;
            try
            {
                otroMapa.AgregarCasilla(otraCasilla);
                Assert.Fail();
            }
            catch (NoExisteCasillaException)
            {
                Assert.Pass("Exception correcta fue lanzda.");
            }
            catch (System.Exception)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void AgregarUnaCasillaConPosicionNulaLanzaPosicionNulaException()
        {
            pos = null;
            Casilla otraCasilla = new Casilla(pos);
            try
            {
                otroMapa.AgregarCasilla(otraCasilla);
                Assert.Fail();
            }
            catch (PosicionNulaException)
            {
                Assert.Pass("Exception correcta fue lanzda.");
            }
            catch (System.Exception)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void AgregarUnaCasillaConPosicionFueraDeRangoLanzaPuntoFueraDeRangoEnMapaException()
        {
            pos = new Punto(ANCHOMAPA + 1, ALTOMAPA - 1);
            Casilla otraCasilla = new Casilla(pos);
            try
            {
                otroMapa.AgregarCasilla(otraCasilla);
                Assert.Fail();
            }
            catch (PuntoFueraDeRangoEnMapaException)
            {
                Assert.Pass("Exception correcta fue lanzda.");
            }
            catch (System.Exception)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void UnMapaCreadoTieneElAnchoIndicadoEnElConstructor()
        { 
            Assert.AreEqual(unMapa.DimensionHorizontal, ANCHOMAPA);
        }

        [Test]
        public void UnMapaCreadoTieneElAltoIndicadoEnElConstructor()
        {
            Assert.AreEqual(unMapa.DimensionVertical, ALTOMAPA);
        }

        [Test]
        public void NuevoMapaContieneCasillaAgregada()
        {
            Punto unaPos = new Punto(4, 2);
            Casilla otraCasilla = FabricaDeCasillas.FabricarCasillaConBloqueCemento(unaPos);
            otroMapa.AgregarCasilla(otraCasilla);
            Assert.IsTrue(otroMapa.ExisteCasillaEnPosicion(unaPos));
        }

        [Test]
        public void NuevoMapaNoContieneCasillas()
        {
            Punto unaPos = new Punto(0, 0);
            Assert.IsFalse(otroMapa.ExisteCasillaEnPosicion(unaPos));
        }

        [Test]
        public void AgregarCasillaDejaLaCasillaEnLaPosicionCorrecta()
        {
            Punto unaPos = new Punto(2, 2);
            Casilla unaCasilla = FabricaDeCasillas.FabricarPasillo(unaPos);
            otroMapa.AgregarCasilla(unaCasilla);
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
            //La posicion NO existe porque en fixtureSetup unMapa es creado de 5x5 con casillas en todas sus posiciones [0..4][0..4]
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
            //La posicion NO existe porque en fixtureSetup unMapa es creado de 5x5 con casillas en todas sus posiciones [0..4][0..4]
            this.pos = new Punto(3, 0);
            Punto posInf = this.pos.PosicionInferior();
            Assert.IsFalse(unMapa.ExisteCasillaEnPosicion(posInf));
        }

        [Test]
        public void ExisteCasillaEnPosicionDevuelveFalseSiPreguntoPorLaPosDeLaDerechaYEsaPosicionNoExisteEnMapa()
        {
            //La posicion NO existe porque en fixtureSetup unMapa es creado de 5x5 con casillas en todas sus posiciones [0..4][0..4]
            this.pos = new Punto(4, 2);
            Punto posDer = this.pos.PosicionDerecha();
            Assert.IsFalse(unMapa.ExisteCasillaEnPosicion(posDer));
        }

        [Test]
        public void ExisteCasillaEnPosicionDevuelveFalseSiPreguntoPorLaPosDeLaIzquierdaYEsaPosicionNoExisteEnMapa()
        {
            //La posicion NO existe porque en fixtureSetup unMapa es creado de 5x5 con casillas en todas sus posiciones [0..4][0..4]
            this.pos = new Punto(0, 3);
            Punto posIzq = this.pos.PosicionIzquierda();
            Assert.IsFalse(unMapa.ExisteCasillaEnPosicion(posIzq));
        }

        [Test]
        public void AgregarCasillaLanzaCasillaYaIngresadaExceptionSiYaFueAgregadaAnteriorMente()
        {
            this.pos = new Punto(0, 0);
            this.unaCasilla = FabricaDeCasillas.FabricarPasillo(this.pos);
            try
            {
                this.unMapa.AgregarCasilla(unaCasilla);
                Assert.Fail();
            }
            catch (CasillaYaIngresadaException)
            {
                Assert.Pass("Exception correcta fue lanzada.");
            }
            catch (System.Exception)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void AgregarCasillaNoAgregadaAnteriorMenteNoLanzaNingunaExcepcion()
        {
            this.pos = new Punto(4, 4);
            this.unaCasilla = FabricaDeCasillas.FabricarPasillo(this.pos);
            try
            {
                this.otroMapa.AgregarCasilla(unaCasilla);
            }
            catch (System.Exception)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void PosicionDeArribaTienePasilloBombitaPuedeMoverseHaciaArriba()
        {
            //ver mapa en el FixtureSetup
            this.pos = new Punto(0, 0);
            movil = new Bombita(this.pos);
            this.unaCasilla = unMapa.ObtenerCasilla(this.pos);
            this.unaCasilla.Transitar(movil);
            Assert.IsTrue(unMapa.PermitidoMoverHaciaArribaA(movil));
        }

        [Test]
        public void PosicionDeLaDerechaTienePasilloBombitaPuedeMoverseHaciaLaDerecha()
        {
            //ver mapa en el FixtureSetup
            this.pos = new Punto(0, 0);
            movil = new Bombita(this.pos);
            this.unaCasilla = unMapa.ObtenerCasilla(this.pos);
            this.unaCasilla.Transitar(movil);
            Assert.IsTrue(unMapa.PermitidoMoverHaciaArribaA(movil));
        }

        [Test]
        public void PosicionDeAbajoTienePasilloBombitaPuedeMoverseHaciaAbajo()
        {
            //ver mapa en el FixtureSetup
            this.pos = new Punto(2, 3);
            movil = new Bombita(this.pos);
            this.unaCasilla = unMapa.ObtenerCasilla(this.pos);
            this.unaCasilla.Transitar(movil);
            Assert.IsTrue(unMapa.PermitidoMoverHaciaAbajoA(movil));
        }

        [Test]
        public void PosicionDeLaIzquierdaTienePasilloBombitaPuedeMoverseHaciaLaIzquierda()
        {
            //ver mapa en el FixtureSetup
            this.pos = new Punto(3, 0);
            movil = new Bombita(this.pos);
            this.unaCasilla = unMapa.ObtenerCasilla(this.pos);
            this.unaCasilla.Transitar(movil);
            Assert.IsTrue(unMapa.PermitidoMoverHaciaIzquierdaA(movil));
        }

        [Test]
        public void PosicionDeAbajoNoExisteBombitaNoPuedeMoverseHaciaAbajo()
        {
            this.pos = new Punto(2, 0);
            movil = new Bombita(this.pos);
            
            this.unaCasilla = unMapa.ObtenerCasilla(this.pos);
            this.unaCasilla.Transitar(movil);
            Assert.IsFalse(unMapa.PermitidoMoverHaciaAbajoA(movil));
        }

        [Test]
        public void PosicionDeArribaNoExisteBombitaNoPuedeMoverseHaciaArriba()
        {
            this.pos = new Punto(3, 4);
            movil = new Bombita(this.pos);

            this.unaCasilla = unMapa.ObtenerCasilla(this.pos);
            this.unaCasilla.Transitar(movil);
            Assert.IsFalse(unMapa.PermitidoMoverHaciaArribaA(movil));
        }

        [Test]
        public void PosicionDeLaDerechaNoExisteBombitaNoPuedeMoverseHaciaLaDerecha()
        {
            this.pos = new Punto(4, 3);
            movil = new Bombita(this.pos);

            this.unaCasilla = unMapa.ObtenerCasilla(this.pos);
            this.unaCasilla.Transitar(movil);
            Assert.IsFalse(unMapa.PermitidoMoverHaciaDerechaA(movil));
        }

        [Test]
        public void PosicionDeLaIzquierdaNoExisteBombitaNoPuedeMoverseHaciaLaIzquierda()
        {
            this.pos = new Punto(0, 4);
            movil = new Bombita(this.pos);

            this.unaCasilla = unMapa.ObtenerCasilla(this.pos);
            this.unaCasilla.Transitar(movil);
            Assert.IsFalse(unMapa.PermitidoMoverHaciaIzquierdaA(movil));
        }

        [Test]
        public void PosicionDeAbajoEsUnObstaculoBombitaNoPuedeMoverseHaciaAbajo()
        {
            //ver mapa en el FixtureSetup
            this.pos = new Punto(1, 2);
            movil = new Bombita(this.pos);
            
            this.unaCasilla = unMapa.ObtenerCasilla(this.pos);
            this.unaCasilla.Transitar(movil);
            Assert.IsFalse(unMapa.PermitidoMoverHaciaAbajoA(movil));
        }

        [Test]
        public void PosicionDeArribaEsUnObstaculoBombitaNoPuedeMoverseHaciaArriba()
        {
            //ver mapa en el FixtureSetup
            this.pos = new Punto(1, 2);
            movil = new Bombita(this.pos);

            this.unaCasilla = unMapa.ObtenerCasilla(this.pos);
            this.unaCasilla.Transitar(movil);
            Assert.IsFalse(unMapa.PermitidoMoverHaciaArribaA(movil));
        }

        [Test]
        public void PosicionDeLaDerechaEsUnObstaculoBombitaNoPuedeMoverseHaciaLaDerecha()
        {
            //ver mapa en el FixtureSetup
            this.pos = new Punto(2, 1);
            movil = new Bombita(this.pos);

            this.unaCasilla = unMapa.ObtenerCasilla(this.pos);
            this.unaCasilla.Transitar(movil);
            Assert.IsFalse(unMapa.PermitidoMoverHaciaDerechaA(movil));
        }

        [Test]
        public void PosicionDeLaIzquierdaEsUnObstaculoBombitaNoPuedeMoverseHaciaLaIzquierda()
        {
            //ver mapa en el FixtureSetup
            this.pos = new Punto(2, 3);
            movil = new Bombita(this.pos);

            this.unaCasilla = unMapa.ObtenerCasilla(this.pos);
            this.unaCasilla.Transitar(movil);
            Assert.IsFalse(unMapa.PermitidoMoverHaciaIzquierdaA(movil));
        }

        [Test]
        public void PosicionDeAbajoEsUnObstaculoUnoDeLosLopezReggaeAladoPuedeMoverseHaciaAbajo()
        {
            //ver mapa en el FixtureSetup
            this.pos = new Punto(1, 2);
            movil = new LosLopezReggaeAlado(this.pos);

            this.unaCasilla = unMapa.ObtenerCasilla(this.pos);
            this.unaCasilla.Transitar(movil);
            Assert.IsTrue(unMapa.PermitidoMoverHaciaAbajoA(movil));
        }

        [Test]
        public void PosicionDeArribaEsUnObstaculoUnoDeLosLopezReggaeAladoPuedeMoverseHaciaArriba()
        {
            //ver mapa en el FixtureSetup
            this.pos = new Punto(1, 2);
            movil = new LosLopezReggaeAlado(this.pos);

            this.unaCasilla = unMapa.ObtenerCasilla(this.pos);
            this.unaCasilla.Transitar(movil);
            Assert.IsTrue(unMapa.PermitidoMoverHaciaArribaA(movil));
        }

        [Test]
        public void PosicionDeLaDerechaEsUnObstaculoUnoDeLosLopezReggaeAladoPuedeMoverseHaciaLaDerecha()
        {
            //ver mapa en el FixtureSetup
            this.pos = new Punto(2, 1);
            movil = new LosLopezReggaeAlado(this.pos);

            this.unaCasilla = unMapa.ObtenerCasilla(this.pos);
            this.unaCasilla.Transitar(movil);
            Assert.IsTrue(unMapa.PermitidoMoverHaciaDerechaA(movil));
        }

        [Test]
        public void PosicionDeLaIzquierdaEsUnObstaculoUnoDeLosLopezReggaeAladoPuedeMoverseHaciaLaIzquierda()
        {
            //ver mapa en el FixtureSetup
            this.pos = new Punto(2, 3);
            movil = new LosLopezReggaeAlado(this.pos);

            this.unaCasilla = unMapa.ObtenerCasilla(this.pos);
            this.unaCasilla.Transitar(movil);
            Assert.IsTrue(unMapa.PermitidoMoverHaciaIzquierdaA(movil));
        }

        [Test]
        public void BombaToleToleExplotaAlcanzandoAUnBloqueDeCementoDevuelveTrueSiElBloqueQuedaDestruido()
        {
            Punto puntoObstaculo = new Punto(1, 1);
            Punto puntoToleTole = new Punto(2, 1);
            unaCasilla = unMapa.ObtenerCasilla(puntoObstaculo);
            Casilla otraCasilla = unMapa.ObtenerCasilla(puntoToleTole);
            BombaToleTole unaBomba = new BombaToleTole(puntoToleTole, 100);
            otraCasilla.PlantarExplosivo(unaBomba);

            unMapa.ManejarExplosion(unaBomba);
            //A esta hora no se me ocurre mucho.. puede ser porque los bloques son de acero que no se estan rompiendo?
            // probe con bloques de ladrillo y tampoco rompe, probe poner mas cerca la bomba y tampoco la rompre
            //pero nombre del test dice bloque de cemento ¿? cambialo arriba .. en el setup
            //otra es mirar bien el manejarexplosion
            //vas por el buen camino.. debe ser una boludez..pero hay que encontrarla!!

            Assert.IsTrue(unaCasilla.Estado.Destruido());


        }



    }
}
