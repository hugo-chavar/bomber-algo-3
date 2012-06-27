using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BombermanModel.Arma;

namespace BombermanModel.Personaje
{
    public class Cecilio : Enemigo
    {
        private const int VIDACECILIO = 5;
        
        public Cecilio(Punto unPunto)
            : base(unPunto)
        {
            this.Lanzador = new LanzadorMolotov();
            this.UnidadesDeResistencia = VIDACECILIO;
            this.Movimiento.Velocidad = 1;
            this.Nombre = Nombres.cecilio;
        }

    }
}
