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
using BombermanModel;
using BombermanModel.Juego;
using BombermanModel.Mapa.Casilla;

namespace BombermanGame
{
    class BloqueVista : ObjetoVivo
    {

        protected Obstaculo obst;
        protected Punto posEnMapa;

        public BloqueVista(Vector2 pos,Punto posMapa, Obstaculo unObstaculo)
            : base(pos)
        {
            //position.X = 64+ Game1.mapa.Location.X;
            //position.Y = 0 + Game1.mapa.Location.Y;
            //spriteName = "ObsLadrillo";
            posEnMapa = posMapa;// this.TransformarAPunto(pos);
            obst = unObstaculo;//Juego.Instancia().Ambiente.ObtenerCasilla(punto).Estado;
            vivo = true;
        }

        private Punto TransformarAPunto(Vector2 unVector2)
        {
            Punto unPuntoReturn = new Punto(0, 0);
            unPuntoReturn.X = ((int)(unVector2.X) - Game1.mapa.Location.X) / 32;
            unPuntoReturn.Y = ((int)(unVector2.Y) - Game1.mapa.Location.Y) / 32;
            return unPuntoReturn;
        }

        public override void LoadContent(ContentManager content)
        {

            spriteIndex = content.Load<Texture2D>("Sprites\\" + spriteName);
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
            Pasillo unpasillo = new Pasillo();
            if (Juego.Instancia().Ambiente.ObtenerCasilla(posEnMapa).Estado.GetType() == unpasillo.GetType())
            {
                vivo = false;

            }

        }
    }
}
