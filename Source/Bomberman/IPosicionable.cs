using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman
{
    public interface IPosicionable
    {
        //declaro la propiedad posicion
        Punto Posicion
        {
            get;
            set;
        }

    }
}
