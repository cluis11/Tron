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
        public int fuel;
        private int fuelConsumption;
        public Direction direction;

        private float stepSize = 16f; // Tamaño del paso en píxeles
        private float timeElapsed = 0f; // Tiempo transcurrido desde el último movimiento
        private float moveInterval = 1f; // Intervalo de tiempo en segundos para mover el sprite


        public Player() {}

        public Player(MapNode mapNode, Texture2D texture, Vector2 position)
        {
            this.head = new PlayerNode(mapNode, texture, position);
            this.estelas = 3;
            this.speed = 2;//new Random().Next(1, 3);
            this.fuel = 100;
            this.direction = Direction.Right;//(Direction)new Random().Next(0, 3);
        }

        public static Player CreateInstance(MapNode mapNode, Texture2D texture, Vector2 position) {
            return new Player(mapNode, texture, position);
        }

        public void AddEstela() {
            PlayerNode current = head;
            while (current.Next != null) {
                current = current.Next;
            }
            switch(direction)
            {
                case Direction.Left:
                    break;
                case Direction.Right:
                    break;
                case Direction.Up: 
                    break;
                case Direction.Down:
                    break;
            }
            //current.Next = new PlayerNode();
        }

        public void Update(GameTime gameTime)
        {
            HandleInput();

            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeElapsed >= moveInterval)
            {
                MovePlayer();
                consumeFuel();
                Debug.WriteLine($"{this.fuel}");
                timeElapsed = 0f;  // Reinicia el temporizador después de mover al jugador
            }
        }

        private void HandleInput()
        {
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
        }

        private void MovePlayer()
        {
            switch (direction)
            {
                case Direction.Right:
                    head.MoverDerecha(this.speed);
                    head.position.X += 16f * this.speed;
                    break;
                case Direction.Left:
                    head.MoverIzquierda(this.speed);
                    head.position.X -= 16f * this.speed;
                    break;
                case Direction.Up:
                    head.MoverArriba(this.speed);
                    head.position.Y -= 16f * this.speed;
                    break;
                case Direction.Down:
                    head.MoverAbajo(this.speed);
                    head.position.Y += 16f * this.speed;
                    break;
            }
        }

        private void consumeFuel()
        {
            this.fuelConsumption += this.speed;
            if (this.fuelConsumption >= 5)
            {
                this.fuel -= (int)(this.fuelConsumption / 5);
                this.fuelConsumption = fuelConsumption % 5;
            }
        }
    }
}
