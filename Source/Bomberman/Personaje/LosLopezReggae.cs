using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Arma;

namespace Bomberman.Personaje
{
    public class LosLopezReggae : Enemigo
    {
        private const int VIDALOSLOPEZREGGAE = 10;

        public LosLopezReggae(Punto unPunto)
            : base(unPunto)
        {
            this.movimiento.MultiplicarVelocidadPor(2);
            this.Lanzador = new LanzadorProyectil();
            this.UnidadesDeResistencia = VIDALOSLOPEZREGGAE;
        }
    }
}
