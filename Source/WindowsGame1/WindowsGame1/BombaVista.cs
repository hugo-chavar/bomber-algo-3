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
using BombermanModel.Arma;
using BombermanModel;
using BombermanModel.Juego;

namespace BombermanGame
{
    public class BombaVista : ObjetoVivo
    {
        private Explosivo explosivo;

        public BombaVista(Vector2 pos, Explosivo bomba)
            : base(pos)
        {

            explosivo = bomba;
            setSpriteName();
            vivo = true;
        }

        public BombaVista()
        {
            vivo = false;
        }

        public Explosivo Explosivo 
        {
            get { return this.explosivo; } 
            set { this.explosivo = value; } 
        }

        public override void LoadContent(ContentManager content)
        {
            //Load Content de las bombas se realiza en Cargar Contenido de la clase MapaVista
        }

        public void setSpriteName()
        {
            if (explosivo.Nombre == Nombres.molotov)
                spriteIndex = MapaVista.Instancia().MolotovSprite;
            else if (explosivo.Nombre == Nombres.toleTole)
                spriteIndex = MapaVista.Instancia().ToleToleSprite;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!vivo) return;
            Vector2 center = new Vector2(spriteIndex.Width / 2, spriteIndex.Height / 2);

            spriteBatch.Draw(spriteIndex, posicion, null, Color.White, MathHelper.ToRadians(rotacion), center, escala, SpriteEffects.None, 0);
        }

        public override void Update()
        {
            if (!vivo) return;
            if (explosivo.EstaExplotado())
            {
                vivo = false;
            }

        }

    }
}


