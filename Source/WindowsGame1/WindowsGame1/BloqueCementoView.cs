using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using BombermanModel.Personaje;
using BombermanModel;
using BombermanModel.Juego;
using BombermanModel.Mapa.Casilla;

namespace BombermanGame
{
    class BloqueCementoView : BloqueView
    {
        public BloqueCementoView(Vector2 pos, Punto posMapa, Obstaculo unObstaculo)
            : base(pos, posMapa, unObstaculo)
        {
            spriteName = "ObsCemento";
        }

    }


}
