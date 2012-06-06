﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Personaje;

namespace Bomberman.Articulo
{
    public interface IComible
    {
        void ModificarComedor(IComedor comedor);

        void Ocultar();
    }
}
