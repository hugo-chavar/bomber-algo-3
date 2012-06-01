using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Articulo;
using Bomberman.Arma;

namespace Bomberman.Personaje
{
    public class Bombita : Personaje , IComedor
    {
        public Bombita()
        {
            this.velocidad = 1;
        }

        public void comer(IComible comible)
        {
            comible.modificarComedor(this);
        }

        
        { 
        public void duplicarVelocidad()
        {
            this.velocidad = 2;
            set { this.velocidad = Velocidad; }
        }


        public void cambiarLanzadorAToleTole()
        {
            this.setLanzadorToleTole();
        }

        public void reducirRetardo()
        {
            // Sin implementar.
        }
    }
}
