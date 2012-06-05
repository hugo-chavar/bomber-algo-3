﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Personaje;

namespace Bomberman.Mapa.Casilla
{
    public class Pasillo : Obstaculo
    {
        public Pasillo()
        {

        }
        public override bool TransitablePor(IMovible movil)
        {
            return true;
        }

        public override void DaniarConBombaToleTole()
        { 
            //Deberia daniar a los personajes alli presentes
        }
        public override void DaniarConBombaMolotov(int UnidadesDaniadas)
        {
            //Deberia daniar a los personajes alli presentes
        }
        public override void DaniarConProyectil(int UnidadesDaniadas)
        {
            
        }

        public override bool PuedeContenerArticulos()
        {
            return true;
        }

    }
}
