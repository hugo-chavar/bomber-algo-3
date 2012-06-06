using Bomberman.Arma;
using Bomberman.Mapa;
using Bomberman.Juego;
using NUnit.Framework;
using Bomberman.Mapa.Casilla;
using Bomberman;



namespace TestBomberman.TestArma
{
    [TestFixture]
    class TestBombaMolotov
    {
        private Punto posicion;
        private Juego juego;
        private Mapa unMapa;
        private const int ANCHOMAPA = 5;
        private BombaMolotov unaBomba;



        [TestFixtureSetUp]
        public void TestSetup()
        {
            juego = new Juego();
            posicion = new Punto(3, 4);
            Casilla unaCasilla = FabricaDeCasillas.FabricarPasillo(posicion);
            juego.Ambiente.AgregarCasilla(unaCasilla);
        }

        public void TestSetupMapa()
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
        public void TestBombaMolotovEstaExplotadaAlSerPlantadaDebeDevolverFalse()
        {
            Bomba bomba = new BombaMolotov(posicion, 0);
            Assert.AreEqual(bomba.EstaExplotado(), false);
        }

        [Test]
        public void TestBombaMolotovEstaExplotadaAlPasarUnTiempoDebeDevolverTrue()
        {
            Bomba bomba = new BombaMolotov(posicion, 0);
            juego.Ambiente.ObtenerCasilla(posicion).PlantarExplosivo(bomba);
            bomba.CuandoPasaElTiempo();
            Assert.AreEqual(bomba.EstaExplotado(), true);
        }

        [Test]
        public void TestDaniarObstaculoConBombaMolotov()
        {
            Bomba bomba = new BombaMolotov(posicion, 0);
            Obstaculo obstaculo = BloqueComun.CrearBloqueCemento();
            bomba.Daniar(obstaculo);
            Assert.AreEqual(obstaculo.UnidadesDeResistencia, 5);
        }

///////////////////// Integradores  ///////////////////////////

        [Test]

        public void TestCuandoExplotaUnaBombaMolotovYContieneASuIzquierdaUnBloqueLadrilloYLoDestruye()
        {
            Punto posicion = new Punto(4,1);
            Punto puntoCasillaDaniada = new Punto(3,1);
            BombaMolotov unaBomba = new BombaMolotov(posicion,0);

            Casilla unaCasillaBomba = unMapa.ObtenerCasilla(posicion);
            unaCasillaBomba.PlantarExplosivo(unaBomba);

            unaBomba.CuandoPasaElTiempo();
            unaBomba.CuandoPasaElTiempo();
            unaBomba.CuandoPasaElTiempo();

            Casilla unaCasillaDaniada = unMapa.ObtenerCasilla(puntoCasillaDaniada);

             Assert.IsInstanceOf(typeof (Pasillo), unaCasillaDaniada.Estado);




        }



    }
}
