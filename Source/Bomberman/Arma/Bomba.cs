using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BombermanModel.Arma
{
    public abstract class Bomba : Explosivo
    {
        protected DateTime horaDePlantado;
        protected float tiempoExplosion;

        public DateTime HoraDePlantado
        {
            get { return this.horaDePlantado; }
            set { this.horaDePlantado = value; }
        }

        public Bomba(Punto unaPosicion)
            : base()
        {
            posicion = unaPosicion;
            horaDePlantado = DateTime.Now;
        }

        public abstract override void Daniar(IDaniable daniable);

        public override void CuandoPasaElTiempo()
        {
            /*this.DisminuirTiempo();
            if (0 >= this.TiempoRestante)
            {
                base.Explotar();
            } */

            DateTime horaActual = DateTime.Now;
            if (horaActual.Subtract(horaDePlantado).Seconds >= tiempoExplosion)
            {
                base.Explotar();
            }
        }
    }

}
