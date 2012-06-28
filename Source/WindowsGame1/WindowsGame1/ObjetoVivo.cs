using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace BombermanGame
{
    public class ObjetoVivo
    {
        protected Vector2 position;
        protected Vector2 movido;
        protected float rotation;
        protected Texture2D spriteIndex;
        protected float speed = 0.0f;
        protected string spriteName = "empty";
        protected float scale = 1.0f;
        protected bool solido = false;
        protected bool vivo = true;
        protected Rectangle area;

        public bool Vivo { get { return this.vivo; } set { this.vivo = value; } }
        public float Rotacion { get { return this.rotation; } set { this.rotation = value; } }
        public Vector2 Posicion { get { return this.position; } set { this.position = value; } }

        public ObjetoVivo(Vector2 pos)
        {
            position = pos;
        }


        public virtual void Update()
        {
            if (!vivo) return;
            area.X = (int)position.X - (spriteIndex.Width / 2);
            area.Y = (int)position.Y - (spriteIndex.Height / 2);
        }

        public virtual void LoadContent(ContentManager content)
        {
            //spriteIndex = content.Load<Texture2D>("Sprites\\"+spriteName);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (!vivo) return;
            //Rectangle size;
            Vector2 center = new Vector2(spriteIndex.Width / 2, spriteIndex.Height / 2);
            spriteBatch.Draw(spriteIndex, position, null, Color.White, MathHelper.ToRadians(rotation), center, scale, SpriteEffects.None, 0);


        }

        public bool colision(float x, float y)//, ObjetoVivo obj
        {

            Rectangle offsetArea = new Rectangle(area.X, area.Y, area.Width, area.Height);
            offsetArea.X += (int)x;
            offsetArea.Y += (int)y;
            foreach (ObjetoVivo o in ListaVivos.objList)
            {
                if (o.solido) //o.GetType() == obj.GetType()&&
                    if (o.area.Intersects(offsetArea))
                        return true;
            }
            return false;
        }

        //public virtual bool EsUnDibujableMovible();
        

    }
}
