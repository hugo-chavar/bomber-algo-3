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
            this.sentido = new Movimiento();
        }
        
        /*public override bool Lanzar(Punto posicion, int reduccionRetardo) //Hugo dice: dejo comentado esto a efectos didácticos lo reemplacé por Disparar()
        {
            Casilla casilla = Juego.Juego.Instancia().Ambiente.ObtenerCasilla(posicion);
            if (casilla.Explosivo == null)
            {
                Bomba bomba = new BombaToleTole(posicion, reduccionRetardo);
                casilla.PlantarExplosivo(bomba);
                return (true); //Las bombas se ponen en la posicion del personaje
            }
            return (false); //Casilla ya ocupada Con Bomba
        }*/ 
        public override void Disparar()
        {
            Juego.Juego.Instancia().Ambiente.ObtenerCasilla(this.posicionDeImpacto).PlantarExplosivo(new BombaToleTole(this.posicionDeImpacto, this.RetardoExplosion));
        }
    }
}
