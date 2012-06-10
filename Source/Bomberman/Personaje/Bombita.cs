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
            comible.Ocultar();
        }

        public override void ReaccionarConArticulo(Articulo.Articulo articulo)
        {
            this.Comer(articulo);
        }

        public override void PartidaGanada()
        {
            Juego.Juego.Instancia().Ambiente.FinalizarNivel(); // Revisar esto!
        }


    }
}
