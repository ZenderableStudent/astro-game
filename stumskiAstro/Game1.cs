using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using Windows.ApplicationModel.Core;
using Microsoft.Xna.Framework.Audio;
using System.Threading;

namespace stumskiAstro
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Rakieta gracz;
        private Meteor wróg;
        private Meteor wrógDrugi;
        private Texture2D rakieta;
        private Texture2D meteor;
        private Texture2D control;
        private Texture2D niebo;
        private Texture2D pocisk;
        private DateTime timeOfGameOver;    
        private SpriteFont wykrytoKolizje, wykrytoKolizjePocisk;
        public int score = 0;
        private bool isGameOver = false;
        SoundEffectInstance wybuchRaz;
        SoundEffect wybuch;

        enum States //stany gry
        {
            GameOver,
            Game,
        }

        States _state;

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
            _state = States.Game;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            niebo = Content.Load<Texture2D>("niebo");
            rakieta = Content.Load<Texture2D>("AnimRakiety");
            meteor = Content.Load<Texture2D>("meteor");
            pocisk = Content.Load<Texture2D>("pocisk2D");
            control = Content.Load<Texture2D>("control");
            wykrytoKolizje = Content.Load<SpriteFont>("File");
            wykrytoKolizjePocisk = Content.Load<SpriteFont>("KolizjaPocisk");
            wybuch = Content.Load<SoundEffect>("wybuch");
            wróg = new Meteor(meteor, 10);
            wrógDrugi = new Meteor(meteor, 20);
            gracz = new Rakieta(rakieta, pocisk);
            wybuchRaz = wybuch.CreateInstance();
        }
        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            switch (_state) //dwa statusy gry - game i game over
            {
                case States.Game:
                    wróg.Update();
                    wrógDrugi.Update();
                    gracz.LotPocisku();
                    if (wróg.Kolizja(gracz) || wrógDrugi.Kolizja(gracz))
                    {
                        wybuchRaz.Play(); //start przed komunikatem
                        isGameOver = true;
                        _state = States.GameOver; //ustawiamy status na GameOver
                        timeOfGameOver = DateTime.Now;
                    }

                    //obsługa klawiaturą
                    if (Keyboard.GetState().IsKeyDown(Keys.X))
                    {
                        CoreApplication.Exit(); //NavigationService GoBack() -> nie można dodać takiej biblioteki
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
                        gracz.Wystrzel();
                    }

                    //obsługa dotykiem
                    TouchCollection mscaDotknięte = TouchPanel.GetState();
                    foreach (TouchLocation dotyk in mscaDotknięte)
                    {
                        Vector2 pozDotyku = dotyk.Position;
                        if (dotyk.State == TouchLocationState.Moved)
                        {
                            //tutaj równanie koła dla każdego z przycisków (x-a)^2+(y-b)^2 <= r^2
                            if (Math.Pow(pozDotyku.X - 110, 2) + (Math.Pow(pozDotyku.Y - 645, 2)) <= 40 * 40) //a=110, b=645, r=40
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
                        if (dotyk.State == TouchLocationState.Pressed)
                        {
                            if (Math.Pow(pozDotyku.X - 375, 2) + (Math.Pow(pozDotyku.Y - 695, 2)) <= 40 * 40) //a=375, b=695, r=40
                            {
                                gracz.Wystrzel();
                            }
                        }
                    }
                    break;
                case States.GameOver:
                    if ((DateTime.Now - timeOfGameOver).TotalSeconds >= 3) //wyłącz aplikację po trzech sekundach
                    {
                        wybuchRaz.Stop(); //stop po komunikacie
                        Thread.Sleep(500); //chwila przerwy po zakończeniu dzwięku, ogólnie zaleca się unikać Thread w MonoGame - tu nie ma znaczenia i tak symulujemy wciśnięcie "Back" po chwili
                        CoreApplication.Exit(); //NavigationService GoBack() -> nie można dodać takiej biblioteki
                    }
                    break;
            }
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            spriteBatch.Draw(niebo, new Vector2(0, 0), Color.White); //niebo
            gracz.Draw(rakieta, spriteBatch); //tylko fragment z rectangle zgodnie z instrukcją
            wróg.Draw(spriteBatch);
            wrógDrugi.Draw(spriteBatch);
            spriteBatch.Draw(control, new Vector2(0, 583), Color.White); //przyciski
            if (isGameOver == true)
            {
                spriteBatch.DrawString(wykrytoKolizje, "Game Over!!!", new Vector2(90, 300), Color.White); //monit o końcu gry
            }
            spriteBatch.DrawString(wykrytoKolizjePocisk, "Score:" + (wróg.GetScore() + wrógDrugi.GetScore()), new Vector2(410, 780), Color.White); //wynik gracza

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}