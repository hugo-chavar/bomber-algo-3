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
        public Bombita() :base() 
        {
            this.lanzador = new LanzadorMolotov();
        }

        public void setReduccionRetardoBombas(int PorcentajeRetardo)
        {
            this.reduccionRetardoBombas = PorcentajeRetardo;
        }

        public void setLanzadorToleTole()
        {
            this.lanzador = new LanzadorToleTole();
        }

        public void comer(IComible comible)
        {
            comible.modificarComedor(this);
        }

        public int Velocidad
        {
            get { return this.velocidad; }
            set { this.velocidad = Velocidad; }
        }


        public void duplicarVelocidad()
        {
            this.velocidad = this.velocidad * 2;
        }


        public void cambiarLanzadorAToleTole()
        {
            setLanzadorToleTole();
        }

        public void reducirRetardo(int retardo)
        {
            setReduccionRetardoBombas(retardo);
        }

    }
}
