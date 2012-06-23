using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BombermanModel.Articulo;

namespace BombermanModel.Personaje
{
    public interface IComedor
    {
        void Comer(IComible comible);

        void DuplicarVelocidad();
        void CambiarLanzadorAToleTole();
        void ReducirRetardo(int retardo);

        void FinalizarNivel();
    }
}
