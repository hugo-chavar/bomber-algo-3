﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BombermanModel.Arma
{
    public class BombaToleTole : Bomba
    {
        private const int TIEMPOEXPLOSION = 5;
        private const int INFINITO = 1000;
        private const int ONDAEXPANSIVATOLETOLE = 6;

        public BombaToleTole(Punto posicion, int porcentajeRetardo)
            : base(posicion)
        {
            this.tiempoExplosion = TIEMPOEXPLOSION * ((100 - porcentajeRetardo) / 100F);
            this.PoderDeDestruccion = INFINITO;
            this.OndaExpansiva = ONDAEXPANSIVATOLETOLE;
            nombre = Nombres.toleTole;
        }

        public override void Daniar(IDaniable daniable)
        {
            daniable.DaniarConBombaToleTole();
        }
    }
}
