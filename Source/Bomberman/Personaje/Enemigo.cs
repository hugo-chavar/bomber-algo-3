using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.Personaje
{
    public abstract class Enemigo : Personaje
    {
        public Enemigo(Punto unPunto)
            : base(unPunto)
        { 
            // Agregar metodos de construccion del enemigo.
        }

        public override void Comer(Articulo.IComible comible)
        {
            //Al intentar comer un articulo, este no hace nada porque no puede comer articulos!
        }

    }
}
