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
        private bool misilActivado;
        

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
            this.explotable = explotable;
            this.direccionPersonaje = direccionPersonaje;
            this.misilActivado = false;
        }

        private Punto CalcularZonaImpacto()
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

        public void AvanzarHacia(Proyectil explotable)
        {
            if (misilActivado)
            {
                switch (this.direccionPersonaje)
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

        }

        public void LanzarMisil()
        {
            Punto puntoFinal;
            puntoFinal = this.CalcularZonaImpacto();   
            if (puntoFinal.EsPuntoValido())
            {
                misilActivado = true;

             }
        }

        public void RealizarExplosion(Explosivo unExplosivo)
        {
            misilActivado = false;
            unExplosivo.Explotar();


        }

        public bool EstaLanzado()
        {
            return this.misilActivado;
        }


}
}
