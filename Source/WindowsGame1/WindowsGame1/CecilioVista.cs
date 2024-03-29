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
using BombermanModel.Arma;
using BombermanModel;
using BombermanModel.Juego;
using BombermanModel.Mapa.Casilla;

namespace BombermanGame
{
    class CecilioVista : EnemigoVista
    {

        public CecilioVista(Personaje pers)
            : base(pers)
        {
            spriteName = "Ceci2";
        }

                       
        public override void LoadContent(ContentManager content)
        {
            posicion.X = 32 * unPersonaje.Posicion.X + Game1.mapa.Location.X;
            posicion.Y = 32 * unPersonaje.Posicion.Y + Game1.mapa.Location.Y;
            spriteIndex = content.Load<Texture2D>("Sprites\\" + spriteName);
            puntoCentro = new Vector2(spriteIndex.Width / 2, spriteIndex.Height / 2);
            CargarObjetivos();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!vivo) return;
            spriteBatch.Draw(spriteIndex, posicion, null, Color.White, MathHelper.ToRadians(rotacion), puntoCentro, escala, SpriteEffects.None, 0);    
        }

        public override void Update()
        {
            if (!vivo) return;
            IDaniable p = this.unPersonaje;
            if (p.Destruido())
            {
                this.Vivo = false;
            }
            if (!vivo) return;
            Random random = new Random();
            int vaADisparar = random.Next(0, 1000); // tiene un 2% de posibilidades de disparar
            if ((vaADisparar == 0))
                {
                    Disparar();
                }
            base.Update();
            
        }
      
    }
}
