using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Excepciones;
using Bomberman.Personaje;

namespace Bomberman.Arma
{
    public class Proyectil : Explosivo , IMovible
    {
        private const int PODERDEDESTRUCCIONPROYECTIL = 1;
        private const int ONDAEXPANSIVA = 1;

        private Queue<Punto> trayectoria;
        private Movimiento movimiento;

        public Movimiento Movimiento
        {
            get { return this.movimiento; }
            set { this.movimiento = value; }
        }

        public void Mover()
        {
            Juego.Juego.Instancia().Ambiente.Mover(this);
        }

        public void ReaccionarConArticulo(Articulo.Articulo unArt)
        {
            //los proyectiles no reaccionan
        }
                
        public Proyectil(Punto unaPos)

        {
            this.poderDeDestruccion = PODERDEDESTRUCCIONPROYECTIL;
            this.ondaExpansiva = ONDAEXPANSIVA;
            posicion = posicion;

        }
          
        public override void Daniar(IDaniable daniable)
        {
            daniable.DaniarConProyectil(this.PoderDeDestruccion);
        }

        public override void CuandoPasaElTiempo()
        {
            this.Mover();
        }
       
        public Queue<Punto> Trayectoria 
        {
            set {this.trayectoria = value;}
        }

        public bool EsDaniable()
        {
            return false;
        }

        public bool AtraviesaObstaculos()
        {
            //el proyectil puede llegar a la posicion de un obstaculo, luego choca y explota
            return true;
        }
    }
}