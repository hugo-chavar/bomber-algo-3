using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.Arma
{
    public abstract class Bomba: Explosivo
    {
        protected float tiempoRestante;

        public float TiempoRestante
        {
            get { return this.tiempoRestante; }
            set { this.tiempoRestante = value; }
        }   
        
        public Bomba(Punto unaPosicion)
            :base()
        {
            posicion = unaPosicion;
        }

        public abstract override void Daniar(IDaniable daniable);
        
        public void CuandoPasaElTiempo()
        {
            this.DisminuirTiempo();
            if (0 >= this.TiempoRestante)
            {
                base.Explotar();     
            }      
        }

        public void DisminuirTiempo()
        {
            this.TiempoRestante = (this.TiempoRestante - 1);
        }
}

}
