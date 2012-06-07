using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Mapa;




namespace Bomberman.Arma
{
    public class ManejadorProyectil
    {
        public const int ARRIBA = 8;
        public const int ABAJO = 2;
        public const int IZQUIERDA = 4;
        public const int DERECHA = 6;

        private Proyectil explotable;
        private int direccionPersonaje;
        private bool misilActivado;
        

        public int DireccionPersonaje
        {
            get { return this.direccionPersonaje; }
            set { this.direccionPersonaje = value; }
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
                case ABAJO:

                    explotable.PosicionFinal.X = explotable.Posicion.X;
                    explotable.PosicionFinal.Y = explotable.Posicion.Y - explotable.Alcance;

                    break;
                case ARRIBA:

                    explotable.PosicionFinal.X = explotable.Posicion.X;
                    explotable.PosicionFinal.Y = explotable.Posicion.Y + explotable.Alcance;
                    break;
                case IZQUIERDA:

                    explotable.PosicionFinal.X = explotable.Posicion.X - explotable.Alcance;
                    explotable.PosicionFinal.Y = explotable.Posicion.Y;
                    break;

                case DERECHA:

                    explotable.PosicionFinal.X = explotable.Posicion.X + explotable.Alcance;
                    explotable.PosicionFinal.Y = explotable.Posicion.Y;

                    break;



            }
            return explotable.PosicionFinal;
        }

        public void AvanzarHacia(Proyectil proyectil)
        {
            if (misilActivado)
            {
                switch (this.direccionPersonaje)
                {
                    case ABAJO:
                        proyectil.Posicion = proyectil.Posicion.PosicionInferior();
                        break;
                    case ARRIBA:
                        proyectil.Posicion= proyectil.Posicion.PosicionSuperior();
                        break;
                    case IZQUIERDA:
                        proyectil.Posicion= proyectil.Posicion.PosicionIzquierda();
                        break;
                    case DERECHA:
                        proyectil.Posicion= proyectil.Posicion.PosicionDerecha();
                        break;
                }
            }

        }

        public void LanzarMisil()
        {
            Punto puntoFinal;
            puntoFinal = this.CalcularZonaImpacto();   
            if (EstaEnRango(puntoFinal))   
            //if (puntoFinal.EsPuntoValido())
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

        public bool EstaEnRango(Punto unPunto)
        {
            return ((Juego.Juego.Instancia().Ambiente.PosicionDentroRango(unPunto))); //Hugo dice: saqué esto && (unPunto.EsPuntoValido()), meti el codigo necesario en el otro metodo Ambiente.PosicionDentroRango(unPunto)

        }

}
}
