using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Arma;

namespace Bomberman.Personaje
{
    public class Cecilio : Enemigo
    {
        private const int VIDACECILIO = 5;

        public Cecilio(Punto unPunto)
            : base(unPunto)
        {
            this.Lanzador = new LanzadorMolotov();
            this.UnidadesDeResistencia = VIDACECILIO;
        }

    }
}
