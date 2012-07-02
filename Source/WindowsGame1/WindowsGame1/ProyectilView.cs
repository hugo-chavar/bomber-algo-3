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
        public const int ARRIBA = 8;
        public const int ABAJO = 2;
        public const int IZQUIERDA = 4;
        public const int DERECHA = 6;

        public ProyectilView(Vector2 pos, float rot, Explosivo bomba)
            : base(pos)
        {
            explosivo = (Proyectil)bomba;
            spriteIndex = MapaVista.proyectilSprite;
            rotation = rot;
            speed = 0.5f;
            vivo = true;
            
        }

        public ProyectilView()
        {
            spriteIndex = MapaVista.proyectilSprite;
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
            //spriteBatch.DrawString(Game1.fuente, "En modelo ->Pos X: " + explosivo.Posicion.X + " Pos Y: " + explosivo.Posicion.Y, new Vector2(10, 10), Color.Yellow);
            //spriteBatch.DrawString(Game1.fuente, "Mvido ->Pos X: " + movido.X + " Pos Y: " + movido.Y + " RealPos X: " + position.X + " Pos Y: " + position.Y, new Vector2(10, Game1.fuente.LineSpacing), Color.Yellow);
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
