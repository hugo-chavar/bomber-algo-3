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

            IMovible otroMovil = new Bombita(new Punto(3, 0));
            Punto posOriginal = otroMovil.Posicion.Clonar();
            unMapa.AgregarPersonaje(otroMovil);
            otroMovil.Movimiento.CambiarAAbajo();
            otroMovil.Mover(); //choca con el limite inferior
            //Assert.IsTrue(otroMovil.Posicion.Equals(posOriginal));


            if  (otroMovil.Posicion.Equals(posOriginal))
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
