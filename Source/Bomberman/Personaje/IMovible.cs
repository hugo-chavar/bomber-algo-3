using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.Personaje
{
    public interface IMovible : IPosicionable
    {
        void Mover();
        bool AtraviesaObstaculos();
        Movimiento Movimiento
        {
            get;
            set;
        }

        void ReaccionarConArticulo(Articulo.Articulo articulo);

        IDaniable ObtenerDaniable();
    }
}
