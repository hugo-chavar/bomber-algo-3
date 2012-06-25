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

        public void Draw(SpriteBatch sprite)
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
                    BloqueAceroView unaPared = new BloqueAceroView(unVector);
                    this.AgregarDibujable(unaPared);

                }
                Pasillo pasilloAux = new Pasillo();
                if (unObstaculo.GetType() == pasilloAux.GetType())
                {
                    Vector2 unVector = this.TransformarPuntoEnVector2(unCasillero.Posicion);
                    PasilloView unaPared = new PasilloView(unVector);
                    this.AgregarDibujable(unaPared);

                }

                Obstaculo unBloqueCemento = BloqueComun.CrearBloqueCemento();
                if ((unObstaculo.GetType() == unBloqueCemento.GetType()) && (((BloqueComun)unObstaculo).EsBloqueCemento()))
                {
                    Vector2 unVector = this.TransformarPuntoEnVector2(unCasillero.Posicion);
                    BloqueCementoView unBloqueCementoView = new BloqueCementoView(unVector);
                    this.AgregarDibujable(unBloqueCementoView);

                }

                Obstaculo unBloqueLadrillo = BloqueComun.CrearBloqueLadrillos();
                if ((unObstaculo.GetType() == unBloqueLadrillo.GetType()) && (!((BloqueComun)unObstaculo).EsBloqueCemento()))
                {
                    Vector2 unVector = this.TransformarPuntoEnVector2(unCasillero.Posicion);
                    BloqueLadrilloView unBloqueLadrilloView = new BloqueLadrilloView(unVector);
                    this.AgregarDibujable(unBloqueLadrilloView);

                }
            }
        }

        private Vector2 TransformarPuntoEnVector2(Punto unPunto)
        {
            Vector2 unVector;
            unVector.X = 32 * (unPunto.X) + Game1.mapa.Location.X;
            unVector.Y = 32 * (unPunto.Y) + Game1.mapa.Location.Y;
            return unVector;
        }

        public void LoadContent(ContentManager content)
        {
            foreach (ObjetoVivo s in objetosDibujables)
            {
                s.LoadContent(content);
            }

        }
        public void Update()
        {
            foreach (ObjetoVivo s in objetosDibujables)
            {
                s.Update();
            }
        }



    }
}
