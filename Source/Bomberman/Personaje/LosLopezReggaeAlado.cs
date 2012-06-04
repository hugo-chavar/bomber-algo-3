using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Arma;

namespace Bomberman.Personaje
{
    public class LosLopezReggaeAlado : Enemigo
    {
        private const int VIDALOSLOPEZREGGAEALADO = 5;

        public LosLopezReggaeAlado(Punto unPunto)
            : base(unPunto)
        {
            this.Lanzador = new LanzadorMolotov();
            this.UnidadesDeResistencia = VIDALOSLOPEZREGGAEALADO;
        }

        public override bool AtraviesaObstaculos()
        {
            return true;
        }
    }
}
