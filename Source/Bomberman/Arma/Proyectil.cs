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

        private Movimiento movimiento;
        private int alcance;

        public Movimiento Movimiento
        {
            get { return this.movimiento; }
            set { this.movimiento = value; }
        }

        public int Alcance
        {
            get { return this.alcance; }
            set { this.alcance = value; }
        }

        public void Mover()
        {
            Punto posicionPrevia = this.Posicion.Clonar();
            Juego.Juego.Instancia().Ambiente.Mover(this);
            this.alcance--;
            //llego a casilla donde impacta o choco contra los limites del mapa
            //estos son 2 de los 3 motivos por los cuales explota un proyectil
            //el otro motivo es el choque contra obstaculos y se maneja como una colision
            if (LlegoADestino() || posicionPrevia.Equals(this.Posicion)) 
            {
                this.Explotar();
            }
        }

        public void ReaccionarConArticulo(Articulo.Articulo unArt)
        {
            //los proyectiles no reaccionan
        }
                
        public Proyectil(Punto unaPos)

        {
            this.movimiento = new Movimiento();
            this.poderDeDestruccion = PODERDEDESTRUCCIONPROYECTIL;
            this.ondaExpansiva = ONDAEXPANSIVA;
            posicion = unaPos;

        }
          
        public override void Daniar(IDaniable daniable)
        {
            daniable.DaniarConProyectil(this.PoderDeDestruccion);
        }

        public override void CuandoPasaElTiempo()
        {
            this.Mover();
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

        public bool LlegoADestino()
        {
            return (this.alcance == 0);
        }

        public bool ImpactaEnObstaculos()
        {
            return true;
        }

        public void Colisionar()
        {
            this.Explotar();
        }
    }
}