using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.Arma
{
    public abstract class Bomba: Explosivo
    {

        protected int poderDeDestruccion;
        protected float tiempoRestante;

        public Bomba(int x, int y)
            :base()
        {
            Punto unaPosicion = new Punto(x, y);
            posicion = unaPosicion;
        }

        public abstract override void Daniar(IDaniable daniable);
        
        public void CuandoPasaElTiempo()
        {
            this.tiempoRestante = this.tiempoRestante - 1;
            if (0 >= tiempoRestante)
            {
                base.Explotar();     
            }      
        }

        public void DisminuirTiempo()
        {
            (this.tiempoRestante)--;
        }

        public float GetTiempoRestante()
        {
            return this.tiempoRestante;
        }


            


        

        //public float getRetardo()
        //{ return this.retardo; }
       
}

}
