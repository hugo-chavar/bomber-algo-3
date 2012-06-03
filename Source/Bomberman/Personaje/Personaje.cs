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

        public Personaje(Punto unPunto)
        {
            this.reduccionRetardoBombas = 0;
            this.velocidad = 1;
            this.Posicion = unPunto;
        }

        public ILanzador Lanzador 
        { 
            get { return this.lanzador; } 
        }

        public int ReduccionRetardoBombas
        { 
            get { return this.reduccionRetardoBombas;}  
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

        public bool AtraviesaObstaculos()
        {
            //hacer un override de este metodo solo en el personaje que atraviesa obstaculos
            return false;
        }

        public Punto Posicion
        {
            get { return this.posicion; }
            set { this.posicion = Posicion; }
        }


        public abstract void Comer(Articulo.IComible comible);

        public abstract void DuplicarVelocidad();

        public abstract void CambiarLanzadorAToleTole();

        public abstract void ReducirRetardo(int retardo);

    }
}
