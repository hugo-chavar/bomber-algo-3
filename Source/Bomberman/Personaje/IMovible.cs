using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.Personaje
{
    public interface IMovible : IPosicionable,IDaniable
    {
        void Mover();
        bool AtraviesaObstaculos();
    }
}
