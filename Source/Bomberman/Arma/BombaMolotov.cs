﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BombermanModel.Arma
{
    public class BombaMolotov : Bomba
    {
        private const int TIEMPOEXPLOSION = 3;
        private const int PODERDEDESTRUCCIONMOLOTOV = 5;
        private const int ONDAEXPANSIVAMOLOTOV = 3;

        public BombaMolotov(Punto posicion, int porcentajeRetardo)
            : base(posicion)
        {
            this.tiempoExplosion = (TIEMPOEXPLOSION * (100 - porcentajeRetardo) / 100F);
            this.PoderDeDestruccion = PODERDEDESTRUCCIONMOLOTOV;
            this.OndaExpansiva = ONDAEXPANSIVAMOLOTOV;
            nombre = Nombres.molotov;
        }

        public override void Daniar(IDaniable daniable)
        {
            daniable.DaniarConBombaMolotov(this.PoderDeDestruccion);
        }
    }
}