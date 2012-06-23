using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.Arma
{
    public class BombaToleTole : Bomba
    {
        private const int TIEMPOEXPLOSION = 5;
        private const int INFINITO = 1000;
        private const int ONDAEXPANSIVATOLETOLE = 6;

        public BombaToleTole(Punto posicion, int porcentajeRetardo)
            : base(posicion)
        {
            this.tiempoExplosion = TIEMPOEXPLOSION * ((100 - porcentajeRetardo) / 100);
            this.PoderDeDestruccion = INFINITO;
            this.OndaExpansiva = ONDAEXPANSIVATOLETOLE;
        }

        public override void Daniar(IDaniable daniable)
        {
            daniable.DaniarConBombaToleTole();
        }
    }
}
