﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Personaje;
using Bomberman.Arma;
using Bomberman;

namespace Bomberman.Mapa.Casilla
{
    public class Casilla
    {
        private Punto posicion;
        private Articulo.Articulo articuloContenido;
        private List<IMovible> transitandoEnCasilla;

        //defino el patron State para determinar si hay un obstaculo o la casilla esta libre
        private Obstaculo estado;
        private Explosivo explosivo;

        public Casilla(Punto pos)
        {
            this.posicion = pos;
            transitandoEnCasilla = new List<IMovible>();
            explosivo = null;
        }

        public List<IMovible> TransitandoEnCasilla
        {
            get { return this.transitandoEnCasilla; }
        }

        public Explosivo Explosivo
        {
            get { return this.explosivo; }
        }

        public bool PermiteTransitarUn(IMovible movil)
        {
            return this.Estado.TransitablePor(movil);
        }

        public bool PermiteExplosivos()
        {
            return this.Estado.PermiteDejarExplosivos();
        }

        public bool TieneUnExplosivo()
        {
            return (this.explosivo != null);
        }

        public void Transitar(IMovible movil)
        {
            this.transitandoEnCasilla.Add(movil);
            movil.Posicion = this.Posicion;
            if ((this.ArticuloContenido != null) && (!this.ArticuloContenido.EstaOculto))
            {
                movil.ReaccionarConArticulo(this.ArticuloContenido);
            }
        }

        public void Dejar(IMovible movil)
        {
            this.transitandoEnCasilla.Remove(movil);
        }

        public Punto Posicion
        {
            get { return this.posicion; }
            set { this.posicion = value; }
        }


        public Obstaculo Estado
        {
            get { return this.estado; }
            set { this.estado = value; }
        }

        public Articulo.Articulo ArticuloContenido
        {
            get { return this.articuloContenido; }
            set { this.articuloContenido = value; }
        }

        public void agregarArticulo(Articulo.Articulo unArticulo)
        {
            if ((this.Estado != null) && (this.estado.PuedeAgregarArticulo()))   
            {
                this.ArticuloContenido = unArticulo;
            }

        }

        public void agregarSalida(Articulo.Salida salida)
        {
            if ((this.Estado != null) && (this.estado.PuedeContenerSalida()) && (this.estado.PuedeAgregarArticulo()))
            {
                this.ArticuloContenido = salida;
            }
        }

        //public abstract void DaniarConBombaToleTole();
        //public abstract void DaniarConBombaMolotov();
        //public abstract void DaniarConProyectil();

        public void PlantarExplosivo(Explosivo unExplosivo)
        {
            //Juego.Juego.Instancia().Ambiente.DependientesDelTiempo.Add(unExplosivo);
            this.explosivo=unExplosivo;
        }

        public void QuitarExplosivo()
        {
                this.explosivo = null;
        }
    }
}
