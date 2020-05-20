using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Threading;

namespace stumskiAstro
{
    public class Meteor
    {
        private Texture2D texture;
        private Vector2 position;
        private int nrKlatki;
        private Random generujLL;
        public Meteor(Texture2D texture) //musi być public, by działało w klasie Game1
        {
            this.texture = texture;
            generujLL = new Random();
            position = new Vector2(generujLL.Next(100, 300), 0);
            nrKlatki = 0;
        }
        Vector2 GetPosition()
        {
            return position;
        }

        public void Draw(SpriteBatch spriteBatch) //tylko ten fragment kodu w metodzie zgodnie z instrukcją
        {
            int szerokośćKlatki = texture.Width / 3;

            Rectangle klatka = new Rectangle(nrKlatki * szerokośćKlatki, 0, szerokośćKlatki, texture.Height);
            Rectangle rectMeteor = new Rectangle((int)position.X, (int)position.Y, klatka.Width, klatka.Height);
            spriteBatch.Draw(texture, rectMeteor, klatka, Color.White);
            nrKlatki++;
            if (nrKlatki == 3)
                nrKlatki = 0;
        }
    }
}