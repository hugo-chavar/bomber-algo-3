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
        /*
        KeyboardState keyboard;
        private Vector2 direccion;
        private Personaje unPersonaje = Juego.Instancia().Protagonista;
        public const int ARRIBA = 8; 
        public const int ABAJO = 2;
        public const int IZQUIERDA = 4;
        public const int DERECHA = 6;
        */

        public BombitaView(Vector2 pos)
            : base(pos)
        {
            unPersonaje = Juego.Instancia().Protagonista;
            speed = unPersonaje.Movimiento.Velocidad;
            spriteName = "Bombita";
            unPersonaje.Posicion.X = 3;
        }

        public override void Update()
        {
            keyboard = Keyboard.GetState();
            speed = unPersonaje.Movimiento.Velocidad + 0.8f;
            if (keyboard.IsKeyDown(Keys.W))
            {
                if (direccion == Vector2.UnitY*-1) //pregunto si ya esta mirando en el sentido que aprete la tecla
                {
                    //if (!colision(0, -speed)) //si no choca avanza normalmente
                        Advance();
                    //else
                    //{
                    //    //si hay colision lo hago rebotar un paso
                    //    position -= direccion * speed;
                    //    movido -= direccion * speed;
                    //}
                }
                else
                {
                    direccion = Vector2.UnitY*-1;
                    unPersonaje.Movimiento.Direccion = ABAJO;
                }
            }
            if (keyboard.IsKeyDown(Keys.A))
            {
                if (direccion == Vector2.UnitX * -1)
                {
                    //if (!colision(-speed, 0))
                        Advance();
                    //else
                    //{
                    //    //si hay colision lo hago rebotar un paso
                    //    position -= direccion * speed;
                    //    movido -= direccion * speed;
                    //}
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
                    //if (!colision(speed, 0))
                        Advance();
                    //else
                    //{
                    //    //si hay colision lo hago rebotar un paso
                    //    position -= direccion * speed;
                    //    movido.X = spriteIndex.Width - 1;//direccion * speed;
                    //}
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
                    //if (!colision(0, speed))
                        Advance();
                    //else
                    //{
                    //    //si hay colision lo hago rebotar un paso
                    //    position -= direccion * speed;
                    //    movido.Y = spriteIndex.Width - 1;//direccion * speed;
                    //}
                }
                else
                {
                    direccion = Vector2.UnitY;
                    unPersonaje.Movimiento.Direccion = ARRIBA;
                }
            }

            if (keyboard.IsKeyDown(Keys.Space))
            {
                if (unPersonaje.LanzarExplosivo())
                {
                    Explosivo bomba=Juego.Instancia().Ambiente.ObtenerCasilla(unPersonaje.Posicion).Explosivo;
                    ListaVivos.objList.Add(new Bomb(position,bomba));
                }

            }
         
            rotation = point_direction(-direccion.Y, -direccion.X);
            
            base.Update();
        }
        /*
        private void Advance()
        {
            if (direccion.Y == Vector2.Zero.Y)
            {
                if ((movido.X == Vector2.Zero.X) || (movido.X == spriteIndex.Width - 1))
                {
                    if (!Juego.Instancia().Ambiente.PermitidoAvanzar(unPersonaje))
                        return;
                }
            }
            else if (direccion.X == Vector2.Zero.X)
            {
                if ((movido.Y == Vector2.Zero.Y)|| (movido.Y == spriteIndex.Height - 1)) //&& (direccion == Vector2.UnitY * -1)
                {
                    if (!Juego.Instancia().Ambiente.PermitidoAvanzar(unPersonaje))
                        return;
                }
            }

            corregirPosicion();
            position += direccion * speed;
            Vector2 deltaPrevio = new Vector2(movido.X,movido.Y);
            movido += direccion * speed;
            

            //considero que el personaje transita la casilla cuando ingreso un tercio de su cuerpo
            //cuando pasa 2/3 de su cuerpo pasa a la posicion siguiente (hablando en terminos del modelo)
            if (((Math.Abs(deltaPrevio.X) < (spriteIndex.Width / 3)) && (Math.Abs(movido.X) >= (spriteIndex.Width / 3)))
                || ((Math.Abs(deltaPrevio.Y) < (spriteIndex.Height / 3)) && (Math.Abs(movido.Y) >= (spriteIndex.Height / 3))))
            { 
                Punto unPto = new Punto(unPersonaje.Posicion.X+(int)direccion.X,unPersonaje.Posicion.Y + (int)direccion.Y);
                Juego.Instancia().Ambiente.ObtenerCasilla(unPto).Transitar(unPersonaje);
                
            }

            if ((Math.Abs(deltaPrevio.X) >= (spriteIndex.Width / 2)) && (Math.Abs(movido.X) < (spriteIndex.Width / 2)))
            {
                Punto unPto = new Punto(unPersonaje.Posicion.X + (int)direccion.X, unPersonaje.Posicion.Y + (int)direccion.Y);

                Juego.Instancia().Ambiente.Avanzar(unPersonaje);
            }


            if ((Math.Abs(deltaPrevio.Y) >= (spriteIndex.Height / 2)) && (Math.Abs(movido.Y) < (spriteIndex.Height / 2)))
            {
                Punto unPto = new Punto(unPersonaje.Posicion.X, unPersonaje.Posicion.Y + (int)direccion.Y);

                Juego.Instancia().Ambiente.Avanzar(unPersonaje);
            }

            if ((Math.Abs(deltaPrevio.X) >= (spriteIndex.Width / 3)) && (Math.Abs(deltaPrevio.X) < (spriteIndex.Width / 2)) && (Math.Abs(movido.X) >= (spriteIndex.Width / 2)))
            {
                Punto unPto = new Punto(unPersonaje.Posicion.X + (int)direccion.X, unPersonaje.Posicion.Y + (int)direccion.Y);
                
                Juego.Instancia().Ambiente.Avanzar(unPersonaje);
            }

            if ((Math.Abs(deltaPrevio.Y) >= (spriteIndex.Height / 3)) && (Math.Abs(deltaPrevio.Y) < (spriteIndex.Height / 2)) && (Math.Abs(movido.Y) >= (spriteIndex.Height / 2)))
            {
                Punto unPto = new Punto(unPersonaje.Posicion.X, unPersonaje.Posicion.Y + (int)direccion.Y);

                Juego.Instancia().Ambiente.Avanzar(unPersonaje);
            }

            if ((Math.Abs(deltaPrevio.X) >= (spriteIndex.Width / 3)) && (Math.Abs(movido.X) < (spriteIndex.Width / 3))) 
            {
                Punto unPto = new Punto(unPersonaje.Posicion.X - (int)direccion.X, unPersonaje.Posicion.Y);
                Juego.Instancia().Ambiente.ObtenerCasilla(unPto).Dejar(unPersonaje);
            }

            if ((Math.Abs(deltaPrevio.Y) >= (spriteIndex.Height / 3)) && (Math.Abs(movido.Y) < (spriteIndex.Height / 3)))
            {
                Punto unPto = new Punto(unPersonaje.Posicion.X, unPersonaje.Posicion.Y - (int)direccion.Y);
                Juego.Instancia().Ambiente.ObtenerCasilla(unPto).Dejar(unPersonaje);
            }

            if (movido.X >= spriteIndex.Width)
            {
                movido.X = 0;
            }

            if (movido.X < 0)
            {
                movido.X = spriteIndex.Width -1;
            }

            if (movido.Y >= spriteIndex.Height)
            {
                movido.Y = 0;
            }

            if (movido.Y < 0)
            {
                movido.Y = spriteIndex.Height - 1;
            }
        }

        public void corregirPosicion()
        {
            
            int dirPrev = unPersonaje.Movimiento.Direccion;
            if ((movido.X > 0) && movido.X <= (spriteIndex.Width - speed) / 2) 
            {
                unPersonaje.Movimiento.Direccion = DERECHA;
                if (!Juego.Instancia().Ambiente.PermitidoAvanzar(unPersonaje))
                {
                    position.X -= movido.X;
                    movido.X = Vector2.Zero.X;
                }
            }
            if ((movido.X < spriteIndex.Width) && (movido.X > (spriteIndex.Width - speed) / 2))
            {
                unPersonaje.Movimiento.Direccion = IZQUIERDA;
                if (!Juego.Instancia().Ambiente.PermitidoAvanzar(unPersonaje))
                {
                    position.X += (spriteIndex.Width- movido.X);
                    movido.X = Vector2.Zero.X;
                }
            }
            if (movido.Y > 0 && movido.Y <= (spriteIndex.Height - speed) / 2)
            {
                unPersonaje.Movimiento.Direccion = ARRIBA;
                if (!Juego.Instancia().Ambiente.PermitidoAvanzar(unPersonaje))
                {
                    position.Y -= movido.Y;
                    movido.Y = Vector2.Zero.Y;
                }
            }
            if ((movido.Y < spriteIndex.Height) && (movido.Y > (spriteIndex.Height - speed) / 2))
            {
                unPersonaje.Movimiento.Direccion = ABAJO;
                if (!Juego.Instancia().Ambiente.PermitidoAvanzar(unPersonaje))
                {
                    position.Y +=( spriteIndex.Height- movido.Y);
                    movido.Y = Vector2.Zero.Y;
                }
            }
            unPersonaje.Movimiento.Direccion = dirPrev;
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
            position.X = 32*unPersonaje.Posicion.X + Game1.mapa.Location.X;
            position.Y = 32*unPersonaje.Posicion.Y + Game1.mapa.Location.Y;
            spriteIndex = content.Load<Texture2D>("Sprites\\" + spriteName);
            area = new Rectangle(0, 0, spriteIndex.Width, spriteIndex.Height);
        }
        */
        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 center = new Vector2(spriteIndex.Width / 2, spriteIndex.Height / 2);
  
            spriteBatch.Draw(spriteIndex, position, null, Color.White, MathHelper.ToRadians(rotation), center, scale, SpriteEffects.None, 0);
            //spriteBatch.DrawString(Game1.fuente, "En modelo ->Pos X: " + unPersonaje.Posicion.X + " Pos Y: " + unPersonaje.Posicion.Y, new Vector2(10, 10), Color.Yellow);
            //spriteBatch.DrawString(Game1.fuente, "Mvido ->Pos X: " + movido.X + " Pos Y: " + movido.Y + " RealPos X: " + position.X + " Pos Y: " + position.Y, new Vector2(10, Game1.fuente.LineSpacing), Color.Yellow); 
        }
    }
}
