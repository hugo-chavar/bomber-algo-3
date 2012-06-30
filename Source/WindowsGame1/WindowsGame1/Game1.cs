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

        BombitaView unaPersona = new BombitaView();

        public static SpriteFont fuente;
        public static SpriteFont fuente2;
        public static Rectangle mapa;
        public static Rectangle pausado;

        bool pausa = true;
        Texture2D pausaTexture;
        Menu unMenu;

        public static Rectangle screen;
        public static string estadoDelJuego = "Inicio";
        
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
            mapa = new Rectangle(100, 75, 32 * elJuego.Ambiente.DimensionHorizontal, 32 * elJuego.Ambiente.DimensionVertical);
            MapaVista.inicialize(elJuego.Ambiente);
            screen = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            unMenu = new Menu();
            MapaVista.CargarMapa();
            MapaVista.CargarProyectiles();
            MapaVista.CargarBombas();

            unaPersona.UnPersonaje = elJuego.Protagonista;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            fuente = Content.Load<SpriteFont>("Fonts\\Segoe14");
            fuente2 = Content.Load<SpriteFont>("Fonts\\Pericles12");
            pausaTexture = Content.Load<Texture2D>("Sprites\\Pausa");
            unaPersona.LoadContent(Content);


             MapaVista.CargarContenido(this.Content);
        }


        protected override void UnloadContent()
        {

        }


        protected override void Update(GameTime gameTime)
        {
            
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || estadoDelJuego == "Salir")
                this.Exit();

            switch (estadoDelJuego)
            {
                case "Jugar":
                    {
                        elJuego.AvanzarElTiempo();
                        MapaVista.Actualizar();
                        unaPersona.Update();
                        break;
                    }
                case "Inicio":
                    {
                        unMenu.Update();
                        break;
                    }
                case "Pausa":
                    {
                        unMenu.Update();
                        break;
                    }
                case "Reiniciar":
                    {
                        elJuego = Juego.Reiniciar();
                        MapaVista.inicialize(elJuego.Ambiente);
                        MapaVista.CargarMapa();
                        MapaVista.CargarProyectiles();
                        MapaVista.CargarBombas();
                        MapaVista.CargarContenido(this.Content);
                        estadoDelJuego = "Jugar";
                        break;
                    }
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            switch (estadoDelJuego)
            {
                case "Jugar":
                    {
                        MapaVista.DibujarMapa(spriteBatch);
                        unaPersona.Draw(spriteBatch);
                        break;
                    }
                case "Inicio":
                    {
                        unMenu.Draw(spriteBatch);
                        break;
                    }
                case "Pausa":
                    {
                        MapaVista.DibujarMapa(spriteBatch);
                        unaPersona.Draw(spriteBatch);
                        spriteBatch.Draw(pausaTexture, screen, Color.White);
                        unMenu.Draw(spriteBatch);
                        break;
                    }

            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
