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
        protected Vector2 puntoCentro;
        Vector2 posicionCentroSprite;

        public PersonajeVista(Personaje pers)
            :base(MapaVista.Instancia().TransformarPuntoEnVector2(pers.Posicion))
        {
            unPersonaje = pers;
            velocidad = unPersonaje.Movimiento.Velocidad;
            posicion = MapaVista.Instancia().TransformarPuntoEnVector2(unPersonaje.Posicion);
            movido = Vector2.Zero;
            unaBmb = null;
        }

        public Vector2 Direccion { get { return this.direccion; } set { this.direccion = value; } }

        public Personaje UnPersonaje { set { this.unPersonaje = value;} }

        public virtual void Disparar()
        {
            unaBmb = (BombaVista)MapaVista.Instancia().ObtenerObjetoContundente(new BombaVista());
            if (unaBmb != null)
            {
                Explosivo bomba = unPersonaje.LanzarExplosivo();
                if (bomba != null)
                {
                    unaBmb.Explosivo = bomba;
                    unaBmb.setSpriteName();
                    unaBmb.Posicion = MapaVista.Instancia().TransformarPuntoEnVector2(unPersonaje.Posicion);
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
            posicion += direccion * velocidad;

            posicionCentroSprite = posicion + new Vector2(spriteIndex.Width / 2, spriteIndex.Height / 2);
            Punto ptEnMapa = MapaVista.Instancia().TransformarVector2EnPunto(posicionCentroSprite);
           
            movido += direccion * velocidad;
            
 
            if (!unPersonaje.Posicion.Equals(ptEnMapa))


            if (Math.Round(movido.X,1) >= spriteIndex.Width)
            {
                movido.X = 0;
            }

            if (Math.Round(movido.X, 1) < 0)
            {
                movido.X = spriteIndex.Width - velocidad; 
            }

            if (Math.Round(movido.Y, 1) >= spriteIndex.Height)
            {
                movido.Y = 0;
            }

            if (Math.Round(movido.Y, 1) < 0)
            {
                movido.Y = spriteIndex.Height-velocidad; 
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
                    posicion.X -= movido.X;
                    movido.X = Vector2.Zero.X;
                }
            }
            if ((Math.Round(movido.X, 1) < spriteIndex.Width) && (Math.Round(movido.X, 1) >= (spriteIndex.Width / 2)))
            {
                unPersonaje.Movimiento.Direccion = IZQUIERDA;
                if (!Juego.Instancia().Ambiente.PermitidoAvanzar(unPersonaje))
                {
                    posicion.X += (spriteIndex.Width - movido.X);
                    movido.X = Vector2.Zero.X;
                }
            }
            if (Math.Round(movido.Y, 1) > 0 && Math.Round(movido.Y, 1) <= (spriteIndex.Height - velocidad) / 2)
            {
                unPersonaje.Movimiento.Direccion = ARRIBA;
                if (!Juego.Instancia().Ambiente.PermitidoAvanzar(unPersonaje))
                {
                    posicion.Y -= movido.Y;
                    movido.Y = Vector2.Zero.Y;
                }
            }
            if ((Math.Round(movido.Y, 1) < spriteIndex.Height) && (Math.Round(movido.Y, 1) > (spriteIndex.Height - velocidad) / 2))
            {
                unPersonaje.Movimiento.Direccion = ABAJO;
                if (!Juego.Instancia().Ambiente.PermitidoAvanzar(unPersonaje))
                {
                    posicion.Y += (spriteIndex.Height - movido.Y);
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
            posicion.X = 32 * unPersonaje.Posicion.X + Game1.mapa.Location.X;
            posicion.Y = 32 * unPersonaje.Posicion.Y + Game1.mapa.Location.Y;
            spriteIndex = content.Load<Texture2D>("Sprites\\" + spriteName);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!vivo) return;
            puntoCentro = new Vector2(spriteIndex.Width / 2, spriteIndex.Height / 2);
            spriteBatch.Draw(spriteIndex, posicion, null, Color.White, MathHelper.ToRadians(rotacion), puntoCentro, escala, SpriteEffects.None, 0);
        }
    }
}
