using NUnit.Framework;
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
    class TestLanzador
    {
        private Juego unJuego;
        //private Mapa unMapa;

        //private const int ANCHOMAPA = 5;
        //private const int ALTOMAPA = 5;

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
            //Mapa unMapa = new Mapa(ANCHOMAPA, ANCHOMAPA);

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
            //        unMapa.AgregarCasilla(unaCasilla);
            //    }

            this.unJuego = Juego.Instancia();
            //unMapa = this.unJuego.Ambiente;
        }

        [Test]
        public void BombitaPlantaUnaBombaYElJuegoDetectaQueLaCasillaTieneUnExplosivo()
        {
            Personaje bombita = this.unJuego.Protagonista;
            //Bombita tiene un Lanzador de Molotov por defecto
            bombita.LanzarExplosivo();

            //Assert.IsFalse(bombita.LanzarExplosivo());
            Assert.IsTrue(this.unJuego.Ambiente.ObtenerCasilla(new Punto(0,0)).TieneUnExplosivo());
        }


        //[Test]
        //Hugo dice: el LanzarExlosivo ya no devuelve boolean, ahora se puede modificar esto para que chequee q si a Bombita tenía una bomba limitada
        //le queda una bomba menos de las que tenia
        //para eso se debe guardar en algun lado (en el Lanzador!!) las cantidades restantes
        //public void TestBombitaPlantaUnaBombaMolotovYNoPuedePlantarOtraEnElMismoLugar()
        //{
        //    Personaje bombita = this.unJuego.Protagonista;
        //    bombita.LanzarExplosivo();

        //    Assert.IsFalse(bombita.LanzarExplosivo());

        //}

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
            Juego.Instancia().AvanzarElTiempo();

            Assert.IsTrue(unCecil.Destruido());
            Assert.IsTrue(bombita.Destruido());
        }

        [Test]
        public void CuandoBombitaPlanta2MolotovDestruyendoALosLopezReggae()
        {
            Punto pBombita = new Punto(1, 0);
            Punto pLopezRaggae = new Punto(0, 0);
            
            Bombita bombita = new Bombita(pBombita);
            LosLopezReggae unLR = new LosLopezReggae(pLopezRaggae);
            Juego.Instancia().Ambiente.AgregarPersonaje(bombita);
            Juego.Instancia().Ambiente.AgregarPersonaje(unLR);

            bombita.LanzarExplosivo();
            bombita.Movimiento.CambiarADerecha();
            bombita.Mover();  // pos Bombita = (2,0)
            bombita.Movimiento.CambiarAArriba();
            bombita.Mover(); // pos Bombita = (2,1) tiene que safar de la explosion para no morir

            Juego.Instancia().AvanzarElTiempo();
            Assert.IsFalse(unLR.UnidadesDeResistencia == 0); //le quedan 5 puntos de vida
            Assert.IsFalse(bombita.Destruido()); //safo bombitaaa

            bombita.Movimiento.CambiarAAbajo();
            bombita.Mover();
            bombita.LanzarExplosivo();

            Juego.Instancia().AvanzarElTiempo();
            Assert.IsTrue(unLR.UnidadesDeResistencia == 0); //le quedan 0 puntos de vida
            Assert.IsTrue(bombita.Destruido()); //esta vez no safo
        }

        [Test]
        public void CuandoBombitaPlantaUnaMolotovDestruyendoAUnLosLopezRaggaeAlado()
        {
            Punto pBombita = new Punto(1, 0);
            Punto pLopezRaggaeAlado = new Punto(0, 0);
            
            Bombita bombita = new Bombita(pBombita);
            LosLopezReggaeAlado unLRA = new LosLopezReggaeAlado(pLopezRaggaeAlado);
            Juego.Instancia().Ambiente.AgregarPersonaje(bombita);
            Juego.Instancia().Ambiente.AgregarPersonaje(unLRA);

            bombita.LanzarExplosivo();
            bombita.Movimiento.CambiarADerecha();
            bombita.Mover();  // pos Bombita = (2,0)
            bombita.Movimiento.CambiarAArriba();
            bombita.Mover(); // pos Bombita = (2,1) tiene que safar de la explosion para no morir

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
            Juego.Instancia().AvanzarElTiempo();

            Assert.IsTrue(unCecil.Destruido());
            Assert.IsTrue(bombita.Destruido());
        }

        [Test]
        public void CuandoCecilioPlantaUnaMolotovDestruyendoABombitaYSafa()
        {
            Punto pBombita = new Punto(1, 0);
            Punto pCecil = new Punto(0, 0);

            Bombita bombita = new Bombita(pBombita);
            Cecilio unCecil = new Cecilio(pCecil);
            Juego.Instancia().Ambiente.AgregarPersonaje(bombita);
            Juego.Instancia().Ambiente.AgregarPersonaje(unCecil);

            unCecil.LanzarExplosivo();
            unCecil.Movimiento.CambiarAArriba();
            unCecil.Mover();
            unCecil.Mover();
            unCecil.Mover();
            unCecil.Mover(); 
            //escapa Cecilio antes de que explote 

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

            Juego.Instancia().AvanzarElTiempo();
            //escapa el alado

            Assert.IsFalse(lRA.Destruido());
            Assert.IsTrue(bombita.Destruido());


        }       // falta pruebas de LopezReggae haciendo lio con el proytectil

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
            bombita.CambiarLanzadorAToleTole(); // harcodeo el lanzador para ver internamente lo que ocurre al cambiar el lanzador
            bombita.LanzarExplosivo();
            Juego.Instancia().AvanzarElTiempo();
            unJuego.AvanzarElTiempo();
            unJuego.AvanzarElTiempo();
            Juego.Instancia().AvanzarElTiempo();
            Juego.Instancia().AvanzarElTiempo();
            
            Assert.IsTrue(unCecil.Destruido());
            Assert.IsTrue(bombita.Destruido());
            Assert.IsInstanceOf(typeof(Pasillo), Juego.Instancia().Ambiente.ObtenerCasilla(pBloqueAcero).Estado);
        }

        [Test]
        public void CuandoBombmitaPlantaUnaToleToleDestruyendoACecilioUbicadoDetrasDeUnBloqueDeAcero()
        {
            Punto pBombita = new Punto(1, 0);
            Punto pCecil = new Punto(1, 2);         //Cecil ubicado detras del bloque
            Punto pBloqueAcero = new Punto(1, 1);

            Bombita bombita = new Bombita(pBombita);
            Cecilio unCecil = new Cecilio(pCecil);
            Juego.Instancia().Ambiente.AgregarPersonaje(bombita);
            Juego.Instancia().Ambiente.AgregarPersonaje(unCecil);
            bombita.CambiarLanzadorAToleTole(); // harcodeo el lanzador para ver internamente lo que ocurre al cambiar el lanzador
            bombita.LanzarExplosivo();
            unJuego.AvanzarElTiempo();
            unJuego.AvanzarElTiempo();
            unJuego.AvanzarElTiempo();
            unJuego.AvanzarElTiempo();
            unJuego.AvanzarElTiempo();

            Assert.IsTrue(unCecil.Destruido());
            Assert.IsTrue(bombita.Destruido());
            Assert.IsInstanceOf(typeof(Pasillo), Juego.Instancia().Ambiente.ObtenerCasilla(pBloqueAcero).Estado);
        }

        [Test]
        public void CuandoBombmitaPlantaUnaToleToleDestruyendoATodosLosPersonajesYObstaculosDentroDeSuAlcance()
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

            bombita.CambiarLanzadorAToleTole(); // harcodeo el lanzador para ver internamente lo que ocurre al cambiar el lanzador
            bombita.LanzarExplosivo();

            unJuego.AvanzarElTiempo();
            unJuego.AvanzarElTiempo();
            unJuego.AvanzarElTiempo();
            unJuego.AvanzarElTiempo();
            unJuego.AvanzarElTiempo();

            Assert.IsTrue(unCecil.Destruido());
            Assert.IsTrue(bombita.Destruido());
            Assert.IsTrue(lRGA.Destruido());
            Assert.IsTrue(lRG.Destruido());
            Assert.IsInstanceOf(typeof(Pasillo), Juego.Instancia().Ambiente.ObtenerCasilla(pBloqueAcero).Estado);
        }


        // faltaria agregar con proyectil y toletoleee //



        /*Empiezo a probar explosiones con articulos!*/

        public void IniciarMapaParaTestsIntegradores() // Metodo que voy a llamar al principio de cada test, uso un setup distinto al original.
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

            unJuego.AvanzarElTiempo();
            unJuego.AvanzarElTiempo();
            unJuego.AvanzarElTiempo();

            unBombita.Movimiento.CambiarAAbajo();
            unBombita.Mover(); //fue a 1,1; come item.

            Assert.AreEqual(2 * velocidad, unBombita.Movimiento.Velocidad);
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

           unJuego.AvanzarElTiempo();
            unJuego.AvanzarElTiempo();
            unJuego.AvanzarElTiempo();

            unBombita.Movimiento.CambiarAAbajo();
            unBombita.Mover(); //fue a 1,1; come item.

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

            unJuego.AvanzarElTiempo();
            unJuego.AvanzarElTiempo();
            unJuego.AvanzarElTiempo();

            unBombita.Movimiento.CambiarAAbajo();
            unBombita.Mover(); //fue a 1,1; come item.

            int retardo = 15;

            Assert.AreEqual(retardo, unBombita.Lanzador.RetardoExplosion);
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

            unJuego.AvanzarElTiempo();
            unJuego.AvanzarElTiempo();
            unJuego.AvanzarElTiempo();
            unJuego.AvanzarElTiempo();
            unJuego.AvanzarElTiempo(); //explota tole tole

            unBombita.Movimiento.CambiarAIzquierda();
            unBombita.Mover(); // 1,1

            Punto puntoFinal = new Punto(1, 1);

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

            unJuego.AvanzarElTiempo();
            unJuego.AvanzarElTiempo();
            unJuego.AvanzarElTiempo();
            unJuego.AvanzarElTiempo();
            unJuego.AvanzarElTiempo();

            Assert.IsTrue(unEnemigo.Destruido());
        }

        [Test]
        public void BombitaAgarraUnArticuloBombaToleToleYAniquilaALosLopezReggae()
        {
            Punto posInicio = new Punto(0, 0);
            Punto posLR = new Punto(4, 4);
            Personaje unBombita = new Bombita(posInicio);
            Personaje unEnemigo = new LosLopezReggae(posLR);

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

            unJuego.AvanzarElTiempo();
            unJuego.AvanzarElTiempo();
            unJuego.AvanzarElTiempo();
            unJuego.AvanzarElTiempo();
            unJuego.AvanzarElTiempo();

            Assert.IsTrue(unEnemigo.Destruido());
        }

        [Test]
        public void BombitaAgarraUnArticuloBombaToleToleYAniquilaALosLopezReggaeAlado()
        {
            Punto posInicio = new Punto(0, 0);
            Punto posLRA = new Punto(4, 4);
            Personaje unBombita = new Bombita(posInicio);
            Personaje unEnemigo = new LosLopezReggaeAlado(posLRA);

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

            unJuego.AvanzarElTiempo();
            unJuego.AvanzarElTiempo();
            unJuego.AvanzarElTiempo();
            unJuego.AvanzarElTiempo();
            unJuego.AvanzarElTiempo();

            Assert.IsTrue(unEnemigo.Destruido());
        }
        
        
        
        [Test]
        public void BombitaIntentaSalirDelJuegoPeroNoPuedePorqueLeQuedaUnEnemigo()
        {
            Punto posInicio = new Punto(0, 0);
            Punto posLRA = new Punto(4, 4);
            Personaje unBombita = new Bombita(posInicio);
            Personaje unEnemigo = new LosLopezReggaeAlado(posLRA);

            IniciarMapaParaTestsIntegradores();

            //Agrego articulo
            Punto posicionCasillaArt = new Punto(1, 0);
            Casilla CasillaConArticulo = this.unJuego.Ambiente.ObtenerCasilla(posicionCasillaArt);
            Articulo unArticulo = new ArticuloBombaToleTole();
            CasillaConArticulo.ArticuloContenido = unArticulo; //Hardcodeo un articulo en el pasillo para agarrarlo con bombita.

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

            unJuego.AvanzarElTiempo();
            unJuego.AvanzarElTiempo();
            unJuego.AvanzarElTiempo();
            unJuego.AvanzarElTiempo();
            unJuego.AvanzarElTiempo();

            unBombita.Movimiento.CambiarAAbajo();
            unBombita.Mover();//1,1
            
            Assert.IsFalse(unJuego.Ambiente.NivelGanado);
            Assert.IsFalse(unJuego.Ambiente.NivelTerminado);

        }

        [Test]
        public void BombitaIntentaSalirDelJuegoYLoLograPorqueEliminaTodosLosEnemigos()
        {
            Punto posInicio = new Punto(0, 0);
            Punto posLRA = new Punto(4, 4);
            Personaje unBombita = new Bombita(posInicio);
            Personaje unEnemigo = new LosLopezReggaeAlado(posLRA);

            IniciarMapaParaTestsIntegradores();

            //Agrego articulo
            Punto posicionCasillaArt = new Punto(1, 0);
            Casilla CasillaConArticulo = this.unJuego.Ambiente.ObtenerCasilla(posicionCasillaArt);
            Articulo unArticulo = new ArticuloBombaToleTole();
            CasillaConArticulo.ArticuloContenido = unArticulo; //Hardcodeo un articulo en el pasillo para agarrarlo con bombita.

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

            unEnemigo.Movimiento.CambiarAAbajo();
            unEnemigo.Mover();//4,3
            unEnemigo.Mover();//4,2
            unEnemigo.Mover();//4,1  esta al alcance de la bomba el enemigo ahora

            unJuego.AvanzarElTiempo();
            unJuego.AvanzarElTiempo();
            unJuego.AvanzarElTiempo();
            unJuego.AvanzarElTiempo();
            unJuego.AvanzarElTiempo();

            unBombita.Movimiento.CambiarAAbajo();
            unBombita.Mover();//1,1
            
            Assert.IsTrue(unJuego.Ambiente.NivelGanado);
            Assert.IsTrue(unJuego.Ambiente.NivelTerminado);

        }
        


       
    }
}

