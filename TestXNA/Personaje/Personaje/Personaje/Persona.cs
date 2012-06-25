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

namespace Personaje
{
    public class Persona : ObjetoVivo
    {
        KeyboardState keyboard;
        KeyboardState prevKeyBoard;
        MouseState mouse;
        MouseState prevMouse;

        public Persona(Vector2 pos): base(pos)
        {
            position = pos;
            speed = 2;
        }

        public override void Update()
        {
            keyboard = Keyboard.GetState();
            mouse = Mouse.GetState();
            if (keyboard.IsKeyDown(Keys.W))
            {
                position.Y -= speed;
            }
            if (keyboard.IsKeyDown(Keys.A))
            {
                position.X -= speed;
            }
            if (keyboard.IsKeyDown(Keys.D))
            {
                position.X += speed;
            }
            if (keyboard.IsKeyDown(Keys.S))
            {
                position.Y += speed;
            }

            rotation = point_direction(position.X, position.Y, mouse.X,mouse.Y);

            prevKeyBoard = keyboard;
            prevMouse = mouse;
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
    }
}
