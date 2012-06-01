using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.Arma
{
    public abstract class Bomba: Armamento
    {

        protected int PoderDeDestruccion;
        protected int Retardo;
        protected bool exploto;
        




        public virtual void inicializarBomba(int x, int y) {
        }
        
        public int getRetardo()
        { return this.Retardo; }
        
        public void cuandoPasaElTiempo(int t)
        {
            if (t >= Retardo)
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
            return OndaExpansiva;
        }

        public void modificarRetardo(int porcentaje)
        {
            this.Retardo = Retardo * porcentaje / 100;
        }



       
}

}
