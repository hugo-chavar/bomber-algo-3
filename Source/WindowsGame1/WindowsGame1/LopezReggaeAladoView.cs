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
using BombermanModel.Arma;
using BombermanModel;
using BombermanModel.Juego;
using BombermanModel.Mapa.Casilla;

namespace BombermanGame
{
    class LopezReggaeAladoView : ObjetoVivo
    {

        private Personaje unLRA = new LosLopezReggaeAlado(new Punto(10,10));
        public LopezReggaeAladoView(Vector2 pos)
            : base(pos)
        {
            speed = 0.3f;// unLRA.Movimiento.Velocidad;
            spriteName = "LRAlado";
        }


        //public float point_direction(float y, float x)
        //{
        //    float res = MathHelper.ToDegrees((float)Math.Atan2(y, x));
        //    res = (res - 180) % 360;
        //    if (res < 0)
        //        res += 360;
        //    return res;
        //}
        public override void LoadContent(ContentManager content)
        {
            position.X = 32*unLRA.Posicion.X + Game1.mapa.Location.X;
            position.Y = 32*unLRA.Posicion.Y + Game1.mapa.Location.Y;
            spriteIndex = content.Load<Texture2D>("Sprites\\" + spriteName);
            area = new Rectangle(0, 0, spriteIndex.Width, spriteIndex.Height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 center = new Vector2(spriteIndex.Width / 2, spriteIndex.Height / 2);
  
            spriteBatch.Draw(spriteIndex, position, null, Color.White, MathHelper.ToRadians(rotation), center, scale, SpriteEffects.None, 0);
            //spriteBatch.DrawString(Game1.fuente, "En modelo ->Pos X: " + unLRA.Posicion.X + " Pos Y: " + unLRA.Posicion.Y, new Vector2(10, 10), Color.Yellow);
        }

        public override void Update()
        {
            rotation = point_direction(position.X, position.Y, Juego.Instancia().Protagonista.Posicion.X * 32 + Game1.mapa.Location.X, Juego.Instancia().Protagonista.Posicion.Y * 32 + Game1.mapa.Location.Y);
            if (!vivo) return;
            PushTo(speed, rotation);
            base.Update();
        }

        public float point_direction(float x, float y, float x2, float y2)
        {
            float diffx = x - x2;
            float diffy = y - y2;
            float adj = diffx;
            float opp = diffy;
            float tan = opp / adj;
            float res = MathHelper.ToDegrees((float)Math.Atan2(opp, adj));
            res = (res - 180) % 360;
            if (res < 0)
                res += 360;
            return res;
        }

        public void PushTo(float pix, float dir)
        {
            float newX = (float)Math.Cos(MathHelper.ToRadians(dir));
            float newY = (float)Math.Sin(MathHelper.ToRadians(dir));
            position.X += pix * (float)newX; //
            position.Y += pix * (float)newY; //pix *
        }

                  
    }
}
