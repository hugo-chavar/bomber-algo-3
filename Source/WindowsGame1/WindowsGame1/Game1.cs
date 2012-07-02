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

        BombitaView unaPersona = null;

        public static SpriteFont fuente;
        public static SpriteFont fuente2;
        public static Rectangle mapa;
        public static Rectangle pausado;

        Texture2D pausaTexture;
        Texture2D gameOverTexture;
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
            //mapa = new Rectangle(100, 75, 32 * elJuego.Ambiente.DimensionHorizontal, 32 * elJuego.Ambiente.DimensionVertical);
            //MapaVista.inicialize(elJuego.Ambiente);
            screen = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            unMenu = new Menu();
            //MapaVista.CargarMapa();
            //MapaVista.CargarProyectiles();
            //MapaVista.CargarBombas();

            //unaPersona.UnPersonaje = elJuego.Protagonista;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            fuente = Content.Load<SpriteFont>("Fonts\\Segoe14");
            fuente2 = Content.Load<SpriteFont>("Fonts\\Pericles12");
            pausaTexture = Content.Load<Texture2D>("Sprites\\Pausa");
            gameOverTexture = Content.Load<Texture2D>("Sprites\\GameOver");

            //unaPersona.LoadContent(Content);


             //MapaVista.CargarContenido(this.Content);
        }


        protected override void UnloadContent()
        {

        }


        protected override void Update(GameTime gameTime)
        {
            
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || estadoDelJuego == "Salir")
                this.Exit();
            if (elJuego.EstadoGeneral == BombermanModel.Estado.perdido)
                estadoDelJuego = "Perdido";

           switch (estadoDelJuego)
            {
                case "Jugar":
                    {
                        if (elJuego.MapaVisible)
                        {
                            elJuego.AvanzarElTiempo();
                            MapaVista.Actualizar();
                            unaPersona.Update();
                        }
                        else estadoDelJuego = "ChequearMapa";
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
                        elJuego.Recomenzar();
                        elJuego.CargarMapa();
                        PrepararMapa();
                        break;
                    }
                case "ChequearMapa":
                    {
                        //if (!elJuego.MapaVisible)
                        //{
                        //    elJuego.CargarMapa();
                        //    PrepararMapa();
                        //}
                        estadoDelJuego = "Reiniciar";
                        break;
                    }
                case "Guardar":
                    {
                        elJuego.GuardarPartida();
                        estadoDelJuego = "Jugar";
                        break;
                    }
                case "Continuar":
                    {
                        elJuego.ContinuarPartidaGuardada();
                        PrepararMapa();
                        break;
                    }
                case "Perdido":
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
            unaPersona = new BombitaView();
            unaPersona.LoadContent(this.Content);
            estadoDelJuego = "Jugar";
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
                case "Perdido":
                    {
                        MapaVista.DibujarMapa(spriteBatch);
                        //unaPersona.Draw(spriteBatch);
                        spriteBatch.Draw(gameOverTexture, screen, Color.White);
                        unMenu.Draw(spriteBatch);
                        break;
                    }

            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
