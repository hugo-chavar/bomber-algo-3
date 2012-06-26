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
    class CecilioView : ObjetoVivo
    {
        private Vector2 direccion;
        private Personaje unCecilio = new Cecilio(new Punto(10, 5));
        public const int ARRIBA = 8;
        public const int ABAJO = 2;
        public const int IZQUIERDA = 4;
        public const int DERECHA = 6;
        private Vector2 puntoCentro;
        private int destinoActual = 0;
        private List<Point> destinosObjetivo = new List<Point>();
        private List<Vector2> versores = new List<Vector2>();

        public CecilioView(Vector2 pos)
            : base(pos)
        {
            speed = unCecilio.Movimiento.Velocidad;
            spriteName = "Cecilio";
            versores.Add(new Vector2(0, -1));
            versores.Add(new Vector2(-1, 0));
            versores.Add(new Vector2(1, 0));
            versores.Add(new Vector2(0, 1));
            Random random = new Random();
            int calculadorDirecciones = random.Next(0, 4);
            direccion = versores.ElementAt(calculadorDirecciones);
            unCecilio.Movimiento.Direccion = ((calculadorDirecciones+1)*2);
        }


        //public float point_direction(float y, float x)
        //{
        //    float res = MathHelper.ToDegrees((float)Math.Atan2(y, x));
        //    res = (res - 180) % 360;
        //    if (res < 0)
        //        res += 360;
        //    return res;
        //}
        private void CargarObjetivos()
        {
            Random direccionRandom = new Random();
            //ingreso una lista al azar de 10 objetivos de Cecilio
            for (int i = 0; i < 10; i++)
            {
                int x = direccionRandom.Next(Juego.Instancia().Ambiente.DimensionHorizontal);
                int y = direccionRandom.Next(Juego.Instancia().Ambiente.DimensionVertical);
                destinosObjetivo.Add(new Point(x, y));
            }
        }

        private void ProximoDestino()
        {
            //si fue a todos los destinos empieza de nuevo
            if (destinoActual == destinosObjetivo.Count - 1) destinoActual = 0;
            else destinoActual++;
        }


        public override void LoadContent(ContentManager content)
        {
            position.X = 32 * unCecilio.Posicion.X + Game1.mapa.Location.X;
            position.Y = 32 * unCecilio.Posicion.Y + Game1.mapa.Location.Y;
            spriteIndex = content.Load<Texture2D>("Sprites\\" + spriteName);
            area = new Rectangle(0, 0, spriteIndex.Width, spriteIndex.Height);
            puntoCentro = new Vector2(spriteIndex.Width / 2, spriteIndex.Height / 2);
            CargarObjetivos();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {


            spriteBatch.Draw(spriteIndex, position, null, Color.White, MathHelper.ToRadians(rotation), puntoCentro, scale, SpriteEffects.None, 0);
            //spriteBatch.DrawString(Game1.fuente, "En modelo ->Pos X: " + unLRA.Posicion.X + " Pos Y: " + unLRA.Posicion.Y, new Vector2(10, 10), Color.Yellow);
        }

        public override void Update()
        {
            rotation = point_direction(-direccion.Y, -direccion.X);
            if (Juego.Instancia().Ambiente.PermitidoAvanzar(unCecilio))
            {
                Advance();
            }
            else
            {
                while (!Juego.Instancia().Ambiente.PermitidoAvanzar(unCecilio))
                {
                    Random random = new Random();
                    int calculadorDirecciones = random.Next(0, 4);
                    direccion = versores.ElementAt(calculadorDirecciones);
                    switch ((calculadorDirecciones + 1) * 2)
                    {
                        case ABAJO: unCecilio.Movimiento.CambiarAAbajo();
                                    break;

                        case IZQUIERDA: unCecilio.Movimiento.CambiarAIzquierda();
                                        break;

                        case DERECHA: unCecilio.Movimiento.CambiarADerecha();
                                      break;

                        case ARRIBA: unCecilio.Movimiento.CambiarAArriba();
                                     break;
                    }
                }
                Advance();
            }
            

            if (!vivo) return;

            //Advance();

            ActualizarPosicion();
            base.Update();
        }

        private void Advance()
        {
            if (direccion.Y == Vector2.Zero.Y)
            {
                if ((movido.X == Vector2.Zero.X) || (movido.X == spriteIndex.Width - 1))
                {
                    if (!Juego.Instancia().Ambiente.PermitidoAvanzar(unCecilio))
                        return;
                }
            }
            else if (direccion.X == Vector2.Zero.X)
            {
                if ((movido.Y == Vector2.Zero.Y) || (movido.Y == spriteIndex.Height - 1)) //&& (direccion == Vector2.UnitY * -1)
                {
                    if (!Juego.Instancia().Ambiente.PermitidoAvanzar(unCecilio))
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
                Punto unPto = new Punto(unCecilio.Posicion.X + (int)direccion.X, unCecilio.Posicion.Y + (int)direccion.Y);
                Juego.Instancia().Ambiente.ObtenerCasilla(unPto).Transitar(unCecilio);

            }

            if ((Math.Abs(deltaPrevio.X) >= (spriteIndex.Width / 2)) && (Math.Abs(movido.X) < (spriteIndex.Width / 2)))
            {
                Punto unPto = new Punto(unCecilio.Posicion.X + (int)direccion.X, unCecilio.Posicion.Y + (int)direccion.Y);

                Juego.Instancia().Ambiente.Avanzar(unCecilio);
            }


            if ((Math.Abs(deltaPrevio.Y) >= (spriteIndex.Height / 2)) && (Math.Abs(movido.Y) < (spriteIndex.Height / 2)))
            {
                Punto unPto = new Punto(unCecilio.Posicion.X, unCecilio.Posicion.Y + (int)direccion.Y);

                Juego.Instancia().Ambiente.Avanzar(unCecilio);
            }

            if ((Math.Abs(deltaPrevio.X) >= (spriteIndex.Width / 3)) && (Math.Abs(deltaPrevio.X) < (spriteIndex.Width / 2)) && (Math.Abs(movido.X) >= (spriteIndex.Width / 2)))
            {
                Punto unPto = new Punto(unCecilio.Posicion.X + (int)direccion.X, unCecilio.Posicion.Y + (int)direccion.Y);

                Juego.Instancia().Ambiente.Avanzar(unCecilio);
            }

            if ((Math.Abs(deltaPrevio.Y) >= (spriteIndex.Height / 3)) && (Math.Abs(deltaPrevio.Y) < (spriteIndex.Height / 2)) && (Math.Abs(movido.Y) >= (spriteIndex.Height / 2)))
            {
                Punto unPto = new Punto(unCecilio.Posicion.X, unCecilio.Posicion.Y + (int)direccion.Y);

                Juego.Instancia().Ambiente.Avanzar(unCecilio);
            }

            if ((Math.Abs(deltaPrevio.X) >= (spriteIndex.Width / 3)) && (Math.Abs(movido.X) < (spriteIndex.Width / 3)))
            {
                Punto unPto = new Punto(unCecilio.Posicion.X - (int)direccion.X, unCecilio.Posicion.Y);
                Juego.Instancia().Ambiente.ObtenerCasilla(unPto).Dejar(unCecilio);
            }

            if ((Math.Abs(deltaPrevio.Y) >= (spriteIndex.Height / 3)) && (Math.Abs(movido.Y) < (spriteIndex.Height / 3)))
            {
                Punto unPto = new Punto(unCecilio.Posicion.X, unCecilio.Posicion.Y - (int)direccion.Y);
                Juego.Instancia().Ambiente.ObtenerCasilla(unPto).Dejar(unCecilio);
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
            int dirPrev = unCecilio.Movimiento.Direccion;
            if ((movido.X > 0) && movido.X <= (spriteIndex.Width - speed) / 2)
            {
                unCecilio.Movimiento.Direccion = DERECHA;
                if (!Juego.Instancia().Ambiente.PermitidoAvanzar(unCecilio))
                {
                    position.X -= movido.X;
                    movido.X = Vector2.Zero.X;
                }
            }
            if ((movido.X < spriteIndex.Width) && (movido.X > (spriteIndex.Width - speed) / 2))
            {
                unCecilio.Movimiento.Direccion = IZQUIERDA;
                if (!Juego.Instancia().Ambiente.PermitidoAvanzar(unCecilio))
                {
                    position.X += (spriteIndex.Width - movido.X);
                    movido.X = Vector2.Zero.X;
                }
            }
            if (movido.Y > 0 && movido.Y <= (spriteIndex.Height - speed) / 2)
            {
                unCecilio.Movimiento.Direccion = ARRIBA;
                if (!Juego.Instancia().Ambiente.PermitidoAvanzar(unCecilio))
                {
                    position.Y -= movido.Y;
                    movido.Y = Vector2.Zero.Y;
                }
            }
            if ((movido.Y < spriteIndex.Height) && (movido.Y > (spriteIndex.Height - speed) / 2))
            {
                unCecilio.Movimiento.Direccion = ABAJO;
                if (!Juego.Instancia().Ambiente.PermitidoAvanzar(unCecilio))
                {
                    position.Y += (spriteIndex.Height - movido.Y);
                    movido.Y = Vector2.Zero.Y;
                }
            }
            unCecilio.Movimiento.Direccion = dirPrev;
        }

        public float point_direction(float y, float x)
        {
            float res = MathHelper.ToDegrees((float)Math.Atan2(y, x));
            res = (res - 180) % 360;
            if (res < 0)
                res += 360;
            return res;
        }
        
        public void ActualizarPosicion()
        {
            int x = ((int)Math.Round(position.X, 0) - Game1.mapa.Location.X) / 32;
            int y = ((int)Math.Round(position.Y, 0) - Game1.mapa.Location.Y) / 32;
            
            /*
            Juego.Instancia().Ambiente.ObtenerCasilla(unCecilio.Posicion).Dejar(unCecilio);
            unCecilio.Posicion = new Punto(x, y);
            Juego.Instancia().Ambiente.ObtenerCasilla(unCecilio.Posicion).Transitar(unCecilio);
             */

            if ((destinosObjetivo[destinoActual].X == x) && (destinosObjetivo[destinoActual].Y == y))
                ProximoDestino();
        }
        
                
        /*
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
            position.X += pix * (float)newX;
            position.Y += pix * (float)newY;
        }
        */

    }
}
