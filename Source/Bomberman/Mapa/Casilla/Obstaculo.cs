using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Personaje;

namespace Bomberman.Mapa.Casilla
{
    public abstract class Obstaculo:IDaniable
    {
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
        public virtual void DaniarConBombaMolotov(int UnidadesDaniadas)
        {
            //Deberia daniar a los personajes alli presentes
            if (!this.Destruido())
            {
                this.UnidadesDeResistencia = CalcularUnidadesRestantes(UnidadesDaniadas);
            }
        }

        //por ahora dejo esto virtual para que funcione, ARREGLAR LOS HARCODEOS!!
        public virtual void DaniarConProyectil(int UnidadesDaniadas)
        {
              // idem DaniarConBombaMolotov
            if (!this.Destruido())
            {
                this.UnidadesDeResistencia = CalcularUnidadesRestantes(UnidadesDaniadas);
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

        public abstract bool PuedeContenerArticulos();

    }
}
