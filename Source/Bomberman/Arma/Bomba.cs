using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.Arma
{
    public abstract class Bomba: Armamento
    {

        protected int poderDeDestruccion;
        protected int retardo;
        protected bool exploto;

        public virtual void inicializarBomba(int x, int y) {
        }
        
        public int getRetardo()
        { return this.retardo; }
        
        public void cuandoPasaElTiempo(int t)
        {
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

        public void modificarRetardo(int porcentaje)
        {
            this.retardo = retardo * porcentaje / 100;
        }



       
}

}
