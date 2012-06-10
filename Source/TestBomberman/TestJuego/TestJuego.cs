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
        private const int ANCHOMAPA = 17;
        private const int ALTOMAPA = 13;
        private Mapa unMapa;
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

            //Punto unaPosicion;
            //Casilla unaCasilla;
            //this.unMapa = new Mapa(ANCHOMAPA, ANCHOMAPA);

            //int i, j;
            //for (i = 0; i < ANCHOMAPA; i++)
            //    for (j = 0; j < ANCHOMAPA; j++)
            //    {
            //        unaPosicion = new Punto(i, j);
            //        if ((i & 1) == 1 && (j & 1) == 1)
            //        {
            //            //ambos son numeros impares
            //            unaCasilla = FabricaDeCasillas.FabricarCasillaConBloqueAcero(unaPosicion);
            //        }
            //        else
            //        {
            //            //uno de los dos es par
            //            unaCasilla = FabricaDeCasillas.FabricarPasillo(unaPosicion);
            //        }
            //        this.unMapa.AgregarCasilla(unaCasilla);
            //    }

            this.unJuego = Juego.Instancia();
            this.unMapa = this.unJuego.Ambiente;

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
            this.unMapa.AgregarPersonaje(this.movil);
            this.movil.Movimiento.CambiarADerecha();
            this.movil.Mover();
            Punto pos = new Punto(1, 0);
            Assert.IsTrue(pos.Equals(this.movil.Posicion));
        }

        [Test]
        public void UnMovilDespuesDeMoverseEnDireccionAUnPasilloSuPosicionNoEsIgualALaOriginal()
        {
            IMovible otroMovil = new Bombita(new Punto(0, 0));
            Punto posOriginal = otroMovil.Posicion.Clonar();
            this.unMapa.AgregarPersonaje(otroMovil);
            otroMovil.Movimiento.CambiarADerecha();
            otroMovil.Mover();
            Assert.IsFalse(posOriginal.Equals(otroMovil.Posicion));
        }

        [Test]
        public void UnMovilDespuesDeMoverseEnDireccionAUnObstaculoSuPosicionEsIgualALaOriginal()
        {
            IMovible otroMovil = new Bombita(new Punto(1, 0));
            Punto posOriginal = otroMovil.Posicion.Clonar();
            this.unMapa.AgregarPersonaje(otroMovil); 
            otroMovil.Movimiento.CambiarAArriba();
            otroMovil.Mover();
            Assert.IsTrue(posOriginal.Equals(otroMovil.Posicion));
        }

        [Test]
        public void BombitaChocaConObstaculoMoverNoCambiaSuPosicion()
        {
            IMovible otroMovil = Juego.Instancia().Protagonista;
            otroMovil.Movimiento.CambiarADerecha();
            otroMovil.Mover();
            otroMovil.Mover();
            Punto posObstaculo = new Punto(2, 0);
            Assert.IsFalse(otroMovil.Posicion.Equals(posObstaculo));
        }

        [Test]
        public void BombitaAvanzaPorTodoElMapaYNoCambiaDePosCuandoChocaConElLimiteDerechoDelMapa()
        {
            IMovible otroMovil = new Bombita(new Punto(10, 1));
            this.unMapa.AgregarPersonaje(otroMovil);
            otroMovil.Movimiento.CambiarAAbajo();
            otroMovil.Mover();//fue a 10,0
            otroMovil.Movimiento.CambiarADerecha();
            otroMovil.Mover();//fue a 11,0
            otroMovil.Mover();//fue a 12,0
            otroMovil.Mover();//fue a 13,0
            otroMovil.Mover();//fue a 14,0
            otroMovil.Mover();//fue a 15,0
            otroMovil.Mover();//fue a 16,0, esta en el limite!
            otroMovil.Mover(); //choca con el limite
            Punto posFinal = new Punto(ANCHOMAPA -1, 0);
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
            IMovible otroMovil = new Bombita(new Punto(2, ALTOMAPA-1));
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
            IMovible otroMovil = new LosLopezReggaeAlado(new Punto(ANCHOMAPA-1, 0));
            this.unMapa.AgregarPersonaje(otroMovil);
            Punto posOriginal = otroMovil.Posicion.Clonar();
            otroMovil.Movimiento.CambiarADerecha();
            otroMovil.Mover();//choca con el limite derecho
            Assert.IsTrue(otroMovil.Posicion.Equals(posOriginal));
        }

        [Test]
        public void UnLopezReggaeAladoNoAtraviesaElLimiteSuperiorDelMapa()
        {
            IMovible otroMovil = new LosLopezReggaeAlado(new Punto(4, ALTOMAPA-1));
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
        public void CuandoExplotaUnaBombaToleToleYTieneUnCasilleroAbajoConCecilioLoDestruye()
        {
            Punto posicionBomba = new Punto(2, 3);
            Punto posicionCecilio = new Punto(2, 2);
            Cecilio cecilio = new Cecilio(posicionCecilio);

            Casilla casillaCecilio = unMapa.ObtenerCasilla(posicionCecilio);
            Casilla casillaBomba = unMapa.ObtenerCasilla(posicionBomba);

            Bomba unaBomba = new BombaToleTole(posicionBomba, 0);
            casillaCecilio.Transitar(cecilio);

            casillaBomba.PlantarExplosivo(unaBomba);
            unaBomba.Explotar();
            Assert.AreEqual(cecilio.UnidadesDeResistencia, 0);
        }

        [Test]
        public void CuandoExplotaUnaBombaMolotovYTieneUnCasilleroAbajoConCecilioLoDestruye()
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
        public void CuandoExplotaUnaBombaMolotovYTieneUnCasilleroAbajoConLopezReggaeLoDania5Unidades()
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
        public void CuandoExplotaUnaBombaMolotovYTieneUnCasilleroAbajoConLopezReggaeAladoLoDestruye()
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
        public void CuandoHagoExplotar2BombasAlMismoTiempoATravesDeJuegoDebenExplotar()
        {
            Punto posicionBomba1 = new Punto(0, 0);
            Punto posicionBomba2 = new Punto(0, 1);

            BombaMolotov bomba1 = new BombaMolotov(posicionBomba1, 0);
            BombaMolotov bomba2 = new BombaMolotov(posicionBomba2, 0);

            Casilla casillaBomba1 = Juego.Instancia().Ambiente.ObtenerCasilla(posicionBomba1);
            Casilla casillaBomba2 = Juego.Instancia().Ambiente.ObtenerCasilla(posicionBomba2);

            casillaBomba1.PlantarExplosivo(bomba1);
            casillaBomba2.PlantarExplosivo(bomba2);

            Juego.Instancia().AvanzarElTiempo();

            Assert.IsTrue(bomba1.EstaExplotado());
            Assert.IsTrue(bomba2.EstaExplotado());
        }

        [Test]
        public void CuandoPlanto1BombaToleTole1MolotovAlMismoTiempoATravesDeJuegoYSoloExplotaLaMolotov()
        {
            Punto posicionBomba1 = new Punto(0, 0);
            Punto posicionBomba2 = new Punto(0, 1);

            BombaMolotov bomba1 = new BombaMolotov(posicionBomba1, 0);
            BombaToleTole bomba2 = new BombaToleTole(posicionBomba2, 0);

            Casilla casillaBomba1 = Juego.Instancia().Ambiente.ObtenerCasilla(posicionBomba1);
            Casilla casillaBomba2 = Juego.Instancia().Ambiente.ObtenerCasilla(posicionBomba2);

            casillaBomba1.PlantarExplosivo(bomba1);
            casillaBomba2.PlantarExplosivo(bomba2);

            Juego.Instancia().AvanzarElTiempo();

            Assert.IsTrue(bomba1.EstaExplotado());
            Assert.IsFalse(bomba2.EstaExplotado());
        }

        [Test]
        public void CuandoPlanto1BombaToleTole1MolotovAlMismoTiempoATravesDeJuegoYExplotanLas2()
        {
            Punto posicionBomba1 = new Punto(0, 0);
            Punto posicionBomba2 = new Punto(0, 1);

            BombaMolotov bomba1 = new BombaMolotov(posicionBomba1, 0);
            BombaToleTole bomba2 = new BombaToleTole(posicionBomba2, 0);

            Casilla casillaBomba1 = Juego.Instancia().Ambiente.ObtenerCasilla(posicionBomba1);
            Casilla casillaBomba2 = Juego.Instancia().Ambiente.ObtenerCasilla(posicionBomba2);

            casillaBomba1.PlantarExplosivo(bomba1);
            casillaBomba2.PlantarExplosivo(bomba2);

            Juego.Instancia().AvanzarElTiempo();
            Juego.Instancia().AvanzarElTiempo();
            Juego.Instancia().AvanzarElTiempo();
            Juego.Instancia().AvanzarElTiempo();
            Juego.Instancia().AvanzarElTiempo();
            Juego.Instancia().AvanzarElTiempo();

            Assert.IsTrue(bomba1.EstaExplotado());
            Assert.IsTrue(bomba2.EstaExplotado());
            Assert.AreEqual(Juego.Instancia().DependientesDelTiempo.Count, 0);
        }

        [Test]
        public void AlLanzarUnaBombaLaDebeAgregarEnListaDeLosQueEsperanParaExplotar()
        {

            Punto posicionBombita1 = new Punto(3, 0);
            int cantDependeTiempo = Juego.Instancia().DependientesDelTiempo.Count;
            //BombaMolotov unaBomba = new BombaMolotov(posicionBombita1, 0);
            Bombita movil = new Bombita(posicionBombita1);

            Casilla casillaBomba1 = Juego.Instancia().Ambiente.ObtenerCasilla(posicionBombita1);
            casillaBomba1.Transitar(movil);
            
            movil.LanzarExplosivo();
            movil.LanzarExplosivo();
            movil.LanzarExplosivo();
            movil.LanzarExplosivo();
            movil.LanzarExplosivo();

            Assert.AreEqual((cantDependeTiempo+1), Juego.Instancia().DependientesDelTiempo.Count);
        }

        [Test] 
        public void CuandoGeneroUnMapaNuevoLaCantidadDeEnemigosEs7()
        {
            Assert.AreEqual(Juego.Instancia().CantidadEnemigosVivos(), 7);
        }

        /*[Test] 
        public void CuandoGeneroUnMapaNuevoYAgregoAUnEnemigoLaCantidadDeEnemigosAumentaA8()
        {

            Punto pCecilio = new Punto(0, 1);

            Cecilio unCecilio = new Cecilio(pCecilio);

            unMapa.AgregarPersonaje(unCecilio);

            Assert.AreEqual(Juego.Instancia().CantidadEnemigosVivos(), 8);

        }*/

        [Test] 
        public void CuandoGeneroUnMapaNuevoYElimino1EnemigoConToleToleDebenQuedar6()
        {
            Punto p = new Punto(7, 0);
            Personaje bombita = new Bombita(p);
            Casilla casillaBomba1 = Juego.Instancia().Ambiente.ObtenerCasilla(p);
            casillaBomba1.Transitar(bombita);
            bombita.CambiarLanzadorAToleTole();
            bombita.Movimiento.CambiarADerecha();
            bombita.LanzarExplosivo();
            bombita.Movimiento.CambiarAIzquierda();
            bombita.Mover();
            bombita.Mover();
            bombita.Mover();
            bombita.Movimiento.CambiarAArriba();
            bombita.Mover();
            Juego.Instancia().AvanzarElTiempo();
            Juego.Instancia().AvanzarElTiempo();
            Juego.Instancia().AvanzarElTiempo();
            Juego.Instancia().AvanzarElTiempo();
            Juego.Instancia().AvanzarElTiempo();

            Assert.AreEqual(Juego.Instancia().CantidadEnemigosVivos(), 6);

        }

        //[Test]  los personajes se van a contar en la clase juego
        //public void CuandoGeneroUnMapaNuevoAgrego2PersonajesYEliminoLos2ConMolotov()
        //{
        //    this.unjuego = Juego.Instancia();
        //    unjuego.Ambiente = unMapa;

        //    Assert.AreEqual(unMapa.CantidadPersonajesVivos, 0);
        //    Punto pBombita = new Punto(0, 0);
        //    Punto pCecilio = new Punto(0, 1);

        //    Cecilio unCecilio = new Cecilio(pCecilio);
        //    Bombita unBombita = new Bombita(pBombita);

        //    unMapa.AgregarPersonaje(unCecilio);
        //    unMapa.AgregarPersonaje(unBombita);

        //    Assert.AreEqual(unMapa.CantidadPersonajesVivos, 2);

        //    unCecilio.DaniarConBombaMolotov(5);
        //    unBombita.DaniarConBombaMolotov(5);
        //    Assert.IsTrue(unCecilio.Destruido());
        //    Assert.AreEqual(unMapa.CantidadPersonajesVivos, 0);
        //}


    }
}
