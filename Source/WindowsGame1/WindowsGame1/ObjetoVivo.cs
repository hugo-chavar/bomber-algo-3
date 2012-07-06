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
    public abstract class ObjetoVivo
    {
        protected Vector2 posicion;
        protected Vector2 movido;
        protected float rotacion;
        protected Texture2D spriteIndex;
        protected float velocidad = 0.0f;
        protected string spriteName = "empty";
        protected float escala = 1.0f;
        protected bool vivo = true;

        public bool Vivo { get { return this.vivo; } set { this.vivo = value; } }
        public float Rotacion { get { return this.rotacion; } set { this.rotacion = value; } }
        public Vector2 Posicion { get { return this.posicion; } set { this.posicion = value; } }

        public ObjetoVivo(Vector2 pos)
        {
            posicion = pos;
        }

        public ObjetoVivo()
        {
        }

        public abstract void Update();

        public abstract void LoadContent(ContentManager content);

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
