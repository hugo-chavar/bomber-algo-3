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

            Punto pBombita = new Punto(5, 0);
            Punto pLopezRaggae = new Punto(6, 1);

            Bombita bombita = new Bombita(pBombita);
            LosLopezReggae unLR = new LosLopezReggae(pLopezRaggae);
            unJuego.Ambiente.AgregarPersonaje(bombita);
            unJuego.AgregarEnemigo(unLR);
            bombita.Movimiento.CambiarADerecha();
            bombita.Mover();  // pos Bombita = (6,0)
            bombita.LanzarExplosivo();
            bombita.Mover(); // 7,0 le hace la gran Jay Jay Ococha y lo deja encerrado con la bomba
            bombita.Mover(); // 8,0
            bombita.Movimiento.CambiarAArriba();
            bombita.Mover(); // pos Bombita = (8,1) tiene que safar de la explosion para no morir

            unJuego.AvanzarElTiempo();

            if (unLR.UnidadesDeResistencia == 5)
            {
                Console.WriteLine("le quedan 5 puntos de vida");
            }
            else
            {
                Console.WriteLine("Erro");
            }


            if (!bombita.Destruido())
            {
                Console.WriteLine("safo bombitaaa");
            }
            else
            {
                Console.WriteLine("Erro");
            }
            // como no lo mato vuelve
            bombita.Movimiento.CambiarAAbajo();
            bombita.Mover();
            bombita.Movimiento.CambiarAIzquierda();
            bombita.Mover();
            bombita.Mover();
            bombita.LanzarExplosivo();
            //no pudo escapar
            unJuego.AvanzarElTiempo();
            if (unLR.UnidadesDeResistencia == 0)
            {
                Console.WriteLine("le quedan 0 puntos de vida");
            }
            else
            {
                Console.WriteLine("Erro");
            }
            if (bombita.Destruido())
            {
                Console.WriteLine("muere bombita");
            }
            else
            {
                Console.WriteLine("Erro");
            }



            //Punto p = new Punto(2, 2);
            //Personaje.Personaje bombita = new Bombita(p);
            //unJuego.Ambiente.AgregarPersonaje(bombita);
            //if (unJuego.CantidadEnemigosVivos() == 7)
            //{
            //    Console.WriteLine("hasta ahora todo ok");
            //}
            //else
            //{
            //    Console.WriteLine("Erro");
            //}
            //bombita.CambiarLanzadorAToleTole();
            //if (unJuego.CantidadEnemigosVivos() == 7)
            //{
            //    Console.WriteLine("hasta ahora todo ok");
            //}
            //else
            //{
            //    Console.WriteLine("Erro");
            //}
            //bombita.LanzarExplosivo();
            //if (unJuego.CantidadEnemigosVivos() == 7)
            //{
            //    Console.WriteLine("hasta ahora todo ok");
            //}
            //else
            //{
            //    Console.WriteLine("Erro");
            //}
            //bombita.Movimiento.CambiarADerecha();
            //bombita.Mover();
            //if (unJuego.CantidadEnemigosVivos() == 7)
            //{
            //    Console.WriteLine("hasta ahora todo ok");
            //}
            //else
            //{
            //    Console.WriteLine("Erro");
            //}
            //bombita.Mover();
            //bombita.Movimiento.CambiarAAbajo();
            //bombita.Mover();
            //unJuego.AvanzarElTiempo();
            //if (unJuego.CantidadEnemigosVivos() == 7)
            //{
            //    Console.WriteLine("hasta ahora todo ok");
            //}
            //else
            //{
            //    Console.WriteLine("Erro");
            //}
            //unJuego.AvanzarElTiempo();
            //if (unJuego.CantidadEnemigosVivos() == 7)
            //{
            //    Console.WriteLine("hasta ahora todo ok");
            //}
            //else
            //{
            //    Console.WriteLine("Erro");
            //}
            //unJuego.AvanzarElTiempo();
            //if (unJuego.CantidadEnemigosVivos() == 7)
            //{
            //    Console.WriteLine("hasta ahora todo ok");
            //}
            //else
            //{
            //    Console.WriteLine("Erro");
            //}
            //unJuego.AvanzarElTiempo();
            //if (unJuego.CantidadEnemigosVivos() == 7)
            //{
            //    Console.WriteLine("hasta ahora todo ok");
            //}
            //else
            //{
            //    Console.WriteLine("Erro");
            //}
            //unJuego.AvanzarElTiempo();


            //if (unJuego.CantidadEnemigosVivos() == 6)
            //{
            //    Console.WriteLine("todo ok");
            //}
            //else
            //{
            //    Console.WriteLine("Erro");
            //}
            //Console.ReadLine();
        }

    }
}
