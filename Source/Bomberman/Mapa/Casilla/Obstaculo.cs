﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BombermanModel.Personaje;

namespace BombermanModel.Mapa.Casilla
{
    public abstract class Obstaculo:IDaniable
    {
        protected int unidadesDeResistencia;
        protected Nombres nombre;

        public Nombres Nombre
        {
            get { return this.nombre; }
            set { nombre = value; }
        }


        public Obstaculo()
        {
            this.UnidadesDeResistencia = 0;
        }

        public Obstaculo(int unidades)
        {
            this.UnidadesDeResistencia = unidades;
        }

        public int UnidadesDeResistencia
        {
            get { return this.unidadesDeResistencia; }
            set { this.unidadesDeResistencia = value; }
        }

        public virtual void DaniarConBombaToleTole() 
        {
            this.UnidadesDeResistencia = 0;
        }

        private int CalcularUnidadesRestantes(int unidadesDestruidas)
        {
            int unidades = (this.UnidadesDeResistencia - unidadesDestruidas);
            if (unidades < 0) return (0);
            else return (unidades);
        }

        public virtual void DaniarConBombaMolotov(int UnidadesDaniadas)
        {
            this.DaniarSiNoEstaDestruido(UnidadesDaniadas);
        }

        public virtual void DaniarConProyectil(int UnidadesDaniadas)
        {
            this.DaniarSiNoEstaDestruido(UnidadesDaniadas);
        }

        private void DaniarSiNoEstaDestruido(int UnidadesDaniadas)
        {
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

        public virtual bool PuedeAgregarArticulo()
        {
            return true;
        }

        public virtual bool PermiteDejarExplosivos()
        {
            return false;
        }

        public abstract bool PuedeContenerSalida();

    }
}
