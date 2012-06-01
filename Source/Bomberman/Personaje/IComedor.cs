using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Articulo;

namespace Bomberman.Personaje
{
    public interface IComedor
    {
        void comer(IComible comible);

        void duplicarVelocidad();

        void cambiarLanzadorAToleTole();

        void reducirRetardo();
    }
}
