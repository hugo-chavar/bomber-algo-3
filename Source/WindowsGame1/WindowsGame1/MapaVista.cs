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
using BombermanModel.Mapa;
using BombermanModel.Mapa.Casilla;
namespace BombermanGame
{
    public class MapaVista
    {
        Mapa mapaInterno;
        List<ObjetoVivo> objetosDibujables;


        public MapaVista(Mapa unMapa)
        {
            this.mapaInterno = unMapa;
            objetosDibujables = new List<ObjetoVivo>();


        }


        public void AgregarDibujable(ObjetoVivo unDibujable)
        {

            this.objetosDibujables.Add(unDibujable);

        }

        public void DibujarMapa(SpriteBatch sprite)
        {
            foreach (ObjetoVivo s in objetosDibujables)
            {
                s.Draw(sprite);
            }

        }

        public void EliminarDibujable(ObjetoVivo unDibujable)
        {
            //try
            {
                this.objetosDibujables.Remove(unDibujable);
            }//catch()

        }

        public void CargarMapa()
        {
            // recorro el tablero entero


            for (int vertical = 0; vertical < mapaInterno.DimensionVertical; vertical++)
            {
                for (int horizontal = 0; horizontal < mapaInterno.DimensionHorizontal; horizontal++)
                {
                    Casilla unaCasilla = mapaInterno.ObtenerCasilla(new Punto(horizontal, vertical));
                    this.AgregarCasillero(unaCasilla);

                }
            }
        }

        private void AgregarCasillero(Casilla unCasillero)
        {
            Obstaculo unObstaculo = unCasillero.Estado;

            if (unObstaculo != null)
            {
                // use reflexion: unica alternativa que se me ocurrio por ahora
                BloqueAcero bloqueAux = new BloqueAcero();
                if (unObstaculo.GetType() == bloqueAux.GetType())
                {
                    Vector2 unVector = this.TransformarPuntoEnVector2(unCasillero.Posicion);
                    Pared unaPared = new Pared(unVector);
                    this.AgregarDibujable(unaPared);

                }
            }
        }

        private Vector2 TransformarPuntoEnVector2(Punto unPunto)
        {
            Vector2 unVector;
            unVector.X = 32 * (unPunto.X);
            unVector.Y = 32 * (unPunto.Y);
            return unVector;
        }

        public void CargarContenido(ContentManager content)
        {
            foreach (ObjetoVivo s in objetosDibujables)
            {
                s.LoadContent(content);
            }

        }
        public void Actualizar()
        {
            foreach (ObjetoVivo s in objetosDibujables)
            {
                s.Update();
            }
        }



    }
}
