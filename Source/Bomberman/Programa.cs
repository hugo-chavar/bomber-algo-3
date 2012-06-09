using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Juego;
using Bomberman.Mapa;
using Bomberman.Mapa.Casilla;
using Bomberman.Personaje;

namespace Bomberman
{
    class Programa
    {
        static void Main(string[] args)
        {
            Juego.Juego unJuego;

            const int ANCHOMAPA = 5;

            //creo un mapa 5x5 con esta distribucion (P = Pasillo, * = BloqueAcero):
            //      P P P P P
            //      P * P * P
            //      P P P P P
            //      P * P * P
            //      P P P P P

            Punto unaPosicion;
            Casilla unaCasilla;
            Mapa.Mapa unMapa = new Mapa.Mapa(ANCHOMAPA, ANCHOMAPA);

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

            unJuego = Juego.Juego.Instancia();
            unJuego.Ambiente = unMapa;

            //Personaje.Personaje bombita = unJuego.Protagonista;
            //bombita.Movimiento.CambiarADerecha();
            ////Bombita tiene un Lanzador de Molotov por defecto
            //bombita.LanzarExplosivo();
            //Punto pBombita = new Punto(1, 0);
            //Punto pCecil = new Punto(0, 0);

            //Bombita bombita = new Bombita(pBombita);
            //Cecilio unCecil = new Cecilio(pCecil);
            //unJuego.Ambiente.AgregarPersonaje(bombita);
            //unJuego.Ambiente.AgregarPersonaje(unCecil);

            //Punto pBloqueAcero = new Punto(1, 1);
            //bombita.CambiarLanzadorAToleTole(); // harcodeo el lanzador para ver internamente lo que ocurre al cambiar el lanzador
            //bombita.LanzarExplosivo();
            //unJuego.Ambiente.CuandoPasaElTiempo();
            //unJuego.Ambiente.CuandoPasaElTiempo();
            //unJuego.Ambiente.CuandoPasaElTiempo();
            //unJuego.Ambiente.CuandoPasaElTiempo();
            //unJuego.Ambiente.CuandoPasaElTiempo();

            Punto unPto = new Punto(3, 4);
            Arma.Bomba bomba = new Arma.BombaMolotov(unPto, 0);
            unJuego.Ambiente.ObtenerCasilla(unPto).PlantarExplosivo(bomba);
            bomba.CuandoPasaElTiempo();

            if (bomba.EstaExplotado())
            {
                Console.WriteLine("Destruido");
            }
            else
            {
                Console.WriteLine("No destruido");
            }
            Console.ReadLine();
        }

    }
}
