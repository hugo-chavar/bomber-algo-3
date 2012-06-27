using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BombermanModel.Personaje;

namespace BombermanModel.Articulo
{
    public class ArticuloBombaToleTole : Articulo
    {
        public ArticuloBombaToleTole()
        {
            this.Activar();
            nombre = Nombres.arToleTole;
        }
        
        public override void ModificarComedor(IComedor comedor)
        {
            comedor.CambiarLanzadorAToleTole();
            this.Ocultar();
        }
    }
}
