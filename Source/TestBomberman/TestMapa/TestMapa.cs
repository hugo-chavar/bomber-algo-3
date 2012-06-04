﻿using NUnit;
using NUnit.Framework;
using Bomberman;
using Bomberman.Mapa;
using Bomberman.Mapa.Casilla;

namespace TestBomberman.TestMapa
{
    [TestFixture]
    class TestMapa
    {
        private const int ANCHOMAPA = 5;
        private const int ALTOMAPA = 5;

        private Mapa unMapa;
        private Casilla unaCasilla;
        private Punto pos;
        //private FabricaDeCasillas fabricaDeCasillas;

        /*[Test]
        public void CrearUnMapaFuncionaBien()
        { 
            unMapa = new Mapa(ANCHOMAPA, ALTOMAPA);
            Casilla[,] unTablero = new Casilla[ANCHOMAPA,ALTOMAPA];
            Assert.AreEqual(unMapa.Tablero, unTablero);
            Assert.AreEqual(unMapa.DimensionHorizontal, ANCHOMAPA);
            Assert.AreEqual(unMapa.DimensionVertical, ALTOMAPA);
        }*/

        [Test]
        public void NuevoMapaContieneCasillaAgregada()
        {
            Mapa otroMapa = new Mapa(ANCHOMAPA, ALTOMAPA);
            //Punto unaPos = new Punto(3, 2);
            pos = new Punto(4, 2);
            Casilla otraCasilla = FabricaDeCasillas.FabricarCasillaConBloqueCemento(pos);//new Casilla(pos);//
            otroMapa.agregarCasilla(otraCasilla);
            Assert.IsTrue(otroMapa.existeCasillaEnPosicion(pos));
        }

        [Test]
        public void NuevoMapaNoContieneCasillas()
        {
            unMapa = new Mapa(ANCHOMAPA, ALTOMAPA);
            Punto unaPos = new Punto(0, 0);
            Assert.IsFalse(unMapa.existeCasillaEnPosicion(unaPos));
            
        }

        /*[Test]
        public void AgregarCasillaDejaLaCasillaEnLaPosicionCorrecta()
        {
            pos = new Punto(2, 3);
            unMapa = new Mapa(ANCHOMAPA, ALTOMAPA);
           // unaFabricaDeCasillas = new FabricaDeCasillas();
            unaCasilla = unaFabricaDeCasillas.FabricarPasillo(pos);
            unMapa.agregarCasilla(unaCasilla);
            Assert.AreSame(unMapa.obtenerCasilla(pos) , unaCasilla);
        }*/
        /*
        [TestFixtureSetUp]
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
            this.unMapa = new Mapa(5,5);
            FabricaDeCasillas unaFabricaDeCasillas = new FabricaDeCasillas();

            unaPosicion = new Punto(0, 0);
            unaCasilla = unaFabricaDeCasillas.FabricarCasillaConBloqueAcero(unaPosicion);
            this.unMapa.agregarCasilla(unaCasilla);

            int i,j;
             for (i=0;i<5;i++)
                for (j = 0; j < 5; j++)
                {
                    unaPosicion = new Punto(i, j);
                    if ((i & 1) == 0 && (j & 1) == 0)
                    {
                        //ambos son numeros pares
                        unaCasilla = unaFabricaDeCasillas.FabricarCasillaConBloqueAcero(unaPosicion);
                    }
                    else
                    { 
                        //uno de los dos es impar
                        unaCasilla = unaFabricaDeCasillas.FabricarPasillo(unaPosicion);
                    }

                    this.unMapa.agregarCasilla(unaCasilla);
                }
            }*/
    }
}
