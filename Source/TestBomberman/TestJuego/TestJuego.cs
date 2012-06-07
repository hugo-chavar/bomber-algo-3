﻿using NUnit.Framework;
using Bomberman.Juego;
using Bomberman.Mapa;
using Bomberman.Mapa.Casilla;
using Bomberman.Personaje;
using Bomberman.Arma;
using Bomberman;
using Bomberman.Articulo;

namespace TestBomberman.TestJuego
{
    [TestFixture]
    public class TestJuego
    {
        private Juego unJuego;

        private const int ANCHOMAPA = 5;
        private const int ALTOMAPA = 5;
        private Mapa unMapa;
        //private Mapa otroMapa;
        //private Casilla unaCasilla;
        private IMovible movil;

        [SetUp]
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
            
            int i, j;
            for (i = 0; i < ANCHOMAPA; i++)
                for (j = 0; j < ANCHOMAPA; j++)
                {
                    unaPosicion = new Punto(i, j);
                    if ((i & 1) == 1 && (j & 1) == 1)
                    {
                        //ambos son numeros impares
                        unaCasilla = FabricaDeCasillas.FabricarCasillaConBloqueAcero(unaPosicion);
                    }
                    else
                    {
                        //uno de los dos es par
                        unaCasilla = FabricaDeCasillas.FabricarPasillo(unaPosicion);
                    }
                    this.unMapa.AgregarCasilla(unaCasilla);
                }
            
            this.unJuego = Juego.Instancia();
            this.unJuego.Ambiente = this.unMapa;

        }

        [Test]
        public void DosReferenciasAJuegoDebenSerElMismoObjeto()
        {
            //verifico que funcione el Singleton
            Juego otroJuego = Juego.Instancia();
            Assert.AreSame(otroJuego, this.unJuego);
        }

        [Test]
        public void AlPausarJuegoSePausaCorrectamente()
        {
            unJuego.PausarJuego();
            Assert.IsTrue(unJuego.JuegoPausado);
        }

        [Test]
        public void AlDesPausarJuegoSeDesPausaCorrectamente()
        {
            unJuego.PausarJuego();
            unJuego.DesPausarJuego();
            Assert.IsFalse(unJuego.JuegoPausado);
        }

        [Test]
        public void PerderVidaDescuentaUnaVidaAlJuegoActual() 
        {
            Juego unJuego = new Juego();
            unJuego.PerderVida();
            Assert.AreEqual(2, unJuego.CantDeVidas);
        }

        [Test]
        public void BombitaConVelocidadNormalCambiaDePosicionEnUnaUnidadDentroDelMapaAlMoversePorPasillo()
        {
            this.movil = new Bombita(new Punto(0, 0));
            Punto posOriginal = this.movil.Posicion.Clonar();
            this.unMapa.AgregarPersonaje(this.movil); // testear todos los exceptions
            this.movil.Movimiento.CambiarADerecha();
            this.movil.Mover();
            Punto pos = new Punto(1,0);
            Assert.IsTrue(pos.Equals(this.movil.Posicion));
        }

        [Test]
        public void UnMovilDespuesDeMoverseEnDireccionAUnPasilloSuPosicionNoEsIgualALaOriginal()
        {
            IMovible otroMovil = new Bombita(new Punto(0, 0));
            Punto posOriginal = otroMovil.Posicion.Clonar();
            this.unMapa.AgregarPersonaje(otroMovil); // testear todos los exceptions
            otroMovil.Movimiento.CambiarADerecha();
            otroMovil.Mover();
            //Punto pos = new Punto(1, 0);
            Assert.IsFalse(posOriginal.Equals(otroMovil.Posicion));
        }

        [Test]
        public void UnMovilDespuesDeMoverseEnDireccionAUnObstaculoSuPosicionEsIgualALaOriginal()
        {
            IMovible otroMovil = new Bombita(new Punto(1, 0));
            Punto posOriginal = otroMovil.Posicion.Clonar();
            this.unMapa.AgregarPersonaje(otroMovil); // testear todos los exceptions
            otroMovil.Movimiento.CambiarAArriba();
            otroMovil.Mover();
            Assert.IsTrue(posOriginal.Equals(otroMovil.Posicion));
        }

        [Test]
        public void BombitaChocaConObstaculoMoverNoCambiaSuPosicion()
        {
            IMovible otroMovil = new Bombita(new Punto(0, 0));
            this.unMapa.AgregarPersonaje(otroMovil); // testear todos los exceptions
            //Punto posOriginal = otroMovil.Posicion.Clonar();
            otroMovil.Movimiento.CambiarADerecha();
            otroMovil.Mover();
            otroMovil.Movimiento.CambiarAArriba();
            otroMovil.Mover();
            Punto posObstaculo = new Punto(1, 1);
            Assert.IsFalse(otroMovil.Posicion.Equals(posObstaculo));
        }

        [Test]
        public void BombitaAvanzaPorTodoElMapaYNoCambiaDePosCuandoChocaConElLimiteDerechoDelMapa()
        {
            IMovible otroMovil = new Bombita(new Punto(0, 0));
            this.unMapa.AgregarPersonaje(otroMovil);
            //Punto posOriginal = otroMovil.Posicion.Clonar();
            otroMovil.Movimiento.CambiarAArriba();
            otroMovil.Mover();//fue a 0,1
            otroMovil.Mover();//fue a 0,2
            otroMovil.Movimiento.CambiarADerecha();
            otroMovil.Mover(); //fue a 1,2
            otroMovil.Mover();//fue a 2,2
            otroMovil.Movimiento.CambiarAArriba();
            otroMovil.Mover(); //fue a 2,3
            otroMovil.Mover();//fue a 2,4
            otroMovil.Movimiento.CambiarADerecha();
            otroMovil.Mover(); //fue a 3,4
            otroMovil.Mover();//fue a 4,4, esta en el limite!
            otroMovil.Mover(); //choca con el limite
            Punto posFinal = new Punto(4, 4);
            Assert.IsTrue(otroMovil.Posicion.Equals(posFinal));
        }

        [Test]
        public void BombitaNoCambiaDePosCuandoChocaConElLimiteIzquierdoDelMapa()
        {
            IMovible otroMovil = new Bombita(new Punto(0, 2));
            Punto posOriginal = otroMovil.Posicion.Clonar();
            this.unMapa.AgregarPersonaje(otroMovil);
            otroMovil.Movimiento.CambiarAIzquierda();
            otroMovil.Mover(); //choca con el limite izquierdo
            Assert.IsTrue(otroMovil.Posicion.Equals(posOriginal));
        }

        [Test]
        public void BombitaNoCambiaDePosCuandoChocaConElLimiteInferiorDelMapa()
        {
            IMovible otroMovil = new Bombita(new Punto(3, 0));
            Punto posOriginal = otroMovil.Posicion.Clonar();
            this.unMapa.AgregarPersonaje(otroMovil);
            otroMovil.Movimiento.CambiarAAbajo();
            otroMovil.Mover(); //choca con el limite inferior
            Assert.IsTrue(otroMovil.Posicion.Equals(posOriginal));
        }

        [Test]
        public void BombitaNoCambiaDePosCuandoChocaConElLimiteSuperiorDelMapa()
        {
            IMovible otroMovil = new Bombita(new Punto(2, 4));
            Punto posOriginal = otroMovil.Posicion.Clonar();
            this.unMapa.AgregarPersonaje(otroMovil); 
            otroMovil.Movimiento.CambiarAArriba();
            otroMovil.Mover(); //choca con el limite superior
            Assert.IsTrue(otroMovil.Posicion.Equals(posOriginal));
        }

        [Test]
        public void UnLopezReggaeAladoAvanzaSobreUnObstaculoSuPosicionEsDistintaALaOriginal()
        {
            IMovible otroMovil = new LosLopezReggaeAlado(new Punto(1, 0));
            Punto posOriginal = otroMovil.Posicion.Clonar();
            this.unMapa.AgregarPersonaje(otroMovil); 
            otroMovil.Movimiento.CambiarAArriba();
            otroMovil.Mover(); //se mueve donde hay un bloque de acero
            Assert.IsFalse(otroMovil.Posicion.Equals(posOriginal));
        }

        [Test]
        public void UnLopezReggaeAladoAtraviesaObstaculos()
        {
            IMovible otroMovil = new LosLopezReggaeAlado(new Punto(0, 0));
            this.unMapa.AgregarPersonaje(otroMovil); 
            //Punto posOriginal = otroMovil.Posicion.Clonar();
            otroMovil.Movimiento.CambiarAArriba();
            otroMovil.Mover();//fue a 0,1
            otroMovil.Movimiento.CambiarADerecha();
            otroMovil.Mover();//fue a 1,1 (esta sobre un bloque de acero)
            otroMovil.Mover(); //fue a 2,1 (esta sobre pasillo)
            otroMovil.Mover();//fue a 3,1 (esta sobre un bloque de acero)
            otroMovil.Movimiento.CambiarAArriba();
            otroMovil.Mover(); //fue a 3,2 (esta sobre pasillo)
            otroMovil.Mover();//fue a 3,3 (esta sobre un bloque de acero)
            otroMovil.Mover(); //fue a 3,4
            Punto posFinal = new Punto(3, 4);
            Assert.IsTrue(otroMovil.Posicion.Equals(posFinal));
        }

        [Test]
        public void UnLopezReggaeAladoNoAtraviesaElLimiteIzquierdoDelMapa()
        {
            IMovible otroMovil = new LosLopezReggaeAlado(new Punto(0, 0));
            this.unMapa.AgregarPersonaje(otroMovil); 
            Punto posOriginal = otroMovil.Posicion.Clonar();
            otroMovil.Movimiento.CambiarAIzquierda();
            otroMovil.Mover();//choca con el limite izquierdo
            Assert.IsTrue(otroMovil.Posicion.Equals(posOriginal));
        }

        [Test]
        public void UnLopezReggaeAladoNoAtraviesaElLimiteDerechoDelMapa()
        {
            IMovible otroMovil = new LosLopezReggaeAlado(new Punto(4, 0));
            this.unMapa.AgregarPersonaje(otroMovil);
            Punto posOriginal = otroMovil.Posicion.Clonar();
            otroMovil.Movimiento.CambiarADerecha();
            otroMovil.Mover();//choca con el limite derecho
            Assert.IsTrue(otroMovil.Posicion.Equals(posOriginal));
        }

        [Test]
        public void UnLopezReggaeAladoNoAtraviesaElLimiteSuperiorDelMapa()
        {
            IMovible otroMovil = new LosLopezReggaeAlado(new Punto(4, 4));
            this.unMapa.AgregarPersonaje(otroMovil);
            Punto posOriginal = otroMovil.Posicion.Clonar();
            otroMovil.Movimiento.CambiarAArriba();
            otroMovil.Mover();//choca con el limite superior
            Assert.IsTrue(otroMovil.Posicion.Equals(posOriginal));
        }

        [Test]
        public void UnLopezReggaeAladoNoAtraviesaElLimiteInferiorDelMapa()
        {
            IMovible otroMovil = new LosLopezReggaeAlado(new Punto(3, 0));
            this.unMapa.AgregarPersonaje(otroMovil); 
            Punto posOriginal = otroMovil.Posicion.Clonar();
            otroMovil.Movimiento.CambiarAAbajo();
            otroMovil.Mover();//choca con el limite inferior
            Assert.IsTrue(otroMovil.Posicion.Equals(posOriginal));
        }


        /*
        public void BombitaConVelocidadNormalCambiaDePosicionEnUnaUnidadDentroDelMapaAlMoversePorPasillo()
        {
            this.movil = new Bombita(new Punto(0, 0));
            Punto posOriginal = this.movil.Posicion.Clonar();
            this.unMapa.AgregarPersonaje(this.movil); // testear todos los exceptions
            this.movil.Movimiento.CambiarADerecha();
            this.movil.Mover();
            Punto pos = new Punto(1, 0);
            Assert.IsTrue(pos.Equals(this.movil.Posicion));
        }*/

        [Test]
        public void TestCuandoExplotaUnaBombaToleToleYTieneUnCasilleroAbajoConCecilioLoDestruye()
        {
            Punto posicionBomba = new Punto(2, 3);
            Punto posicionCecilio = new Punto(2, 2);
            Cecilio cecilio=new Cecilio(posicionCecilio);

            Casilla casillaCecilio= unMapa.ObtenerCasilla(posicionCecilio);
            Casilla casillaBomba = unMapa.ObtenerCasilla(posicionBomba);

            Bomba unaBomba = new BombaToleTole(posicionBomba, 0);
            casillaCecilio.Transitar(cecilio);

            casillaBomba.PlantarExplosivo(unaBomba);
            unaBomba.Explotar();
            Assert.AreEqual(cecilio.UnidadesDeResistencia,0);

        }

        [Test]
        public void TestCuandoExplotaUnaBombaMolotovYTieneUnCasilleroAbajoConCecilioLoDestruye()
        {
            Punto posicionBomba = new Punto(2, 3);
            Punto posicionCecilio = new Punto(2, 2);
            Cecilio cecilio = new Cecilio(posicionCecilio);

            Casilla casillaCecilio = unMapa.ObtenerCasilla(posicionCecilio);
            Casilla casillaBomba = unMapa.ObtenerCasilla(posicionBomba);

            Bomba unaBomba = new BombaMolotov(posicionBomba, 0);
            casillaCecilio.Transitar(cecilio);

            casillaBomba.PlantarExplosivo(unaBomba);
            unaBomba.Explotar();
            Assert.AreEqual(cecilio.UnidadesDeResistencia, 0);

        }

        [Test]
        public void TestCuandoExplotaUnaBombaMolotovYTieneUnCasilleroAbajoLopezReggaeLoDania()
        {
            Punto posicionBomba = new Punto(2, 3);
            Punto posicionLopezReggae = new Punto(2, 2);
            LosLopezReggae lopezReggae = new LosLopezReggae(posicionLopezReggae);

            Casilla casillaLopez = unMapa.ObtenerCasilla(posicionLopezReggae);
            Casilla casillaBomba = unMapa.ObtenerCasilla(posicionBomba);

            Bomba unaBomba = new BombaMolotov(posicionBomba, 0);
            casillaLopez.Transitar(lopezReggae);
            casillaBomba.PlantarExplosivo(unaBomba);
            unaBomba.Explotar();
            Assert.AreEqual(lopezReggae.UnidadesDeResistencia, 5);

        }

        [Test]
        public void TestCuandoExplotaUnaBombaMolotovYTieneUnCasilleroAbajoLopezReggaeAladoLoDania()
        {
            Punto posicionBomba = new Punto(2, 3);
            Punto posicionAlado = new Punto(2, 2);
            LosLopezReggaeAlado alado = new LosLopezReggaeAlado(posicionAlado);

            Casilla casillaAlado = unMapa.ObtenerCasilla(posicionAlado);
            Casilla casillaBomba = unMapa.ObtenerCasilla(posicionBomba);

            Bomba unaBomba = new BombaMolotov(posicionBomba, 0);
            casillaAlado.Transitar(alado);
            casillaBomba.PlantarExplosivo(unaBomba);
            unaBomba.Explotar();
            Assert.AreEqual(alado.UnidadesDeResistencia, 0);

        }

        [Test]
        public void TestCuandoHagoExplotar2BombasAlMismoTiempoATravesDeJuego()
        {
            Punto posicionBomba1 = new Punto(0,0);
            Punto posicionBomba2 = new Punto (0,1);

            BombaMolotov bomba1 = new BombaMolotov(posicionBomba1, 0);
            BombaMolotov bomba2 = new BombaMolotov(posicionBomba2, 0);

            Casilla casillaBomba1 = Juego.Instancia().Ambiente.ObtenerCasilla(posicionBomba1);
            Casilla casillaBomba2 = Juego.Instancia().Ambiente.ObtenerCasilla(posicionBomba2);

            casillaBomba1.PlantarExplosivo(bomba1);
            casillaBomba2.PlantarExplosivo(bomba2);

            Juego.Instancia().Ambiente.CuandoPasaElTiempo();
    

            Assert.IsTrue(bomba1.EstaExplotado());
            Assert.IsTrue(bomba2.EstaExplotado());
 
        }
        
        /*METO TESTS DE EZE ACA!!!!!*/

        
        

        [Test]
        public void TestCuandoPlanto1BombaToleTole1MolotovAlMismoTiempoATravesDeJuegoYSoloExplotaLaMolotov()
        {
            Punto posicionBomba1 = new Punto(0, 0);
            Punto posicionBomba2 = new Punto(0, 1);

            BombaMolotov bomba1 = new BombaMolotov(posicionBomba1, 0);
            BombaToleTole bomba2 = new BombaToleTole(posicionBomba2, 0);

            Casilla casillaBomba1 = Juego.Instancia().Ambiente.ObtenerCasilla(posicionBomba1);
            Casilla casillaBomba2 = Juego.Instancia().Ambiente.ObtenerCasilla(posicionBomba2);

            casillaBomba1.PlantarExplosivo(bomba1);
            casillaBomba2.PlantarExplosivo(bomba2);

            Juego.Instancia().Ambiente.CuandoPasaElTiempo();

            Assert.IsTrue(bomba1.EstaExplotado());
            Assert.IsFalse(bomba2.EstaExplotado());
          }

        [Test]
        public void TestCuandoPlanto1BombaToleTole1MolotovAlMismoTiempoATravesDeJuegoYExplotanLas2()
        {
            Punto posicionBomba1 = new Punto(0, 0);
            Punto posicionBomba2 = new Punto(0, 1);

            BombaMolotov bomba1 = new BombaMolotov(posicionBomba1, 0);
            BombaToleTole bomba2 = new BombaToleTole(posicionBomba2, 0);

            Casilla casillaBomba1 = Juego.Instancia().Ambiente.ObtenerCasilla(posicionBomba1);
            Casilla casillaBomba2 = Juego.Instancia().Ambiente.ObtenerCasilla(posicionBomba2);

            casillaBomba1.PlantarExplosivo(bomba1);
            casillaBomba2.PlantarExplosivo(bomba2);

            Juego.Instancia().Ambiente.CuandoPasaElTiempo();
            Juego.Instancia().Ambiente.CuandoPasaElTiempo();
            Juego.Instancia().Ambiente.CuandoPasaElTiempo();
            Juego.Instancia().Ambiente.CuandoPasaElTiempo();
            Juego.Instancia().Ambiente.CuandoPasaElTiempo();
            Juego.Instancia().Ambiente.CuandoPasaElTiempo();

            Assert.IsTrue(bomba1.EstaExplotado());
            Assert.IsTrue(bomba2.EstaExplotado());
            Assert.AreEqual(Juego.Instancia().Ambiente.EsperaParaExplotar.Count, 0);

        }

        [Test]
        public void TestAgregadoEnLista()
        {
            Punto posicionBombita1 = new Punto(0, 0);
            Punto posicionBombita2 = new Punto(2, 0);
            BombaMolotov unaBomba = new BombaMolotov(posicionBombita1, 0);
            Bombita movil = new Bombita(posicionBombita1);

            Casilla casillaBomba1 = Juego.Instancia().Ambiente.ObtenerCasilla(posicionBombita1);



            movil.LanzarExplosivo();
            movil.Movimiento.CambiarAArriba();
            movil.Mover();
            



            Assert.AreEqual(1, Juego.Instancia().Ambiente.EsperaParaExplotar.Count);






        }       
    

        /*Empiezo a probar explosiones con articulos!*/

        public void IniciarMapaParaTestsIntegradores()
        {
            int AnchoYLargo = 5;
            
            Mapa unMapa = new Mapa(AnchoYLargo, AnchoYLargo);
            Punto unaPosicion;
            Casilla unaCasilla;
            int i, j;
            
            // Inicializo el mapa con ladrillos!!!
            for (i = 0; i < AnchoYLargo; i++)
                for (j = 0; j < AnchoYLargo; j++)
                {
                    unaPosicion = new Punto(i, j);
                    if ((i & 1) == 1 && (j & 1) == 1)
                    {
                        //ambos son numeros impares
                        unaCasilla = FabricaDeCasillas.FabricarCasillaConBloqueLadrillos(unaPosicion);
                    }
                    else
                    {
                        //uno de los dos es par
                        unaCasilla = FabricaDeCasillas.FabricarPasillo(unaPosicion);
                    }
                    unMapa.AgregarCasilla(unaCasilla);
                }

            this.unJuego.Ambiente = unMapa;
        }

        [Test]
        public void ExplotoUnObstaculoQueContieneUnaChalaYLuegoLoComeBombita()
        {
            int AnchoYLargo = 5;

            Mapa unMapa = new Mapa(AnchoYLargo, AnchoYLargo);
            Punto posInicio = new Punto(0, 0);
            Punto posFinal = new Punto(1, 1);
            Personaje unBombita = new Bombita(posInicio);

            //Inicio Mapa.
            IniciarMapaParaTestsIntegradores();

            //Agrego articulo
            Punto posicionCasillaArt = new Punto(1, 1);
            Casilla CasillaConArticulo = this.unJuego.Ambiente.ObtenerCasilla(posicionCasillaArt);
            Articulo unArticulo = new Chala();
            CasillaConArticulo.agregarArticulo(unArticulo);
            
            //Muevo a bombita para dejarlo cerca de un Bloque y explotarlo.
            this.unJuego.Ambiente.AgregarPersonaje(unBombita);
            int velocidad = unBombita.Movimiento.Velocidad;

            unBombita.Movimiento.CambiarAArriba();
            unBombita.Mover();//fue a 0,1
            unBombita.LanzarExplosivo();
            
            //Pongo a bombita lejos de la explosion
            unBombita.Mover();//fue a 0,2
            unBombita.Movimiento.CambiarADerecha();
            unBombita.Mover(); //fue a 1,2

            this.unJuego.Ambiente.CuandoPasaElTiempo();
            this.unJuego.Ambiente.CuandoPasaElTiempo();
            this.unJuego.Ambiente.CuandoPasaElTiempo();
                                    
            unBombita.Movimiento.CambiarAAbajo();
            unBombita.Mover(); //fue a 1,1

            Assert.AreEqual(2*velocidad, unBombita.Movimiento.Velocidad);
        }

        [Test]
        public void ExplotoUnObstaculoQueContieneUnArticuloBombaToleToleYLuegoLoComeBombita()
        {
            int AnchoYLargo = 5;

            Mapa unMapa = new Mapa(AnchoYLargo, AnchoYLargo);
            Punto posInicio = new Punto(0, 0);
            Punto posFinal = new Punto(1, 1);
            Personaje unBombita = new Bombita(posInicio);

            //Inicio Mapa.
            IniciarMapaParaTestsIntegradores();

            //Agrego articulo
            Punto posicionCasillaArt = new Punto(1, 1);
            Casilla CasillaConArticulo = this.unJuego.Ambiente.ObtenerCasilla(posicionCasillaArt);
            Articulo unArticulo = new ArticuloBombaToleTole();
            CasillaConArticulo.agregarArticulo(unArticulo);

            //Muevo a bombita para dejarlo cerca de un Bloque y explotarlo.
            this.unJuego.Ambiente.AgregarPersonaje(unBombita);
            int velocidad = unBombita.Movimiento.Velocidad;

            unBombita.Movimiento.CambiarAArriba();
            unBombita.Mover();//fue a 0,1
            unBombita.LanzarExplosivo();

            //Pongo a bombita lejos de la explosion
            unBombita.Mover();//fue a 0,2
            unBombita.Movimiento.CambiarADerecha();
            unBombita.Mover(); //fue a 1,2

            this.unJuego.Ambiente.CuandoPasaElTiempo();
            this.unJuego.Ambiente.CuandoPasaElTiempo();
            this.unJuego.Ambiente.CuandoPasaElTiempo();

            unBombita.Movimiento.CambiarAAbajo();
            unBombita.Mover(); //fue a 1,1

            Assert.IsInstanceOf(typeof(LanzadorToleTole), unBombita.Lanzador);
        }

        [Test]
        public void ExplotoUnObstaculoQueContieneUnTimerYLuegoLoComeBombita()
        {
            int AnchoYLargo = 5;

            Mapa unMapa = new Mapa(AnchoYLargo, AnchoYLargo);
            Punto posInicio = new Punto(0, 0);
            Punto posFinal = new Punto(1, 1);
            Personaje unBombita = new Bombita(posInicio);

            //Inicio Mapa.
            IniciarMapaParaTestsIntegradores();//Inicio con bloques de ladrillo.

            //Agrego articulo
            Punto posicionCasillaArt = new Punto(1, 1);
            Casilla CasillaConArticulo = this.unJuego.Ambiente.ObtenerCasilla(posicionCasillaArt);
            Articulo unArticulo = new Timer();
            CasillaConArticulo.agregarArticulo(unArticulo);

            //Muevo a bombita para dejarlo cerca de un Bloque y explotarlo.
            this.unJuego.Ambiente.AgregarPersonaje(unBombita);
            int velocidad = unBombita.Movimiento.Velocidad;

            unBombita.Movimiento.CambiarAArriba();
            unBombita.Mover();//fue a 0,1
            unBombita.LanzarExplosivo();

            //Pongo a bombita lejos de la explosion
            unBombita.Mover();//fue a 0,2
            unBombita.Movimiento.CambiarADerecha();
            unBombita.Mover(); //fue a 1,2

            this.unJuego.Ambiente.CuandoPasaElTiempo();
            this.unJuego.Ambiente.CuandoPasaElTiempo();
            this.unJuego.Ambiente.CuandoPasaElTiempo();

            unBombita.Movimiento.CambiarAAbajo();
            unBombita.Mover(); //fue a 1,1

            int retardo = 15;

            Assert.AreEqual(retardo, unBombita.ReduccionRetardoBombas);
        }

        [Test]
        public void AgarroUnArticuloBombaToleToleConBombitaYDestruyoUnBloqueDeAcero()
        {
            Punto posInicio = new Punto(0, 0);
            Personaje unBombita = new Bombita(posInicio);
                        
            //Agrego articulo
            
            Punto posicionCasillaArt = new Punto(1, 0);
            Casilla CasillaConArticulo = this.unJuego.Ambiente.ObtenerCasilla(posicionCasillaArt);
            Articulo unArticulo = new ArticuloBombaToleTole();
            CasillaConArticulo.ArticuloContenido = unArticulo; //Hardcodeo un articulo en el pasillo para agarrarlo con bombita.

            unJuego.Ambiente.AgregarPersonaje(unBombita);

            unBombita.Movimiento.CambiarADerecha();
            unBombita.Mover(); // 1,0, como articulo.
            unBombita.LanzarExplosivo(); // lanzo tole tole
            unBombita.Mover(); // 2,0.
            unBombita.Movimiento.CambiarAArriba();
            unBombita.Mover(); // 2,1.

            unJuego.Ambiente.CuandoPasaElTiempo();
            unJuego.Ambiente.CuandoPasaElTiempo();
            unJuego.Ambiente.CuandoPasaElTiempo();
            unJuego.Ambiente.CuandoPasaElTiempo();
            unJuego.Ambiente.CuandoPasaElTiempo(); //explota tole tole

            unBombita.Movimiento.CambiarAIzquierda();
            unBombita.Mover(); // 1,1

            Punto puntoFinal = new Punto (1,1);

            Assert.AreEqual(puntoFinal, unBombita.Posicion);

        }

        [Test]
        public void BombitaAgarraUnArticuloBombaToleToleYAniquilaACecilio()
        {
            Punto posInicio = new Punto(0, 0);
            Punto posInicioCecilio = new Punto(4, 4);
            Personaje unBombita = new Bombita(posInicio);
            Personaje unEnemigo = new Cecilio(posInicioCecilio);

            //Agrego articulo

            Punto posicionCasillaArt = new Punto(1, 0);
            Casilla CasillaConArticulo = this.unJuego.Ambiente.ObtenerCasilla(posicionCasillaArt);
            Articulo unArticulo = new ArticuloBombaToleTole();
            CasillaConArticulo.ArticuloContenido = unArticulo; //Hardcodeo un articulo en el pasillo para agarrarlo con bombita.

            unJuego.Ambiente.AgregarPersonaje(unBombita);
            unJuego.Ambiente.AgregarPersonaje(unEnemigo);

            unBombita.Movimiento.CambiarADerecha();
            unBombita.Mover(); // 1,0, como articulo.
            unBombita.Mover(); // 2,0.
            unBombita.Movimiento.CambiarAArriba();
            unBombita.Mover(); // 2,1
            unBombita.Mover(); // 2,2

            unBombita.LanzarExplosivo();
            unBombita.Mover(); // 2,3
            unBombita.Mover(); // 2,4
            unBombita.Movimiento.CambiarAIzquierda();
            unBombita.Mover(); // 1,4

            unEnemigo.Movimiento.CambiarAAbajo(); //4,4
            unEnemigo.Mover(); // 4,3
            unEnemigo.Mover(); // 4,2
            unEnemigo.Movimiento.CambiarAIzquierda();
            unEnemigo.Mover(); // 3,2

            unJuego.Ambiente.CuandoPasaElTiempo();
            unJuego.Ambiente.CuandoPasaElTiempo();
            unJuego.Ambiente.CuandoPasaElTiempo();
            unJuego.Ambiente.CuandoPasaElTiempo();
            unJuego.Ambiente.CuandoPasaElTiempo();

            Assert.IsTrue(unEnemigo.Destruido());
        }
    }

}
