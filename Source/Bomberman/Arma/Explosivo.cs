using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BombermanModel.Personaje;

namespace BombermanModel.Arma
{
    public abstract class Explosivo : IDependienteDelTiempo 
    {
        protected int ondaExpansiva;
        protected Punto posicion;
        protected bool exploto;
        protected int poderDeDestruccion;
        protected Nombres nombre;

        public Nombres Nombre
        {
            get { return this.nombre; }
            set { nombre = value; }
        }

        public Explosivo()
        {
            this.exploto = false;
        }

        public int OndaExpansiva
        {
            get{ return this.ondaExpansiva; }
            set { this.ondaExpansiva = value; }
        }

        public Punto Posicion
        {
            get { return this.posicion; }
            set { this.posicion = value; }
        }

        public int PoderDeDestruccion
        {
            get { return this.poderDeDestruccion; }
            set { this.poderDeDestruccion = value; }
        }

        public virtual void Explotar()
        {
            this.exploto = true;
            Juego.Juego.Instancia().Ambiente.ManejarExplosion(this);
        }

        public bool EstaExplotado()
        {
            return this.exploto;
        }

        public bool DejoDeDependerDelTiempo()
        {
            return this.exploto;
        }

        public abstract void Daniar(IDaniable daniable);

        public abstract void CuandoPasaElTiempo();

    }
}
