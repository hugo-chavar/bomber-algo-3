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

namespace BombermanGame
{
    class Persona : ObjetoVivo
    {
        KeyboardState keyboard;
        private Vector2 direccion;
        

        public Persona(Vector2 pos)
            : base(pos)
        {
            position = pos;
            speed = 4;
            spriteName = "Cecilio";
        }

        public override void Update()
        {
            keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.W))
            {
                direccion.X = 0;
                direccion.Y = -1;
            }
            if (keyboard.IsKeyDown(Keys.A))
            {
                direccion.Y = 0;
                direccion.X = -1;
            }
            if (keyboard.IsKeyDown(Keys.D))
            {
                direccion.Y = 0;
                direccion.X = 1;
            }
            if (keyboard.IsKeyDown(Keys.S))
            {
                direccion.X = 0;
                direccion.Y = 1;
            }
            rotation = point_direction(-direccion.Y, -direccion.X);
            if (keyboard.IsKeyDown(Keys.F))
                position += direccion * speed;

            base.Update();
        }

        public float point_direction(float y, float x)
        {
            float res = MathHelper.ToDegrees((float)Math.Atan2(y, x));
            res = (res - 180) % 360;
            if (res < 0)
                res += 360;
            return res;


        }
    }
}
