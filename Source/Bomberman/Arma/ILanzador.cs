using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.Arma
{
    public interface ILanzador
    {
        bool Lanzar(int x, int y, int reduccionRetardo);
    }
}
