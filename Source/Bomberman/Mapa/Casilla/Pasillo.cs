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
            // TODO: Complete member initialization
            //this.unaPosicion = unaPosicion;
        }
        public override bool TransitablePor(IMovible movil)
        {
            return true;
        }

        public override void DaniarConBombaToleTole()
        { 
            //Deberia daniar a los personajes alli presentes
        }
        public override void DaniarConBombaMolotov()
        {
            //Deberia daniar a los personajes alli presentes
        }
        public override void DaniarConProyectil()
        {
            
        }

        //public abstract void daniarConProyectil();
    }
}