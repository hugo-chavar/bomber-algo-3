using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BombermanModel.Arma;

namespace BombermanModel.Personaje
{
    public class LosLopezReggae : Enemigo
    {
        private const int VIDALOSLOPEZREGGAE = 10;
        private const int MULTIPLICADORVELOCIDADLOSLOPEZREGGAE = 2;

        public LosLopezReggae(Punto unPunto)
            : base(unPunto)
        {
            this.movimiento.MultiplicarVelocidadPor(MULTIPLICADORVELOCIDADLOSLOPEZREGGAE);
            this.Lanzador = new LanzadorProyectil();
            this.UnidadesDeResistencia = VIDALOSLOPEZREGGAE;
            this.Nombre = Nombres.lopezReggae;
        }

    }
}
