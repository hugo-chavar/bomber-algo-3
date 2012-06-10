using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Juego;
using Bomberman.Mapa;
using Bomberman.Mapa.Casilla;
using Bomberman.Personaje;
using Bomberman.Arma;

namespace Bomberman
{
    class Programa
    {
        static void Main(string[] args)
        {
            Juego.Juego unJuego;
            Mapa.Mapa unMapa;

            //const int ANCHOMAPA = 5;

            //creo un mapa 5x5 con esta distribucion (P = Pasillo, * = BloqueAcero):
            //      P P P P P
            //      P * P * P
            //      P P P P P
            //      P * P * P
            //      P P P P P

            //Punto unaPosicion;
            //Casilla unaCasilla;
            //Mapa.Mapa unMapa = new Mapa.Mapa(ANCHOMAPA, ANCHOMAPA);

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
            //Console.WriteLine("Dio en el blanco");

            unJuego = Juego.Juego.Instancia();
            unMapa = unJuego.Ambiente;

            Punto pBombita = new Punto(1, 0);
            Punto pCecil = new Punto(0, 0);

            Bombita bombita = new Bombita(pBombita);
            Cecilio unCecil = new Cecilio(pCecil);
            unJuego.Ambiente.AgregarPersonaje(bombita);
            unJuego.Ambiente.AgregarPersonaje(unCecil);

            bombita.LanzarExplosivo();
            unJuego.AvanzarElTiempo();


            if (unCecil.Destruido())
            {
                Console.WriteLine("Dio en el blanco");
            }
            else
            {
                Console.WriteLine("Erro");
            }
            Console.ReadLine();
        }

    }
}
