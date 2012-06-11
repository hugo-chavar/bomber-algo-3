using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Juego;
using Bomberman.Mapa.Casilla;
using Bomberman.Personaje;

namespace Bomberman.Arma
{
    public class LanzadorMolotov:Lanzador
    {

        private const int ALCANCELANZAMIENTO = 0;

        public LanzadorMolotov()
        {
            this.Alcance = ALCANCELANZAMIENTO;
        }

        public override void Disparar()
        {
            if(Juego.Juego.Instancia().Ambiente.ObtenerCasilla(this.posicionDeTiro).Explosivo == null)
            Juego.Juego.Instancia().AlojarExplosivo(new BombaMolotov(this.posicionDeTiro, this.RetardoExplosion));
        }
    }
}
