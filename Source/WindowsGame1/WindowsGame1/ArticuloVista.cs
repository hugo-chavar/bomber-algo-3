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

        public ArticuloVista(Vector2 pos, Articulo unArticulo)
            : base(pos)
        {
            articulo = unArticulo;
            CargarSprite();
            vivo = false;
         
        }

        public void CargarSprite()
        {
            if (articulo.Nombre== Nombres.timer)
                spriteIndex = MapaVista.artTimerView;
            else if (articulo.Nombre == Nombres.arToleTole)
                spriteIndex = MapaVista.artToleTole;
            else if (articulo.Nombre == Nombres.chala)
                spriteIndex = MapaVista.artChala;
            else if (articulo.Nombre == Nombres.salida)
                spriteIndex = MapaVista.salida;

        }



        public override void LoadContent(ContentManager content)
        {
        //   if (articulo.GetType() == typeof(Timer))
          //  spriteIndex = content.Load<Texture2D>("Sprites\\" + "ArtTimer" );
           /*else if(articulo.GetType() == typeof(Chala))
                spriteIndex = content.Load<Texture2D>("Sprites\\" + "ArtChala"); */
            //else if(articulo.GetType() == typeof(ArticuloBombaToleTole)) 
              //  spriteIndex = content.Load<Texture2D>("Sprites\\" + "ArtToleTole");
            /* else if(articulo.GetType() == typeof(Salida))
                spriteIndex = content.Load<Texture2D>("Sprites\\" + "Salida"); */
            
            area = new Rectangle(0, 0, spriteIndex.Width, spriteIndex.Height); // Martin: Esto no se si sirve, lo pongo igual...
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //Rectangle size;
            Vector2 center = new Vector2(spriteIndex.Width / 2, spriteIndex.Height / 2);
            //if (this.vivo)
            spriteBatch.Draw(spriteIndex, position, null, Color.White, MathHelper.ToRadians(rotation), center, scale, SpriteEffects.None, 0);
        }

        public override void Update()
        {
            if ((articulo.EstaActivo) && (articulo.EstaOculto))
            {
                MapaVista.EliminarDibujable(this);
                PasilloView unPasillo = new PasilloView(position);
                MapaVista.AgregarDibujable(unPasillo);

            }

            if (articulo.EstaActivo)
            {
                vivo = true;
            }
            
        }

    }
}
