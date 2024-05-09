using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_6___Keyboard___Mouse_Input
{
    public class Game1 : Game
    {
        Texture2D pacTexture;
        Rectangle pacLocation;
        KeyboardState keyboardState;

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

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            pacTexture = Content.Load<Texture2D>("PacRight");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                pacLocation.Y -= 2;
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                pacLocation.Y += 2;
            }
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                pacLocation.X -= 2;
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                pacLocation.X += 2;
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}