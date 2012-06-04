using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.Arma
{
    public class LanzadorToleTole : ILanzador
    {
        public bool Lanzar(Punto posicion, int reduccionRetardo)
        {
            Bomba bomba = new BombaToleTole(posicion, reduccionRetardo);
            Juego.Juego.Instancia().Ambiente.ObtenerCasilla(posicion).PlantarExplosivo(bomba);
            return (true);//Las bombas se ponen en la posicion del personaje
        }
    }
}
