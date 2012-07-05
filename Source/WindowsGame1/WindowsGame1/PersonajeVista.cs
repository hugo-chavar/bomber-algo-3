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
    public abstract class PersonajeVista : ObjetoVivo
    {
        protected KeyboardState keyboard;
        protected Vector2 direccion;
        protected Personaje unPersonaje;
        protected BombaVista unaBmb;
        public const int ARRIBA = 8;
        public const int ABAJO = 2;
        public const int IZQUIERDA = 4;
        public const int DERECHA = 6;
        Vector2 centro;
        Vector2 puntoCentro;

        public PersonajeVista(Personaje pers)
            :base(MapaVista.TransformarPuntoEnVector2(pers.Posicion))
        {
            unPersonaje = pers;
            speed = unPersonaje.Movimiento.Velocidad;
            position = MapaVista.TransformarPuntoEnVector2(unPersonaje.Posicion);
            movido = Vector2.Zero;
            unaBmb = null;
        }

        public Vector2 Direccion { get { return this.direccion; } set { this.direccion = value; } }

        public Personaje UnPersonaje { set { this.unPersonaje = value;} }

        public virtual void Disparar()
        {
            unaBmb = (BombaVista)MapaVista.ObtenerObjetoContundente(new BombaVista());
            if (unaBmb != null)
            {
                Explosivo bomba = unPersonaje.LanzarExplosivo();
                if (bomba != null)
                {
                    unaBmb.Explosivo = bomba;
                    unaBmb.setSpriteName();
                    unaBmb.Posicion = MapaVista.TransformarPuntoEnVector2(unPersonaje.Posicion);
                    unaBmb.Vivo = true;
                }
            }
                
        }

        protected virtual void Advance()
        {
            if (direccion.Y == Vector2.Zero.Y)
            {
                if ((Math.Round(movido.X, 1) == Vector2.Zero.X) || (Math.Round(movido.X, 1) == spriteIndex.Width)) 
                {
                    if (!Juego.Instancia().Ambiente.PermitidoAvanzar(unPersonaje))
                        return;
                }
            }
            else if (direccion.X == Vector2.Zero.X)
            {
                if ((Math.Round(movido.Y, 1) == Vector2.Zero.Y) || ((Math.Round(movido.Y, 1) == spriteIndex.Height))) 
                {
                    if (!Juego.Instancia().Ambiente.PermitidoAvanzar(unPersonaje))
                        return;
                }
            }

            corregirPosicion();
            position += direccion * speed;

            puntoCentro = position + new Vector2(spriteIndex.Width / 2, spriteIndex.Height / 2);
            Punto ptEnMapa = MapaVista.TransformarVector2EnPunto(puntoCentro);
           
            Vector2 deltaPrevio = new Vector2(movido.X, movido.Y);
            movido += direccion * speed;
            
            Juego.Instancia().Ambiente.ObtenerCasilla(unPersonaje.Posicion).Dejar(unPersonaje);
            unPersonaje.Posicion = ptEnMapa;
            Juego.Instancia().Ambiente.AgregarPersonaje(unPersonaje);

            //considero que el personaje transita la casilla cuando ingreso un tercio de su cuerpo
            //cuando pasa 2/3 de su cuerpo pasa a la posicion siguiente (hablando en terminos del modelo)
            //if (((Math.Abs(deltaPrevio.X) < (spriteIndex.Width / 3)) && (Math.Abs(movido.X) >= (spriteIndex.Width / 3)) && (Math.Abs(movido.X) < 2*(spriteIndex.Width / 3)))
            //    || ((Math.Abs(deltaPrevio.Y) < (spriteIndex.Height / 3)) && (Math.Abs(movido.Y) >= (spriteIndex.Height / 3)) && (Math.Abs(movido.Y) < 2*(spriteIndex.Height / 3))))
            //{
            //    Punto unPto = new Punto(unPersonaje.Posicion.X + (int)direccion.X, unPersonaje.Posicion.Y + (int)direccion.Y);
            //    Punto puntoAnterior = new Punto(unPersonaje.Posicion.X, unPersonaje.Posicion.Y);
            //    Juego.Instancia().Ambiente.ObtenerCasilla(puntoAnterior).Dejar(unPersonaje);
            //    Juego.Instancia().Ambiente.ObtenerCasilla(unPto).Transitar(unPersonaje);

            //}

            //if ((Math.Abs(deltaPrevio.X) >= (spriteIndex.Width / 2)) && (Math.Abs(movido.X) < (spriteIndex.Width / 2)))
            //{
            //    Punto unPto = new Punto(unPersonaje.Posicion.X + (int)direccion.X, unPersonaje.Posicion.Y + (int)direccion.Y);
            //    Punto puntoAnterior = new Punto(unPersonaje.Posicion.X, unPersonaje.Posicion.Y);
            //    Juego.Instancia().Ambiente.ObtenerCasilla(puntoAnterior).Dejar(unPersonaje);
            //    Juego.Instancia().Ambiente.Avanzar(unPersonaje);
            //}


            //if ((Math.Abs(deltaPrevio.Y) >= (spriteIndex.Height / 2)) && (Math.Abs(movido.Y) < (spriteIndex.Height / 2)))
            //{
            //    Punto unPto = new Punto(unPersonaje.Posicion.X, unPersonaje.Posicion.Y + (int)direccion.Y);
            //    Punto puntoAnterior = new Punto(unPersonaje.Posicion.X, unPersonaje.Posicion.Y);
            //    Juego.Instancia().Ambiente.ObtenerCasilla(puntoAnterior).Dejar(unPersonaje);
            //    Juego.Instancia().Ambiente.Avanzar(unPersonaje);
            //}

            //if ((deltaPrevio.X >= (spriteIndex.Width / 3)) && (deltaPrevio.X < (spriteIndex.Width / 2)) && (movido.X >= (spriteIndex.Width / 2)))
            //{
            //    Punto unPto = new Punto(unPersonaje.Posicion.X + (int)direccion.X, unPersonaje.Posicion.Y + (int)direccion.Y);
            //    Punto puntoAnterior = new Punto(unPersonaje.Posicion.X, unPersonaje.Posicion.Y);
            //    Juego.Instancia().Ambiente.ObtenerCasilla(puntoAnterior).Dejar(unPersonaje);
            //    Juego.Instancia().Ambiente.Avanzar(unPersonaje);
            //}

            //if ((deltaPrevio.Y >= (spriteIndex.Height / 3)) && (deltaPrevio.Y < (spriteIndex.Height / 2)) && (movido.Y >= (spriteIndex.Height / 2)))
            //{
            //    Punto unPto = new Punto(unPersonaje.Posicion.X, unPersonaje.Posicion.Y + (int)direccion.Y);
            //    Punto puntoAnterior = new Punto(unPersonaje.Posicion.X, unPersonaje.Posicion.Y);
            //    Juego.Instancia().Ambiente.ObtenerCasilla(puntoAnterior).Dejar(unPersonaje);
            //    Juego.Instancia().Ambiente.Avanzar(unPersonaje);
            //}

            //if ((Math.Abs(deltaPrevio.X) >= (spriteIndex.Width / 3)) && (Math.Abs(movido.X) < (spriteIndex.Width / 3)))
            //{
            //    Punto unPto = new Punto(unPersonaje.Posicion.X - (int)direccion.X, unPersonaje.Posicion.Y);

            //    Juego.Instancia().Ambiente.ObtenerCasilla(unPto).Dejar(unPersonaje);
            //}

            //if ((Math.Abs(deltaPrevio.Y) >= (spriteIndex.Height / 3)) && (Math.Abs(movido.Y) < (spriteIndex.Height / 3)))
            //{
            //    Punto unPto = new Punto(unPersonaje.Posicion.X, unPersonaje.Posicion.Y - (int)direccion.Y);
            //    Juego.Instancia().Ambiente.ObtenerCasilla(unPto).Dejar(unPersonaje);
            //}

            if (Math.Round(movido.X,1) >= spriteIndex.Width)
            {
                //position.X += (movido.X - spriteIndex.Width);
                movido.X = 0;
                //position.X -= movido.X;
                //movido.X = Vector2.Zero.X;
            }

            if (Math.Round(movido.X, 1) < 0)
            {
               // float delta = 0 - movido.X;
               // position.X += delta;
                movido.X = spriteIndex.Width - speed; 
            }

            if (Math.Round(movido.Y, 1) >= spriteIndex.Height)
            {
                movido.Y = 0;
            }

            if (Math.Round(movido.Y, 1) < 0)
            {
                movido.Y = spriteIndex.Height-speed; 
            }
        }

        public void corregirPosicion()
        {

            int dirPrev = unPersonaje.Movimiento.Direccion;
            if ((Math.Round(movido.X, 1) > 0) && movido.X < (spriteIndex.Width) / 2)
            {
                unPersonaje.Movimiento.Direccion = DERECHA;
                if (!Juego.Instancia().Ambiente.PermitidoAvanzar(unPersonaje))
                {
                    position.X -= movido.X;
                    movido.X = Vector2.Zero.X;
                }
            }
            if ((Math.Round(movido.X, 1) < spriteIndex.Width) && (Math.Round(movido.X, 1) >= (spriteIndex.Width / 2)))
            {
                unPersonaje.Movimiento.Direccion = IZQUIERDA;
                if (!Juego.Instancia().Ambiente.PermitidoAvanzar(unPersonaje))
                {
                    position.X += (spriteIndex.Width - movido.X);
                    movido.X = Vector2.Zero.X;
                }
            }
            if (Math.Round(movido.Y, 1) > 0 && Math.Round(movido.Y, 1) <= (spriteIndex.Height - speed) / 2)
            {
                unPersonaje.Movimiento.Direccion = ARRIBA;
                if (!Juego.Instancia().Ambiente.PermitidoAvanzar(unPersonaje))
                {
                    position.Y -= movido.Y;
                    movido.Y = Vector2.Zero.Y;
                }
            }
            if ((Math.Round(movido.Y, 1) < spriteIndex.Height) && (Math.Round(movido.Y, 1) > (spriteIndex.Height - speed) / 2))
            {
                unPersonaje.Movimiento.Direccion = ABAJO;
                if (!Juego.Instancia().Ambiente.PermitidoAvanzar(unPersonaje))
                {
                    position.Y += (spriteIndex.Height - movido.Y);
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
            position.X = 32 * unPersonaje.Posicion.X + Game1.mapa.Location.X;
            position.Y = 32 * unPersonaje.Posicion.Y + Game1.mapa.Location.Y;
            spriteIndex = content.Load<Texture2D>("Sprites\\" + spriteName);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!vivo) return;
            centro = new Vector2(spriteIndex.Width / 2, spriteIndex.Height / 2);

            spriteBatch.Draw(spriteIndex, position, null, Color.White, MathHelper.ToRadians(rotation), centro, scale, SpriteEffects.None, 0);
            //spriteBatch.DrawString(Game1.fuente, "En modelo ->Pos X: " + protagonista.Posicion.X + " Pos Y: " + protagonista.Posicion.Y, new Vector2(10, 10), Color.Yellow);
            //spriteBatch.DrawString(Game1.fuente, "Mvido ->Pos X: " + movido.X + " Pos Y: " + movido.Y + " RealPos X: " + position.X + " Pos Y: " + position.Y, new Vector2(10, Game1.fuente.LineSpacing), Color.Yellow); 
        }
    }
}
