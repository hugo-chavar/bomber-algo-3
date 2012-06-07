using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Personaje;

namespace Bomberman.Arma
{
    public class LanzadorToleTole : ILanzador
    {
                
        public override bool Lanzar(Punto posicion, int reduccionRetardo)
        {
            Bomba bomba = new BombaToleTole(posicion, reduccionRetardo);
            Juego.Juego.Instancia().Ambiente.ObtenerCasilla(posicion).PlantarExplosivo(bomba);
            return (true);//Las bombas se ponen en la posicion del personaje
            // QUE ES ESE TRUE?
        }
    }
}
