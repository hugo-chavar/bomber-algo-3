using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Excepciones;

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
        
        public Casilla FabricarCasillaConBloqueAcero(Punto pos)
        {
            Casilla unaCasilla = new Casilla(pos);
            unaCasilla.Estado = new BloqueAcero(pos);
            if (unaCasilla.Estado == null)
                throw new EstadoNuloException();
            return unaCasilla;
        }

        public Casilla FabricarCasillaConBloqueLadrillos(Punto pos)
        {
            Casilla unaCasilla = new Casilla(pos);
            unaCasilla.Estado = BloqueComun.CrearBloqueLadrillos(pos);
            if (unaCasilla.Estado == null)
                throw new EstadoNuloException();
            return unaCasilla;
        }

        public Casilla FabricarCasillaConBloqueCemento(Punto pos)
        {
            Casilla unaCasilla = new Casilla(pos);
            unaCasilla.Estado = BloqueComun.CrearBloqueCemento(pos);
            if (unaCasilla.Estado == null)
                throw new EstadoNuloException();
            return unaCasilla;
        }
    }
}
