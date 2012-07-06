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
using BombermanModel.Articulo;
using BombermanModel;
using BombermanModel.Juego;

namespace BombermanGame
{
    class ArticuloVista : ObjetoVivo
    {
        private Articulo articulo;
        private Punto posEnMapa;

        public ArticuloVista(Punto posMapa)
        {
            vivo = false ;
            posEnMapa = posMapa;
        }

        public ArticuloVista(Punto p, Articulo unArt, Vector2 pos)
        {
            articulo = unArt;
            position = pos;
            vivo = true;
        }

        public void CargarSprite()
        {
            if (articulo.Nombre== Nombres.timer)
                spriteIndex = MapaVista.Instancia().ArtTimerSprite;
            else if (articulo.Nombre == Nombres.arToleTole)
                spriteIndex = MapaVista.Instancia().ArtToleToleSprite;
            else if (articulo.Nombre == Nombres.chala)
                spriteIndex = MapaVista.Instancia().ArtChalaSprite;
            else if (articulo.Nombre == Nombres.salida)
                spriteIndex = MapaVista.Instancia().SalidaSprite;

        }

        public override void LoadContent(ContentManager content)
        {
            CargarSprite();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!vivo) return;
            Vector2 center = new Vector2(spriteIndex.Width / 2, spriteIndex.Height / 2);
            spriteBatch.Draw(spriteIndex, position, null, Color.White, MathHelper.ToRadians(rotation), center, scale, SpriteEffects.None, 0);
        }

        public override void Update()
        {
            if ((articulo.EstaActivo) && (!articulo.EstaOculto))
            {
                vivo = true;
            }
            else
                vivo = false;   
        }

    }
}
