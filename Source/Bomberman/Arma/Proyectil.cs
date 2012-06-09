using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.Arma
{
    public class Proyectil : Explosivo
    {
        private const int ALCANCEPROYECTIL = 3;
        private const int PODERDEDESTRUCCIONPROYECTIL = 5;
        private const int ONDAEXPANSIVA = 3;

        private Punto posicionFinal;
        //private Punto posicionActual;
        private List<Punto> trayectoria;
        private int alcance = ALCANCEPROYECTIL;
        private int tiempoRestante;
        private ManejadorProyectil unManejador;


        public int Alcance
        {
            get { return this.alcance; }
            set { this.alcance = value; }
        }
        
        public Punto PosicionFinal
        {
            get { return this.posicionFinal; }
            set { this.posicionFinal = value; }
        }
                
        public Proyectil(Punto posicionDestino)

        {
            this.poderDeDestruccion = PODERDEDESTRUCCIONPROYECTIL;
            this.ondaExpansiva = ONDAEXPANSIVA;
            Punto PosicionFinal = new Punto(0, 0);
            posicionFinal = PosicionFinal;
            tiempoRestante = 3;
        }
          
        public override void Daniar(IDaniable daniable)
        {
            daniable.DaniarConProyectil(this.PoderDeDestruccion);

            // cuando explota genera el mismo danio que la Molotov //
        }

        public void LanzarMisil(int direccionPersonaje)
        {
            if (unManejador == null)
            {
                unManejador = new ManejadorProyectil(this, direccionPersonaje);
                unManejador.LanzarMisil();
            }
        }

        public int TiempoRestante()
        {
            return this.tiempoRestante;
        }

        public override void  CuandoPasaElTiempo()
        {

            if (tiempoRestante > 0)
            {
                this.DisminuirTiempo();
                unManejador.AvanzarHacia(this);
            }
            if ((tiempoRestante == 0) && (unManejador.EstaLanzado()))
            {
                base.Explotar();
            }
        }

        public void DisminuirTiempo()
        {
            this.tiempoRestante = this.tiempoRestante - 1;
        }

        public List<Punto> Trayectoria 
        {
            //get ; 
            set {this.trayectoria = value;}
        }
    }
}