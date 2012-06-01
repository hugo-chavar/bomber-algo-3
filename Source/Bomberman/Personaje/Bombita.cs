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

        public int Velocidad 
        { 
            get { return this.velocidad; }
            set { this.velocidad = Velocidad; }
        }


        public void duplicarVelocidad()
        {
            this.velocidad = this.velocidad*2;
        }
    }
}
