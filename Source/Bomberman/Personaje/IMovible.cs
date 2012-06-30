using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BombermanModel.Personaje
{
    public interface IMovible : IPosicionable 
    {
        void Mover();
        bool AtraviesaObstaculos();
        bool ImpactaEnObstaculos();
        Movimiento Movimiento
        {
            get;
            set;
        }

        Nombres Nombre
        {
            get;
        }

        void ReaccionarConArticulo(Articulo.Articulo articulo);

        bool EsDaniable();

        void Colisionar();
    }
}
