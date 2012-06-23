using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BombermanModel.Personaje;

namespace BombermanModel.Arma
{
    public abstract class Lanzador
    {
        protected Punto posicionDeTiro;
        protected int retardoExplosion;
        protected int alcance;
        protected IDependienteDelTiempo carga;

        public abstract void Disparar(); 

        public int Alcance
        {
            get { return this.alcance; }
            set { this.alcance = value; }
        }

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

        public virtual void Cargar(IMovible movil)
        {
            this.PosicionDeTiro = movil.Posicion.Clonar();
        }
    }

}
