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
    class Persona : ObjetoVivo
    {
        KeyboardState keyboard;
        private Vector2 direccion;
        private Personaje bombita = Juego.Instancia().Protagonista;
        public const int ARRIBA = 8; //<--quedo harcodeado nomas..jaja
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
            speed = bombita.Movimiento.Velocidad;
            if (keyboard.IsKeyDown(Keys.W))
            {
                if (direccion == Vector2.UnitY*-1) //pregunto si ya esta mirando en el sentido que aprete la tecla
                {
                    if (!colision(0, -speed)) //si no choca avanza normalmente
                        Advance();
                    else
                    {
                        //si hay colision lo hago rebotar un paso
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
                        //si hay colision lo hago rebotar un paso
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
                        //si hay colision lo hago rebotar un paso
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
                    if (!colision(0, speed))
                        Advance();
                    else
                    {
                        //si hay colision lo hago rebotar un paso
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

            if (keyboard.IsKeyDown(Keys.Space))
            {
                if (bombita.LanzarExplosivo())
                {
                    Explosivo bomba=Juego.Instancia().Ambiente.ObtenerCasilla(bombita.Posicion).Explosivo;
                    ListaVivos.objList.Add(new Bomb(position,bomba));
                }

            }
         
            rotation = point_direction(-direccion.Y, -direccion.X);

            base.Update();
        }

        private void Advance()
        {
            //Vector2 margen = new Vector2(movido.X, movido.Y);
            //if (margen.)
            //margen.Normalize(); //&& (margen = Vector2.UnitX )
            if (((movido.X <= speed * direccion.X*-1) && (movido.X >= Vector2.Zero.X) && (direccion == Vector2.UnitX * -1)) || ((movido.X <= spriteIndex.Width) && (movido.X >= (spriteIndex.Width - speed * direccion.X)) && (direccion == Vector2.UnitX))
                || ((movido.Y <= speed * direccion.Y) && (movido.Y >= Vector2.Zero.Y) && (direccion == Vector2.UnitY * -1)) || ((movido.Y <= spriteIndex.Height) && (movido.Y >= (spriteIndex.Height - speed * direccion.Y)) && (direccion == Vector2.UnitY)))
                if (!Juego.Instancia().Ambiente.PermitidoAvanzar(bombita))
                    return;

            position += direccion * speed;
            Vector2 deltaPrevio = new Vector2(movido.X,movido.Y);
            movido += direccion * speed;

            //considero que el personaje transita la casilla cuando ingreso un tercio de su cuerpo
            //cuando pasa 2/3 de su cuerpo pasa a la posicion siguiente (hablando en terminos del modelo)
            if (((Math.Abs(deltaPrevio.X) < (spriteIndex.Width / 3)) && (Math.Abs(movido.X) >= (spriteIndex.Width / 3)))
                || ((Math.Abs(deltaPrevio.Y) < (spriteIndex.Height / 3)) && (Math.Abs(movido.X) >= (spriteIndex.Height / 3))))
            { 
                Punto unPto = new Punto(bombita.Posicion.X+(int)direccion.X,bombita.Posicion.Y + (int)direccion.Y);
                Juego.Instancia().Ambiente.ObtenerCasilla(unPto).Transitar(bombita);
                
            }

            if ((Math.Abs(deltaPrevio.X) >= (spriteIndex.Width / 2)) && (Math.Abs(movido.X) < (spriteIndex.Width / 2)))
            {
                Punto unPto = new Punto(bombita.Posicion.X + (int)direccion.X, bombita.Posicion.Y + (int)direccion.Y);

                Juego.Instancia().Ambiente.Avanzar(bombita);
            }


            if ((Math.Abs(deltaPrevio.Y) >= (spriteIndex.Height / 2)) && (Math.Abs(movido.Y) < (spriteIndex.Height / 2)))
            {
                Punto unPto = new Punto(bombita.Posicion.X, bombita.Posicion.Y + (int)direccion.Y);

                Juego.Instancia().Ambiente.Avanzar(bombita);
            }

            if ((Math.Abs(deltaPrevio.X) >= (spriteIndex.Width / 3)) && (Math.Abs(deltaPrevio.X) < (spriteIndex.Width / 2)) && (Math.Abs(movido.X) >= (spriteIndex.Width / 2)))
            {
                Punto unPto = new Punto(bombita.Posicion.X + (int)direccion.X, bombita.Posicion.Y + (int)direccion.Y);
                
                Juego.Instancia().Ambiente.Avanzar(bombita);
            }

            if ((Math.Abs(deltaPrevio.Y) >= (spriteIndex.Height / 3)) && (Math.Abs(deltaPrevio.Y) < (spriteIndex.Height / 2)) && (Math.Abs(movido.Y) >= (spriteIndex.Height / 2)))
            {
                Punto unPto = new Punto(bombita.Posicion.X, bombita.Posicion.Y + (int)direccion.Y);

                Juego.Instancia().Ambiente.Avanzar(bombita);
            }

            if ((Math.Abs(deltaPrevio.X) >= (spriteIndex.Width / 3)) && (Math.Abs(movido.X) < (spriteIndex.Width / 3))) 
            {
                Punto unPto = new Punto(bombita.Posicion.X - (int)direccion.X, bombita.Posicion.Y);
                Juego.Instancia().Ambiente.ObtenerCasilla(unPto).Dejar(bombita);
            }

            if ((Math.Abs(deltaPrevio.Y) >= (spriteIndex.Height / 3)) && (Math.Abs(movido.Y) < (spriteIndex.Height / 3)))
            {
                Punto unPto = new Punto(bombita.Posicion.X, bombita.Posicion.Y - (int)direccion.Y);
                Juego.Instancia().Ambiente.ObtenerCasilla(unPto).Dejar(bombita);
            }

            if (Math.Abs(movido.X) > spriteIndex.Width )
            {
                movido.X = 0;
            }
            if (Math.Abs(movido.Y) > spriteIndex.Height )
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
