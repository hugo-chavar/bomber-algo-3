﻿using NUnit.Framework;
using Bomberman.Juego;
using Bomberman.Mapa;
using Bomberman.Mapa.Casilla;
using Bomberman.Personaje;
using Bomberman.Articulo;
using Bomberman.Arma;
using Bomberman;
namespace TestBomberman.TestIntegracion
{
    class TestIntegracionExplosivoYPersonaje
    {
        private Juego unJuego;
        private const int ANCHOMAPA = 6;
        private const int ALTOMAPA = 6;
        private Mapa unMapa;


        [SetUp]
        public void TestSetup()
        {
            //creo un mapa 6x6 con esta distribucion (P = Pasillo, * = BloqueLadrillos):
            //      P P P P P P
            //      P * P * P *
            //      P P P P P P
            //      P * P * P *
            //      P P P P P P

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
                        unaCasilla = FabricaDeCasillas.FabricarCasillaConBloqueLadrillos(unaPosicion);
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

        [Test]
        public void TestBombitaPlantaUnaMolotovSeMueveFueraDeSuAlcanceAgarraArticuloYLuegoDeQueLaBombaExplotaVuelveYPlantaUnaToleTole()
        {
            Juego.Instancia().Ambiente.ObtenerCasilla(new Punto(1, 1)).agregarArticulo(new ArticuloBombaToleTole());
            Punto PosicionDePlantado = new Punto(1, 0);
            Bombita bombita = new Bombita(PosicionDePlantado);
            Juego.Instancia().Ambiente.ObtenerCasilla(PosicionDePlantado).Transitar(bombita);
            bombita.LanzarExplosivo();
            bombita.Movimiento.CambiarADerecha();
            bombita.Mover();
            bombita.Movimiento.CambiarAArriba();
            bombita.Mover();
            Juego.Instancia().Ambiente.CuandoPasaElTiempo();
            bombita.Movimiento.CambiarAIzquierda();
            bombita.Mover();
            bombita.Movimiento.CambiarAAbajo();
            bombita.Mover();
            bombita.LanzarExplosivo();
            Assert.IsInstanceOf(typeof(BombaToleTole), Juego.Instancia().Ambiente.ObtenerCasilla(PosicionDePlantado).Explosivo);
            Assert.IsFalse(bombita.Destruido());
        }

        [Test]
        public void TestLopezReggaeAladoPlantaUnaMolotovSeMueveFueraDeSuAlcanceYLuegoDeQueLaBombaExplotaVuelveYPlantaOtra()
        {
            Punto PosicionDePlantado = new Punto(0, 1);
            LosLopezReggaeAlado personaje = new LosLopezReggaeAlado(PosicionDePlantado);
            Juego.Instancia().Ambiente.ObtenerCasilla(PosicionDePlantado).Transitar(personaje);
            personaje.LanzarExplosivo();
            personaje.Movimiento.CambiarADerecha();
            personaje.Mover();
            personaje.Mover();
            personaje.Mover();
            personaje.Mover();
            Juego.Instancia().Ambiente.CuandoPasaElTiempo();
            personaje.Movimiento.CambiarAIzquierda();
            personaje.Mover();
            personaje.Mover();
            personaje.Mover();
            personaje.Mover();
            personaje.LanzarExplosivo();
            Assert.IsInstanceOf(typeof(BombaMolotov), Juego.Instancia().Ambiente.ObtenerCasilla(PosicionDePlantado).Explosivo);
            Assert.IsFalse(personaje.Destruido());
        }

       [Test]
       public void TestLopezReggaeLanzaUnProyectilSinObtaculosEnFrenteYElProyectilDebeExplotarAlpasar3TiemposYDaniarElObstaculoDeArriba()
       {
           Punto PosicionDePartida= new Punto(0,0);
           LosLopezReggae personaje = new LosLopezReggae(PosicionDePartida);
           Juego.Instancia().Ambiente.ObtenerCasilla(PosicionDePartida).Transitar(personaje);
           personaje.Movimiento.CambiarADerecha();
           personaje.LanzarExplosivo();
           Juego.Instancia().Ambiente.CuandoPasaElTiempo();
           Juego.Instancia().Ambiente.CuandoPasaElTiempo();
           Juego.Instancia().Ambiente.CuandoPasaElTiempo();
           Assert.AreEqual(4, Juego.Instancia().Ambiente.ObtenerCasilla(new Punto(3,1)).Estado.UnidadesDeResistencia);
       }
        




    }
}
