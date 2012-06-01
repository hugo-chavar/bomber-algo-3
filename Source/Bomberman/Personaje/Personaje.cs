using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman;

namespace Bomberman.Personaje
{
    public abstract class Personaje : IMovible, IPosicionable
    {
        protected int velocidad;
        protected Punto posicion;
        protected Lanzador lanzador;


        public void mover()
        {
            //falta implementar
        }

        public bool atraviesaObstaculos()
        {
            //hacer un override de este metodo solo en el personaje que atraviesa obstaculos
            return false;
        }

        public Punto getPosicion()
        {
            return this.posicion;
        }

    }
}
