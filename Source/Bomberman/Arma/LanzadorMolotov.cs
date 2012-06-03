using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.Arma
{
    public class LanzadorMolotov:ILanzador
    {
        public bool Lanzar(Punto posicion, int reduccionRetardo)
        {
            Bomba bomba = new BombaMolotov(posicion,reduccionRetardo);
            //Deberia Modificar el mapa con agregando una bomba a un casillero
            //getMapa().ColocarPosicionable(bomba);
            return (true);//Las bombas se ponen en la posicion dl personaje
        }
    }
}
