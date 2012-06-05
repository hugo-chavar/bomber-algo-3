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
            this.Comer(comible); // esto es una llamada recursiva??? verificar!!!!!
        }

        public override void DuplicarVelocidad() // ver que esta clase no debe e
        {
            // No hace nada, no puede comer un Articulo por ser enemigo
        }

        public override void CambiarLanzadorAToleTole()
        {
            // No hace nada, no puede comer un Articulo por ser enemigo
        }

        public override void ReducirRetardo(int retardo)
        {
            // No hace nada, no puede comer un Articulo por ser enemigo
        }
    }
}
