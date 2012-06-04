using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.Arma
{
    public class ManejadorProyectil
    {

        private Proyectil explotable;
        private int direccionPersonaje;

        public int DireccionPersonaje
        {
            get { return this.direccionPersonaje; }
            set { this.direccionPersonaje = DireccionPersonaje; }
        }

        public Proyectil Explotable
        {
            get { return this.explotable; }
            set { this.explotable = value; }
        }

        public ManejadorProyectil(Proyectil explotable, int direccionPersonaje)
        {
            this.Explotable = explotable;
            this.DireccionPersonaje = direccionPersonaje;
        }

        private Punto CalcularZonaImpacto(int direccionPersonaje)
        {


            // no es del todo feliz esta resolucion pero al menos por ahora lo dejo //
            switch (direccionPersonaje)
            {
                case 0:

                    explotable.PosicionFinal.X = explotable.Posicion.X;
                    explotable.PosicionFinal.PosicionSuperior(-(explotable.Alcance));
                    break;
                case 1:

                    explotable.PosicionFinal.X = explotable.Posicion.X;
                    explotable.PosicionFinal.PosicionSuperior(explotable.Alcance);
                    break;
                case 3:

                    explotable.PosicionFinal.PosicionDerecha(-(explotable.Alcance));
                    explotable.PosicionFinal.Y = explotable.Posicion.Y;
                    break;

                case 4:

                    explotable.PosicionFinal.PosicionDerecha(explotable.Alcance);
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
                    explotable.PosicionInicial.PosicionSuperior(-1);
                    break;
                case 1:
                    explotable.PosicionInicial.PosicionSuperior(1);
                    break;
                case 3:
                    explotable.PosicionInicial.PosicionDerecha(-1);
                    break;
                case 4:
                    explotable.PosicionInicial.PosicionDerecha(1);
                    break;
            }
        }

        public int LanzarMisil ()
        {
            Punto puntoFinal;
            puntoFinal = this.CalcularZonaImpacto(direccionPersonaje);   //falta chequear que los valores X e Y finales no sean negativos, pero creo que otra clase en un nivel mas arriba deberia implementar eso //
            if (explotable.PosicionFinal.X == explotable.Posicion.X)
            {
                while (explotable.Posicion.Y != explotable.PosicionFinal.Y)
                {
                    this.AvanzarHacia(direccionPersonaje);
                }
              if ((explotable.PosicionFinal.X == explotable.Posicion.X) && (explotable.PosicionFinal.Y == explotable.Posicion.Y))
                {
                    explotable.Explotar();
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
