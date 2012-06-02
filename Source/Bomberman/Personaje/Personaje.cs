using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman;
using Bomberman.Arma;

namespace Bomberman.Personaje
{
    public abstract class Personaje : IMovible, IPosicionable
    {
        protected int velocidad;
        protected Punto posicion;
        protected Lanzador lanzador;
        protected int reduccionRetardoBombas;

        public Personaje()
        {
            this.reduccionRetardoBombas = 0;
            this.velocidad = 1;
        }

        public Lanzador Lanzador 
        { 
            get { return this.lanzador; } 
        }

        public int ReduccionRetardoBombas
        { 
            get { return this.reduccionRetardoBombas;}  
        }

        public void LanzarExplosivo(int x, int y, int retardo)
        {
            this.lanzador.lanzar(x, y, retardo);
        }

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
