﻿using BombermanModel.Personaje;

namespace BombermanModel.Articulo
{
    public class Timer : Articulo
    {
        const int PORCENTAJEDERETRASO = 15;

        public Timer()
        {
            this.Activar();
        }

        public override void ModificarComedor(IComedor comedor)
        {
            comedor.ReducirRetardo(PORCENTAJEDERETRASO);
            this.EstaOculto = true;
        }
    }
}
