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
        }
        
        public override bool Lanzar(Punto posicion, int reduccionRetardo)
        {
            Casilla casilla = Juego.Juego.Instancia().Ambiente.ObtenerCasilla(posicion);
            if (casilla.Explosivo == null)
            {
                Bomba bomba = new BombaToleTole(posicion, reduccionRetardo);
                casilla.PlantarExplosivo(bomba);
                return (true); //Las bombas se ponen en la posicion del personaje
            }
            return (false); //Casilla ya ocupada Con Bomba
        }
    }
}
