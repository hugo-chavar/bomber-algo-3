using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Personaje;

namespace Bomberman.Articulo
{
    public abstract class Articulo : IComible
    {

        public abstract void ModificarComedor(IComedor comedor);

    }
}
