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
using BombermanModel.Arma;
using BombermanModel;
using BombermanModel.Juego;
using BombermanModel.Mapa.Casilla;

namespace BombermanGame
{
    class LopezReggaeAladoView : EnemigoView
    {

        //private Personaje LRA = new LosLopezReggaeAlado(new Punto(10,10));
        private Vector2 puntoCentro;
        //private int destinoActual =  0;
        //private List<Point> destinosObjetivo = new List<Point>();

        public LopezReggaeAladoView(Vector2 pos)
            : base(pos)
        {
            speed = 4f;// unLRA.Movimiento.Velocidad;
            spriteName = "LRA";
            unPersonaje = new LosLopezReggaeAlado(new Punto(10, 10));
        }


        /*
        private void CargarObjetivos()
        {
            Random direccionRandom = new Random();
            //ingreso una lista al azar de 10 objetivos del LRA
            for (int i = 0; i < 10; i++)
            {
                int x = direccionRandom.Next(Juego.Instancia().Ambiente.DimensionHorizontal);
                int y = direccionRandom.Next(Juego.Instancia().Ambiente.DimensionVertical);
                destinosObjetivo.Add(new Point(x, y));
            }
        }

        private void ProximoDestino()
        {
            //si fue a todos los destinos empieza de nuevo
            if (destinoActual == destinosObjetivo.Count - 1) destinoActual = 0;
            else destinoActual++;
        }
        */

        public override void LoadContent(ContentManager content)
        {
            position.X = 32*unPersonaje.Posicion.X + Game1.mapa.Location.X;
            position.Y = 32*unPersonaje.Posicion.Y + Game1.mapa.Location.Y;
            spriteIndex = content.Load<Texture2D>("Sprites\\" + spriteName);
            area = new Rectangle(0, 0, spriteIndex.Width, spriteIndex.Height);
            puntoCentro = new Vector2(spriteIndex.Width / 2, spriteIndex.Height / 2);
            CargarObjetivos();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            

            spriteBatch.Draw(spriteIndex, position, null, Color.White, MathHelper.ToRadians(rotation), puntoCentro, scale, SpriteEffects.None, 0);
            //spriteBatch.DrawString(Game1.fuente, "En modelo ->Pos X: " + unLRA.Posicion.X + " Pos Y: " + unLRA.Posicion.Y, new Vector2(10, 10), Color.Yellow);
        }

        public override void Update()
        {

            rotation = point_direction(position.X, position.Y, destinosObjetivo[destinoActual].X * 32 + Game1.mapa.Location.X, destinosObjetivo[destinoActual].Y * 32 + Game1.mapa.Location.Y);
            if (!vivo) return;
            PushTo(speed, rotation);
            ActualizarPosicion();

            //base.Update();//ACA ESTA TIRANDO QUILOMBO!
        }
        /*
        public override void ActualizarPosicion()
        {
           
            int x = (int)Math.Round((position.X - Game1.mapa.Location.X) / 32,0);
            int y = (int)Math.Round((position.Y - Game1.mapa.Location.Y) / 32, 0);
            Juego.Instancia().Ambiente.ObtenerCasilla(unPersonaje.Posicion).Dejar(unPersonaje);
            unPersonaje.Posicion = new Punto(x, y);
            Juego.Instancia().Ambiente.ObtenerCasilla(unPersonaje.Posicion).Transitar(unPersonaje);
            if ((destinosObjetivo[destinoActual].X == x) && (destinosObjetivo[destinoActual].Y == y))
                ProximoDestino();
        }
        */
        public float point_direction(float x, float y, float x2, float y2)
        {
            float diffx = x - x2;
            float diffy = y - y2;
            float adj = diffx;
            float opp = diffy;
            float tan = opp / adj;
            float res = MathHelper.ToDegrees((float)Math.Atan2(opp, adj));
            res = (res - 180) % 360;
            if (res < 0)
                res += 360;
            return res;
        }

        public void PushTo(float pix, float dir)
        {
            float newX = (float)Math.Cos(MathHelper.ToRadians(dir));
            float newY = (float)Math.Sin(MathHelper.ToRadians(dir));
            position.X += pix * (float)newX;
            position.Y += pix * (float)newY; 
        }

                  
    }
}
