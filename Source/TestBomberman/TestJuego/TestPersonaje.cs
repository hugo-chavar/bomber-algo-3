using NUnit.Framework;
using Bomberman.Juego;
using Bomberman.Mapa;
using Bomberman.Mapa.Casilla;
using Bomberman.Personaje;
using Bomberman.Arma;
using Bomberman;


namespace TestBomberman.TestJuego
{
    [TestFixture]
    class TestPersonaje
    {
        private Juego unJuego;

        private const int ANCHOMAPA = 5;
        private const int ALTOMAPA = 5;

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
            Mapa unMapa = new Mapa(ANCHOMAPA, ANCHOMAPA);

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
                    unMapa.AgregarCasilla(unaCasilla);
                }

            this.unJuego = Juego.Instancia();
            this.unJuego.Ambiente = unMapa;
        }

        [Test]
        public void TestBombitaPlantaUnaBombaMolotovYNoPuedePlantarOtraEnElMismoLugar()
        {
            Personaje bombita = this.unJuego.Protagonista;
            bombita.LanzarExplosivo();

            Assert.IsFalse(bombita.LanzarExplosivo());
        }

        [Test]
        public void TestCuandoBombmitaPlantaUnaMolotovDestruyendoACecilio()
        {
            Punto pBombita = new Punto(1, 0);
            Punto pCecil = new Punto(0, 0);
            
            Bombita bombita = new Bombita(pBombita);
            Cecilio unCecil = new Cecilio(pCecil);
            Juego.Instancia().Ambiente.AgregarPersonaje(bombita);
            Juego.Instancia().Ambiente.AgregarPersonaje(unCecil);

            bombita.LanzarExplosivo();
            Juego.Instancia().Ambiente.CuandoPasaElTiempo();

            Assert.IsTrue(unCecil.Destruido());
            Assert.IsTrue(bombita.Destruido());
        }

        [Test]
        public void TestCuandoBombmitaPlanta2MolotovDestruyendoALosLopezRaggae()
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

            Juego.Instancia().Ambiente.CuandoPasaElTiempo();
            Assert.IsFalse(unLR.UnidadesDeResistencia == 0); //le quedan 5 puntos de vida
            Assert.IsFalse(bombita.Destruido()); //safo bombitaaa

            bombita.Movimiento.CambiarAAbajo();
            bombita.Mover();
            bombita.LanzarExplosivo();
            
            Juego.Instancia().Ambiente.CuandoPasaElTiempo();
            Assert.IsTrue(unLR.UnidadesDeResistencia == 0); //le quedan 0 puntos de vida
            Assert.IsTrue(bombita.Destruido()); //esta vez no safo
        }

        [Test]
        public void TestCuandoBombmitaPlantaUnaMolotovDestruyendoAUnLosLopezRaggaeAlado()
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

            Juego.Instancia().Ambiente.CuandoPasaElTiempo();
            Assert.IsTrue(unLRA.UnidadesDeResistencia == 0); //chau chau Adios loslopezreggae 
            Assert.IsFalse(bombita.Destruido()); //safo bombitaaa
        }

        [Test]
        public void TestCuandoCecilioPlantaUnaMolotovDestruyendoABombitaYASiMismo()
        {
            Punto pBombita = new Punto(1, 0);
            Punto pCecil = new Punto(0, 0);
            
            Bombita bombita = new Bombita(pBombita);
            Cecilio unCecil = new Cecilio(pCecil);
            Juego.Instancia().Ambiente.AgregarPersonaje(bombita);
            Juego.Instancia().Ambiente.AgregarPersonaje(unCecil);

            unCecil.LanzarExplosivo();
            Juego.Instancia().Ambiente.CuandoPasaElTiempo();

            Assert.IsTrue(unCecil.Destruido());
            Assert.IsTrue(bombita.Destruido());
        }

        [Test]
        public void TestCuandoCecilioPlantaUnaMolotovDestruyendoABombitaYSafa()
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

            Juego.Instancia().Ambiente.CuandoPasaElTiempo();

            Assert.IsFalse(unCecil.Destruido()); 
            Assert.IsTrue(bombita.Destruido());
        }
        
        [Test]
        public void TestCuandoLopezReggaePlantaUnaMolotovDestruyendoABombitaYSafa()
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
 
            Juego.Instancia().Ambiente.CuandoPasaElTiempo();
            //escapa el alado

            Assert.IsFalse(lRA.Destruido());
            Assert.IsTrue(bombita.Destruido());


        }       // falta pruebas de LopezReggae haciendo lio con el proytectil

        [Test]
        public void TestCuandoBombmitaPlantaUnaToleToleDestruyendoACecilioYAlBloqueDeAcero()
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
            Juego.Instancia().Ambiente.CuandoPasaElTiempo();
            Juego.Instancia().Ambiente.CuandoPasaElTiempo();
            Juego.Instancia().Ambiente.CuandoPasaElTiempo();
            Juego.Instancia().Ambiente.CuandoPasaElTiempo();
            Juego.Instancia().Ambiente.CuandoPasaElTiempo();
            
            Assert.IsTrue(unCecil.Destruido());
            Assert.IsTrue(bombita.Destruido());
            Assert.IsInstanceOf(typeof(Pasillo), Juego.Instancia().Ambiente.ObtenerCasilla(pBloqueAcero).Estado);
        }

        [Test]
        public void TestCuandoBombmitaPlantaUnaToleToleDestruyendoACecilioUbicadoDetrasDeUnBloqueDeAcero()
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
            Juego.Instancia().Ambiente.CuandoPasaElTiempo();
            Juego.Instancia().Ambiente.CuandoPasaElTiempo();
            Juego.Instancia().Ambiente.CuandoPasaElTiempo();
            Juego.Instancia().Ambiente.CuandoPasaElTiempo();
            Juego.Instancia().Ambiente.CuandoPasaElTiempo();

            Assert.IsTrue(unCecil.Destruido());
            Assert.IsTrue(bombita.Destruido());
            Assert.IsInstanceOf(typeof(Pasillo), Juego.Instancia().Ambiente.ObtenerCasilla(pBloqueAcero).Estado);
        }

        [Test]
        public void TestCuandoBombmitaPlantaUnaToleToleDestruyendoATodosLosPersonajesYObstaculosDentroDeSuAlcance()
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

            Juego.Instancia().Ambiente.CuandoPasaElTiempo();
            Juego.Instancia().Ambiente.CuandoPasaElTiempo();
            Juego.Instancia().Ambiente.CuandoPasaElTiempo();
            Juego.Instancia().Ambiente.CuandoPasaElTiempo();
            Juego.Instancia().Ambiente.CuandoPasaElTiempo();

            Assert.IsTrue(unCecil.Destruido());
            Assert.IsTrue(bombita.Destruido());
            Assert.IsTrue(lRGA.Destruido());
            Assert.IsTrue(lRG.Destruido());
            Assert.IsInstanceOf(typeof(Pasillo), Juego.Instancia().Ambiente.ObtenerCasilla(pBloqueAcero).Estado);
        }

        








        // faltaria agregar con proyectil y toletoleee //
    }
}

