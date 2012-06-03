using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.Personaje
{
    public interface IMovible
    {
        void Mover();
        bool AtraviesaObstaculos();
    }
}
