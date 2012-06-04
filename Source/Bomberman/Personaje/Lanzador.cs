using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Arma;

namespace Bomberman.Personaje
{
    public abstract class Lanzador : ILanzador
    {

        public abstract bool lanzar(int x, int y, int reduccionRetardo);
    }
}
