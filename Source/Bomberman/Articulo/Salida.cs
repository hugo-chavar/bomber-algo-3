using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.Articulo
{
    class Salida : Articulo
    {
        public Salida()
        {
            this.estaActivo = false;
        }
        
        public override void ModificarComedor(Personaje.IComedor comedor)
        {
            if (this.EstaActivo)
            {
                comedor.PartidaGanada();
            }
        }
    }
}
