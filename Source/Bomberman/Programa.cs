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

            Punto PosicionDePartida = new Punto(0, 0);
            LosLopezReggae personaje = new LosLopezReggae(PosicionDePartida);
            Casilla casillaAux = unJuego.Ambiente.ObtenerCasilla(new Punto(3, 0));
            casillaAux.Estado = BloqueComun.CrearBloqueLadrillos();
            unJuego.Ambiente.ObtenerCasilla(PosicionDePartida).Transitar(personaje);
            personaje.Movimiento.CambiarADerecha();
            personaje.LanzarExplosivo();
            unJuego.Ambiente.CuandoPasaElTiempo();
            unJuego.Ambiente.CuandoPasaElTiempo();
            unJuego.Ambiente.CuandoPasaElTiempo();
            unJuego.Ambiente.CuandoPasaElTiempo();

            //Punto unPto = new Punto(2, 2);
            //Bomba bomba = new BombaMolotov(unPto, 0);
            //Casilla otCasilla = unMapa.ObtenerCasilla(unPto);

            //otCasilla.PlantarExplosivo(bomba);
            //bomba.CuandoPasaElTiempo();
            //(bomba.EstaExplotado())//

            if  ((unJuego.Ambiente.ObtenerCasilla(new Punto(3, 0)).Estado.UnidadesDeResistencia) == 4)
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
