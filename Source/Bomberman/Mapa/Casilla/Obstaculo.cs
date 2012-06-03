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
        public const int DANIOMOLOTOV = 5;
        public const int DANIOPROYECTIL = 5;


        protected int unidadesDeResistencia;

        public Obstaculo(Punto posicion, int unidades)
        {
            this.Posicion = posicion;
            this.UnidadesDeResistencia = unidades;
        }

        public int UnidadesDeResistencia
        {
            get { return this.unidadesDeResistencia; }
            set { this.unidadesDeResistencia = value; }
        }

        public override void DaniarConBombaToleTole()
        {
            //Deberia daniar a los personajes alli presentes
            this.UnidadesDeResistencia = 0;
        }

        private int CalcularUnidadesRestantes(int unidadesDestruidas)
        {
            int unidades = (this.UnidadesDeResistencia - unidadesDestruidas);
            if (unidades < 0) return (0);
            else return (unidades);
        
        }
        public override void DaniarConBombaMolotov()
        {
            //Deberia daniar a los personajes alli presentes
            if (!this.Destruido())
            {
                this.UnidadesDeResistencia = CalcularUnidadesRestantes(DANIOMOLOTOV);
            }
        }

        public override void DaniarConProyectil()
        {
              // idem DaniarConBombaMolotov
            if (!this.Destruido())
            {
                this.UnidadesDeResistencia = CalcularUnidadesRestantes(DANIOPROYECTIL);
            }
        }



        //public override void daniarConProyectil();

        public bool Destruido()
        {
            return((this.UnidadesDeResistencia)<1);
        }

        public override bool TransitablePor(IMovible movil)
        {
            return movil.AtraviesaObstaculos();
        }
    }
}
