using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.Mapa.Casilla
{
    public class FabricaDeCasillas
    {
        public Casilla FabricarPasillo(Punto pos)
        {
            Casilla unaCasilla = new Casilla(pos);
            unaCasilla.Estado = new Pasillo();
            return unaCasilla;
        }
    }
}
