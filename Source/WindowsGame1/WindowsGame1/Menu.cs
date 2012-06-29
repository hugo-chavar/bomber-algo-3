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
    class Menu
    {
        KeyboardState keyboard;
        KeyboardState prevKeyboard;
        MouseState mouse;
        MouseState prevMouse;

        SpriteFont fuente3;
        int seleccionado = 0;

        List<string> labelList = new List<string>();

        public Menu()
        {
            labelList.Add("Jugar");
            labelList.Add("Salir");
        }

        public void LoadContent(ContentManager content)
        {
            fuente3 = content.Load<SpriteFont>("Fonts\\Pericles12");
        }

        public void Update()
        {
            keyboard = Keyboard.GetState();
            mouse = Mouse.GetState();

            if (CheckKeyboard(Keys.Up))
            {
                if (seleccionado > 0) seleccionado--;
            }
            if (CheckKeyboard(Keys.Down))
            {
                if (seleccionado < labelList.Count -1) seleccionado++;
            }

            if (CheckKeyboard(Keys.Enter) || CheckKeyboard(Keys.Space))
            {
                switch (seleccionado)
                {
                    case 0:
                        Game1.estadoDelJuego = "Jugar";
                        break;
                    case 1:
                        Game1.estadoDelJuego = "Salir";
                        break;
                    //case 0:
                    //    break;
                    //case 0:
                    //    break;
                }
            }


            prevMouse = mouse;
            prevKeyboard = keyboard;

        }

        public bool CheckMouse()
        { 
            return(mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton == ButtonState.Released);
        }

        public bool CheckKeyboard(Keys key)
        { 
            return (keyboard.IsKeyDown(key) && !prevKeyboard.IsKeyDown(key));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < labelList.Count; i++)
            { 
                Color color;
                if (i == seleccionado) color = Color.Yellow; else color = Color.Black;
                spriteBatch.DrawString(Game1.fuente2, labelList[i], new Vector2(((Game1.screen.Width / 2) - (Game1.fuente2.MeasureString(labelList[i]).X/2)),
                    ((Game1.screen.Height / 2) - ((Game1.fuente2.LineSpacing * labelList.Count) / 2) + Game1.fuente2.LineSpacing * i)), color);
            }
        }

    }
}
