using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Personaje;

namespace Bomberman.Articulo
{
    public class ArticuloBombaToleTole : Articulo
    {

        public override void ModificarComedor(IComedor comedor)
        {
            comedor.CambiarLanzadorAToleTole();
        }
    }
}
