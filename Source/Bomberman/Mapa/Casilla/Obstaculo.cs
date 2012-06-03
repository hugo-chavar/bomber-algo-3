using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Personaje;

namespace Bomberman.Mapa.Casilla
{
    public abstract class Obstaculo
    {
        //Deberia ir en la clase bomba pero no la implementamos todavia
        public const int DANIOMOLOTOV = 5;
        public const int DANIOPROYECTIL = 5;


        protected int unidadesDeResistencia;


        public Obstaculo()
        {
            this.UnidadesDeResistencia = 0;
        }


        public Obstaculo(int unidades)
        {
            //this.Posicion = posicion;
            this.UnidadesDeResistencia = unidades;
        }

        public int UnidadesDeResistencia
        {
            get { return this.unidadesDeResistencia; }
            set { this.unidadesDeResistencia = value; }
        }

        
        public virtual void DaniarConBombaToleTole() 
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

        //por ahora dejo esto virtual para que funcione, ARREGLAR LOS HARCODEOS!!
        public virtual void DaniarConBombaMolotov()
        {
            //Deberia daniar a los personajes alli presentes
            if (!this.Destruido())
            {
                this.UnidadesDeResistencia = CalcularUnidadesRestantes(DANIOMOLOTOV);
            }
        }

        //por ahora dejo esto virtual para que funcione, ARREGLAR LOS HARCODEOS!!
        public virtual void DaniarConProyectil()
        {
              // idem DaniarConBombaMolotov
            if (!this.Destruido())
            {
                this.UnidadesDeResistencia = CalcularUnidadesRestantes(DANIOPROYECTIL);
            }
        }

        public bool Destruido()
        {
            return((this.UnidadesDeResistencia)<1);
        }

        public virtual bool TransitablePor(IMovible movil)
        {
            return movil.AtraviesaObstaculos();
        }
    }
}
