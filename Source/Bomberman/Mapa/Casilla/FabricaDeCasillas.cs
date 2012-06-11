


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Excepciones;

namespace Bomberman.Mapa.Casilla
{
    public class FabricaDeCasillas
    {
        public static Casilla FabricarPasillo(Punto pos)
        {
            Casilla unaCasilla = new Casilla(pos);
            unaCasilla.Estado = new Pasillo();
            if (unaCasilla.Estado == null)
                throw new EstadoNuloException();
            return unaCasilla;
        }
        
        public static Casilla FabricarCasillaConBloqueAcero(Punto pos)
        {
            Casilla unaCasilla = new Casilla(pos);
            unaCasilla.Estado = new BloqueAcero();
            if (unaCasilla.Estado == null)
                throw new EstadoNuloException();
            return unaCasilla;
        }

        public static Casilla FabricarCasillaConBloqueLadrillos(Punto pos)
        {
            Casilla unaCasilla = new Casilla(pos);
            unaCasilla.Estado = BloqueComun.CrearBloqueLadrillos();
            if (unaCasilla.Estado == null)
                throw new EstadoNuloException();
            return unaCasilla;
        }

        public static Casilla FabricarCasillaConBloqueCemento(Punto pos)
        {
            Casilla unaCasilla = new Casilla(pos);
            unaCasilla.Estado = BloqueComun.CrearBloqueCemento();
            if (unaCasilla.Estado == null)
                throw new EstadoNuloException();
            return unaCasilla;
        }
    }
}
