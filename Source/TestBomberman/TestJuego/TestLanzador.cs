﻿using NUnit.Framework;
using BombermanModel.Juego;
using BombermanModel.Mapa;
using BombermanModel.Mapa.Casilla;
using BombermanModel.Personaje;
using BombermanModel.Arma;
using BombermanModel;
using BombermanModel.Articulo;


namespace TestBombermanModel.TestJuego
{
    [TestFixture]
    class TestLanzador
    {
        private Juego unJuego;

        [SetUp]
        public void TestSetup()
        {
            this.unJuego = Juego.Instancia();
            this.unJuego.ComenzarDesdeElPrincipio();
            this.unJuego.SeleccionarMapa();
            this.unJuego.CargarMapa();
        }

        [TearDown]
        public void TearDown()
        {
            Juego.Reiniciar();
        }

        [Test]
        public void BombitaPlantaUnaBombaYElJuegoDetectaQueLaCasillaTieneUnExplosivo()
        {
            Personaje bombita = this.unJuego.Protagonista;
            //Bombita tiene un Lanzador de Molotov por defecto
            bombita.LanzarExplosivo();
            Assert.IsTrue(this.unJuego.Ambiente.ObtenerCasilla(new Punto(0,0)).TieneUnExplosivo());
        }

        [Test]
        public void CuandoBombitaPlantaUnaMolotovDestruyendoACecilio()
        {
            Punto pBombita = new Punto(1, 0);
            Punto pCecil = new Punto(0, 0);
            
            Bombita bombita = new Bombita(pBombita);
            Cecilio unCecil = new Cecilio(pCecil);
            Juego.Instancia().Ambiente.AgregarPersonaje(bombita);
            Juego.Instancia().Ambiente.AgregarPersonaje(unCecil);

            bombita.LanzarExplosivo();
            System.Threading.Thread.Sleep(3000);
            Juego.Instancia().AvanzarElTiempo();
            Juego.Instancia().AvanzarElTiempo();
            Juego.Instancia().AvanzarElTiempo();

            Assert.IsTrue(unCecil.Destruido());
            Assert.IsTrue(bombita.Destruido());
        }

        [Test]
        public void CuandoBombitaPlanta2MolotovDestruyendoALosLopezReggae()
        {
            Punto pBombita = new Punto(5, 0);
            Punto pLopezRaggae = new Punto(6, 1);

            Bombita bombita = new Bombita(pBombita);
            LosLopezReggae unLR = new LosLopezReggae(pLopezRaggae);
            unJuego.Ambiente.AgregarPersonaje(bombita);
            unJuego.AgregarEnemigo(unLR);
            bombita.Movimiento.CambiarADerecha();
            bombita.Mover();  // pos Bombita = (6,0)
            bombita.LanzarExplosivo();
            bombita.Mover(); // 7,0 le hace la gran Jay Jay Ococha y lo deja encerrado con la bomba
            bombita.Mover(); // 8,0
            bombita.Movimiento.CambiarAArriba();
            bombita.Mover(); // pos Bombita = (8,1) tiene que safar de la explosion para no morir
            System.Threading.Thread.Sleep(3000);
            Juego.Instancia().AvanzarElTiempo();
            Juego.Instancia().AvanzarElTiempo();
            Juego.Instancia().AvanzarElTiempo();
            Assert.IsFalse(unLR.UnidadesDeResistencia == 0); //le quedan 5 puntos de vida
            Assert.IsFalse(bombita.Destruido()); //safo bombitaaa
            // como no lo mato vuelve
            bombita.Movimiento.CambiarAAbajo();
            bombita.Mover();
            bombita.Movimiento.CambiarAIzquierda();
            bombita.Mover();
            bombita.Mover();
            bombita.LanzarExplosivo();
            //no pudo escapar
            System.Threading.Thread.Sleep(3000);
            Juego.Instancia().AvanzarElTiempo();
            Juego.Instancia().AvanzarElTiempo();
            Juego.Instancia().AvanzarElTiempo();
            Assert.IsTrue(unLR.UnidadesDeResistencia == 0); //le quedan 0 puntos de vida
            Assert.IsTrue(bombita.Destruido()); //esta vez no safo
        }

        [Test]
        public void CuandoBombitaPlantaUnaMolotovDestruyendoAUnLosLopezRaggaeAlado()
        {
            Punto pBombita = new Punto(0,3);
            Punto pLopezRaggaeAlado = new Punto(0, 4);
            
            Bombita bombita = new Bombita(pBombita);
            LosLopezReggaeAlado unLRA = new LosLopezReggaeAlado(pLopezRaggaeAlado);
            Juego.Instancia().Ambiente.AgregarPersonaje(bombita);
            Juego.Instancia().Ambiente.AgregarPersonaje(unLRA);

            bombita.LanzarExplosivo();
            bombita.Movimiento.CambiarAArriba();
            bombita.Mover(); 
            bombita.Movimiento.CambiarADerecha();
            bombita.Mover();
            System.Threading.Thread.Sleep(3000);
            Juego.Instancia().AvanzarElTiempo();
            Juego.Instancia().AvanzarElTiempo();
            Juego.Instancia().AvanzarElTiempo();
            Assert.IsTrue(unLRA.UnidadesDeResistencia == 0); //chau chau Adios loslopezreggae 
            Assert.IsFalse(bombita.Destruido()); //safo bombitaaa
        }

        [Test]
        public void CuandoCecilioPlantaUnaMolotovDestruyendoABombitaYASiMismo()
        {
            Punto pBombita = new Punto(1, 0);
            Punto pCecil = new Punto(0, 0);
            
            Bombita bombita = new Bombita(pBombita);
            Cecilio unCecil = new Cecilio(pCecil);
            Juego.Instancia().Ambiente.AgregarPersonaje(bombita);
            Juego.Instancia().Ambiente.AgregarPersonaje(unCecil);

            unCecil.LanzarExplosivo();
            System.Threading.Thread.Sleep(3000);
            Juego.Instancia().AvanzarElTiempo();
            Juego.Instancia().AvanzarElTiempo();
            Juego.Instancia().AvanzarElTiempo();

            Assert.IsTrue(unCecil.Destruido());
            Assert.IsTrue(bombita.Destruido());
        }

        [Test]
        public void CuandoCecilioPlantaUnaMolotovDestruyendoABombitaYSafa()
        {
            Punto pBombita = new Punto(0, 0);
            Punto pCecil = new Punto(0, 1);

            Bombita bombita = new Bombita(pBombita);
            Cecilio unCecil = new Cecilio(pCecil);
            Juego.Instancia().Ambiente.AgregarPersonaje(bombita);
            Juego.Instancia().Ambiente.AgregarPersonaje(unCecil);

            unCecil.LanzarExplosivo();
            unCecil.Movimiento.CambiarAAbajo();
            unCecil.Mover();
            unCecil.Movimiento.CambiarADerecha();
            unCecil.Mover();
 
            //escapa Cecilio antes de que explote 
            System.Threading.Thread.Sleep(3000);
            Juego.Instancia().AvanzarElTiempo();
            Juego.Instancia().AvanzarElTiempo();
            Juego.Instancia().AvanzarElTiempo();

            Assert.IsFalse(unCecil.Destruido()); 
            Assert.IsTrue(bombita.Destruido());
        }
        
        [Test]
        public void CuandoLopezReggaePlantaUnaMolotovDestruyendoABombitaYSafa()
        {
            Punto pBombita = new Punto(1, 0);
            Punto pReggaeAlado = new Punto(0, 0);

            Bombita bombita = new Bombita(pBombita);
            LosLopezReggaeAlado lRA = new LosLopezReggaeAlado(pReggaeAlado);
            Juego.Instancia().Ambiente.AgregarPersonaje(bombita);
            Juego.Instancia().Ambiente.AgregarPersonaje(lRA);

            lRA.LanzarExplosivo();
            lRA.Movimiento.CambiarAArriba();
            lRA.Mover();
            lRA.Mover();
            lRA.Mover();
            lRA.Mover();
            System.Threading.Thread.Sleep(3000);
            Juego.Instancia().AvanzarElTiempo();
            Juego.Instancia().AvanzarElTiempo();
            Juego.Instancia().AvanzarElTiempo();
            //escapa el alado

            Assert.IsFalse(lRA.Destruido());
            Assert.IsTrue(bombita.Destruido());


        }

        [Test]
        public void CuandoBombmitaPlantaUnaToleToleDestruyendoACecilioYAlBloqueDeAcero()
        {
            Punto pBombita = new Punto(1, 0);
            Punto pCecil = new Punto(0, 0);
            Punto pBloqueAcero = new Punto (1,1);

            Bombita bombita = new Bombita(pBombita);
            Cecilio unCecil = new Cecilio(pCecil);
            Juego.Instancia().Ambiente.AgregarPersonaje(bombita);
            Juego.Instancia().Ambiente.AgregarPersonaje(unCecil);
            bombita.CambiarLanzadorAToleTole(); 
            bombita.LanzarExplosivo();
            System.Threading.Thread.Sleep(5000);//Pasan 5 segundos
            Juego.Instancia().AvanzarElTiempo();
            
            Assert.IsTrue(unCecil.Destruido());
            Assert.IsTrue(bombita.Destruido());
            Assert.IsInstanceOf(typeof(Pasillo), Juego.Instancia().Ambiente.ObtenerCasilla(pBloqueAcero).Estado);
        }

        [Test]
        public void CuandoBombmitaPlantaUnaToleToleYNoDestruyeACecilioUbicadoDetrasDeUnBloqueDeAcero()
        {
            Punto pBombita = new Punto(1, 0);
            Punto pCecil = new Punto(1, 2);         //Cecil ubicado detras del bloque
            Punto pBloqueAcero = new Punto(1, 1);

            Bombita bombita = new Bombita(pBombita);
            Cecilio unCecil = new Cecilio(pCecil);
            Juego.Instancia().Ambiente.AgregarPersonaje(bombita);
            Juego.Instancia().Ambiente.AgregarPersonaje(unCecil);
            bombita.CambiarLanzadorAToleTole(); 
            bombita.LanzarExplosivo();
            System.Threading.Thread.Sleep(5000);//Pasan 5 segundos
            unJuego.AvanzarElTiempo();

            Assert.IsFalse(unCecil.Destruido());
            Assert.IsTrue(bombita.Destruido());
            Assert.IsInstanceOf(typeof(Pasillo), Juego.Instancia().Ambiente.ObtenerCasilla(pBloqueAcero).Estado);
        }

        [Test]
        public void CuandoBombmitaPlantaUnaToleToleDestruyendoATodosLosPersonajesYObstaculosDentroDeSuAlcancePeroNoLosQueEstanAtrasDeUnBloque()
        {
            Punto pBombita = new Punto(1, 0);
            Punto pLopezReggae = new Punto(1, 2);
            Punto pLopezReggaeAlado = new Punto(2, 0);
            Punto pCecil = new Punto(1, 3);
        
            Punto pBloqueAcero = new Punto(1, 1);

            Bombita bombita = new Bombita(pBombita);
            Cecilio unCecil = new Cecilio(pCecil);
            LosLopezReggae lRG = new LosLopezReggae(pLopezReggae);
            LosLopezReggaeAlado lRGA = new LosLopezReggaeAlado(pLopezReggaeAlado);

            Juego.Instancia().Ambiente.AgregarPersonaje(bombita);
            Juego.Instancia().Ambiente.AgregarPersonaje(lRG);
            Juego.Instancia().Ambiente.AgregarPersonaje(lRGA);
            Juego.Instancia().Ambiente.AgregarPersonaje(unCecil);

            bombita.CambiarLanzadorAToleTole(); 
            bombita.LanzarExplosivo();

            System.Threading.Thread.Sleep(5000);//Pasan 5 segundos
            unJuego.AvanzarElTiempo();

            Assert.IsFalse(unCecil.Destruido());
            Assert.IsTrue(bombita.Destruido());
            Assert.IsTrue(lRGA.Destruido());
            Assert.IsFalse(lRG.Destruido());
            Assert.IsInstanceOf(typeof(Pasillo), Juego.Instancia().Ambiente.ObtenerCasilla(pBloqueAcero).Estado);
        }



        [Test]
        public void ExplotoUnObstaculoQueContieneUnaChalaYLuegoLoComeBombita()
        {
            int AnchoYLargo = 5;

            Tablero unMapa = new Tablero(AnchoYLargo, AnchoYLargo);
            Punto posInicio = new Punto(0, 0);
            Punto posFinal = new Punto(1, 1);
            Personaje unBombita = new Bombita(posInicio);

            //Pongo un BombaToleTole en el pasillo para agarrarlo con bombita y poder romper el bloque de acero donde esta la Chala
            Punto posicionCasillaArt = new Punto(0, 1);
            Casilla CasillaConArticulo = Juego.Instancia().Ambiente.ObtenerCasilla(posicionCasillaArt);
            Articulo unArticulo = new ArticuloBombaToleTole();
            CasillaConArticulo.ArticuloContenido = unArticulo;

            //Agrego articulo
            posicionCasillaArt = new Punto(1, 1);
            CasillaConArticulo = this.unJuego.Ambiente.ObtenerCasilla(posicionCasillaArt);
            unArticulo = new Chala();
            CasillaConArticulo.agregarArticulo(unArticulo);

            //Muevo a bombita para dejarlo cerca de un Bloque y explotarlo.
            this.unJuego.Ambiente.AgregarPersonaje(unBombita);
            float velocidad = unBombita.Movimiento.Velocidad;

            unBombita.Movimiento.CambiarAArriba();
            unBombita.Mover();//fue a 0,1
            unBombita.LanzarExplosivo();

            //Pongo a bombita lejos de la explosion
            unBombita.Movimiento.CambiarAAbajo();
            unBombita.Mover();//fue a 0,0
            unBombita.Movimiento.CambiarADerecha();
            unBombita.Mover(); //fue a 0,1

            System.Threading.Thread.Sleep(5000);//Pasan 5 segundos
            unJuego.AvanzarElTiempo();

            unBombita.Movimiento.CambiarAArriba();
            unBombita.Mover(); //fue a 1,1; come item.

            Assert.AreEqual(2 * velocidad, unBombita.Movimiento.Velocidad);
        }

        [Test]
        public void ExplotoUnObstaculoQueContieneUnArticuloBombaToleToleYLuegoLoComeBombita()
        {

            Punto posInicio = new Punto(14,7);
            //Posision del articulo Bomba ToleTole (14,8)
            Personaje unBombita = new Bombita(posInicio);

            //Muevo a bombita para dejarlo cerca de un Bloque y explotarlo.
            this.unJuego.Ambiente.AgregarPersonaje(unBombita);

            unBombita.LanzarExplosivo();
            //Pongo a bombita lejos de la explosion
            unBombita.Movimiento.CambiarAAbajo();
            unBombita.Mover();//fue a 14,6
            unBombita.Movimiento.CambiarADerecha();
            unBombita.Mover();//fue a 15,6
            System.Threading.Thread.Sleep(3000);
            Juego.Instancia().AvanzarElTiempo();
            Juego.Instancia().AvanzarElTiempo();
            Juego.Instancia().AvanzarElTiempo();
            unBombita.Movimiento.CambiarAIzquierda();
            unBombita.Mover(); //fue a 14,6
            unBombita.Movimiento.CambiarAArriba();
            unBombita.Mover(); //fue a 14,7
            unBombita.Mover(); //fue a 14,8 come el item

            Assert.IsInstanceOf(typeof(LanzadorToleTole), unBombita.Lanzador);
        }

        [Test]
        public void ExplotoUnObstaculoQueContieneUnTimerYLuegoLoComeBombita()
        {

            Punto posInicio = new Punto(12, 6);
            Personaje unBombita = new Bombita(posInicio);

            //Muevo a bombita para dejarlo cerca de un Bloque y explotarlo.
            this.unJuego.Ambiente.AgregarPersonaje(unBombita);
            float velocidad = unBombita.Movimiento.Velocidad;

            unBombita.Movimiento.CambiarAIzquierda();
            
            unBombita.LanzarExplosivo();

            //Pongo a bombita lejos de la explosion

            unBombita.Movimiento.CambiarADerecha();
            unBombita.Mover(); //fue a 13,6
            unBombita.Mover();//fue a 14,6
            unBombita.Movimiento.CambiarAArriba();
            unBombita.Mover();//fue a 14,7

            System.Threading.Thread.Sleep(3000);
            Juego.Instancia().AvanzarElTiempo();
            Juego.Instancia().AvanzarElTiempo();
            Juego.Instancia().AvanzarElTiempo();

            unBombita.Movimiento.CambiarAAbajo();
            unBombita.Mover(); //fue a 14,6
            unBombita.Movimiento.CambiarAIzquierda();
            unBombita.Mover(); //fue a 13,6
            unBombita.Mover();//fue a 12,6
            unBombita.Mover();// come timer en 11,6.

            Assert.AreEqual(15, unBombita.Lanzador.RetardoExplosion);
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
            CasillaConArticulo.ArticuloContenido = unArticulo; //Pongo un articulo en el pasillo para agarrarlo con bombita.

            unJuego.Ambiente.AgregarPersonaje(unBombita);

            unBombita.Movimiento.CambiarADerecha();
            unBombita.Mover(); // 1,0, como articulo.
            unBombita.LanzarExplosivo(); // lanzo tole tole
            unBombita.Movimiento.CambiarAIzquierda();
            unBombita.Mover(); // 0,0
            unBombita.Movimiento.CambiarAArriba();
            unBombita.Mover(); // 0,1.

            System.Threading.Thread.Sleep(5000);//Pasan 5 segundos
            unJuego.AvanzarElTiempo(); //explota tole tole

            unBombita.Movimiento.CambiarADerecha();
            unBombita.Mover(); // 1,1

            Punto puntoFinal = new Punto(1, 1);

            Assert.AreEqual(puntoFinal.X, unBombita.Posicion.X);
            Assert.AreEqual(puntoFinal.Y, unBombita.Posicion.Y);
        }

        [Test]
        public void BombitaAgarraUnArticuloBombaToleToleYAniquilaACecilio()
        {
            Punto posInicio = new Punto(3, 0);
            Punto posInicioCecilio = new Punto(6, 2);
            Personaje unBombita = new Bombita(posInicio);
            Personaje unEnemigo = new Cecilio(posInicioCecilio);

            //Agrego articulo
            Punto posicionCasillaArt = new Punto(4, 0);
            Casilla CasillaConArticulo = unJuego.Ambiente.ObtenerCasilla(posicionCasillaArt);
            Articulo unArticulo = new ArticuloBombaToleTole();
            CasillaConArticulo.ArticuloContenido = unArticulo; //Pongo un articulo en el pasillo para agarrarlo con bombita.

            unJuego.Ambiente.AgregarPersonaje(unBombita);
            unJuego.Ambiente.AgregarPersonaje(unEnemigo);
            unJuego.AgregarEnemigo(unEnemigo);

            unBombita.Movimiento.CambiarADerecha();
            unBombita.Mover(); // 4,0, como articulo.
            unBombita.Movimiento.CambiarAArriba();
            unBombita.Mover(); // 4,1
            unBombita.Mover(); // 4,2
            unBombita.LanzarExplosivo();
            unBombita.Movimiento.CambiarAIzquierda();
            unBombita.Mover(); // 3,2
            unBombita.Mover(); // 2,2
            unEnemigo.Movimiento.CambiarAAbajo();
            unBombita.Mover(); // 2,1 bombita se oculta
            System.Threading.Thread.Sleep(5000);//Pasan 5 segundos
            unJuego.AvanzarElTiempo();

            Assert.IsTrue(unEnemigo.Destruido());
        }

        [Test]
        public void BombitaAgarraUnArticuloBombaToleToleYAniquilaALosLopezReggae()
        {

            Punto posInicio = new Punto(0, 0);
            Punto posLR = new Punto(0, 1);
            
            Personaje unBombita = new Bombita(posInicio);
            Personaje unEnemigo = new LosLopezReggae(posLR);

            //Agrego articulo
            Punto posicionCasillaArt = new Punto(1, 0);
            Casilla CasillaConArticulo = this.unJuego.Ambiente.ObtenerCasilla(posicionCasillaArt);
            Articulo unArticulo = new ArticuloBombaToleTole();
            CasillaConArticulo.ArticuloContenido = unArticulo; ; //Pongo un articulo en el pasillo para agarrarlo con bombita.
            
            unJuego.Ambiente.AgregarPersonaje(unBombita);
            unJuego.Ambiente.AgregarPersonaje(unEnemigo);

            unBombita.Movimiento.CambiarADerecha();
            unBombita.Mover(); // 1,0, como articulo.
            Assert.IsInstanceOf(typeof(Bombita), Juego.Instancia().Ambiente.ObtenerCasilla(posicionCasillaArt).TransitandoEnCasilla[0]);
            Assert.IsInstanceOf(typeof(LanzadorToleTole), unBombita.Lanzador);

            unBombita.Movimiento.CambiarAIzquierda();
            unBombita.Mover(); // 0,0
  
            unBombita.LanzarExplosivo();
            System.Threading.Thread.Sleep(5000);//Pasan 5 segundos
            unJuego.AvanzarElTiempo();

            Assert.IsTrue(unEnemigo.Destruido());
        }

        [Test]
        public void BombitaAgarraUnArticuloBombaToleToleYAniquilaALosLopezReggaeAlado()
        {
            Punto posInicio = new Punto(0, 3);
            Punto posLRA = new Punto(4, 4);
            Personaje unBombita = new Bombita(posInicio);
            Personaje unEnemigo = new LosLopezReggaeAlado(posLRA);

            //Agrego articulo
            Punto posicionCasillaArt = new Punto(0, 4);
            Casilla CasillaConArticulo = this.unJuego.Ambiente.ObtenerCasilla(posicionCasillaArt);
            Articulo unArticulo = new ArticuloBombaToleTole();
            CasillaConArticulo.ArticuloContenido = unArticulo; //Pongo un articulo en el pasillo para agarrarlo con bombita.

            unJuego.Ambiente.AgregarPersonaje(unBombita);
            unJuego.Ambiente.AgregarPersonaje(unEnemigo);

            unBombita.Movimiento.CambiarAArriba();
            unBombita.Mover(); // 0,4, como articulo.
            unBombita.Movimiento.CambiarADerecha();
            unBombita.Mover(); // 1,4.
            unBombita.LanzarExplosivo();
            unBombita.Movimiento.CambiarAIzquierda();
            unBombita.Mover(); // 0,4
            unBombita.Movimiento.CambiarAAbajo();
            unBombita.Mover(); // 0,3

            System.Threading.Thread.Sleep(5000);//Pasan 5 segundos
            unJuego.AvanzarElTiempo();

            Assert.IsTrue(unEnemigo.Destruido());
        }
        
        
        
        [Test]
        public void BombitaIntentaSalirDelJuegoPeroNoPuedePorqueLeQuedanEnemigos()
        {
            Punto posInicio = new Punto(0, 0);
            Punto posLRA = new Punto(4, 4);
            Personaje unBombita = new Bombita(posInicio);
            Personaje unEnemigo = new LosLopezReggaeAlado(posLRA);


            //Agrego articulo
            Punto posicionCasillaArt = new Punto(1, 0);
            Casilla CasillaConArticulo = this.unJuego.Ambiente.ObtenerCasilla(posicionCasillaArt);
            Articulo unArticulo = new ArticuloBombaToleTole();
            CasillaConArticulo.ArticuloContenido = unArticulo; //Pongo un articulo en el pasillo para agarrarlo con bombita.

            Punto posSalida = new Punto(1, 1);
            Casilla casillaConSalida = this.unJuego.Ambiente.ObtenerCasilla(posSalida);

            Salida salida = new Salida();
            casillaConSalida.agregarSalida(salida);
            
            unJuego.Ambiente.AgregarPersonaje(unBombita);
            unJuego.Ambiente.AgregarPersonaje(unEnemigo);

            unBombita.Movimiento.CambiarADerecha();
            unBombita.Mover(); // 1,0, como articulo.
            unBombita.Mover(); // 2,0.
            unBombita.Movimiento.CambiarAArriba();
            unBombita.Mover(); // 2,1

            unBombita.LanzarExplosivo();
            unBombita.Mover(); // 2,2
            unBombita.Movimiento.CambiarAIzquierda();
            unBombita.Mover(); // 1,2

            unEnemigo.Movimiento.CambiarAAbajo(); //4,4

            System.Threading.Thread.Sleep(5000);//Pasan 5 segundos
            unJuego.AvanzarElTiempo();

            unBombita.Movimiento.CambiarAAbajo();
            unBombita.Mover();//1,1
            
            Assert.IsFalse(unJuego.Ambiente.NivelGanado);
            Assert.IsFalse(unJuego.Ambiente.NivelTerminado);

        }

   }
}

