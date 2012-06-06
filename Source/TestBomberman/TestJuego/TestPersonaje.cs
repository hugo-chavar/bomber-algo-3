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
    }
}

