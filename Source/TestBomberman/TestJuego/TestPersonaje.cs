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
            Cecilio unLR = new Cecilio(pLopezRaggae);
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



        }


    }
}

