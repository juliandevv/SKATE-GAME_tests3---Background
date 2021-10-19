using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace SKATE_GAME_tests3___Background
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        KeyboardState keyboardState;

        Texture2D road, bush, skater;
        SpriteFont title;
        Rectangle roadrect;
        bgObject bush1, tree1;
        List<bgObject> bgObjects;
        Random generator;

        enum Screen
        {
            Intro,
            Road
        }

        Screen currentScreen;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _graphics.PreferredBackBufferWidth = 1200;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();

            roadrect = new Rectangle(0, 400, 1200, 200);
            currentScreen = Screen.Intro;
            bgObjects = new List<bgObject>();
            generator = new Random();

            base.Initialize();

            bgObjects.Add(new bgObject(bush, new Rectangle(600, 366, 64, 64), new Rectangle(0, 0, bush.Width, bush.Height), new Vector2((float)-2, 0)));
            bgObjects.Add(new bgObject(bush, new Rectangle(800,370, 64, 64), new Rectangle(0, 0, bush.Width, bush.Height), new Vector2((float)-2, 0)));
            bgObjects.Add(new bgObject(bush, new Rectangle(200, 370, 64, 64), new Rectangle(0, 0, bush.Width, bush.Height), new Vector2((float)-2, 0)));

            //bgObject[] bgObjects = new bgObject[2] {bush1, tree1};
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            road = Content.Load<Texture2D >("ROAD");
            bush = Content.Load<Texture2D>("Asset 3");
            skater = Content.Load<Texture2D>("boarding_2");
            Content.Load<Texture2D>("boarding_1");
            title = Content.Load<SpriteFont>("titleFont");

        }

        protected override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            if (keyboardState.IsKeyDown(Keys.Space))
                currentScreen = Screen.Road;

            if (currentScreen == Screen.Intro )
            {
                Intro();
            }
            
            else if (currentScreen == Screen.Road)
            {
                Road();
            }


            base.Update(gameTime);
        }

        protected void Intro()
        {

        }

        protected void Road()
        {
            //bush1.move();
            //tree1.move();

            foreach (bgObject i in bgObjects)
            {
                i.move();

                if (i.Bounds.X <= (0 - i.Bounds.Width))
                {
                    i.Bounds = new Rectangle(generator.Next(1200, 1300), generator.Next(350, 370), i.Bounds.Width, i.Bounds.Height);
                }
            }
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            if (currentScreen == Screen.Intro)
            {
                _spriteBatch.Begin();
                DrawIntro();
                _spriteBatch.End();
            }
            
            else if (currentScreen == Screen.Road)
            {
                _spriteBatch.Begin();
                DrawRoad();
                _spriteBatch.End();
            }

            base.Draw(gameTime);
        }

        protected void DrawIntro()
        {
            _spriteBatch.DrawString(title, "SKATE FAST", new Vector2(900, 300), Color.White, 0f, new Vector2(this._graphics.PreferredBackBufferWidth/2, this._graphics.PreferredBackBufferHeight/2), 1f, SpriteEffects.None, 0f);
        }

        protected void DrawRoad()
        {
            _spriteBatch.Draw(road, roadrect, Color.White);

            foreach (bgObject i in bgObjects)
            {
                i.Draw(_spriteBatch);
            }

            _spriteBatch.Draw(skater, new Rectangle(300, 340, 94, 127), Color.White);
        }
    }
}
