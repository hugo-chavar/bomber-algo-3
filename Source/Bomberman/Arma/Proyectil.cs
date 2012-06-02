using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.Arma
{
    public class Proyectil:Explosivo{

        

        // Buscar ENUMERADOS: ARRIBA = 1; ABAJO = 0; DERECHA = 2; IZQUIERDA = 3 //

    
        private int alcance = 3 ; // establezco arbitrariamente un alcance de 3 casilleros y explota //
        private Punto posicionFinal;

    

        public override void daniar(IDaniable daniable)
        {
            //daniable.daniarConProyectil();
        }

        public Punto calcularZonaImpacto (int direccionPersonaje)
        {
            // no es del todo feliz esta resolucion pero al menos por ahora lo dejo //
            switch(direccionPersonaje)
            {
                case 0:
                    
                   posicionFinal.X = this.Posicion.X;
                   posicionFinal.Y = this.Posicion.Y - alcance;
                   break;
                case 1:
                    
                   posicionFinal.X = this.Posicion.X;
                   posicionFinal.Y = this.Posicion.Y + alcance;
                   break;
                case 3:
                    
                   posicionFinal.X = this.Posicion.X - alcance;
                   posicionFinal.Y = this.Posicion.Y;
                   break;       
                
                case 4:
                    
                   posicionFinal.X = this.Posicion.X + alcance;
                   posicionFinal.Y = this.Posicion.Y;

                    break;

        

             }
            return posicionFinal;
        }

    }

}