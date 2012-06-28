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
    class PasilloView : ObjetoVivo
    {
        //private Obstaculo ladri = Juego.Instancia().Ambiente.ObtenerCasilla(new Punto(2, 0)).Estado;
        private Obstaculo pasilloModel;

        public PasilloView(Vector2 pos)
            : base(pos)
        {
            spriteName = "ObsPasillo";
            solido = true;
            spriteIndex = MapaVista.pasilloView;
            Punto unPunto = new Punto(0, 0);
            unPunto.X = ((int)(pos.X) - Game1.mapa.Location.X) / 32;
            unPunto.Y = ((int)(pos.Y) - Game1.mapa.Location.Y) / 32;
            pasilloModel = Juego.Instancia().Ambiente.ObtenerCasilla(unPunto).Estado;
        }

        public override void LoadContent(ContentManager content)
        {

            spriteIndex = content.Load<Texture2D>("Sprites\\" + spriteName);
            area = new Rectangle(0, 0, spriteIndex.Width, spriteIndex.Height);

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!vivo) return;
            Vector2 center = new Vector2(spriteIndex.Width / 2, spriteIndex.Height / 2);

            spriteBatch.Draw(spriteIndex, position, null, Color.White, MathHelper.ToRadians(rotation), center, scale, SpriteEffects.None, 0);


        }
    }
}
