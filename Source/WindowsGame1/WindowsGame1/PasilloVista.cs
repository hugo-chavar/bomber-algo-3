﻿using System;
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
    class PasilloVista : ObjetoVivo //BloqueView
    {

        public PasilloVista(Vector2 pos)
            : base(pos)
        {
            spriteName = "ObsPasillo";
            vivo = false;
        }

        public override void LoadContent(ContentManager content)
        {

            spriteIndex = content.Load<Texture2D>("Sprites\\" + spriteName);
            vivo = true;
        }

        public override void Update()
        {

        }



        public override void Draw(SpriteBatch spriteBatch)
        {

            if (!vivo) return;
            Vector2 center = new Vector2(spriteIndex.Width / 2, spriteIndex.Height / 2);

            spriteBatch.Draw(spriteIndex, posicion, null, Color.White, MathHelper.ToRadians(rotacion), center, escala, SpriteEffects.None, 0);


        }

    }
}
