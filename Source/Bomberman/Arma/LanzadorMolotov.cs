using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Juego;
using Bomberman.Mapa.Casilla;

namespace Bomberman.Arma
{
    public class LanzadorMolotov:ILanzador
    {
        public bool Lanzar(Punto posicion, int reduccionRetardo)
        {
            Bomba bomba = new BombaMolotov(posicion,reduccionRetardo);
            Juego.Juego.Instancia().Ambiente.ObtenerCasilla(posicion).PlantarExplosivo(bomba);
            return (true);//Las bombas se ponen en la posicion del personaje
            // QUE ES ESE TRUE?
        }
    }
}
