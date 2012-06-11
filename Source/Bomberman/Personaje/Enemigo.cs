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

        public override void ReaccionarConArticulo(Articulo.Articulo articulo)
        { 
            // No hace nada porque no puede comer items.
        }
    }
}
