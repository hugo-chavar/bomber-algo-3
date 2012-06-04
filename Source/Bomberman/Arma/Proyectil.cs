﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.Arma
{
    public class Proyectil : Explosivo
    {
        // Buscar ENUMERADOS: ARRIBA = 1; ABAJO = 0; DERECHA = 2; IZQUIERDA = 3 //
        // establezco arbitrariamente un alcance de 3 casilleros (cuanto avanza el proytectil antes de explotar) y explota //

        private Punto posicionFinal;
        private int alcance = 3;
        private int tiempoRestante;
        private ManejadorProyectil unManejador;


        public int Alcance
        {
            get { return this.alcance; }
            set { this.alcance = value; }
        }

        public Punto PosicionInicial
        {
            get { return this.posicion; }
            set { this.PosicionInicial = value; }
        }

        public Punto PosicionFinal
        {
            get { return this.posicionFinal; }
            set { this.posicionFinal = value; }
        }

        
        public Proyectil(Punto posicionInicial)

        {
            this.poderDeDestruccion = 5;
            this.ondaExpansiva = 3;
            Punto PosicionFinal = new Punto(0, 0);
            posicionFinal = PosicionFinal;
            posicion = posicionInicial;
            tiempoRestante = 3;


        }

    

        public override void Daniar(IDaniable daniable)
        {
            daniable.DaniarConProyectil(this.PoderDeDestruccion);

            // cuando explota genera el mismo danio que la Molotov //
        }

        public void LanzarMisil(int direccionPersonaje)
        {
            if ( unManejador == null )
            {
                unManejador = new ManejadorProyectil(this, direccionPersonaje);
                unManejador.LanzarMisil();
 
            }

        }

        public void CuandoPasaElTiempo()
        {
            this.DisminuirTiempo(tiempoRestante);
            if (tiempoRestante > 1)
            {
                unManejador.AvanzarHacia();
            }
            if (tiempoRestante == 0)
            {
                unManejador.RealizarExplosion(this);
            }

            
        }

        public void DisminuirTiempo(int tiempo)
        {
            tiempo = tiempo - 1;

        }


        



     
}

}