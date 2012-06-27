using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BombermanModel.Personaje;

namespace BombermanModel.Articulo
{
    public class Chala : Articulo
    {
        public Chala()
        {
            this.Activar();
        }

        public override void  ModificarComedor(IComedor comedor)
        {
            comedor.DuplicarVelocidad();
            this.EstaOculto = true;
        }
    }
}
