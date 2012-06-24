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

namespace BombermanGame
{
    class Persona : ObjetoVivo
    {
        KeyboardState keyboard;
        private Vector2 direccion;
        private Personaje bombita = Juego.Instancia().Protagonista;
        public const int ARRIBA = 8;
        public const int ABAJO = 2;
        public const int IZQUIERDA = 4;
        public const int DERECHA = 6;
        

        public Persona(Vector2 pos)
            : base(pos)
        {
            speed = bombita.Movimiento.Velocidad;
            spriteName = "Cecilio";
        }

        public override void Update()
        {
            keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.W))
            {
                if (direccion == Vector2.UnitY*-1)
                {
                    if (!colision(0, -speed))
                        Advance();
                    else
                    {
                        position -= direccion * speed;
                        movido -= direccion * speed;
                    }
                }
                else
                {
                    direccion = Vector2.UnitY*-1;
                    bombita.Movimiento.Direccion = ABAJO;
                }
            }
            if (keyboard.IsKeyDown(Keys.A))
            {
                if (direccion == Vector2.UnitX * -1)
                {
                    if (!colision(-speed, 0))
                        Advance();
                    else
                    {
                        position -= direccion * speed;
                        movido -= direccion * speed;
                    }

                }
                else
                {
                    direccion = Vector2.UnitX * -1;
                    bombita.Movimiento.Direccion = IZQUIERDA;
                }
            }
            if (keyboard.IsKeyDown(Keys.D))
            {
                if (direccion == Vector2.UnitX )
                {
                    if (!colision(speed, 0))
                        Advance();
                    else
                    {
                        position -= direccion * speed;
                        movido -= direccion * speed;
                    }
                }
                else
                {
                    direccion = Vector2.UnitX;
                    bombita.Movimiento.Direccion = DERECHA;
                }
            }
            if (keyboard.IsKeyDown(Keys.S))
            {
                if (direccion == Vector2.UnitY)
                {
                    if (!colision(-speed, 0))
                        Advance();
                    else
                    {
                        position -= direccion * speed;
                        movido -= direccion * speed;
                    }
                }
                else
                {
                    direccion = Vector2.UnitY;
                    bombita.Movimiento.Direccion = ARRIBA;
                }
            }
         
            rotation = point_direction(-direccion.Y, -direccion.X);

            base.Update();
        }

        private void Advance()
        {
           
            position += direccion * speed;
            movido += direccion * speed;

            if (movido.X > 32 || movido.X < -32)
                movido.X = 0;
            if (movido.Y > 32 || movido.Y < -32)
                movido.Y = 0;
        }

        public float point_direction(float y, float x)
        {
            float res = MathHelper.ToDegrees((float)Math.Atan2(y, x));
            res = (res - 180) % 360;
            if (res < 0)
                res += 360;
            return res;


        }
        public override void LoadContent(ContentManager content)
        {
            position.X = bombita.Posicion.X + Game1.mapa.Location.X;
            position.Y = bombita.Posicion.Y + Game1.mapa.Location.Y;
            spriteIndex = content.Load<Texture2D>("Sprites\\" + spriteName);
            area = new Rectangle(0, 0, spriteIndex.Width, spriteIndex.Height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 center = new Vector2(spriteIndex.Width / 2, spriteIndex.Height / 2);
  
            spriteBatch.Draw(spriteIndex, position, null, Color.White, MathHelper.ToRadians(rotation), center, scale, SpriteEffects.None, 0);
            spriteBatch.DrawString(Game1.fuente, "En modelo ->Pos X: " + bombita.Posicion.X + " Pos Y: " + bombita.Posicion.Y, new Vector2(10, 10), Color.Yellow);
            spriteBatch.DrawString(Game1.fuente, "Mvido ->Pos X: " + movido.X + " Pos Y: " + movido.Y, new Vector2(10, Game1.fuente.LineSpacing), Color.Yellow); 


        }
    }
}
