using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Articulo;

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


        public void duplicarVelocidad()
        {
            this.velocidad = this.velocidad*2;
        }



        public void cambiarLanzadorAToleTole()
        {
            // Sin implementar.
        }

        public void reducirRetardo()
        {
            // Sin implementar.
        }

    }
}
