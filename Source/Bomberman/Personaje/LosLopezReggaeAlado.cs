﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BombermanModel.Arma;

namespace BombermanModel.Personaje
{
    public class LosLopezReggaeAlado : Enemigo
    {
        private const int VIDALOSLOPEZREGGAEALADO = 5;

        public LosLopezReggaeAlado(Punto unPunto)
            : base(unPunto)
        {
            this.Lanzador = new LanzadorMolotov();
            this.UnidadesDeResistencia = VIDALOSLOPEZREGGAEALADO;
            this.Nombre = Nombres.lopezReggaeAlado;
        }

        public override bool AtraviesaObstaculos()
        {
            return true;
        }
    }
}
