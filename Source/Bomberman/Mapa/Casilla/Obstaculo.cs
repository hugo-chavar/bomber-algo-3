using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.Mapa.Casilla
{
    public abstract class Obstaculo:IDaniable
    {
        //Deberia ir en la clase bomba pero no la implementamos todavia
        public const int danioMolotov = 5;


        protected Punto posicion;
        protected int unidadesDeResistencia;

        public Obstaculo(Punto Posicion, int Unidades)
        {
            this.posicion = Posicion;
            this.unidadesDeResistencia = Unidades;
        }

        public Punto Posicion
        {
            get { return this.posicion;}
            set { this.posicion = Posicion; }
        }

        public int UnidadesDeResistencia
        {
            get { return (this.unidadesDeResistencia);}
            set { this.unidadesDeResistencia = value;}
        }

        public void daniarConBombaToleTole()
        {
            this.unidadesDeResistencia = 0;
        }

        private int calcularUnidadesRestantes(int unidadesDestruidas)
        {
            int unidades = this.unidadesDeResistencia - unidadesDestruidas;
            if (unidades < 0) return (0);
            else return (unidades);
        
        }
        public virtual void daniarConBombaMolotov()
        {
            if (!this.destruido())
            {
                this.unidadesDeResistencia = calcularUnidadesRestantes(danioMolotov);
            }
        }

        public bool destruido()
        {
            return((this.unidadesDeResistencia)<1);
        }
    }
}
