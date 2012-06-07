using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Personaje;

namespace Bomberman.Arma
{
    public abstract class ILanzador
    {
        public abstract bool Lanzar(Punto posicion, int reduccionRetardo);

        protected Movimiento sentido;
        protected Punto posicionDeTiro;
        protected int retardoExplosion;

        public int RetardoExplosion
        {
            get { return this.retardoExplosion; }
            set { this.retardoExplosion = value; }
        }

        public Punto PosicionDeTiro
        {
            get { return this.posicionDeTiro; }
            set { this.posicionDeTiro = value; }
        }

        public Movimiento Sentido
        {
            get { return this.sentido; }
            set { this.sentido = value; }
        }
    }

}
