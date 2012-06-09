using NUnit.Framework;
using Bomberman.Juego;
using Bomberman.Mapa;
using Bomberman.Mapa.Casilla;
using Bomberman.Personaje;
using Bomberman.Arma;
using Bomberman;
namespace TestBomberman.TestIntegracion
{
    class TestIntegracionExplosivoYPersonaje
    {
        private Juego unJuego;
        private const int ANCHOMAPA = 5;
        private const int ALTOMAPA = 5;
        private Mapa unMapa;


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
        public void TestBombitaPlantaUnaMolotovSeMueveFueraDeSuAlcanceYNoEsDaniadoPorLaBomba()
        {
            Punto PosicionDePlantado = new Punto(0, 0);
            Bombita bombita = new Bombita(PosicionDePlantado);
            Juego.Instancia().Ambiente.ObtenerCasilla(PosicionDePlantado).Transitar(bombita);
            bombita.LanzarExplosivo();
            bombita.Movimiento.CambiarADerecha();
            bombita.Mover();
            bombita.Mover();
            bombita.Mover();
            bombita.Mover();
            Juego.Instancia().Ambiente.CuandoPasaElTiempo();
            Assert.IsFalse(bombita.Destruido());
        }


        [Test]
        public void TestBombitaPlantaUnaMolotovSeMueveFueraDeSuAlcanceYLuegoDeQueLaBombaExplotaVuelveYPlantaOtraMolotov()
        {
            Punto PosicionDePlantado=new Punto(0,0);
            Bombita bombita = new Bombita(PosicionDePlantado);
            Juego.Instancia().Ambiente.ObtenerCasilla(PosicionDePlantado).Transitar(bombita);
            bombita.LanzarExplosivo();
            bombita.Movimiento.CambiarADerecha();
            bombita.Mover();
            bombita.Mover();
            bombita.Mover();
            bombita.Mover();
            Juego.Instancia().Ambiente.CuandoPasaElTiempo();
            bombita.Movimiento.CambiarAIzquierda();
            bombita.Mover();
            bombita.Mover();
            bombita.Mover();
            bombita.Mover();
            bombita.LanzarExplosivo();
            Assert.IsInstanceOf( typeof(BombaMolotov),Juego.Instancia().Ambiente.ObtenerCasilla(PosicionDePlantado).Explosivo);
            Assert.IsFalse(bombita.Destruido());
        }

        /*
        [Test]
        public void TestLopezReggaeLanzaUnProyectilSinObtaculosEnFrenteYElProyectilDebeExplotarAlpasar3Tiempos()
        {
            Punto PosicionDePartida= new Punto(0, 0);
            Punto PosicionDeLlegada= new Punto(3, 0);
            LosLopezReggae personaje = new LosLopezReggae(PosicionDePartida);
            Juego.Instancia().Ambiente.ObtenerCasilla(PosicionDePartida).Transitar(personaje);
            personaje.Movimiento.CambiarADerecha();
            personaje.LanzarExplosivo();
            Juego.Instancia().Ambiente.CuandoPasaElTiempo();
            Juego.Instancia().Ambiente.CuandoPasaElTiempo();
            Juego.Instancia().Ambiente.CuandoPasaElTiempo();
            Assert.AreEqual(typeof(Proyectil), Juego.Instancia().Ambiente.ObtenerCasilla(PosicionDeLlegada).explosivo);
        
        }
         */


    }
}
