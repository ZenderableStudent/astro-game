using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using Windows.ApplicationModel.Core;

namespace stumskiAstro
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Rakieta gracz;
        private Texture2D rakieta;
        private Texture2D control;
        private Texture2D niebo;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 480;
            graphics.PreferredBackBufferHeight = 800;
            graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            niebo = Content.Load<Texture2D>("niebo");
            rakieta = Content.Load<Texture2D>("AnimRakiety");
            control = Content.Load<Texture2D>("control");
            
            gracz = new Rakieta(rakieta);
        }
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            //obsługa klawiaturą
            if (Keyboard.GetState().IsKeyDown(Keys.X))
            {
                CoreApplication.Exit();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                gracz.MoveU();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                gracz.MoveD();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                gracz.MoveL();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                gracz.MoveR();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                //TODO: strzelanie
            }

            //obsługa dotykiem
            TouchCollection mscaDotknięte = TouchPanel.GetState();
            foreach (TouchLocation dotyk in mscaDotknięte)
            {
                Vector2 pozDotyku = dotyk.Position;
                if(dotyk.State == TouchLocationState.Moved)
                {
                    //tutaj równanie koła dla każdego z przycisków (x-a)^2+(y-b)^2 <= r^2
                    if (Math.Pow(pozDotyku.X - 110, 2) + (Math.Pow(pozDotyku.Y - 645, 2)) <= 40*40) //a=110, b=645, r=40
                    {
                        gracz.MoveU();
                    }
                    if (Math.Pow(pozDotyku.X - 110, 2) + (Math.Pow(pozDotyku.Y - 740, 2)) <= 40 * 40) //a=110, b=740, r=40
                    {
                        gracz.MoveD();
                    }
                    if (Math.Pow(pozDotyku.X - 60, 2) + (Math.Pow(pozDotyku.Y - 690, 2)) <= 40 * 40) //a=60, b=690, r=40
                    {
                        gracz.MoveL();
                    }
                    if (Math.Pow(pozDotyku.X - 160, 2) + (Math.Pow(pozDotyku.Y - 690, 2)) <= 40 * 40) //a=160, b=690, r=40
                    {
                        gracz.MoveR();
                    }
                }
                if(dotyk.State == TouchLocationState.Pressed)
                {
                    //TODO: strzelanie
                }
            }
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(niebo, new Vector2(0, 0), Color.White); //niebo
            gracz.Draw(rakieta, spriteBatch); //tylko fragment z rectangle zgodnie z instrukcją
            spriteBatch.Draw(control, new Vector2(0, 583), Color.White); //przyciski
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}