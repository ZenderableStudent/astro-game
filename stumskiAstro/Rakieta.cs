﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace stumskiAstro
{
    public class Rakieta
    {
        private Texture2D texture;
        private Vector2 position;
        private Texture2D pocisk2D;
        private int nrKlatki;
        private int szerokośćKlatki;
        private Pocisk strzał;
        public Rakieta(Texture2D texture, Texture2D pocisk2D) //musi być public, by działało w klasie Game1, to samo niżej
        {
            this.texture = texture;
            this.pocisk2D = pocisk2D;
            position = new Vector2(210, 480);
            nrKlatki = 0;
            strzał.wystrzelony = false;
        }
        public Vector2 GetPocisk()
        {
            return strzał.position;
        }
        public void Wystrzel()
        {
            strzał.wystrzelony = true;
            strzał.position = position;
        }
        public void deletePocisk()
        {
            strzał.wystrzelony = false;
            strzał.position = new Vector2(1000, 1000); //ustawia pozycje pocisku poza mapę, bez tego wykrywało dalej kolizję
        }
        private struct Pocisk
        {
            public Vector2 position;
            public bool wystrzelony;

        }
        public void LotPocisku()
        {
            if (strzał.wystrzelony)
            {
                strzał.position.Y -= 10;
                if (strzał.position.Y < 0)
                    strzał.wystrzelony = false;

            }
        }
        public Vector2 GetPosition()
        {
            return position;
        }
        public Vector2 GetSize()
        {
            return new Vector2(texture.Width / 6, texture.Height);
        }
        public void MoveL() //musi być public, by działało w klasie Game1
        {
            if(position.X <= 400)
            {
                position.X -= 5;
                if (position.X < 0) //bo position.X może być -5 i wypada poza mapę w moim przypadku
                    position.X = 0;
            }
        }
        public void MoveR() //musi być public, by działało w klasie Game1
        {
            if (position.X >= 0)
            {
                position.X += 5;
                if (position.X > 400) //bo position.X może być 405 i wypada poza mapę
                    position.X = 400;
            }    
        }
        public void MoveU() //musi być public, by działało w klasie Game1
        {
            if (position.Y >= 0)
            {
                position.Y -= 5;
                if (position.Y < 0) //bo position.Y może być -5 i wypada poza mapę
                    position.Y = 0;
            }
        }
        public void MoveD() //musi być public, by działało w klasie Game1
        {
            if (position.Y <= 477)
            {
                position.Y += 5;
                if (position.Y > 477) //bo position.Y może być 482 i wypada poza mapę
                    position.Y = 477;
            }
        }
        public void Draw(Texture2D rakieta, SpriteBatch spriteBatch) //tylko ten fragment kodu w metodzie zgodnie z instrukcją, argument rakieta zostawiony w celu pokazania, że był wczesniej używany
        {
            szerokośćKlatki = texture.Width / 6;

            //Rectangle rectGracza = new Rectangle((int)GetPosition().X,(int)GetPosition().Y, rakieta.Width, rakieta.Height); //zostawione w celu pokazania, że było robione zgodnie z instrukcją
            Rectangle klatka = new Rectangle(nrKlatki * szerokośćKlatki, 0, szerokośćKlatki, texture.Height);
            Rectangle rectGracza = new Rectangle((int)position.X, (int)position.Y, klatka.Width, klatka.Height);
            spriteBatch.Draw(texture, rectGracza, klatka, Color.White);
            if (strzał.wystrzelony)
            {
                spriteBatch.Draw(pocisk2D, strzał.position, Color.White);
            }
            nrKlatki++;
            if (nrKlatki == 6)
                nrKlatki = 0;
            //spriteBatch.Draw(rakieta, rectGracza, Color.White); //poprzednie punkty w instrukcji  
        }
    }
}