using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.Personaje
{
    public interface IMovible : IPosicionable,IDaniable,IComedor
    {
        void Mover();
        bool AtraviesaObstaculos();
        Movimiento Movimiento
        {
            get;
            set;
        }
    }
}
