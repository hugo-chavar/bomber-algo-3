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


        /*
         * Implemento la clase IComible, aunque con metodos vacios, 
         * porque el enemigo no puede "levantar" un articulo y comerlo,
         * pero para intentar simplificar que despues vos le pones
         * personaje.Comer(articulo) de la casilla y si es enemigo
         * no hace nada... no se si esta bien, pero se me ocurrio que 
         * asi iba a simplificar... si no esta bien alguien que me diga
         * como podriamos hacerlo!
         */

        public override void Comer(Articulo.IComible comible)
        {
            // No hace nada, no puede comer un Articulo por ser enemigo
        }

        public override void DuplicarVelocidad() 
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
