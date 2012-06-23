using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BombermanModel.Personaje;

namespace BombermanModel.Articulo
{
    public abstract class Articulo : IComible
    {
        protected bool estaOculto;
        protected bool estaActivo;

        public abstract void ModificarComedor(IComedor comedor);

        public bool EstaOculto
        {
            get { return this.estaOculto; }
            set { this.estaOculto = value; }        
        }

        public bool EstaActivo
        {
            get { return this.estaActivo; }
            set { this.estaActivo = value; }
        }

        public void Ocultar()
        {
            this.EstaOculto = true;
        }

        public void Activar()
        {
            this.EstaActivo = true;
        }
    }
}
