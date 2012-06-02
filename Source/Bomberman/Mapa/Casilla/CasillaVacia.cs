using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Personaje;

namespace Bomberman.Mapa.Casilla
{
    class CasillaVacia : Casilla
    {
        public override bool transitablePor(IMovible movil)
        {
            return true;
        }

        public override void daniarConBombaToleTole()
        { 
            //Deberia daniar a los personajes alli presentes
        }
        public override void daniarConBombaMolotov()
        {
            //Deberia daniar a los personajes alli presentes
        }

        //public abstract void daniarConProyectil();
    }
}
