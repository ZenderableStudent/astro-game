using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
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
                Vector2 pozDotyku = dotyk.Position; //równanie okręgu dla kliknięcie też?
                if(dotyk.State == TouchLocationState.Moved)
                {
                    //jeśli pozDotyku spełnia któreś z równać okręgu to dana akcja, punkt S=(a,b)
                    Vector2 upButton = new Vector2(110, 645); //r=40
                    Vector2 downButton = new Vector2(110, 740); //r=40
                    Vector2 leftButton = new Vector2(60, 690); //r=40
                    Vector2 rightButton = new Vector2(160, 690); //r=40
                   
                    //dotyk musiałby mieć promień mniejszy niż przycisk?
                    if (pozDotyku == upButton)
                    {
                        gracz.MoveU();
                    }
                    if (pozDotyku == downButton)
                    {
                        gracz.MoveD();
                    }
                    if (pozDotyku == leftButton)
                    {
                        gracz.MoveL();
                    }
                    if (pozDotyku == rightButton)
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
            spriteBatch.Draw(niebo, new Vector2(0, 0), Color.White);
            gracz.Draw(rakieta, spriteBatch); //tylko fragment z rectangle zgodnie z instrukcją
            spriteBatch.Draw(control, new Vector2(0, 583), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}