using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Monogame_6___Keyboard___Mouse_Input
{
    public class Game1 : Game
    {
        Texture2D pacRightTexture, pacLeftTexture, pacUpTexture, pacDownTexture, pacSleepTexture, exitTexture, barrierTexture, coinTexture;
        Rectangle pacLocation, exitRect;
        List<Rectangle> coins;
        List<Rectangle> barriers;
        KeyboardState keyboardState;
        MouseState mouseState;
        SpriteFont text;
        int speed, speedHelp;

        private KeyboardState oldState;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 500;
            this.Window.Title = "Keyboard and Mouse Stuff";
            pacLocation = new Rectangle(10, 10, 75, 75);
            speed = 2;
            speedHelp = speed - 1;
            barriers = new List<Rectangle>();
            barriers.Add(new Rectangle(0, 250, 350, 75));
            barriers.Add(new Rectangle(450, 250, 350, 75));
            coins = new List<Rectangle>();
            coins.Add(new Rectangle(400, 50, 50, 50));
            coins.Add(new Rectangle(475, 50, 50, 50));
            coins.Add(new Rectangle(200, 300, 50, 50));
            coins.Add(new Rectangle(400, 300, 50, 50));
            exitRect = new Rectangle(700, 380, 100, 100);
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            pacRightTexture = Content.Load<Texture2D>("PacRight");
            pacLeftTexture = Content.Load<Texture2D>("PacLeft");
            pacUpTexture = Content.Load<Texture2D>("PacUp");
            pacDownTexture = Content.Load<Texture2D>("PacDown");
            pacSleepTexture = Content.Load<Texture2D>("PacSleep");
            text = Content.Load<SpriteFont>("speedtext");

            barrierTexture = Content.Load<Texture2D>("rock_barrier");
            exitTexture = Content.Load<Texture2D>("hobbit_door");
            coinTexture = Content.Load<Texture2D>("coin");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            keyboardState = Keyboard.GetState();
            KeyboardState newState = Keyboard.GetState();
            mouseState = Mouse.GetState();

            if (oldState.IsKeyUp(Keys.E) && newState.IsKeyDown(Keys.E))
            {
                speed++;
            }
            if (oldState.IsKeyUp(Keys.Q) && newState.IsKeyDown(Keys.Q))
            {
                speed--;
            }
            if (oldState.IsKeyUp(Keys.W) && newState.IsKeyDown(Keys.W))
            {
                speed = 2;
            }

            oldState = newState;  

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                pacLocation.Y -= speed;
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                pacLocation.Y += speed;
            }
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                pacLocation.X -= speed;
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                pacLocation.X += speed;
            }

            if (pacLocation.Right > _graphics.PreferredBackBufferWidth || pacLocation.Left < 0)
            {
                pacLocation.X *= -speedHelp;
            }
            if (pacLocation.Bottom > _graphics.PreferredBackBufferHeight || pacLocation.Top < 0)
            {
                pacLocation.Y *= -speedHelp;
            }

            //foreach (Rectangle barrier in barriers)
            //    if (pacLocation.Intersects(barrier))
                    //pacLocation.Offset(-pacSpeed);

            for (int i = 0; i < coins.Count; i++)
            {
                if (pacLocation.Intersects(coins[i]))
                {
                    coins.RemoveAt(i);
                    i--;
                }
            }

            if (exitRect.Contains(pacLocation))
                Exit(); 

            if (mouseState.LeftButton == ButtonState.Pressed)
                if (exitRect.Contains(mouseState.X, mouseState.Y))
                    Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                _spriteBatch.Draw(pacUpTexture, pacLocation, Color.White);
            }
            else if (keyboardState.IsKeyDown(Keys.Down))
            {
                _spriteBatch.Draw(pacDownTexture, pacLocation, Color.White);
            }
            else if (keyboardState.IsKeyDown(Keys.Left))
            {
                _spriteBatch.Draw(pacLeftTexture, pacLocation, Color.White);
            }
            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                _spriteBatch.Draw(pacRightTexture, pacLocation, Color.White);
            }
            else
            {
                _spriteBatch.Draw(pacSleepTexture, pacLocation, Color.White);
            }

            foreach (Rectangle barrier in barriers)
                _spriteBatch.Draw(barrierTexture, barrier, Color.White);
            _spriteBatch.Draw(exitTexture, exitRect, Color.White);
            foreach (Rectangle coin in coins)
                _spriteBatch.Draw(coinTexture, coin, Color.White);
            _spriteBatch.DrawString(text, "Speed: " + speed, new Vector2(0, 0), Color.Black);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}