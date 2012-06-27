using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BombermanModel.Personaje;

namespace BombermanModel.Articulo
{
    public interface IComible
    {
        void ModificarComedor(IComedor comedor);

        void Ocultar();
    }
}
