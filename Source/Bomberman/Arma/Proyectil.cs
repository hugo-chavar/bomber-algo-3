using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Excepciones;

namespace Bomberman.Arma
{
    public class Proyectil : Explosivo
    {
        private const int PODERDEDESTRUCCIONPROYECTIL = 1;
        private const int ONDAEXPANSIVA = 1;

        private Punto posicionFinal;
       // private Punto posicion;
        private Queue<Punto> trayectoria;

        //public Punto Posicion
        //{
        //    get { return this.posicion; }
        //    set { this.posicion = value; }
        //}

        public void Avanzar()
        {
            try
            {
                //me muevo a la siguiente posision programada por el lanzador
                this.posicion = this.trayectoria.Dequeue();
                if (this.posicion.Equals(this.posicionFinal))
                {
                    //llegue al punto de impacto
                    Juego.Juego.Instancia().Ambiente.ObtenerCasilla(this.posicion).PlantarExplosivo(this);
                    this.Explotar();
                }
            }
            catch (InvalidOperationException)
            {
                throw new AvanzarProyectilNoValidoException();
            }
        }
                
        public Proyectil(Punto posicionDestino)

        {
            this.poderDeDestruccion = PODERDEDESTRUCCIONPROYECTIL;
            this.ondaExpansiva = ONDAEXPANSIVA;
            posicionFinal = posicionDestino;
        }
          
        public override void Daniar(IDaniable daniable)
        {
            daniable.DaniarConProyectil(this.PoderDeDestruccion);
        }

        public override void CuandoPasaElTiempo()
        {
            this.Avanzar();
        }

        //public override void DejoDeDependerDelTiempo()
        //{
        //    //this.Avanzar();
        //}           
        public Queue<Punto> Trayectoria 
        {
            set {this.trayectoria = value;}
        }
    }
}