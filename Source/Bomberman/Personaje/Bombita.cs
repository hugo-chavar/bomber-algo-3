using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Articulo;
using Bomberman.Arma;

namespace Bomberman.Personaje
{
    public class Bombita : Personaje
    {
        private const int VIDABOMBITA = 1;

        public Bombita(Punto unPunto) :base(unPunto) 
        {
            this.lanzador = new LanzadorMolotov();
            this.unidadesDeResistencia = VIDABOMBITA;
        }


        public override void Comer(IComible comible)
        {
            comible.ModificarComedor(this);
        }


        public override void DuplicarVelocidad()
        {
            this.Velocidad = (this.Velocidad * 2);
        }


        public override void CambiarLanzadorAToleTole()
        {
            this.lanzador = new LanzadorToleTole();
        }

        public override void ReducirRetardo(int retardo)
        {
            this.ReduccionRetardoBombas = retardo;
        }

    }
}
