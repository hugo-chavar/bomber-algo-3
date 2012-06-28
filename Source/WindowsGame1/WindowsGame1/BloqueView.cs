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
    class BloqueView : ObjetoVivo
    {
        
        private Obstaculo obst ;
        Punto punto;

        public BloqueView(Vector2 pos)
            : base(pos)
        {
            //position.X = 64+ Game1.mapa.Location.X;
            //position.Y = 0 + Game1.mapa.Location.Y;
            //spriteName = "ObsLadrillo";
            solido = true;
            punto = this.TransformarAPunto(pos);
            obst = Juego.Instancia().Ambiente.ObtenerCasilla(punto).Estado;
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
            if (obst.Destruido())
            {
                if (Juego.Instancia().Ambiente.ObtenerCasilla(punto).ArticuloContenido == null)
                {
                    PasilloView unPasillo = new PasilloView(position);
                    MapaVista.AgregarDibujable(unPasillo);
                }
                else
                {
                    ArticuloVista unArticuloVista = new ArticuloVista(position, Juego.Instancia().Ambiente.ObtenerCasilla(punto).ArticuloContenido);
                    if (!Juego.Instancia().Ambiente.ObtenerCasilla(punto).ArticuloContenido.EstaActivo)
                        MapaVista.AgregarDibujable(new PasilloView(position));
                    MapaVista.AgregarDibujable(unArticuloVista);
                }
                MapaVista.EliminarDibujable(this);
                //vivo = false;
            }
        }

        





    }
}
