using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.Arma
{
    public class BombaToleTole:Bomba
    {
        private const int TIEMPOEXPLOSION = 5;
        private const int INFINITO = 1000;

        public BombaToleTole(int x, int y, int porcentajeRetardo)
            :base(x,y)
        {
            this.retardo = TIEMPOEXPLOSION * (100 - porcentajeRetardo) / 100;
            this.poderDeDestruccion = INFINITO;
            this.ondaExpansiva = 6;
        }

        public override void daniar(IDaniable daniable)
        {
            daniable.daniarConBombaToleTole();        
        }
    }
}
