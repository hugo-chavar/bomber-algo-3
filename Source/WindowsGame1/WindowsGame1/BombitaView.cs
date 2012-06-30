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
    class BombitaView : PersonajeView
    {
        KeyboardState prevKey;

        public BombitaView()
            : base(Juego.Instancia().Protagonista)
        {
            speed = unPersonaje.Movimiento.Velocidad;
            spriteName = "Bombita";
        }

        public override void LoadContent(ContentManager content)
        {
            this.UnPersonaje = Juego.Instancia().Protagonista;
            position.X = 32 * unPersonaje.Posicion.X + Game1.mapa.Location.X;
            position.Y = 32 * unPersonaje.Posicion.Y + Game1.mapa.Location.Y;
            spriteIndex = content.Load<Texture2D>("Sprites\\" + spriteName);
        }

        public override void Update()
        {

            if (!this.Vivo) return;
            IDaniable p = this.unPersonaje;
            if (p.Destruido())
            {
                this.Vivo = false;
            }
            
            keyboard = Keyboard.GetState();
            speed = unPersonaje.Movimiento.Velocidad;
            if (keyboard.IsKeyDown(Keys.P))
            {
                Game1.estadoDelJuego = "Pausa";
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
         
            rotation = point_direction(-direccion.Y, -direccion.X);
            
            base.Update();
        }
     
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!this.Vivo) return;
            Vector2 center = new Vector2(spriteIndex.Width / 2, spriteIndex.Height / 2);
  
            spriteBatch.Draw(spriteIndex, position, null, Color.White, MathHelper.ToRadians(rotation), center, scale, SpriteEffects.None, 0);
            spriteBatch.DrawString(Game1.fuente, "En modelo ->Pos X: " + unPersonaje.Posicion.X + " Pos Y: " + unPersonaje.Posicion.Y, new Vector2(10, 10), Color.Yellow);
            spriteBatch.DrawString(Game1.fuente, "Mvido ->Pos X: " + Math.Round(movido.X, 1) + " Pos Y: " + Math.Round(movido.Y, 1) + " RealPos X: " + Math.Round(position.X,1) + " Pos Y: " + Math.Round(position.Y,1), new Vector2(10, Game1.fuente.LineSpacing), Color.Yellow);
            spriteBatch.DrawString(Game1.fuente, "Vida " + Juego.Instancia().Protagonista.UnidadesDeResistencia, new Vector2(10, 2 * Game1.fuente.LineSpacing), Color.Yellow);

            List<Punto> l = new List<Punto>();
            
            for (int vertical = 0; vertical < Juego.Instancia().Ambiente.DimensionVertical; vertical++)
            {
                for (int horizontal = 0; horizontal < Juego.Instancia().Ambiente.DimensionHorizontal; horizontal++)
                {
                    Punto p = new Punto(horizontal, vertical);
                    Casilla unaCas = Juego.Instancia().Ambiente.ObtenerCasilla(p);
                    if (unaCas.TransitaEnCasillaUn(new Bombita(new Punto(7, 2))))
                    {
                        l.Add(p);
                    }

                }
            }
            spriteBatch.DrawString(Game1.fuente, "Bombita transitando en: ", new Vector2(580, 10), Color.Black);
            for (int j = 0;j<l.Count;j++)
            {
                spriteBatch.DrawString(Game1.fuente, "X: "+l[j].X+" Y: "+l[j].Y, new Vector2(600, (25 + Game1.fuente2.LineSpacing * j)), Color.Black);
            }

        }

    }
}
