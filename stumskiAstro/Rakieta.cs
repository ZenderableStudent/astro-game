﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stumskiAstro
{
    public class Rakieta
    {
        private Texture2D texture;
        private Vector2 position;
        public Rakieta(Texture2D texture) //musi być public, by działało w klasie Game1
        {
            this.texture = texture;
            position = new Vector2(210, 480);
        }
        Vector2 GetPosition()
        {
            return position;
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
        public void Draw(Texture2D rakieta, SpriteBatch spriteBatch) //tylko ten fragment kodu w metodzie zgodnie z instrukcją
        {
            Rectangle rectGracza = new Rectangle((int)GetPosition().X,(int)GetPosition().Y, rakieta.Width, rakieta.Height);
            spriteBatch.Draw(rakieta, rectGracza, Color.White);
        }
    }
}