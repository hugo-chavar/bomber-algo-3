using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Articulo;

namespace Bomberman.Personaje
{
    public interface IComedor
    {
        void Comer(IComible comible);

        void DuplicarVelocidad();
        void CambiarLanzadorAToleTole();

        void ReducirRetardo(int retardo);
    }
}
