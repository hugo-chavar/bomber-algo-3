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
        protected bool vivo = true;

        public bool Vivo { get { return this.vivo; } set { this.vivo = value; } }
        public float Rotacion { get { return this.rotation; } set { this.rotation = value; } }
        public Vector2 Posicion { get { return this.position; } set { this.position = value; } }

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
        }



        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }

       

    }
}
