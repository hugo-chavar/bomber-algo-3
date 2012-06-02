using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Personaje;

namespace Bomberman.Mapa.Casilla
{
    public abstract class Obstaculo : Casilla
    {
        //Deberia ir en la clase bomba pero no la implementamos todavia
        public const int danioMolotov = 5;

        protected int unidadesDeResistencia;

        public Obstaculo(Punto Posicion, int Unidades)
        {
            this.posicion = Posicion;
            this.unidadesDeResistencia = Unidades;
        }

        public int UnidadesDeResistencia
        {
            get { return (this.unidadesDeResistencia);}
            set { this.unidadesDeResistencia = value;}
        }

        public override void daniarConBombaToleTole()
        {
            //Deberia daniar a los personajes alli presentes
            this.unidadesDeResistencia = 0;
        }

        private int calcularUnidadesRestantes(int unidadesDestruidas)
        {
            int unidades = this.unidadesDeResistencia - unidadesDestruidas;
            if (unidades < 0) return (0);
            else return (unidades);
        
        }
        public override void daniarConBombaMolotov()
        {
            //Deberia daniar a los personajes alli presentes
            if (!this.destruido())
            {
                this.unidadesDeResistencia = calcularUnidadesRestantes(danioMolotov);
            }
        }

        //public override void daniarConProyectil();

        public bool destruido()
        {
            return((this.unidadesDeResistencia)<1);
        }

        public override bool transitablePor(IMovible movil)
        {
            return movil.AtraviesaObstaculos();
        }
    }
}
