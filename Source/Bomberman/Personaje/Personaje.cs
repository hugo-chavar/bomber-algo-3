using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman;
using Bomberman.Juego;
using Bomberman.Arma;

namespace Bomberman.Personaje
{
    public abstract class Personaje : IMovible, IPosicionable
    {
        protected int velocidad;
        protected Punto posicion;
        protected Lanzador lanzador;
        protected String direccion;
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
        }

    }
}
