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
using BombermanModel.Arma;
using BombermanModel;
using BombermanModel.Juego;
using BombermanModel.Mapa.Casilla;
using System;

namespace BombermanGame
{
    class LopezReggaeView : EnemigoView
    {
        private Vector2 puntoCentro;

        public LopezReggaeView(Personaje pers)
            : base(pers)
        {
            spriteName = "LopezReggae";
        }

        public override void LoadContent(ContentManager content)
        {
            position.X = 32 * unPersonaje.Posicion.X + Game1.mapa.Location.X;
            position.Y = 32 * unPersonaje.Posicion.Y + Game1.mapa.Location.Y;
            spriteIndex = content.Load<Texture2D>("Sprites\\" + spriteName);
            puntoCentro = new Vector2(spriteIndex.Width / 2, spriteIndex.Height / 2);
            CargarObjetivos();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!vivo) return;
            spriteBatch.Draw(spriteIndex, position, null, Color.White, MathHelper.ToRadians(rotation), puntoCentro, scale, SpriteEffects.None, 0);
            //spriteBatch.DrawString(Game1.fuente, "En modelo ->Pos X: " + unPersonaje.Posicion.X + " Pos Y: " + unPersonaje.Posicion.Y, new Vector2(10, 10), Color.Yellow);
            //spriteBatch.DrawString(Game1.fuente, "Mvido ->Pos X: " + movido.X + " Pos Y: " + movido.Y + " RealPos X: " + position.X + " Pos Y: " + position.Y, new Vector2(10, Game1.fuente.LineSpacing), Color.Yellow);
        }

        public override void Disparar()
        {
            Proyectil bomba = (Proyectil)unPersonaje.LanzarExplosivo();
            if (bomba != null)
            {

                //Proyectil proyectil = (Proyectil)Juego.Instancia().Ambiente.ObtenerCasilla(unPersonaje.Posicion).Explosivo;
                //ProyectilView p = new ProyectilView();
                foreach (ObjetoVivo o in MapaVista.ObjetosDibujables)
                {
                    if ((o.GetType() == typeof (ProyectilView)) && (!o.Vivo))
                    {
                        o.Rotacion = rotation;
                        o.Posicion = position;
                        ((ProyectilView)o).Explosivo = bomba;
                        o.Vivo = true;
                        return;
                    }
                }
           }
        }

        public bool TieneEspacioParaDisparar()
        {
            if (Juego.Instancia().Ambiente.PermitidoAvanzar(unPersonaje))
            {
                Personaje otro = new LosLopezReggae(new Punto(unPersonaje.Posicion.X + (int)this.Direccion.X, unPersonaje.Posicion.Y + (int)this.Direccion.Y));
                if (Juego.Instancia().Ambiente.PermitidoAvanzar(otro))
                    return true;
            }
            return false;
        }


        public override void Update()
        {
            if (!vivo) return;
            IDaniable p = this.unPersonaje;
            if (p.Destruido())
            {
                this.Vivo = false;
            }
            //si el chabon no esta vivo.. no hago nada
            if (!vivo) return;
            Random random = new Random();
            int vaADisparar = random.Next(0,50); // tiene un 2% de posibilidades de disparar
            if (TieneEspacioParaDisparar() && (vaADisparar == random.Next(0, 1)))
                Disparar();

            base.Update();
        }
    }
}
