using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.Arma
{
    public class BombaMolotov: Bomba
    {
        private const int TIEMPOEXPLOSION = 1;

        public BombaMolotov(int x, int y, int porcentajeRetardo)
            :base(x,y)
        {
            this.retardo = TIEMPOEXPLOSION * (100 - porcentajeRetardo) / 100;
            this.poderDeDestruccion = 5;
            this.ondaExpansiva = 3;
            this.tiempoRestante = retardo;
        }

        public override void daniar(IDaniable daniable)
        {
            daniable.daniarConBombaMolotov();
        }

        
        /*public override void inicializarBomba(int x, int y)
        {                     
                this.retardo = 1;
                this.poderDeDestruccion = 5;
                this.ondaExpansiva = 3;
                this.exploto = false;
                Punto unaPosicion = new Punto(x,y);
                posicion = unaPosicion;
               
        }
*/

        //  lo llevo a el manejador
/*       public Punto[] calcularCasillerosExplotados (){
             int i;
             int j = 1;
             Punto[] listaDevolucion = new Punto[15];
      


             Punto unPuntoAux = new Punto(1,1);



             unPuntoAux.Y = posicion.Y;
             for (i = -(ondaExpansiva); i < ondaExpansiva+1; i++){
            
                
              unPuntoAux.X = i + posicion.X;
             listaDevolucion[j] = unPuntoAux;
                 j = j + 1;
         }
            

            

             return listaDevolucion;
     }
 */
    }
}