using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BombermanModel;
using BombermanModel.Juego;
using BombermanModel.Arma;

namespace BombermanModel.Personaje
{
    public abstract class Personaje : IMovible , IDaniable
    {
        protected Movimiento movimiento;
        protected Punto posicion;
        protected Lanzador lanzador;
        protected int unidadesDeResistencia;
        private Nombres nombre;

        private const float VELOCIDADMAX = 4f;

        public Personaje(Punto unPunto)
        {
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

        public Nombres Nombre
        {
            get { return this.nombre;}
            set { this.nombre = value;}
        }
        public Explosivo LanzarExplosivo()
        {
            Explosivo bomba = null;
            //Impedimos que los que atraviesan obstaculos pongan bombas sobre el
            if (Juego.Juego.Instancia().Ambiente.ObtenerCasilla(this.Posicion).PermiteExplosivos())
            {
                //ahora uso el lanzador para disparar
                this.Lanzador.Cargar(this);
                bomba = this.Lanzador.Disparar();
                //el explosivo fue lanzado
                return bomba;
            }
            else
            {
                //el explosivo no fue lanzado
                return null;
            }
        }

        public Punto PosicionDestino()
        {
            return this.Posicion.PosicionHaciaUnaDireccion(this.Movimiento.Direccion);
        }

        public void Mover()
        {
            if (!(this.Destruido()) && Juego.Juego.Instancia().Ambiente.PermitidoAvanzar(this))
                Juego.Juego.Instancia().Ambiente.Avanzar(this);
        }

        public virtual bool AtraviesaObstaculos()
        {
            return false;
        }

        public bool ImpactaEnObstaculos()
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
            }
        }
        
        public void DuplicarVelocidad()
        {
            if (this.Movimiento.Velocidad < VELOCIDADMAX)
            {
                this.movimiento.MultiplicarVelocidadPor(2);
            }
        }

        public void CambiarLanzadorAToleTole()
        {
            this.lanzador = new LanzadorToleTole();
        }

        public void ReducirRetardo(int retardo)
        {
            this.Lanzador.RetardoExplosion = (this.Lanzador.RetardoExplosion + retardo);
        }

        public void Colisionar()
        {

        }

        public abstract void ReaccionarConArticulo(Articulo.Articulo articulo);
        
    }
}
