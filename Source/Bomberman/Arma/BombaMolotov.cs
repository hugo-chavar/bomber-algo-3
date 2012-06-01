using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.Arma
{
    public class BombaMolotov: Bomba
    {
        
        public override void inicializarBomba(int x, int y)
        {                     
                this.Retardo = 1;
                this.PoderDeDestruccion = 5;
                this.OndaExpansiva = 3;
                this.exploto = false;
                Punto unaPosicion = new Punto(x,y);
                Posicion = unaPosicion;

                
                     
        }


        public Punto[] calcularCasillerosExplotados (){
            int i;
            int j = 1;
            Punto[] listaDevolucion = new Punto[15];
      


            Punto unPuntoAux = new Punto(1,1);



            unPuntoAux.Y = Posicion.Y;
            for (i = -(OndaExpansiva); i < OndaExpansiva+1; i++){
            
                
             unPuntoAux.X = i + Posicion.X;
            listaDevolucion[j] = unPuntoAux;
                j = j + 1;
        }
            

            

            return listaDevolucion;
    }
       
 







            

}
    }