using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Fysik_YakupY
{
    class Ball : MoveableObj
    {
        public float radie = 0.25f; // Radien
        float vinkel;               // Nuvarande rotations vinkel
        float vinkelHast;           // rotationshastighet
        float massan;               
        public Color col;
        Vector2 normalen;
        KeyboardState kState;

        public Ball(Vector2 pos, Vector2 vel, Vector2 acc, float angle, float angleVel, float elast, float mass)
            : base(pos, vel, acc)
        {
            this.vinkel = angle;
            this.vinkelHast = angleVel;
            this.elast = elast;
            this.massan = mass;
            col = Color.White;
        }


        public override void Update(GameTime gameTime)
        {
            kState = Keyboard.GetState();

            if (pos.X >= Game1.screenWidth / pixelPerMeter - radie)
            {
                normalen = new Vector2(-1, 0);

                /* Om bollen åkt för långt på skärmen, 
                 * justera genom att flytta tillbaka bollen */
                if (pos.X > Game1.screenWidth / pixelPerMeter - radie)
                    pos.X = Game1.screenWidth / pixelPerMeter - radie;
                Collision();
            }
            if (pos.X <= radie)
            {
                normalen = new Vector2(1, 0);
                if (pos.X < radie)
                    pos.X = radie;
                Collision();
            }
            if (pos.Y >= Game1.screenHeight / pixelPerMeter - radie)
            {
                normalen = new Vector2(0, -1);
                if (pos.Y > (Game1.screenHeight / pixelPerMeter) - radie)
                    pos.Y = (Game1.screenHeight / pixelPerMeter) - radie;
                Collision();
            }
            if (pos.Y <= radie)
            {
                normalen = new Vector2(0, 1);
                if (pos.Y < radie)
                    pos.Y = radie;
                Collision();
            }

            // Ändra rotationsvinkeln med rotationsaccerelationen multiplicerat med tiden
            vinkel += vinkelHast * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Kör uppdate metoden som finns i MoveableObj
            base.Update(gameTime);

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Skapar en skalningsvektor
            Vector2 scale;
            // Skalningsvektorns värden
            scale.X = pixelPerMeter * 2 * radie / Game1.ballTexture.Width;
            scale.Y = pixelPerMeter * 2 * radie / Game1.ballTexture.Height;
            // Axeln för bollens rotation
            Vector2 origin = new Vector2(0.5f * Game1.ballTexture.Width, 0.5f * Game1.ballTexture.Height);
            // Ritar ut bollen
            spriteBatch.Draw(Game1.ballTexture, pixelPerMeter * pos, null, col, vinkel, origin, scale, SpriteEffects.None, 0);
        }

        // Denna metod sköter bollarnas kollision
        public void Collision()
        {
            // p = rörelsemängd

            // Rörelsemängd = vektor(hastigheten) * massan(skalären)
            Vector2 pBefore = hastighet * massan;

            // Vector2.Dot = skalärprodukt
            /* P In = skalärprodukten av P (x)Pinnan (y) normalen 
             * delat på skalärprodukten av (x & y)normalen gånger normalen */
            Vector2 Pin = (Vector2.Dot(pBefore, normalen) / Vector2.Dot(normalen, normalen)) * normalen;

            // P Out = negativ elasticitet * P In
            Vector2 pOut = -Etot() * Pin;
            Vector2 pOutWith = pBefore - Pin;
            Vector2 pAfter = pOutWith + pOut;
            hastighet = pAfter / massan;

            // Rotationen beror på hastigheten
            vinkelHast = (hastighet.X > 0 ? 1 : -1) * ((float)hastighet.Length() / radie);

        }

        // Detta slumpar fram ett tal mellan två decimaltal
        public static double RandNr(double min, double max)
        {
            Random rand = new Random();
            return (max - min) * rand.NextDouble() + min;
        }


        public float Etot()
        {
            // Slumpar en elasticitet mellan 0.7 och 1.0. oändlig elasticitet.
            return elast * (float)RandNr(0.7, 1.0);
        }
    }
}
