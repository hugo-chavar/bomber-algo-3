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

            Punto posInicio = new Punto(0, 0);
            Punto posInicioCecilio = new Punto(4, 4);
            Personaje.Personaje unBombita = new Bombita(posInicio);
            Personaje.Personaje unEnemigo = new Cecilio(posInicioCecilio);

            //Agrego articulo
            Punto posicionCasillaArt = new Punto(1, 0);
            Casilla CasillaConArticulo = this.unJuego.Ambiente.ObtenerCasilla(posicionCasillaArt);
            Articulo.Articulo unArticulo = new Articulo.ArticuloBombaToleTole();
            CasillaConArticulo.ArticuloContenido = unArticulo; //Pongo un articulo en el pasillo para agarrarlo con bombita.

            unJuego.Ambiente.AgregarPersonaje(unBombita);
            unJuego.Ambiente.AgregarPersonaje(unEnemigo);

            unBombita.Movimiento.CambiarADerecha();
            unBombita.Mover(); // 1,0, como articulo.
            unBombita.Mover(); // 2,0.
            unBombita.Movimiento.CambiarAArriba();
            unBombita.Mover(); // 2,1
            unBombita.Mover(); // 2,2

            unBombita.LanzarExplosivo();
            unBombita.Mover(); // 2,3
            unBombita.Mover(); // 2,4
            unBombita.Movimiento.CambiarAIzquierda();
            unBombita.Mover(); // 1,4

            unEnemigo.Movimiento.CambiarAAbajo(); //4,4
            unEnemigo.Mover(); // 4,3
            unEnemigo.Mover(); // 4,2
            unEnemigo.Movimiento.CambiarAIzquierda();
            unEnemigo.Mover(); // 3,2

            unJuego.AvanzarElTiempo();
            unJuego.AvanzarElTiempo();
            unJuego.AvanzarElTiempo();
            unJuego.AvanzarElTiempo();
            unJuego.AvanzarElTiempo();

            Assert.IsTrue(unEnemigo.Destruido());

            if (bomba1.EstaExplotado())
            {
                Console.WriteLine("bomba1.EstaExplotado()");
            }
            else
            {
                Console.WriteLine("Erro");
            }
            if (bomba2.EstaExplotado())
            {
                Console.WriteLine("bomba2.EstaExplotado()");
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
