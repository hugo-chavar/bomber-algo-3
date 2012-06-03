using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman;
using Bomberman.Juego;
using Bomberman.Arma;

namespace Bomberman.Personaje
{
    public abstract class Personaje : IMovible, IPosicionable, IComedor
    {
        protected int velocidad;
        protected Punto posicion;
        protected ILanzador lanzador;
        protected String direccion;
        protected int reduccionRetardoBombas;
        protected int unidadesDeResistencia;

        protected const int VELOCIDAD = 1;

        public Personaje(Punto unPunto)
        {
            this.reduccionRetardoBombas = 0;
            this.velocidad = VELOCIDAD;
            this.Posicion = unPunto;
        }

        public int Velocidad
        {
            get { return this.velocidad; }
            set { this.velocidad = value; }
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

        public void LanzarExplosivo(int x, int y, int retardo)
        {
            this.lanzador.Lanzar(x, y, retardo);
        }


      
        public void Mover()
        {
            //falta implementar
            if (this.direccion == "Arriba")
            {

            }
        }

        public virtual bool AtraviesaObstaculos()
        {
            return false;
        }



        public abstract void Comer(Articulo.IComible comible);

        public abstract void DuplicarVelocidad();

        public abstract void CambiarLanzadorAToleTole();

        public abstract void ReducirRetardo(int retardo);

    }
}
