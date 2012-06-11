﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Personaje;

namespace Bomberman.Mapa
{
    interface ITransitable
    {
        bool PermitidoMoverHaciaArribaA(IMovible movil);
        bool PermitidoMoverHaciaAbajoA(IMovible movil);
        bool PermitidoMoverHaciaIzquierdaA(IMovible movil);
        bool PermitidoMoverHaciaDerechaA(IMovible movil);
    }
}
