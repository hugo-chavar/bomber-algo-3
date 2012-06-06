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
        private Casilla unaCasilla2;
        



        [TestFixtureSetUp]
        public void TestSetup()
        {
            juego = new Juego();
            posicion = new Punto(3, 4);
            Casilla unaCasilla = FabricaDeCasillas.FabricarPasillo(posicion);
            juego.Ambiente.AgregarCasilla(unaCasilla);

            //creo un mapa 5x5 con esta distribucion (P = Pasillo, * = BloqueAcero):
            //      P P P P P
            //      P * P * P
            //      P P P P P
            //      P * P * P
            //      P P P P P

            Punto unaPosicion;
            
            this.unMapa = new Mapa(ANCHOMAPA, ANCHOMAPA);

            int i, j;
            for (i = 0; i < ANCHOMAPA; i++)
                for (j = 0; j < ANCHOMAPA; j++)
                {
                    unaPosicion = new Punto(i, j);
                    if ((i & 1) == 1 && (j & 1) == 1)
                    {
                        //ambos son numeros impares
                        unaCasilla2 = FabricaDeCasillas.FabricarCasillaConBloqueLadrillos(unaPosicion);//.FabricarCasillaConBloqueAcero(unaPosicion);
                    }
                    else
                    {
                        //uno de los dos es par
                        unaCasilla2 = FabricaDeCasillas.FabricarPasillo(unaPosicion);
                    }
                    this.unMapa.AgregarCasilla(unaCasilla2);
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



        [Test]

        public void TestCuandoExplotaUnaBombaMolotovYTieneUnCasilleroASuIzquierdaConBloqueLadrilloYLoDestruye()
        {
            Punto posicion = new Punto(4,1);
            Punto puntoCasillaDaniada = new Punto(3,1);
            Bomba unaBomba = new BombaMolotov(posicion, 0);

            Casilla unaCasillaBomba = unMapa.ObtenerCasilla(posicion);
            unaCasillaBomba.PlantarExplosivo(unaBomba);

            unMapa.ManejarExplosion(unaBomba);

            Casilla unaCasillaDaniada = unMapa.ObtenerCasilla(puntoCasillaDaniada);

             Assert.IsInstanceOf(typeof (Pasillo), unaCasillaDaniada.Estado);




        }
       [Test]
        public void TestCuandoExplotaUnaBombaMolotovYTieneUnCasilleroASuDerechaConBloqueLadrilloYLoDestruye()
        {
            Punto posicion = new Punto(0, 1);
            Punto puntoCasillaDaniada = new Punto(1, 1);
            Bomba unaBomba = new BombaMolotov(posicion, 0);

            Casilla unaCasillaBomba = unMapa.ObtenerCasilla(posicion);
            unaCasillaBomba.PlantarExplosivo(unaBomba);

            unMapa.ManejarExplosion(unaBomba);

            Casilla unaCasillaDaniada = unMapa.ObtenerCasilla(puntoCasillaDaniada);

            Assert.IsInstanceOf(typeof(Pasillo), unaCasillaDaniada.Estado);




        }
        [Test]
        public void TestCuandoExplotaUnaBombaMolotovYTieneUnCasilleroArribaConBloqueLadrilloYLoDestruye()
        {
            Punto posicion = new Punto(1, 2);
            Punto puntoCasillaDaniada = new Punto(1, 3);
            Bomba unaBomba = new BombaMolotov(posicion, 0);

            Casilla unaCasillaBomba = unMapa.ObtenerCasilla(posicion);
            unaCasillaBomba.PlantarExplosivo(unaBomba);

            unMapa.ManejarExplosion(unaBomba);

            Casilla unaCasillaDaniada = unMapa.ObtenerCasilla(puntoCasillaDaniada);

            Assert.IsInstanceOf(typeof(Pasillo), unaCasillaDaniada.Estado);




        }
                [Test]
                public void TestCuandoExplotaUnaBombaMolotovYTieneUnCasilleroAbajoConBloqueLadrilloYLoDestruye()
        {
            Punto posicion = new Punto(3, 4);
            Punto puntoCasillaDaniada = new Punto(3, 3);
            Bomba unaBomba = new BombaMolotov(posicion, 0);

            Casilla unaCasillaBomba = unMapa.ObtenerCasilla(posicion);
            unaCasillaBomba.PlantarExplosivo(unaBomba);

            unMapa.ManejarExplosion(unaBomba);

            Casilla unaCasillaDaniada = unMapa.ObtenerCasilla(puntoCasillaDaniada);

            Assert.IsInstanceOf(typeof(Pasillo), unaCasillaDaniada.Estado);




        }




    }
}
