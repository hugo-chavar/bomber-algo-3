﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.Arma
{
    class ManejadorProyectil
    {

        private Proyectil explotable;
        private int direccionPersonaje;



        public ManejadorProyectil(Proyectil Explotable, int direccionPersonaje)
        {
            this.explotable = Explotable;
            this.direccionPersonaje = direccionPersonaje;
        }

        private Punto CalcularZonaImpacto(int direccionPersonaje)
        {


            // no es del todo feliz esta resolucion pero al menos por ahora lo dejo //
            switch (direccionPersonaje)
            {
                case 0:

                    explotable.PosicionFinal.X = explotable.Posicion.X;
                    explotable.PosicionFinal.Y = explotable.Posicion.Y - explotable.Alcance;
                    break;
                case 1:

                    explotable.PosicionFinal.X = explotable.Posicion.X;
                    explotable.PosicionFinal.Y = explotable.Posicion.Y + explotable.Alcance;
                    break;
                case 3:

                    explotable.PosicionFinal.X = explotable.Posicion.X - explotable.Alcance;
                    explotable.PosicionFinal.Y = explotable.Posicion.Y;
                    break;

                case 4:

                    explotable.PosicionFinal.X = explotable.Posicion.X + explotable.Alcance;
                    explotable.PosicionFinal.Y = explotable.Posicion.Y;

                    break;



            }
            return explotable.PosicionFinal;
        }

        public void AvanzarHacia(int direccion)
        {
            switch (direccion)
            {
                case 0:
                    explotable.PosicionInicial.Y = explotable.PosicionInicial.Y - 1;
                    break;
                case 1:
                    explotable.PosicionInicial.Y = explotable.PosicionInicial.Y + 1;
                    break;
                case 2:
                    explotable.PosicionInicial.X = explotable.PosicionInicial.X + 1;
                    break;
                case 3:
                    explotable.PosicionInicial.X = explotable.PosicionInicial.X - 1;
                    break;
            }
        }

        public int lanzarMisil ()
        {
            Punto puntoFinal;
            puntoFinal = this.CalcularZonaImpacto(direccionPersonaje);   //falta chequear que los valores X e Y finales no sean negativos, pero creo que otra clase en un nivel mas arriba deberia implementar eso //
            if (explotable.PosicionFinal.X == explotable.Posicion.X)
            {
                while (explotable.Posicion.Y != explotable.PosicionFinal.Y)
                {
                    this.AvanzarHacia(direccionPersonaje);
                }
            }
            if (explotable.PosicionFinal.Y == explotable.Posicion.Y)
            {
                while (explotable.Posicion.X != explotable.PosicionFinal.X)
                {
                    this.AvanzarHacia(direccionPersonaje);
                }
            }

            if ((explotable.PosicionFinal.X == explotable.Posicion.X) && (explotable.PosicionFinal.Y == explotable.Posicion.Y))
            {
                explotable.Explotar();
            }

            return 1;

        }
    }
}
