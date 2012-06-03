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
        public const int danioProyectil = 5;


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

        public override void DaniarConBombaToleTole()
        {
            //Deberia daniar a los personajes alli presentes
            this.unidadesDeResistencia = 0;
        }

        private int CalcularUnidadesRestantes(int unidadesDestruidas)
        {
            int unidades = this.unidadesDeResistencia - unidadesDestruidas;
            if (unidades < 0) return (0);
            else return (unidades);
        
        }
        public override void DaniarConBombaMolotov()
        {
            //Deberia daniar a los personajes alli presentes
            if (!this.Destruido())
            {
                this.unidadesDeResistencia = CalcularUnidadesRestantes(danioMolotov);
            }
        }

        public override void DaniarConProyectil()
        {
              // idem DaniarConBombaMolotov
            if (!this.Destruido())
            {
                this.unidadesDeResistencia = CalcularUnidadesRestantes(danioProyectil);
            }
        }



        //public override void daniarConProyectil();

        public bool Destruido()
        {
            return((this.unidadesDeResistencia)<1);
        }

        public override bool TransitablePor(IMovible movil)
        {
            return movil.AtraviesaObstaculos();
        }
    }
}
