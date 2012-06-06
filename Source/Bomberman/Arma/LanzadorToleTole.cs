﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Mapa.Casilla;

namespace Bomberman.Arma
{
    public class LanzadorToleTole : ILanzador
    {
        public bool Lanzar(Punto posicion, int reduccionRetardo)
        {
            Casilla casilla = Juego.Juego.Instancia().Ambiente.ObtenerCasilla(posicion);
            if (casilla.Explosivo == null)
            {
                Bomba bomba = new BombaToleTole(posicion, reduccionRetardo);
                casilla.PlantarExplosivo(bomba);
                return (true);//Las bombas se ponen en la posicion del personaje
            }
            return (false);//casilla ya ocupada Con Bomba
        }
    }
}
