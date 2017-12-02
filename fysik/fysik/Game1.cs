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

namespace Fysik_YakupY
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        double keyTimer = 1000;
        public static int screenWidth, screenHeight;
        Point ballMPos;

        public static Texture2D ballTexture;
        SpriteFont myFont;
        
        Ball ball;
        Ball ball1;
        
        KeyboardState kState;
        
        List<PhysicalObj> ballList;

        public Game1()
        {
            IsFixedTimeStep = false;
            graphics = new GraphicsDeviceManager(this);
            screenWidth = graphics.PreferredBackBufferWidth = 800;
            screenHeight = graphics.PreferredBackBufferHeight = 600;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            ballList = new List<PhysicalObj>();

            ball1 = new Ball(
                /*positionen*/new Vector2((float)Ball.RandNr(3.0, 5.0), (float)0.5),
                /*hastigheten*/ new Vector2(6, 10),
                /*accelerationen*/ new Vector2(0, 9.82f),
                /*Rotationsvinkeln*/ 1,
                /*rotationsaccelerationen*/ 0.5f,
                /*elasticiteten*/ 0.75f,
                /*massan*/ 5.0f);
            ballList.Add(ball1);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ballTexture = Content.Load<Texture2D>(@"8t");
            myFont = Content.Load<SpriteFont>("SpriteFont1");

            IsMouseVisible = true;
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            kState = Keyboard.GetState();

            keyTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;

            // Om "Space" trycks ned: Skapa en ny boll
            if (kState.IsKeyDown(Keys.Space))
            {
                if (keyTimer <= 0)
                {
                    keyTimer = 1000f;
                    // Ta bort tidigare skapade bollar. Ha det bortkommenterat!!
                    //objectListan.Remove(b);
                    ball = new Ball(
                        /* positionen */ new Vector2((float)Ball.RandNr(1.0, 7.0), (float)0.5),
                        /* hastigheten */ new Vector2(6, 11),
                        /* Tyngdkraftens acceleration 
                         * (9,82 är Sveriges tyngdkraftsacceleration) */ new Vector2(0, 9.82f),
                        /* Rotationsvinkeln */ 1,
                        /* Rotationsaccelerationen*/ 0.5f,
                        /* Elasticiteten*/ 0.95f,
                        /* Massan */ 3.0f);
                    ballList.Add(ball);
                }
            }
            if (kState.IsKeyDown(Keys.Delete))
                ballList.Remove(ball);

            for (int i = 0; i < ballList.Count; i++)
                ballList[i].Update(gameTime);

            base.Update(gameTime);
        }

        /* Då Y-axeln börjar uppifrån och går ned
         * "vänd på" axeln. Finns snyggare/effektivare 
         * sätt att koda detta på! */
        int InvertBallOneY()
        {
            if (ballMPos.Y == 0)
                ballMPos.Y = 5;
            else if (ballMPos.Y == 1)
                ballMPos.Y = 4;
            else if (ballMPos.Y == 2)
                ballMPos.Y = 3;
            else if (ballMPos.Y == 3)
                ballMPos.Y = 2;
            else if (ballMPos.Y == 4)
                ballMPos.Y = 1;
            else if (ballMPos.Y == 5)
                ballMPos.Y = 0;

            return ballMPos.Y;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGreen);

            spriteBatch.Begin();

            // Skriv ut första bollens position (i meter)
            ballMPos.X = (int)ball1.pos.X;
            ballMPos.Y = (int)ball1.pos.Y;
            InvertBallOneY();

            string ballX = "Boll nr1 X: " + ballMPos.X + "m";
            string ballY = "Boll nr1 Y: " + ballMPos.Y + "m";

            for (int i = 0; i < ballList.Count(); i++)
                ballList[i].Draw(spriteBatch);

            spriteBatch.DrawString(myFont, ballX, new Vector2(0, 0), Color.Gold);
            spriteBatch.DrawString(myFont, ballY, new Vector2(0, 20), Color.Gold);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}