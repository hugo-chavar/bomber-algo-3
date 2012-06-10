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
            //this.sentido = new Movimiento();
        }

        public override void Disparar()
        {
            //Juego.Juego.Instancia().Ambiente.ObtenerCasilla(this.posicionDeTiro).PlantarExplosivo(new BombaMolotov(this.posicionDeTiro, this.RetardoExplosion));
            Juego.Juego.Instancia().AlojarExplosivo(new BombaMolotov(this.posicionDeTiro, this.RetardoExplosion));
        }
    }
}
