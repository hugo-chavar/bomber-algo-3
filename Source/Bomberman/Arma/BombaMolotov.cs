using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.Arma
{
    public class BombaMolotov: Bomba
    {
        private const int TIEMPOEXPLOSION = 1;
        private const int PODERDEDESTRUCCIONMOLOTOV = 5;
        private const int ONDAEXPANSIVAMOLOTOV = 3;

        public BombaMolotov(Punto posicion, int porcentajeRetardo)
            :base(posicion)
        {
            this.TiempoRestante = (TIEMPOEXPLOSION * (100 - porcentajeRetardo) / 100); //Hugo: llamada para acordarme de arreglar el tiempo de retardo..
            this.PoderDeDestruccion = PODERDEDESTRUCCIONMOLOTOV;
            this.OndaExpansiva = ONDAEXPANSIVAMOLOTOV;
        }

        public override void Daniar(IDaniable daniable)
        {
            daniable.DaniarConBombaMolotov(this.PoderDeDestruccion);
        }
    }
}