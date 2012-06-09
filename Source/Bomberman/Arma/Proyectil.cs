using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.Arma
{
    public class Proyectil : Explosivo
    {
        //private const int ALCANCEPROYECTIL = 3;
        private const int PODERDEDESTRUCCIONPROYECTIL = 1;
        private const int ONDAEXPANSIVA = 1;

        private Punto posicionFinal;
        private Punto posicionActual;
        private Queue<Punto> trayectoria;
        //private int alcance = ALCANCEPROYECTIL;
        //private int tiempoRestante;
        //private ManejadorProyectil unManejador;


        //public int Alcance
        //{
        //    get { return this.alcance; }
        //    set { this.alcance = value; }
        //}
        
        //public Punto PosicionFinal
        //{
        //    get { return this.posicionFinal; }
        //    set { this.posicionFinal = value; }
        //}

        public Punto PosicionActual
        {
            get { return this.posicionActual; }
            //set { this.posicionFinal = value; } esto no va!!
        }

        public void Avanzar()
        {
            try
            {
                //me muevo a la siguiente posision programada por el lanzador
                this.posicionActual = this.trayectoria.Dequeue();
            }
            catch (InvalidOperationException)
            {
                //si la trayectoria esta vacia, llegue al punto de impacto
                Juego.Juego.Instancia().Ambiente.ObtenerCasilla(this.posicionActual).PlantarExplosivo(this);

                this.Explotar();
            }
        }
                
        public Proyectil(Punto posicionDestino)

        {
            this.poderDeDestruccion = PODERDEDESTRUCCIONPROYECTIL;
            this.ondaExpansiva = ONDAEXPANSIVA;
            posicionFinal = posicionDestino;
            ///tiempoRestante = 3;
        }
          
        public override void Daniar(IDaniable daniable)
        {
            daniable.DaniarConProyectil(this.PoderDeDestruccion);

            // cuando explota genera el mismo danio que la Molotov //
        }

        //public void LanzarMisil(int direccionPersonaje)
        //{
        //    if (unManejador == null)
        //    {
        //        unManejador = new ManejadorProyectil(this, direccionPersonaje);
        //        unManejador.LanzarMisil();
        //    }
        //}

        //public int TiempoRestante()
        //{
        //    return this.tiempoRestante;
        //}

        public override void  CuandoPasaElTiempo()
        {
            this.Avanzar();
            
            //if (tiempoRestante > 0)
            //{
            //    this.DisminuirTiempo();
            //    unManejador.AvanzarHacia(this);
            //}
            //if ((tiempoRestante == 0) && (unManejador.EstaLanzado()))
            //{
            //    base.Explotar();
            //}
        }

        //public void DisminuirTiempo()
        //{
        //    this.tiempoRestante = this.tiempoRestante - 1;
        //}

        public Queue<Punto> Trayectoria 
        {
            //get ; 
            set {this.trayectoria = value;}
        }
    }
}