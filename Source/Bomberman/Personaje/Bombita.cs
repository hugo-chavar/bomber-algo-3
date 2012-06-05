using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Articulo;
using Bomberman.Arma;

namespace Bomberman.Personaje
{
    public class Bombita : Personaje, IComedor
    {
        private const int VIDABOMBITA = 1;

        public Bombita(Punto unPunto) :base(unPunto) 
        {
            this.lanzador = new LanzadorMolotov();
            this.unidadesDeResistencia = VIDABOMBITA;
        }


        public void Comer(IComible comible)
        {
            comible.ModificarComedor(this);
        }


        public void DuplicarVelocidad()
        {
            this.movimiento.MultiplicarVelocidadPor(2);
        }


        public void CambiarLanzadorAToleTole()
        {
            this.lanzador = new LanzadorToleTole();
        }

        public void ReducirRetardo(int retardo)
        {
            this.ReduccionRetardoBombas = retardo;
        }


    }
}
