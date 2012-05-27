﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman
{
    public abstract class Obstaculo:IDañable
    {
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
        public virtual void daniarConBombaMolotov(int unidadesDestruidas)
        {
            if (!this.destruido())
            {
                this.unidadesDeResistencia = calcularUnidadesRestantes(unidadesDestruidas);
            }
        }

        public bool destruido()
        {
            return((this.unidadesDeResistencia)<1);
        }
    }
}
