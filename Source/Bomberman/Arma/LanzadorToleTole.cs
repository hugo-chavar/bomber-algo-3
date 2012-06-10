using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Personaje;
using Bomberman.Mapa.Casilla;

namespace Bomberman.Arma
{
    public class LanzadorToleTole : Lanzador
    {

        private const int ALCANCELANZAMIENTO = 0;

        public LanzadorToleTole()
        {
            this.Alcance = ALCANCELANZAMIENTO;
            //this.sentido = new Movimiento();
        }
        
        public override void Disparar()
        {
            //Juego.Juego.Instancia().Ambiente.ObtenerCasilla(this.posicionDeTiro).PlantarExplosivo(new BombaToleTole(this.posicionDeTiro, this.RetardoExplosion));
            if (Juego.Juego.Instancia().Ambiente.ObtenerCasilla(this.posicionDeTiro).Explosivo == null)
            Juego.Juego.Instancia().AlojarExplosivo(new BombaToleTole(this.posicionDeTiro, this.RetardoExplosion));
        }
    }
}
