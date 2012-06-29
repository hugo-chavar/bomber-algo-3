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
using BombermanModel.Articulo;
namespace BombermanGame
{
    public static class MapaVista
    {
        static Mapa mapaInterno;
        static List<ObjetoVivo> pasillosDibujables;
        static List<ObjetoVivo> objetosDibujables;
        static List<ObjetoVivo> enemigosDibujables;

        public static Texture2D molotovSprite;//ANDY: NOSE SI ES ACA DONDE LO TENGO QUE ALMACENAR
        public static Texture2D toleToleSprite;
        public static Texture2D proyectilSprite;
        public static Texture2D pasilloView;
        public static Texture2D artTimerView;
        public static Texture2D artToleTole;
        public static Texture2D artChala;
        public static Texture2D salida;


        public static void inicialize(Mapa unMapa)
        {
            mapaInterno = unMapa;
            objetosDibujables = new List<ObjetoVivo>();
            enemigosDibujables = new List<ObjetoVivo>();
            pasillosDibujables = new List<ObjetoVivo>();
        }

        public static List<ObjetoVivo> ObjetosDibujables
        {
            get { return objetosDibujables;}
        }

        public static void CargarProyectiles()
        {
            for (int i = 0; i < 10; i++)
            {
                AgregarDibujable(new ProyectilView());
            }
        }

        public static void CargarBombas()
        {
            for (int i = 0; i < 20; i++)
            {
                AgregarDibujable(new Bomb());
            }
        }

        public static void AgregarDibujable(ObjetoVivo unDibujable)
        {
         
                objetosDibujables.Add(unDibujable);

        }

        public static ObjetoVivo ObtenerObjetoContundente(ObjetoVivo v)
        {
            foreach (ObjetoVivo o in objetosDibujables)
            {
                if ((o.GetType() == v.GetType()) && (!o.Vivo))
                {
                    return o;
                }
            }
            return null;
        }

        public static void DibujarMapa(SpriteBatch sprite)
        {
            foreach (ObjetoVivo s in pasillosDibujables)
            {
                s.Draw(sprite);
            }
            
            foreach (ObjetoVivo s in objetosDibujables)
            {
                //if (s.Vivo)
                    s.Draw(sprite);
            }
            foreach (ObjetoVivo s in enemigosDibujables)
                if (s.Vivo)
                    s.Draw(sprite);
        

        }

        //public static void EliminarDibujable(ObjetoVivo unDibujable)
        //{
        //    //try
        //    {
        //        objetosDibujables.Remove(unDibujable);
        //    }//catch()

        //}

        public static void CargarMapa()
        {
            // recorro el tablero entero
            for (int vertical = 0; vertical < mapaInterno.DimensionVertical; vertical++)
            {
                for (int horizontal = 0; horizontal < mapaInterno.DimensionHorizontal; horizontal++)
                {
                    Punto p = new Punto(horizontal, vertical);
                    Casilla unaCasilla = mapaInterno.ObtenerCasilla(p);
                    if (unaCasilla.ArticuloContenido != null)
                    {
                        Articulo unArt = unaCasilla.ArticuloContenido;
                        AgregarDibujable(new ArticuloVista(p, unArt, TransformarPuntoEnVector2(p)));
                    }
                    AgregarCasillero(unaCasilla);
                    pasillosDibujables.Add(new PasilloView(TransformarPuntoEnVector2(p)));

                }
            }

            foreach (Personaje p in Juego.Instancia().EnemigosVivos)
            {
                AgregarEnemigo(p);
            }

        }

        private static void AgregarEnemigo(Personaje p)
        {
            Vector2 unVector = TransformarPuntoEnVector2(p.Posicion);
            EnemigoView unEnemigo;
            switch (p.Nombre)
            { 
                case Nombres.cecilio:
                    //Vector2 unVector = TransformarPuntoEnVector2(p.Posicion);
                    unEnemigo = new CecilioView(p);
                    //AgregarDibujable(unEnemigo); 
                    enemigosDibujables.Add(unEnemigo);
                    break;
                case Nombres.lopezReggae:
                    //Vector2 unVector = TransformarPuntoEnVector2(p.Posicion);
                    unEnemigo = new LopezReggaeView(p);
                    //AgregarDibujable(unEnemigo); 
                    enemigosDibujables.Add(unEnemigo);
                    break;
                case Nombres.lopezReggaeAlado:
                    //Vector2 unVector = TransformarPuntoEnVector2(p.Posicion);
                    unEnemigo = new LopezReggaeAladoView(p);
                    //AgregarDibujable(unEnemigo); 
                    enemigosDibujables.Add(unEnemigo);
                    break;
            }
                       
           
        }

        private static void AgregarCasillero(Casilla unCasillero) // No estaria mejor un case?
        {

            Obstaculo unObstaculo = unCasillero.Estado;
            

            if (unObstaculo != null)
            {
                // use reflexion: unica alternativa que se me ocurrio por ahora
                if (unObstaculo.Nombre == Nombres.bAcero)
                {
                    Vector2 unVector = TransformarPuntoEnVector2(unCasillero.Posicion);
                    BloqueAceroView unBloqueDeAcero = new BloqueAceroView(unVector, unCasillero.Posicion, unObstaculo);
                    AgregarDibujable(unBloqueDeAcero);

                }


                if (unObstaculo.Nombre == Nombres.bCemento)
                {
                    Vector2 unVector = TransformarPuntoEnVector2(unCasillero.Posicion);
                    BloqueCementoView unBloqueCementoView = new BloqueCementoView(unVector, unCasillero.Posicion, unObstaculo);
                    AgregarDibujable(unBloqueCementoView);

                }

                if (unObstaculo.Nombre == Nombres.bLadrillo)
                {
                    Vector2 unVector = TransformarPuntoEnVector2(unCasillero.Posicion);
                    BloqueLadrilloView unBloqueLadrilloView = new BloqueLadrilloView(unVector, unCasillero.Posicion, unObstaculo);
                    AgregarDibujable(unBloqueLadrilloView);

                }
            }
        }

        public static Vector2 TransformarPuntoEnVector2(Punto unPunto)
        {
            Vector2 unVector;
            unVector.X = 32 * (unPunto.X) + Game1.mapa.Location.X;
            unVector.Y = 32 * (unPunto.Y) + Game1.mapa.Location.Y;

            return unVector;
        }

        public static void CargarContenido(ContentManager content)
        {
            // Carga de contenidos de objetos que se dibujan en ejecucion
            molotovSprite = content.Load<Texture2D>("Sprites\\" + "BmbMolotov");
            toleToleSprite = content.Load<Texture2D>("Sprites\\" + "BmbTole");
            proyectilSprite = content.Load<Texture2D>("Sprites\\" + "Proyectil");
            pasilloView = content.Load<Texture2D>("Sprites\\" + "ObsPasillo");
            artTimerView = content.Load<Texture2D>("Sprites\\" + "ArtTimer");
            artToleTole = content.Load<Texture2D>("Sprites\\" + "ArtToleTole");
            artChala = content.Load<Texture2D>("Sprites\\" + "ArtChala");
            salida = content.Load<Texture2D>("Sprites\\" + "Salida");

            // Carga de contendios de objetos que provienen del mapa
            foreach (ObjetoVivo s in pasillosDibujables)
            {
                s.LoadContent(content);
            }

            foreach (ObjetoVivo s in objetosDibujables)
            {
                s.LoadContent(content);
            }
            foreach (ObjetoVivo s in enemigosDibujables)
            {
                s.LoadContent(content);
            }


        }
        public static void Actualizar()
        {
            List<ObjetoVivo> objetosActualizablesAux = new List<ObjetoVivo>();
            for (int i = 0; i < objetosDibujables.Count; i++)
            {
                objetosActualizablesAux.Add(objetosDibujables[i]);
            }
            for (int i = 0; i < enemigosDibujables.Count; i++)
            {
                objetosActualizablesAux.Add(enemigosDibujables[i]);
            }

            foreach (ObjetoVivo s in objetosActualizablesAux)
            {
                s.Update();
            }
        }



    }
}