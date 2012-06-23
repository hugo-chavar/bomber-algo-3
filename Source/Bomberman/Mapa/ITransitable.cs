using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BombermanModel.Personaje;

namespace BombermanModel.Mapa
{
    interface ITransitable
    {
        bool PermitidoMoverHaciaArribaA(IMovible movil);
        bool PermitidoMoverHaciaAbajoA(IMovible movil);
        bool PermitidoMoverHaciaIzquierdaA(IMovible movil);
        bool PermitidoMoverHaciaDerechaA(IMovible movil);
    }
}
