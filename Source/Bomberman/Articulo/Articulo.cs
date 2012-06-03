﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Personaje;

namespace Bomberman.Articulo
{
    public abstract class Articulo : IComible
    {   
        protected bool estaOculto;

        public Articulo()
        {
            estaOculto = true;
        }

        public abstract void ModificarComedor(IComedor comedor);

        public bool EstaOculto
        {
            set { this.estaOculto = EstaOculto; }
            get { return this.estaOculto; }
        }

    }
}
