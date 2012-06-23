using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BombermanModel
{
    public interface IPosicionable
    {
        Punto Posicion
        {
            get;
            set;
        }
    }
}
