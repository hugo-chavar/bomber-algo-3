using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using BombermanModel.Arma;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using BombermanModel.Juego;
using BombermanModel;

namespace BombermanGame
{
    class ProyectilView : ObjetoVivo
    {
        private Proyectil explosivo;
        protected Vector2 direccion;
        public const int ARRIBA = 8;
        public const int ABAJO = 2;
        public const int IZQUIERDA = 4;
        public const int DERECHA = 6;
        
        public ProyectilView(Vector2 pos, Explosivo bomba)
            : base(pos)
        {
            //position.X = 64+ Game1.mapa.Location.X;
            //position.Y = 0 + Game1.mapa.Location.Y;

            explosivo = (Proyectil)bomba;
            spriteName = "Proyectil";
            direccion = Vector2.UnitX;
            speed = 0.001f;
        }

        public override void LoadContent(ContentManager content)
        {
            position.X = 96 + Game1.mapa.Location.X;
            position.Y = Game1.mapa.Location.Y;
            spriteIndex = content.Load<Texture2D>("Sprites\\" + spriteName);
            area = new Rectangle(0, 0, spriteIndex.Width, spriteIndex.Height);
            //area.X = (int)position.X - (spriteIndex.Width / 2);
            //area.Y = (int)position.Y - (spriteIndex.Height / 2);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //Rectangle size;
            Vector2 center = new Vector2(spriteIndex.Width / 2, spriteIndex.Height / 2);

            spriteBatch.Draw(spriteIndex, position, null, Color.White, MathHelper.ToRadians(rotation), center, scale, SpriteEffects.None, 0);
            spriteBatch.DrawString(Game1.fuente, "En modelo ->Pos X: " + explosivo.Posicion.X + " Pos Y: " + explosivo.Posicion.Y, new Vector2(10, 10), Color.Yellow);
            spriteBatch.DrawString(Game1.fuente, "Mvido ->Pos X: " + movido.X + " Pos Y: " + movido.Y + " RealPos X: " + position.X + " Pos Y: " + position.Y, new Vector2(10, Game1.fuente.LineSpacing), Color.Yellow); 
        }

        public override void Update()
        {
            if (explosivo.EstaExplotado())
            {
                vivo = false;


                //PasilloView unPasillo = new PasilloView(position);
                MapaVista.EliminarDibujable(this);
                //MapaVista.AgregarDibujable(unPasillo);
            }
            else 
            {
                rotation = point_direction(-direccion.Y, -direccion.X);
                Advance();
            }

        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * */
        /* OJO QUE ACA ESTOY DUPLICANDO BANDA DE CODIGO!!!!!!*/
        /* * * * * * * * * * * * * * * * * * * * * * * * * * */
        protected virtual void Advance()
        {
            if (direccion.Y == Vector2.Zero.Y)
            {
                if ((movido.X == Vector2.Zero.X) || (movido.X == spriteIndex.Width - 1))
                {
                    if (!Juego.Instancia().Ambiente.PermitidoAvanzar(explosivo))
                        return;
                }
            }
            else if (direccion.X == Vector2.Zero.X)
            {
                if ((movido.Y == Vector2.Zero.Y) || (movido.Y == spriteIndex.Height - 1)) //&& (direccion == Vector2.UnitY * -1)
                {
                    if (!Juego.Instancia().Ambiente.PermitidoAvanzar(explosivo))
                        return;
                }
            }

            corregirPosicion();
            position += direccion * speed;
            Vector2 deltaPrevio = new Vector2(movido.X, movido.Y);
            movido += direccion * speed;


            //considero que el personaje transita la casilla cuando ingreso un tercio de su cuerpo
            //cuando pasa 2/3 de su cuerpo pasa a la posicion siguiente (hablando en terminos del modelo)
            if (((Math.Abs(deltaPrevio.X) < (spriteIndex.Width / 3)) && (Math.Abs(movido.X) >= (spriteIndex.Width / 3)))
                || ((Math.Abs(deltaPrevio.Y) < (spriteIndex.Height / 3)) && (Math.Abs(movido.Y) >= (spriteIndex.Height / 3))))
            {
                Punto unPto = new Punto(explosivo.Posicion.X + (int)direccion.X, explosivo.Posicion.Y + (int)direccion.Y);
                Juego.Instancia().Ambiente.ObtenerCasilla(unPto).Transitar(explosivo);

            }

            if ((Math.Abs(deltaPrevio.X) >= (spriteIndex.Width / 2)) && (Math.Abs(movido.X) < (spriteIndex.Width / 2)))
            {
                Punto unPto = new Punto(explosivo.Posicion.X + (int)direccion.X, explosivo.Posicion.Y + (int)direccion.Y);

                Juego.Instancia().Ambiente.Avanzar(explosivo);
            }


            if ((Math.Abs(deltaPrevio.Y) >= (spriteIndex.Height / 2)) && (Math.Abs(movido.Y) < (spriteIndex.Height / 2)))
            {
                Punto unPto = new Punto(explosivo.Posicion.X, explosivo.Posicion.Y + (int)direccion.Y);

                Juego.Instancia().Ambiente.Avanzar(explosivo);
            }

            if ((Math.Abs(deltaPrevio.X) >= (spriteIndex.Width / 3)) && (Math.Abs(deltaPrevio.X) < (spriteIndex.Width / 2)) && (Math.Abs(movido.X) >= (spriteIndex.Width / 2)))
            {
                Punto unPto = new Punto(explosivo.Posicion.X + (int)direccion.X, explosivo.Posicion.Y + (int)direccion.Y);

                Juego.Instancia().Ambiente.Avanzar(explosivo);
            }

            if ((Math.Abs(deltaPrevio.Y) >= (spriteIndex.Height / 3)) && (Math.Abs(deltaPrevio.Y) < (spriteIndex.Height / 2)) && (Math.Abs(movido.Y) >= (spriteIndex.Height / 2)))
            {
                Punto unPto = new Punto(explosivo.Posicion.X, explosivo.Posicion.Y + (int)direccion.Y);

                Juego.Instancia().Ambiente.Avanzar(explosivo);
            }

            if ((Math.Abs(deltaPrevio.X) >= (spriteIndex.Width / 3)) && (Math.Abs(movido.X) < (spriteIndex.Width / 3)))
            {
                Punto unPto = new Punto(explosivo.Posicion.X - (int)direccion.X, explosivo.Posicion.Y);
                Juego.Instancia().Ambiente.ObtenerCasilla(unPto).Dejar(explosivo);
            }

            if ((Math.Abs(deltaPrevio.Y) >= (spriteIndex.Height / 3)) && (Math.Abs(movido.Y) < (spriteIndex.Height / 3)))
            {
                Punto unPto = new Punto(explosivo.Posicion.X, explosivo.Posicion.Y - (int)direccion.Y);
                Juego.Instancia().Ambiente.ObtenerCasilla(unPto).Dejar(explosivo);
            }

            if (movido.X >= spriteIndex.Width)
            {
                movido.X = 0;
            }

            if (movido.X < 0)
            {
                movido.X = spriteIndex.Width - 1;
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

            int dirPrev = explosivo.Movimiento.Direccion;
            if ((movido.X > 0) && movido.X <= (spriteIndex.Width - speed) / 2)
            {
                explosivo.Movimiento.Direccion = DERECHA;
                if (!Juego.Instancia().Ambiente.PermitidoAvanzar(explosivo))
                {
                    position.X -= movido.X;
                    movido.X = Vector2.Zero.X;
                }
            }
            if ((movido.X < spriteIndex.Width) && (movido.X > (spriteIndex.Width - speed) / 2))
            {
                explosivo.Movimiento.Direccion = IZQUIERDA;
                if (!Juego.Instancia().Ambiente.PermitidoAvanzar(explosivo))
                {
                    position.X += (spriteIndex.Width - movido.X);
                    movido.X = Vector2.Zero.X;
                }
            }
            if (movido.Y > 0 && movido.Y <= (spriteIndex.Height - speed) / 2)
            {
                explosivo.Movimiento.Direccion = ARRIBA;
                if (!Juego.Instancia().Ambiente.PermitidoAvanzar(explosivo))
                {
                    position.Y -= movido.Y;
                    movido.Y = Vector2.Zero.Y;
                }
            }
            if ((movido.Y < spriteIndex.Height) && (movido.Y > (spriteIndex.Height - speed) / 2))
            {
                explosivo.Movimiento.Direccion = ABAJO;
                if (!Juego.Instancia().Ambiente.PermitidoAvanzar(explosivo))
                {
                    position.Y += (spriteIndex.Height - movido.Y);
                    movido.Y = Vector2.Zero.Y;
                }
            }
            explosivo.Movimiento.Direccion = dirPrev;
        }

        public float point_direction(float y, float x)
        {
            float res = MathHelper.ToDegrees((float)Math.Atan2(y, x));
            res = (res - 180) % 360;
            if (res < 0)
                res += 360;
            return res;
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * */
        /* OJO QUE HASTA ACA ESTOY DUPLICANDO BANDA DE CODIGO*/
        /* * * * * * * * * * * * * * * * * * * * * * * * * * */


    }
}
