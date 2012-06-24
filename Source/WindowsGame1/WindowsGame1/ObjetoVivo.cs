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

namespace BombermanGame
{
    class ObjetoVivo
    {
        protected Vector2 position;
        protected float rotation;
        protected Texture2D spriteIndex;
        protected float speed = 0.0f;
        protected string spriteName;
        protected float scale = 1.0f;



        public ObjetoVivo(Vector2 pos)
        {
            position = pos;
        }

        public ObjetoVivo()
        {
        }

        public virtual void Update()
        {

        }

        public virtual void LoadContent(ContentManager content)
        {
            spriteIndex = content.Load<Texture2D>("Sprites\\"+spriteName);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //Rectangle size;
            Vector2 center = new Vector2(spriteIndex.Width / 2, spriteIndex.Height / 2);
            spriteBatch.Draw(spriteIndex, position, null, Color.White, MathHelper.ToRadians(rotation), center, scale, SpriteEffects.None, 0);

        }
    }
}
