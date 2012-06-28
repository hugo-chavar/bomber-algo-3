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
    class EnemigoView : PersonajeView
    {
        protected int destinoActual = 0;
        protected List<Point> destinosObjetivo = new List<Point>();
        protected List<Vector2> versores = new List<Vector2>();


        public EnemigoView(Personaje pers)
            : base(pers) 
        {
            versores.Add(Vector2.UnitY*-1);
            versores.Add(Vector2.UnitX*-1);
            versores.Add(Vector2.UnitX);
            versores.Add(Vector2.UnitY);
            this.direccion = versores.ElementAt(0);
            unPersonaje.Movimiento.Direccion = 2;

        }
        
        protected void CargarObjetivos()
        {
            Random direccionRandom = new Random();
            //ingreso una lista al azar de 10 objetivos de enemigos
            for (int i = 0; i < 10; i++)
            {
                int x = direccionRandom.Next(Juego.Instancia().Ambiente.DimensionHorizontal);
                int y = direccionRandom.Next(Juego.Instancia().Ambiente.DimensionVertical);
                destinosObjetivo.Add(new Point(x, y));
            }
        }

        protected void ProximoDestino()
        {
            //si fue a todos los destinos empieza de nuevo
            if (destinoActual == destinosObjetivo.Count - 1) destinoActual = 0;
            else destinoActual++;
        }


        public override void Update()
        {
            if (!vivo) return;
            int validas;
            if (((movido.X == Vector2.Zero.X) || (movido.X == spriteIndex.Width - 1)) && ((movido.Y == Vector2.Zero.Y) || (movido.Y == spriteIndex.Height - 1)))
            {
                if (!Juego.Instancia().Ambiente.PermitidoAvanzar(unPersonaje))
                    recalcularDireccion();
                else
                {
                    validas = TestDireccionesValidas();
                    if (validas > 2) // Si tiene 3 o 4 opciones de movimiento, recalcula su direccion.
                        recalcularDireccion();
                }
            }
            rotation = point_direction(-direccion.Y, -direccion.X);
            Advance();

            base.Update();
        }


        public int TestDireccionesValidas()
        {
            int dirPrev = unPersonaje.Movimiento.Direccion;
            int cant = 0;
            unPersonaje.Movimiento.Direccion = ARRIBA;
            if (Juego.Instancia().Ambiente.PermitidoAvanzar(unPersonaje))
            {
                cant++;
            }
            unPersonaje.Movimiento.Direccion = ABAJO;
            if (Juego.Instancia().Ambiente.PermitidoAvanzar(unPersonaje))
            {
                cant++;
            }
            unPersonaje.Movimiento.Direccion = IZQUIERDA;
            if (Juego.Instancia().Ambiente.PermitidoAvanzar(unPersonaje))
            {
                cant++;
            }
            unPersonaje.Movimiento.Direccion = DERECHA;
            if (Juego.Instancia().Ambiente.PermitidoAvanzar(unPersonaje))
            {
                cant++;
            }
            unPersonaje.Movimiento.Direccion = dirPrev;
            return cant;
        }

        public void recalcularDireccion()
        {
            Random random = new Random();
            int calculadorDirecciones = random.Next(0, 4);
            direccion = versores.ElementAt(calculadorDirecciones);
            switch ((calculadorDirecciones + 1) * 2)
            {
                case ABAJO: unPersonaje.Movimiento.CambiarAAbajo();
                    break;

                case IZQUIERDA: unPersonaje.Movimiento.CambiarAIzquierda();
                    break;

                case DERECHA: unPersonaje.Movimiento.CambiarADerecha();
                    break;

                case ARRIBA: unPersonaje.Movimiento.CambiarAArriba();
                    break;
            }
        }


        public virtual void ActualizarPosicion()
        {
            int x = (int)Math.Round((position.X - Game1.mapa.Location.X) / 32,0);
            int y = (int)Math.Round((position.Y - Game1.mapa.Location.Y) / 32, 0);

            if ((destinosObjetivo[destinoActual].X == x) && (destinosObjetivo[destinoActual].Y == y))
                ProximoDestino();
        }

    }
}
