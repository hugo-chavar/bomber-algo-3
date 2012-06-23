using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BombermanModel.Personaje;

namespace BombermanModel.Mapa.Casilla
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
        }

        public override bool PuedeContenerSalida()
        {
            return true;
        }

        public override bool PuedeAgregarArticulo()
        {
            return false;
        }

        public override bool PermiteDejarExplosivos()
        {
            return true;
        }

    }
}
