using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Personaje;

namespace Bomberman.Mapa.Casilla
{
    public class Pasillo : Obstaculo
    {
        public Pasillo()
        { }

        public override bool TransitablePor(IMovible movil)
        {
            return true;
        }

        public override void DaniarConBombaToleTole()
        { 
           
        }
        public override void DaniarConBombaMolotov(int UnidadesDaniadas)
        {
            
        }
        public override void DaniarConProyectil(int UnidadesDaniadas)
        {
            //Deberia daniar a los personajes alli presentes
        }

        public override bool PuedeContenerSalida()
        {
            return true;
        }

        public override bool PuedeAgregarArticulo()
        {
            return false;
        }

    }
}
