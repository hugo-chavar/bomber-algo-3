using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.Arma
{
    public class LanzadorMolotov:ILanzador
    {
        public bool lanzar(int x, int y, int reduccionRetardo)
        {
            Bomba bomba = new BombaMolotov(x, y,reduccionRetardo);
            //Deberia Modificar el mapa con agregando una bomba a un casillero
            //Juego.getInstacia().Nivel.Mapa.ColocarPosicionable(bomba);
            return (true);//Las bombas se ponen en la posicion dl personaje
        }
    }
}
