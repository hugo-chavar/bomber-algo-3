using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.Arma
{
    public abstract class Explosivo
    {
        protected int ondaExpansiva;
        protected Punto posicion;
        protected bool exploto;

        public Explosivo()
        {
            this.exploto = false;
        }

        public int OndaExpansiva
        {
            get{return this.ondaExpansiva;}
            set{this.ondaExpansiva=value;}
        }

        public Punto Posicion
        {
            get { return this.posicion; }
            set { this.posicion = value; }
        }

        public virtual void Explotar()
        {
            ManejadorExplosiones manejador = new ManejadorExplosiones(this);
            this.exploto = true; // true = EXPLOTADO , false = ACTIVADA Y NO EXPLOTADA, //
            //getMapa().QuitarPosicionable(bomba);
        }

        public bool EstaExplotada()
        {
            return this.exploto;
        }

        public abstract void Daniar(IDaniable daniable);
    }
}
