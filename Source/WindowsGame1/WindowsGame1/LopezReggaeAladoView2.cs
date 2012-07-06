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
    class LopezReggaeAladoView2 : EnemigoVista
    {

        private Vector2 puntoCentro;


        public LopezReggaeAladoView2(Personaje pers) 
            : base(pers)
        {
            spriteName = "LRA";
        }

        public override void LoadContent(ContentManager content)
        {
            posicion.X = 32*unPersonaje.Posicion.X + Game1.mapa.Location.X;
            posicion.Y = 32*unPersonaje.Posicion.Y + Game1.mapa.Location.Y;
            spriteIndex = content.Load<Texture2D>("Sprites\\" + spriteName);
            puntoCentro = new Vector2(spriteIndex.Width / 2, spriteIndex.Height / 2);
            CargarObjetivos();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!vivo) return;
            spriteBatch.Draw(spriteIndex, posicion, null, Color.White, MathHelper.ToRadians(rotacion), puntoCentro, escala, SpriteEffects.None, 0);
            //spriteBatch.DrawString(Game1.fuente, "En modelo ->Pos X: " + unLRA.Posicion.X + " Pos Y: " + unLRA.Posicion.Y, new Vector2(10, 10), Color.Yellow);
        }

        public override void Update()
        {
            if (!vivo) return;
            IDaniable p = this.unPersonaje;
            if (p.Destruido())
            {
                this.Vivo = false;
            }
            rotacion = point_direction(posicion.X, posicion.Y, destinosObjetivo[destinoActual].X * 32 + Game1.mapa.Location.X, destinosObjetivo[destinoActual].Y * 32 + Game1.mapa.Location.Y);
            
            Random random = new Random();
            int vaADisparar = random.Next(0, 1000); 
            if ((vaADisparar == 0))
                Disparar();

            PushTo(velocidad, rotacion);
            RecalcularPosicion();
            ActualizarPosicion();

           //base.Update();//LRA se comporta diferente que los demas
        }

        private void RecalcularPosicion()
        {
            int x = (int)Math.Round((posicion.X - Game1.mapa.Location.X) / 32, 0);
            int y = (int)Math.Round((posicion.Y - Game1.mapa.Location.Y) / 32, 0);

            this.unPersonaje.Posicion.X = x;
            this.unPersonaje.Posicion.Y = y;

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
            posicion.X += pix * (float)newX;
            posicion.Y += pix * (float)newY; 
        }

                  
    }
}
