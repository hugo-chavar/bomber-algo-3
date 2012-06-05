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




        public void DaniarConBombaToleTole()
        {
        }
        public void DaniarConBombaMolotov(int UnidadesDaniadas)
        { 
        }
        public void DaniarConProyectil(int UnidadesDaniadas)
        { 
        }

    }
}
