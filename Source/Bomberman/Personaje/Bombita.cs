using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Articulo;
using Bomberman.Arma;

namespace Bomberman.Personaje
{
    public class Bombita : Personaje , IComible 
    {
        private const int VIDABOMBITA = 1;

        public Bombita(Punto unPunto) :base(unPunto) 
        {
            this.lanzador = new LanzadorMolotov();
            this.unidadesDeResistencia = VIDABOMBITA;
        }

        public void SetReduccionRetardoBombas(int PorcentajeRetardo)
        {
            this.reduccionRetardoBombas = PorcentajeRetardo;
        }

        public void SetLanzadorToleTole()
        {
            this.lanzador = new LanzadorToleTole();
        }

        public override void Comer(IComible comible)
        {
            comible.ModificarComedor(this);
        }

        public int Velocidad
        {
            get { return this.velocidad; }
            set { this.velocidad = Velocidad; }
        }


        public override void DuplicarVelocidad()
        {
            this.velocidad = this.velocidad * 2;
        }


        public override void CambiarLanzadorAToleTole()
        {
            SetLanzadorToleTole();
        }

        public override void ReducirRetardo(int retardo)
        {
            SetReduccionRetardoBombas(retardo);
        }


        public void ModificarComedor(IComedor comedor)
        {
            // ACA TENEMOS QUE MATAR A BOMBITA!
        }
    }
}
