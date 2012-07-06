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
    class BombitaVista : PersonajeVista
    {
        KeyboardState prevKey;
        ContentManager miContent;

        public BombitaVista()
            : base(Juego.Instancia().Protagonista)
        {
            spriteName = "Bombita";
        }

        public override void LoadContent(ContentManager content)
        {
            miContent = content;
            this.UnPersonaje = Juego.Instancia().Protagonista;
            posicion.X = 32 * unPersonaje.Posicion.X + Game1.mapa.Location.X;
            posicion.Y = 32 * unPersonaje.Posicion.Y + Game1.mapa.Location.Y;
            spriteIndex = content.Load<Texture2D>("Sprites\\" + spriteName);
        }

        public override void Update()
        {
            IDaniable p = this.unPersonaje;

            if (p.Destruido())
            {
                this.Vivo = false;
                this.LoadContent(miContent);
                movido = Vector2.Zero;
                return;
            } else this.Vivo = true;
            
            if (!this.Vivo) return;

            keyboard = Keyboard.GetState();
            velocidad = unPersonaje.Movimiento.Velocidad;
            if (keyboard.IsKeyDown(Keys.P))
            {
                Juego.Instancia().Pausar();
                return;
            }

            if (keyboard.IsKeyDown(Keys.W))
            {
                //si esta mirando en el sentido que indica la tecla presionada avanzo
                if (direccion == Vector2.UnitY*-1) 
                {
                    Advance();
                }
                else //sino cambio al direccion hacia la indicada
                {
                    direccion = Vector2.UnitY*-1;
                    unPersonaje.Movimiento.Direccion = ABAJO;
                }
            }
            if (keyboard.IsKeyDown(Keys.A))
            {
                if (direccion == Vector2.UnitX * -1)
                {
                    Advance();
                }
                else
                {
                    direccion = Vector2.UnitX * -1;
                    unPersonaje.Movimiento.Direccion = IZQUIERDA;
                }
            }
            if (keyboard.IsKeyDown(Keys.D))
            {
                if (direccion == Vector2.UnitX )
                {
                    Advance();
                }
                else
                {
                    direccion = Vector2.UnitX;
                    unPersonaje.Movimiento.Direccion = DERECHA;
                }
            }
            if (keyboard.IsKeyDown(Keys.S))
            {
                if (direccion == Vector2.UnitY)
                {
                    Advance();
                }
                else
                {
                    direccion = Vector2.UnitY;
                    unPersonaje.Movimiento.Direccion = ARRIBA;
                }
            }

            if ((keyboard.IsKeyDown(Keys.Space)) && (!prevKey.IsKeyDown(Keys.Space)))
            {
              
            
                    this.Disparar();
               
                
            }

            prevKey = keyboard;
         
            rotacion = point_direction(-direccion.Y, -direccion.X);
            
        }
     
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!this.Vivo) return;
            Vector2 center = new Vector2(spriteIndex.Width / 2, spriteIndex.Height / 2);
  
            spriteBatch.Draw(spriteIndex, posicion, null, Color.White, MathHelper.ToRadians(rotacion), center, escala, SpriteEffects.None, 0);
            spriteBatch.DrawString(Game1.fuente2, "Vidas: " + Juego.Instancia().CantDeVidas, new Vector2(10, 10), Color.Yellow);
            spriteBatch.DrawString(Game1.fuente2, "Nivel: " + Juego.Instancia().Ambiente.NroNivel, new Vector2(250, 10), Color.Black);
            spriteBatch.DrawString(Game1.fuente2, "Stage: " + Juego.Instancia().Ambiente.StageName, new Vector2(450, 10), Color.Black);


        }

    }
}
