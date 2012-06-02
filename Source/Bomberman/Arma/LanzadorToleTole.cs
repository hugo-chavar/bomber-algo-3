using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.Arma
{
    public class LanzadorToleTole:ILanzador
    {
        public bool Lanzar(int x, int y, int reduccionRetardo)
        {
            Bomba bomba = new BombaToleTole(x, y, reduccionRetardo);
            //Deberia Modificar el mapa con agregando una bomba a un casillero
            //getMapa().ColocarPosicionable(bomba);
            return (true);//Las bombas se ponen en la posicion dl personaje
        }
    }
}
