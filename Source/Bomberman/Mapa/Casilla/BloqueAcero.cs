﻿using BombermanModel;
namespace BombermanModel.Mapa.Casilla
{
    public class BloqueAcero : Obstaculo
    {
        private const int RESISTENCIAACERO = 10;
        public BloqueAcero()
            : base(RESISTENCIAACERO)
        { nombre = Nombres.bAcero; }

        public override void DaniarConBombaMolotov(int UnidadesDaniadas)
        {           
            // NO SE DANIA CON MOLOTOV!
        }

        public override void DaniarConProyectil(int UnidadesDaniadas)
        {
            // NO SE DANIA CON PROYECTIL!
        }
        
        public override bool PuedeContenerSalida()
        {
            return false;
        }
     }
}
