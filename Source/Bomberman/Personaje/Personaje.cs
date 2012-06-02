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
        }

        public void mover()
        {
            //falta implementar
            if (this.direccion == "Arriba")
            {
                //suponiendo que en Bomberman.Juego hay una clase Juego
                //como puedo preguntar si el mapa que está en el juego (es decir el mapa es un atributo de la clase juego) me permite
                //moverme hacia arriba desde la posicion que estoy
                //la idea es no estar pasando como parametro al Juego por todos lados
                //lo que quiero escribir es algo asi: (suponiendo que existen los metodos mencionados)
                //if (Bomberman.Juego.getJuegoInstanciado.getMapa().permitidoMoverArribaDesde(posicion)
                //     mover (aca tendria que volver a ir al mapa y quitar el personaje de la pos y moverlo a la nueva)

            }
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
