using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman;

namespace Bomberman.Arma
{
    public class Armamento: IPosicionable
    {
        protected int ondaExpansiva;
        protected Punto posicion;


        public Punto getPosicion()
        {
            return this.posicion;
        }
    }
}
