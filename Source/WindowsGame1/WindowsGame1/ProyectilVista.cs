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
    class ProyectilVista : ObjetoVivo
    {
        private Proyectil explosivo;
        public const int ARRIBA = 8;
        public const int ABAJO = 2;
        public const int IZQUIERDA = 4;
        public const int DERECHA = 6;

        public ProyectilVista(Vector2 pos, float rot, Explosivo bomba)
            : base(pos)
        {
            explosivo = (Proyectil)bomba;
            spriteIndex = MapaVista.Instancia().ProyectilSprite;
            rotation = rot;
            speed = 0.5f;
            vivo = true;
            
        }

        public ProyectilVista()
        {
            spriteIndex = MapaVista.Instancia().ProyectilSprite;
            speed = 0.5f;
            vivo = false;
        }

        public Proyectil Explosivo { get { return this.explosivo; } set { this.explosivo = value; } }

        public override void LoadContent(ContentManager content)
        {
            spriteIndex = content.Load<Texture2D>("Sprites\\" + "Proyectil");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!vivo) return;
            Vector2 center = new Vector2(spriteIndex.Width / 2, spriteIndex.Height / 2);
            spriteBatch.Draw(spriteIndex, position, null, Color.White, MathHelper.ToRadians(rotation), center, scale, SpriteEffects.None, 0);
        }

        public override void Update()
        {
            if (!vivo) return;

            if (explosivo == null || explosivo.EstaExplotado())
            {
                vivo = false;
            }
            else
            {
                position.X = 32 * explosivo.Posicion.X + Game1.mapa.Location.X;
                position.Y = 32 * explosivo.Posicion.Y + Game1.mapa.Location.Y;
            }
        }

    }
}
