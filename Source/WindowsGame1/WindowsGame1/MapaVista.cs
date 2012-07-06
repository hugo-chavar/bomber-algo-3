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
    public class MapaVista 
    {
        //instancia Singleton
        private static MapaVista instanciaMapaVista;
        
        private Tablero mapaInterno; 
        private List<ObjetoVivo> pasillosDibujables;
        private List<ObjetoVivo> objetosDibujables;
        private List<ObjetoVivo> enemigosDibujables;

        private Texture2D molotovSprite; 
        private Texture2D toleToleSprite;
        private Texture2D proyectilSprite;
        private Texture2D artTimerSprite;
        private Texture2D artToleToleSprite;
        private Texture2D artChalaSprite;
        private Texture2D salidaSprite;

        public Texture2D MolotovSprite
        {
            get { return this.molotovSprite; }
        }

        public Texture2D ToleToleSprite
        {
            get { return this.toleToleSprite; }
        }

        public Texture2D ProyectilSprite
        {
            get { return this.proyectilSprite; }
        }

        public Texture2D ArtTimerSprite
        {
            get { return this.artTimerSprite; }
        }

        public Texture2D ArtToleToleSprite
        {
            get { return this.artToleToleSprite; }
        }

        public Texture2D ArtChalaSprite
        {
            get { return this.artChalaSprite; }
        }

        public Texture2D SalidaSprite
        {
            get { return this.salidaSprite; }
        }


        private MapaVista()
        {
           
        }

        public static MapaVista Instancia()
        {
           if (instanciaMapaVista == null)
            {
                instanciaMapaVista = new MapaVista();
            }
            return instanciaMapaVista;
        }


        public void Inicializar(Tablero unMapa)
        {
            mapaInterno = unMapa;
            objetosDibujables = new List<ObjetoVivo>();
            enemigosDibujables = new List<ObjetoVivo>();
            pasillosDibujables = new List<ObjetoVivo>();
            
        }


        public List<ObjetoVivo> ObjetosDibujables
        {
            get { return objetosDibujables;}
        }

        public void CargarProyectiles()
        {
            for (int i = 0; i < 10; i++)
            {
                AgregarDibujable(new ProyectilVista());
            }
        }

        public void CargarBombas() 
        {
            for (int i = 0; i < 25; i++)
            {
                AgregarDibujable(new BombaVista());
            }
        }

        public void AgregarDibujable(ObjetoVivo unDibujable) 
        {
         
                objetosDibujables.Add(unDibujable);

        }

        public ObjetoVivo ObtenerObjetoContundente(ObjetoVivo v) 
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

        public void DibujarMapa(SpriteBatch sprite)
        {
            foreach (ObjetoVivo s in pasillosDibujables)
            {
                s.Draw(sprite);
            }
            
            foreach (ObjetoVivo s in objetosDibujables)
            {
                s.Draw(sprite);
            }
            foreach (ObjetoVivo s in enemigosDibujables)
                if (s.Vivo)
                    s.Draw(sprite);
        }

        public void CargarMapa()
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
                    pasillosDibujables.Add(new PasilloVista(TransformarPuntoEnVector2(p)));
                }
            }

            foreach (Personaje p in Juego.Instancia().EnemigosVivos)
            {
                AgregarEnemigo(p);
            }

        }

        private void AgregarEnemigo(Personaje p)
        {
            Vector2 unVector = TransformarPuntoEnVector2(p.Posicion);
            EnemigoVista unEnemigo;
            switch (p.Nombre)
            { 
                case Nombres.cecilio:
                    unEnemigo = new CecilioVista(p);
                    enemigosDibujables.Add(unEnemigo);
                    break;
                case Nombres.lopezReggae:
                    unEnemigo = new LopezReggaeView(p);
                    enemigosDibujables.Add(unEnemigo);
                    break;
                case Nombres.lopezReggaeAlado:
                    unEnemigo = new LopezReggaeAladoView(p);
                    enemigosDibujables.Add(unEnemigo);
                    break;
            }
                       
           
        }

        private void AgregarCasillero(Casilla unCasillero)
        {

            Obstaculo unObstaculo = unCasillero.Estado;
            

            if (unObstaculo != null)
            {
                // use reflexion
                if (unObstaculo.Nombre == Nombres.bAcero)
                {
                    Vector2 unVector = TransformarPuntoEnVector2(unCasillero.Posicion);
                    BloqueAceroVista unBloqueDeAcero = new BloqueAceroVista(unVector, unCasillero.Posicion, unObstaculo);
                    AgregarDibujable(unBloqueDeAcero);
                }

                if (unObstaculo.Nombre == Nombres.bCemento)
                {
                    Vector2 unVector = TransformarPuntoEnVector2(unCasillero.Posicion);
                    BloqueCementoVista unBloqueCementoView = new BloqueCementoVista(unVector, unCasillero.Posicion, unObstaculo);
                    AgregarDibujable(unBloqueCementoView);
                }

                if (unObstaculo.Nombre == Nombres.bLadrillo)
                {
                    Vector2 unVector = TransformarPuntoEnVector2(unCasillero.Posicion);
                    BloqueLadrilloVista unBloqueLadrilloView = new BloqueLadrilloVista(unVector, unCasillero.Posicion, unObstaculo);
                    AgregarDibujable(unBloqueLadrilloView);
                }
            }
        }

        public Vector2 TransformarPuntoEnVector2(Punto unPunto)
        {
            Vector2 unVector;
            unVector.X = 32 * (unPunto.X) + Game1.mapa.Location.X;
            unVector.Y = 32 * (unPunto.Y) + Game1.mapa.Location.Y;

            return unVector;
        }

        public Punto TransformarVector2EnPunto(Vector2 unv2)
        {
            int a = (int)((unv2.X - Game1.mapa.Location.X) / 32);
            int b = (int)((unv2.Y - Game1.mapa.Location.Y) / 32);
            Punto unPunto = new Punto(a,b);

            return unPunto;
        }

        public void CargarContenido(ContentManager content)
        {
            // Carga de contenidos de objetos que se dibujan en ejecucion
            molotovSprite = content.Load<Texture2D>("Sprites\\" + "BmbMolotov");
            toleToleSprite = content.Load<Texture2D>("Sprites\\" + "BmbTole");
            proyectilSprite = content.Load<Texture2D>("Sprites\\" + "Proyectil");
            artTimerSprite = content.Load<Texture2D>("Sprites\\" + "NewTimer");
            artToleToleSprite = content.Load<Texture2D>("Sprites\\" + "ArtToleTole");
            artChalaSprite = content.Load<Texture2D>("Sprites\\" + "ArtChala");
            salidaSprite = content.Load<Texture2D>("Sprites\\" + "Salida");

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
        public void Actualizar()
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