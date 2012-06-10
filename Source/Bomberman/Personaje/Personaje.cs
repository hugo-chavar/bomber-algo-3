using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman;
using Bomberman.Juego;
using Bomberman.Arma;

namespace Bomberman.Personaje
{
    public abstract class Personaje : IMovible , IDaniable
    {
        protected Movimiento movimiento;
        protected Punto posicion;
        protected Lanzador lanzador;
        //protected int reduccionRetardoBombas;
        protected int unidadesDeResistencia;

        public Personaje(Punto unPunto)
        {
            //this.reduccionRetardoBombas = 0;
            this.movimiento = new Movimiento();
            this.Posicion = unPunto;
        }

        public Movimiento Movimiento
        {
            get { return this.movimiento; }
            set { this.movimiento = value; }
        }

        public Lanzador Lanzador 
        { 
            get { return this.lanzador; }
            set { this.lanzador = value; }
        }

        //public int ReduccionRetardoBombas
        //{ 
        //    get { return this.reduccionRetardoBombas;}
        //    set { this.reduccionRetardoBombas = value; }
        //}

        public int UnidadesDeResistencia
        {
            get { return this.unidadesDeResistencia; }
            set { this.unidadesDeResistencia = value; }
        }

        public Punto Posicion
        {
            get { return this.posicion; }
            set { this.posicion = value; }
        }

        public bool LanzarExplosivo()
        {
            //return (this.lanzador.Lanzar(this.Posicion, this.reduccionRetardoBombas)); nuevo lanzador dejo esto por un tiempo
            //Apuntar();
            //this.Lanzador.Disparar();
            //el explosivo fue lanzado
           // return true;

            //Impedimos que los que atraviesan obstaculos pongan bombas sobre el
            if (Juego.Juego.Instancia().Ambiente.ObtenerCasilla(this.Posicion).PermiteExplosivos())
            {
                //ahora uso el lanzador para disparar
                this.Lanzador.Cargar(this);
                this.Lanzador.Disparar();
                //el explosivo fue lanzado
                return true;
            }
            else
            {
                //el explosivo no fue lanzado
                return false;
            }
        }

        public void Mover()
        {
            if (!(this.Destruido()))                //Resolucion para que los personajes no sean zombies ! 
                Juego.Juego.Instancia().Ambiente.Mover(this);
        }

        public virtual bool AtraviesaObstaculos()
        {
            return false;
        }

        public bool Destruido()
        {
            return ((this.unidadesDeResistencia) < 1);
        }

        public bool EsDaniable()
        {
            return true;
        }
        
        private int CalcularUnidadesRestantes(int unidadesDestruidas)
        {
            int unidades = (this.UnidadesDeResistencia - unidadesDestruidas);
            if (unidades < 0)
                return (0);
            else 
                return (unidades);
        }

        public virtual void DaniarConBombaMolotov(int UnidadesDaniadas)
        {
            this.DaniarSiNoEstaDestruido(UnidadesDaniadas);
        }
        public virtual void DaniarConBombaToleTole()
        {
            this.UnidadesDeResistencia = 0;
            //Juego.Juego.Instancia().Ambiente.DecrementarCantidadDePersonajesVivos();
        }

        public virtual void DaniarConProyectil(int UnidadesDaniadas)
        {
            this.DaniarSiNoEstaDestruido(UnidadesDaniadas);
        }

        private void DaniarSiNoEstaDestruido(int UnidadesDaniadas)
        {
            if (!this.Destruido())
            {
                this.UnidadesDeResistencia = CalcularUnidadesRestantes(UnidadesDaniadas);
                //if (this.UnidadesDeResistencia == 0) //SE MANEJA DESDE CLASE JUEGO
                //{
                //    Juego.Juego.Instancia().Ambiente.DecrementarCantidadDePersonajesVivos();
                //}
            }
        }
        
        public void DuplicarVelocidad()
        {
            this.movimiento.MultiplicarVelocidadPor(2);
        }

        public void CambiarLanzadorAToleTole()
        {
            this.lanzador = new LanzadorToleTole();
        }

        public void ReducirRetardo(int retardo)
        {
            this.Lanzador.RetardoExplosion = retardo;
        }

        //public abstract void PartidaGanada();


        public abstract void ReaccionarConArticulo(Articulo.Articulo articulo);
        
        //public IDaniable ObtenerDaniable()
        //{
        //    return this;
        //}
    }
}
