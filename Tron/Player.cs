using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace Tron
{

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
    internal class Player
    {
        public PlayerNode head;
        private int estelas;
        private int speed;
        public Direction direction;

        private float stepSize = 16f; // Tamaño del paso en píxeles
        private float timeElapsed = 0f; // Tiempo transcurrido desde el último movimiento
        private float moveInterval = 1f; // Intervalo de tiempo en segundos para mover el sprite


        public Player() {}

        public Player(MapNode mapNode, Texture2D texture, Vector2 position)
        {
            this.head = new PlayerNode(mapNode, texture, position);
            this.estelas = 3;
            this.speed = 1;
            this.direction = (Direction)new Random().Next(0, 3);
        }

        public static Player CreateInstance(MapNode mapNode, Texture2D texture, Vector2 position) {
            return new Player(mapNode, texture, position);
        }

        public void Update(GameTime gameTime) {

            // Check for direction change based on key inputs
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Right) && direction != Direction.Left)
            {
                direction = Direction.Right;
            }
            else if (keyboardState.IsKeyDown(Keys.Left) && direction != Direction.Right)
            {
                direction = Direction.Left;
            }
            else if (keyboardState.IsKeyDown(Keys.Up) && direction != Direction.Down)
            {
                direction = Direction.Up;
            }
            else if (keyboardState.IsKeyDown(Keys.Down) && direction != Direction.Up)
            {
                direction = Direction.Down;
            }

            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeElapsed >= moveInterval)
            {
                if (direction == Direction.Right)
                {
                    head.MoverDerecha();
                    head.position.X += 16f;
                }
                else if (direction == Direction.Left) 
                {
                    head.MoverIzquierda();
                    head.position.X -= 16f;
                }
                else if (direction == Direction.Up)
                {
                    head.MoverArriba();
                    head.position.Y -= 16f;
                }
                else if (direction == Direction.Down)
                {
                    head.MoverAbajo();
                    head.position.Y += 16f;
                }
                timeElapsed = 0f;
            }    
        }

        /*public override void Update(GameTime gameTime) { 
            base.Update(gameTime);
            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeElapsed >= moveInterval)
            {
                // Mover el sprite 16 píxeles en la coordenada X
                position.X += stepSize;

                // Reiniciar el temporizador
                timeElapsed = 0f;
            }

        }*/
    }
}
