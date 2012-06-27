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
        
        public override Explosivo Disparar()
        {
            BombaToleTole bomba = null;
            if (Juego.Juego.Instancia().Ambiente.ObtenerCasilla(this.posicionDeTiro).Explosivo == null)
            {
                bomba = new BombaToleTole(this.posicionDeTiro, this.RetardoExplosion);
                Juego.Juego.Instancia().AlojarExplosivo(bomba);
            }
            return bomba;
        }
    }
}
