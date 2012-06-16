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
        private bool ciudadLiberada;

        public bool CiudadLiberada
        {
            get { return this.ciudadLiberada; }
        }

        public Bombita(Punto unPunto) :base(unPunto) 
        {
            this.lanzador = new LanzadorMolotov();
            this.unidadesDeResistencia = VIDABOMBITA;
            this.ciudadLiberada = false;
        }
        
        public void Comer(IComible comible)
        {
            comible.ModificarComedor(this);
            comible.Ocultar();
        }

        public override void ReaccionarConArticulo(Articulo.Articulo articulo) 
        {
            this.Comer(articulo);
        }

        public void FinalizarNivel()
        {
            this.ciudadLiberada = true;
        }


    }
}
