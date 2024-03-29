﻿using BombermanModel.Personaje;

namespace BombermanModel.Mapa.Casilla
{
    public class BloqueComun:Obstaculo
    {
        private const int RESISTENCIALADRILLO = 5;
        private const int RESISTENCIACEMENTO = 10;

        private BloqueComun(int UnidadesResistencia)
            : base(UnidadesResistencia)
        { }

        public static BloqueComun CrearBloqueCemento()
        { 
            BloqueComun BloqueCemento = new BloqueComun(RESISTENCIACEMENTO);
            BloqueCemento.nombre = Nombres.bCemento;
            return (BloqueCemento);
        }

        public static BloqueComun CrearBloqueLadrillos()
        {
            BloqueComun BloqueLadrillos = new BloqueComun(RESISTENCIALADRILLO);
            BloqueLadrillos.nombre = Nombres.bLadrillo;
            return (BloqueLadrillos);
        }

        public override bool PuedeContenerSalida()
        {
            return true;
        }

        public bool EsBloqueCemento()
        {
            return (UnidadesDeResistencia == RESISTENCIACEMENTO);
        }

    }
}
