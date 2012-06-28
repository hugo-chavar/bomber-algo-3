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
using BombermanModel.Arma;
using BombermanModel;
using BombermanModel.Juego;

namespace BombermanGame
{
    class Bomb : ObjetoVivo
    {
        private Explosivo explosivo;

        public Bomb(Vector2 pos, Explosivo bomba)
            : base(pos)
        {
            //position.X = 64+ Game1.mapa.Location.X;
            //position.Y = 0 + Game1.mapa.Location.Y;

            explosivo = bomba;
            if (explosivo.Nombre == Nombres.molotov)
                spriteIndex = MapaVista.molotovSprite;
            else if (explosivo.Nombre == Nombres.toleTole)
                spriteIndex = MapaVista.toleToleSprite;
        }

        public override void LoadContent(ContentManager content)
        {
            position.X = 96 + Game1.mapa.Location.X;
            position.Y = Game1.mapa.Location.Y;
            // spriteIndex = content.Load<Texture2D>("Sprites\\" + spriteName);
            area = new Rectangle(0, 0, spriteIndex.Width, spriteIndex.Height);
            //area.X = (int)position.X - (spriteIndex.Width / 2);
            //area.Y = (int)position.Y - (spriteIndex.Height / 2);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //Rectangle size;
            Vector2 center = new Vector2(spriteIndex.Width / 2, spriteIndex.Height / 2);

            spriteBatch.Draw(spriteIndex, position, null, Color.White, MathHelper.ToRadians(rotation), center, scale, SpriteEffects.None, 0);
        }

        public override void Update()
        {
            if (!vivo) return;
            if (explosivo.EstaExplotado())
            {
                vivo = false;


                //PasilloView unPasillo = new PasilloView(position);
               // MapaVista.EliminarDibujable(this);
                //MapaVista.AgregarDibujable(unPasillo);
            }

        }

    }
}


