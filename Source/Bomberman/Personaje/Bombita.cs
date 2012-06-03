using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Articulo;
using Bomberman.Arma;

namespace Bomberman.Personaje
{
    public class Bombita : Personaje , IComedor
    {
        public Bombita(Punto unPunto) :base(unPunto) 
        {
            this.lanzador = new LanzadorMolotov();
        }

        public void SetReduccionRetardoBombas(int PorcentajeRetardo)
        {
            this.reduccionRetardoBombas = PorcentajeRetardo;
        }

        public void SetLanzadorToleTole()
        {
            this.lanzador = new LanzadorToleTole();
        }

        public void Comer(IComible comible)
        {
            comible.ModificarComedor(this);
        }

        public int Velocidad
        {
            get { return this.velocidad; }
            set { this.velocidad = Velocidad; }
        }


        public void DuplicarVelocidad()
        {
            this.velocidad = this.velocidad * 2;
        }


        public void CambiarLanzadorAToleTole()
        {
            SetLanzadorToleTole();
        }

        public void ReducirRetardo(int retardo)
        {
            SetReduccionRetardoBombas(retardo);
        }

    }
}
