using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

namespace Tron
{
    public class Game1 : Game
    {
        //bool keyPress = false;

       
        private Texture2D _borderTexture;

        private Map mapa;
        private SpriteFont font;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D arrowTexture;


        Texture2D _pixel;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 1000;  // Width of the window in pixels
            _graphics.PreferredBackBufferHeight = 800;
            _graphics.ApplyChanges();
            //ballPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2); //asigna la posicion de la bola en el centro de la pantalla
            //ballSpeed = 64f;
            mapa = new Map(50,50);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            arrowTexture = Content.Load<Texture2D>("arrow");

            _pixel = new Texture2D(GraphicsDevice, 1, 1);
            _pixel.SetData(new[] { Color.White });

            _borderTexture = CreateRectangleBorderTexture(GraphicsDevice, Color.Red);

            // TODO: use this.Content to load your game content here

            font = Content.Load<SpriteFont>("Fonts/tfont");

            mapa.LoadContent(Content);
            mapa.Initialize_Item_Power();
            mapa.Initialize_Player();
            mapa.Initialize_Enemy();
            /*for (int i = 0; i < 10; i++) {
                sprites.Add(new MovingSprite(headTexture, new Vector2(0, 10 * i), i));
            }*/

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            mapa.update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            for (int y = 0; y < 800; y += 16)
            {
                for (int x = 0; x < 800; x += 16)
                {
                    // Top border
                    _spriteBatch.Draw(_pixel, new Rectangle(x, y, 16, 1), Color.White);
                    // Bottom border
                    _spriteBatch.Draw(_pixel, new Rectangle(x, y + 15, 16, 1), Color.White);
                    // Left border
                    _spriteBatch.Draw(_pixel, new Rectangle(x, y, 1, 16), Color.White);
                    // Right border
                    _spriteBatch.Draw(_pixel, new Rectangle(x + 15, y, 1, 16), Color.White);
                }
            }

         
            //DrawRectangleBorder(new Rectangle(0, 0, 800, 800), 5, Color.Red);
            mapa.Draw(_spriteBatch);
            if (mapa.player != null) { mapa.player.Draw(_spriteBatch); mapa.player.DrawInfo(_spriteBatch, font, arrowTexture); }
            if (mapa.enemy != null) { mapa.enemy.Draw(_spriteBatch); }
            

            //_spriteBatch.DrawString(font, "Player fuel:"+mapa.player.fuel, new Vector2(850, 50), Color.White);
            //_spriteBatch.DrawString(font, "Enemy fuel:" + mapa.enemy.fuel, new Vector2(850, 100), Color.Red);

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
