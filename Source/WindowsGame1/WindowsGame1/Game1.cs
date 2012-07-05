using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using BombermanModel.Juego;

namespace BombermanGame
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Juego elJuego = Juego.Instancia();
        public GamerServicesComponent gamerServices;

        private BombitaVista unaPersona = null;

        public static SpriteFont fuente;
        public static SpriteFont fuente2;
        public static Rectangle mapa;
        public static Rectangle pausado;

        private Texture2D pausaTexture;
        private Texture2D gameOverTexture;
        private Texture2D juegoGanadoTexture;
        private Menu unMenu;

        public static Rectangle screen;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            Inicializar();
            base.Initialize();
        }

        private void Inicializar()
        {

            screen = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            unMenu = new Menu();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            fuente = Content.Load<SpriteFont>("Fonts\\Segoe14");
            fuente2 = Content.Load<SpriteFont>("Fonts\\Pericles12");
            pausaTexture = Content.Load<Texture2D>("Sprites\\Pausa");
            gameOverTexture = Content.Load<Texture2D>("Sprites\\GameOver");
            juegoGanadoTexture = Content.Load<Texture2D>("Sprites\\JuegoGanado");
        }


        protected override void UnloadContent()
        {

        }


        protected override void Update(GameTime gameTime)
        {
            
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || elJuego.Salir())
                this.Exit();


            switch (elJuego.EstadoGeneral)
            {
                case BombermanModel.Estado.EnJuego:
                    {
                        elJuego.AvanzarElTiempo();
                        MapaVista.Actualizar();
                        unaPersona.Update();
                        break;
                    }

                case BombermanModel.Estado.Reiniciar:
                    {
                        elJuego.ComenzarDesdeElPrincipio();
                        elJuego.SeleccionarMapa();
                        break;
                    }
                case BombermanModel.Estado.RecargarMapa:
                    {
                        elJuego.CargarMapa();
                        PrepararMapa();
                        elJuego.Jugar();
                        break;
                    }
                case BombermanModel.Estado.GuardarPartida:
                    {
                        elJuego.GuardarPartida();
                        break;
                    }
                case BombermanModel.Estado.ContinuarPartidaGuardada:
                    {
                        elJuego.SeleccionarMapa();
                        break;
                    }
                default:
                    {
                        unMenu.Update();
                        break;
                    }
            }      

            base.Update(gameTime);
        }

        private void PrepararMapa()
        {
            mapa = new Rectangle(100, 75, 32 * elJuego.Ambiente.DimensionHorizontal, 32 * elJuego.Ambiente.DimensionVertical);
            MapaVista.inicialize(elJuego.Ambiente);
            MapaVista.CargarMapa();
            MapaVista.CargarProyectiles();
            MapaVista.CargarBombas();
            
            MapaVista.CargarContenido(this.Content);
            unaPersona = new BombitaVista();
            unaPersona.LoadContent(this.Content);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            switch (elJuego.EstadoGeneral)
            {
                case BombermanModel.Estado.EnJuego:
                    {
                        MapaVista.DibujarMapa(spriteBatch);
                        unaPersona.Draw(spriteBatch);
                        break;
                    }
                case BombermanModel.Estado.Inicial:
                    {
                        unMenu.Draw(spriteBatch);
                        break;
                    }
                case BombermanModel.Estado.Pausa:
                    {
                        MapaVista.DibujarMapa(spriteBatch);
                        unaPersona.Draw(spriteBatch);
                        spriteBatch.Draw(pausaTexture, screen, Color.White);
                        unMenu.Draw(spriteBatch);
                        break;
                    }
                case BombermanModel.Estado.Perdido:
                    {
                        MapaVista.DibujarMapa(spriteBatch);
                        spriteBatch.Draw(gameOverTexture, screen, Color.White);
                        unMenu.Draw(spriteBatch);
                        break;
                    }
                case BombermanModel.Estado.Ganado:
                    {
                        MapaVista.DibujarMapa(spriteBatch);
                        spriteBatch.Draw(juegoGanadoTexture, screen, Color.White);
                        unMenu.Draw(spriteBatch);
                        break;
                    }

            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
