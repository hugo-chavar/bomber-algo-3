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
        protected bool exploto;

        public Bomba(int x, int y)
        {
            this.exploto = false;
            Punto unaPosicion = new Punto(x, y);
            posicion = unaPosicion;
        }

        //public float getRetardo()
        //{ return this.retardo; }

        
        public void cuandoPasaElTiempo(int t)
        {
        this.retardo = this.retardo - 1;
        if (t >= retardo)
            {
                this.explotar();     
            }      
        }

        public void explotar()
        {
            this.exploto = true; // true = EXPLOTADO , false = ACTIVADA Y NO EXPLOTADA, //
        }

        public bool estaExplotada()
        {
            return this.exploto;
        }

        public int getOndaExpansiva()
        {
            return ondaExpansiva;
        }

        //Deberia modificarse en personaje para que a partir de ese momento
        //cualquier bomba que se cree tenga ese retardo
        /* public void modificarRetardo(int porcentaje)
        {
            this.retardo = retardo * porcentaje / 100;
        }
         */ 



       
}

}
