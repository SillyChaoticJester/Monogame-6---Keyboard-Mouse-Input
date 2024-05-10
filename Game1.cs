using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_6___Keyboard___Mouse_Input
{
    public class Game1 : Game
    {
        Texture2D pacRightTexture, pacLeftTexture, pacUpTexture, pacDownTexture, pacSleepTexture;
        Rectangle pacLocation;
        KeyboardState keyboardState;
        SpriteFont text;
        int speed;

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
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            keyboardState = Keyboard.GetState();
            KeyboardState newState = Keyboard.GetState();

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
                pacLocation.X *= -2;
            }
            if (pacLocation.Bottom > _graphics.PreferredBackBufferHeight || pacLocation.Top < 0)
            {
                pacLocation.Y *= -2;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            _spriteBatch.DrawString(text, "Speed: " + speed, new Vector2(0, 0), Color.Black);

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

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}