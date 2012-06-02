using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.Arma
{
    public abstract class Bomba: Explosivo
    {

        protected int poderDeDestruccion;
        protected float retardo;

        public Bomba(int x, int y)
            :base()
        {
            Punto unaPosicion = new Punto(x, y);
            posicion = unaPosicion;
        }

        public abstract override void daniar(IDaniable daniable);
        
        public void cuandoPasaElTiempo(int t)
        {
        this.retardo = this.retardo - 1;
        if (t >= retardo)
            {
                base.explotar();     
            }      
        }

        //public float getRetardo()
        //{ return this.retardo; }
       
}

}
