using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.Articulo
{
    class Salida : Articulo
    {
        public override void ModificarComedor(Personaje.IComedor comedor)
        {
            if (Juego.Juego.Instancia().Ambiente.CantidadPersonajesVivos == Juego.Juego.Instancia().Ambiente.ObtenerCantidadPersonajes())
            {
                comedor.PartidaGanada();
            }
        }
    }
}
