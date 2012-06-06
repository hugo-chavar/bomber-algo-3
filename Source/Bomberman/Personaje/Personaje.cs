using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman;
using Bomberman.Juego;
using Bomberman.Arma;

namespace Bomberman.Personaje
{
    public abstract class Personaje : IMovible
    {
        protected Movimiento movimiento;
        protected Punto posicion;
        protected ILanzador lanzador;
        protected int reduccionRetardoBombas;
        protected int unidadesDeResistencia;

        public Personaje(Punto unPunto)
        {
            this.reduccionRetardoBombas = 0;
            this.movimiento = new Movimiento();
            this.Posicion = unPunto;
        }

        public Movimiento Movimiento
        {
            get { return this.movimiento; }
            set { this.movimiento = value; }
        }

        public ILanzador Lanzador 
        { 
            get { return this.lanzador; }
            set { this.lanzador = value; }
        }

        public int ReduccionRetardoBombas
        { 
            get { return this.reduccionRetardoBombas;}
            set { this.reduccionRetardoBombas = value; }
        }

        public int UnidadesDeResistencia
        {
            get { return this.unidadesDeResistencia; }
            set { this.unidadesDeResistencia = value; }
        }

        public Punto Posicion
        {
            get { return this.posicion; }
            set { this.posicion = value; }
        }

        public void LanzarExplosivo(Punto posicion, int retardo)
        {
            this.lanzador.Lanzar(posicion, retardo);
        }

        public void Mover()
        {
            if (!(this.Destruido()))                //Resolucion para que los personajes no sean zombies ! 
            Juego.Juego.Instancia().Ambiente.Mover(this);
        }

        public virtual bool AtraviesaObstaculos()
        {
            return false;
        }

        public bool Destruido()
        {
            return ((this.unidadesDeResistencia) < 1);
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

        public virtual void DaniarConBombaToleTole()
        {
            this.UnidadesDeResistencia = 0;
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

    }
}
