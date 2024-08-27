using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

namespace Tron
{
    public class Game1 : Game
    {
        //Texture2D ballTexture;

        //Vector2 ballPosition;
        //float ballSpeed;
        //bool keyPress = false;

        //MovingSprite headSprite;
        //ColorSprite colorHeadSprite;

        //List<MovingSprite> sprites;
        private Texture2D _borderTexture;

        Map mapa;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;

            _graphics.PreferredBackBufferWidth = 800;  // Width of the window in pixels
            _graphics.PreferredBackBufferHeight = 800;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.ApplyChanges();
            //ballPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2); //asigna la posicion de la bola en el centro de la pantalla
            //ballSpeed = 64f;
            mapa = new Map(50,50);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _borderTexture = CreateRectangleBorderTexture(GraphicsDevice, Color.Red);

            // TODO: use this.Content to load your game content here
            //Texture2D headTexture = Content.Load<Texture2D>("head");
            //headSprite = new MovingSprite(headTexture, Vector2.Zero, 1f);
            //colorHeadSprite = new ColorSprite(headTexture, new Vector2(0, 64), Color.Blue);
            //ballTexture = Content.Load<Texture2D>("ball");
            mapa.LoadContent(Content);
            mapa.Initialize_Item_Power();

            /*for (int i = 0; i < 10; i++) {
                sprites.Add(new MovingSprite(headTexture, new Vector2(0, 10 * i), i));
            }*/

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //headSprite.Update(gameTime);

            /*foreach (MovingSprite sprite in sprites) { 
                sprite.Update();
            }*/

            // TODO: Add your update logic here
            /*float updatedBallSpeed = ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            var kstate = Keyboard.GetState();

            if (!keyPress && kstate.IsKeyDown(Keys.Up))
            {
                Debug.WriteLine($"{updatedBallSpeed}, {ballSpeed}");
                keyPress = true;
                ballPosition.Y -= ballSpeed;
            }

            if (kstate.IsKeyUp(Keys.Up))
            {
                keyPress = false;
            }

            if (kstate.IsKeyDown(Keys.Down))
            {
                ballPosition.Y += updatedBallSpeed;
            }

            if (kstate.IsKeyDown(Keys.Left))
            {
                ballPosition.X -= updatedBallSpeed;
            }

            if (kstate.IsKeyDown(Keys.Right))
            {
                ballPosition.X += updatedBallSpeed;
            }*/

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            /*foreach (MovingSprite sprite in sprites) {
                _spriteBatch.Draw(sprite.texture, sprite.Rect, Color.White);
            }*/
            DrawRectangleBorder(new Rectangle(0, 0, 800, 800), 5, Color.Red);
            mapa.Draw(_spriteBatch);
            //_spriteBatch.Draw(headSprite.texture, headSprite.Rect, Color.White);
            //_spriteBatch.Draw(colorHeadSprite.texture, colorHeadSprite.Rect, colorHeadSprite.color);
            //_spriteBatch.Draw(ballTexture, ballPosition, null, Color.White, 0f, new Vector2(ballTexture.Width / 2, ballTexture.Height / 2), Vector2.One, SpriteEffects.None, 0f);
            

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        Texture2D CreateRectangleBorderTexture(GraphicsDevice graphicsDevice, Color color)
        {
            Texture2D texture = new Texture2D(graphicsDevice, 1, 1);
            texture.SetData(new[] { color });
            return texture;
        }

        private void DrawRectangleBorder(Rectangle rect, int borderThickness, Color borderColor)
        {
            // Draw the top border
            _spriteBatch.Draw(_borderTexture, new Rectangle(rect.X, rect.Y, rect.Width, borderThickness), borderColor);

            // Draw the bottom border
            _spriteBatch.Draw(_borderTexture, new Rectangle(rect.X, rect.Bottom - borderThickness, rect.Width, borderThickness), borderColor);

            // Draw the left border
            _spriteBatch.Draw(_borderTexture, new Rectangle(rect.X, rect.Y, borderThickness, rect.Height), borderColor);

            // Draw the right border
            _spriteBatch.Draw(_borderTexture, new Rectangle(rect.Right - borderThickness, rect.Y, borderThickness, rect.Height), borderColor);
        }
    }
}
