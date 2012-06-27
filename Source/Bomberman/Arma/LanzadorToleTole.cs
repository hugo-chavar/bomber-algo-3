using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BombermanModel.Personaje;
using BombermanModel.Mapa.Casilla;

namespace BombermanModel.Arma
{
    public class LanzadorToleTole : Lanzador
    {

        private const int ALCANCELANZAMIENTO = 0;

        public LanzadorToleTole()
        {
            this.Alcance = ALCANCELANZAMIENTO;
        }
        
        public override void Disparar()
        {
            if (Juego.Juego.Instancia().Ambiente.ObtenerCasilla(this.posicionDeTiro).Explosivo == null)
            Juego.Juego.Instancia().AlojarExplosivo(new BombaToleTole(this.posicionDeTiro, this.RetardoExplosion));
        }
    }
}
