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
    class Pared : ObjetoVivo
    {
        //private Obstaculo aceroModel;

        public Pared(Vector2 pos)
            : base(pos)
        {
            //position.X = 64+ Game1.mapa.Location.X;
            //position.Y = 0 + Game1.mapa.Location.Y;
            spriteName = "ObsAcero";
            /*solido = true;
            Punto unPunto = new Punto(0, 0);
            unPunto.X = ((int)(pos.X) - Game1.mapa.Location.X) / 32;
            unPunto.Y = ((int)(pos.Y) - Game1.mapa.Location.Y) / 32;
            aceroModel = Juego.Instancia().Ambiente.ObtenerCasilla(unPunto).Estado;*/
        }
        /*
        public override void LoadContent(ContentManager content)
        {

            spriteIndex = content.Load<Texture2D>("Sprites\\" + spriteName);
            area = new Rectangle(0, 0, spriteIndex.Width, spriteIndex.Height);
            //area.X = (int)position.X - (spriteIndex.Width / 2);
            //area.Y = (int)position.Y - (spriteIndex.Height / 2);
        }
        
        public override void Update()
        {
            if (aceroModel.Destruido())
            {
                PasilloView unPasillo = new PasilloView(position);
                MapaVista.EliminarDibujable(this);
                MapaVista.AgregarDibujable(unPasillo);
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //Rectangle size;
            Vector2 center = new Vector2(spriteIndex.Width / 2, spriteIndex.Height / 2);

            spriteBatch.Draw(spriteIndex, position, null, Color.White, MathHelper.ToRadians(rotation), center, scale, SpriteEffects.None, 0);


        }*/
    }
}
