using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BombermanCommon;

namespace BombermanModel.Personaje
{
    public interface IMovible : IPosicionable //, IUpdateableObject
    {
        void Mover();
        bool AtraviesaObstaculos();
        bool ImpactaEnObstaculos();
        Movimiento Movimiento
        {
            get;
            set;
        }

        void ReaccionarConArticulo(Articulo.Articulo articulo);

        bool EsDaniable();

        void Colisionar();
    }
}
