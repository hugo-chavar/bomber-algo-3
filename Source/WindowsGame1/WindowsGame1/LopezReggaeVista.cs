﻿using System.Linq;
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
using System;

namespace BombermanGame
{
    class LopezReggaeVista : EnemigoVista
    {

        public LopezReggaeVista(Personaje pers)
            : base(pers)
        {
            spriteName = "LopezReggae";
        }

        public override void LoadContent(ContentManager content)
        {
            posicion.X = 32 * unPersonaje.Posicion.X + Game1.mapa.Location.X;
            posicion.Y = 32 * unPersonaje.Posicion.Y + Game1.mapa.Location.Y;
            spriteIndex = content.Load<Texture2D>("Sprites\\" + spriteName);
            puntoCentro = new Vector2(spriteIndex.Width / 2, spriteIndex.Height / 2);
            CargarObjetivos();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!vivo) return;
            spriteBatch.Draw(spriteIndex, posicion, null, Color.White, MathHelper.ToRadians(rotacion), puntoCentro, escala, SpriteEffects.None, 0);
         }



        public override void Disparar()
        {
            Proyectil bomba = (Proyectil)unPersonaje.LanzarExplosivo();
            if (bomba != null)
            {
                ProyectilVista unProy = (ProyectilVista)MapaVista.Instancia().ObtenerObjetoContundente(new ProyectilVista());

                unProy.Explosivo = bomba;
                unProy.Rotacion = rotacion;
                unProy.Posicion = posicion;
                unProy.Vivo = true;
            }
            
        }

        public bool TieneEspacioParaDisparar()
        {
            if (Juego.Instancia().Ambiente.PermitidoAvanzar(unPersonaje))
            {
                Personaje otro = new LosLopezReggae(new Punto(unPersonaje.Posicion.X + (int)this.Direccion.X, unPersonaje.Posicion.Y + (int)this.Direccion.Y));
                if (Juego.Instancia().Ambiente.PermitidoAvanzar(otro))
                    return true;
            }
            return false;
        }


        public override void Update()
        {
            if (!vivo) return;
            IDaniable p = this.unPersonaje;
            if (p.Destruido())
            {
                this.Vivo = false;
            }
            Random random = new Random();
            int vaADisparar = random.Next(0,100); // tiene un 1% de posibilidades de disparar
            if (TieneEspacioParaDisparar() && (vaADisparar == 0))
            {
                Disparar();
            }
            base.Update();
        }
    }
}
