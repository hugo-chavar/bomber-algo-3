using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BombermanModel.Excepciones;
using BombermanModel.Personaje;

namespace BombermanModel.Arma
{
    public class Proyectil : Explosivo , IMovible
    {
        private const int PODERDEDESTRUCCIONPROYECTIL = 1;
        private const int ONDAEXPANSIVA = 1;
        private const float VELOCIDADMOV = 250F;

        private DateTime ultimoMovimiento;
        private Movimiento movimiento;
        private int alcance;

        //xna
        public void Initialize()
        {
        }

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
            ultimoMovimiento = DateTime.Now;
            Punto posicionPrevia = this.Posicion.Clonar();
            if (Juego.Juego.Instancia().Ambiente.PermitidoAvanzar(this))
                Juego.Juego.Instancia().Ambiente.Avanzar(this);
                //Juego.Juego.Instancia().Ambiente.Mover(this); //TODO: Primer cambio jodido
            this.alcance--;
            //llego a casilla donde impacta o choco contra los limites del mapa
            //estos son 2 de los 3 motivos por los cuales explota un proyectil
            //el otro motivo es el choque contra obstaculos y se maneja como una colision
            if (LlegoADestino() || posicionPrevia.Equals(this.Posicion)) 
            {
                this.Colisionar();
            }
            if (this.exploto)
                Juego.Juego.Instancia().Ambiente.ObtenerCasilla(this.posicion).Dejar(this);
        }

        public void ReaccionarConArticulo(Articulo.Articulo unArt)
        {
            //los proyectiles no reaccionan al encontrarse con articulos
        }
                
        public Proyectil(Punto unaPos)

        {
            ultimoMovimiento = DateTime.Now;
            this.movimiento = new Movimiento();
            this.poderDeDestruccion = PODERDEDESTRUCCIONPROYECTIL;
            this.ondaExpansiva = ONDAEXPANSIVA;
            nombre = Nombres.proyectil;
            posicion = unaPos;

        }
          
        public override void Daniar(IDaniable daniable)
        {
            daniable.DaniarConProyectil(this.PoderDeDestruccion);
        }

        public override void CuandoPasaElTiempo()
        {
            DateTime horaActual = DateTime.Now;
            if (horaActual.Subtract(ultimoMovimiento).Milliseconds >= VELOCIDADMOV)
            this.Mover();
        }

        public bool EsDaniable()
        {
            return false;
        }

        public Punto PosicionDestino()
        {
            return this.Posicion.PosicionHaciaUnaDireccion(this.Movimiento.Direccion);
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
            Juego.Juego.Instancia().ObjetoContundenteDestruido(this);
        }
    }
}