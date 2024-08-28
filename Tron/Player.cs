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
        public ColaItem colaItem = new ColaItem();
        public bool isDestroy = false;

        private float stepSize = 16f; // Tamaño del paso en píxeles
        private float timeElapsed = 0f; // Tiempo transcurrido desde el último movimiento
        private float moveInterval = 0.5f; // Intervalo de tiempo en segundos para mover el sprite
        

        public Player() {}

        public Player(MapNode mapNode, Texture2D texture, Vector2 position)
        {
            this.head = new PlayerNode(mapNode, texture, position);
            this.estelas = 3;
            this.speed = 1;//new Random().Next(1, 3);
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
            if (timeElapsed >= moveInterval && !isDestroy)
            {
                MovePlayer();
                consumeFuel();
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
                    MoverDerecha(this.speed);
                    head.position.X += stepSize * this.speed;
                    break;
                case Direction.Left:
                    MoverIzquierda(this.speed);
                    head.position.X -= stepSize * this.speed;
                    break;
                case Direction.Up:
                    MoverArriba(this.speed);
                    head.position.Y -= stepSize * this.speed;
                    break;
                case Direction.Down:
                    MoverAbajo(this.speed);
                    head.position.Y += stepSize * this.speed;
                    break;
            }
        }

        private int CheckNextNode(MapNode node) 
        {
            if (node.contenido is Item) 
            {
                if (node.contenido is Bomba) 
                {
                    return -1;
                }
                else if (node.contenido is Combustible)
                {
                    colaItem.Enqueue((Item)node.contenido, 0);
                }
                else 
                {
                    colaItem.Enqueue((Item)node.contenido, 1);
                }
                return 1;
            }
            return 0;
        }

        public void MoverDerecha(int speed)
        {
            for (int i = 0; i < speed; i++)
            {
                int action = CheckNextNode(head.MapNode.derecha);
                head.MapNode = head.MapNode.derecha;
                head.MapNode.contenido = head;
                head.MapNode.izquierda.contenido = null;
                if (action == -1)
                {
                    isDestroy = true;
                    head.MapNode.contenido = null;
                    return;
                }
            }
        }

        private void MoverIzquierda(int speed)
        {
            for (int i = 0; i < speed; i++)
            {
                int action = CheckNextNode(head.MapNode.izquierda);
                head.MapNode = head.MapNode.izquierda;
                head.MapNode.contenido = head;
                head.MapNode.derecha.contenido = null;
                if (action == -1)
                {
                    isDestroy = true;
                    head.MapNode.contenido = null;
                    return;
                }
            }
        }

        private void MoverArriba(int speed)
        {
            for (int i = 0; i < speed; i++)
            {
                int action = CheckNextNode(head.MapNode.arriba);
                head.MapNode = head.MapNode.arriba;
                head.MapNode.contenido = head;
                head.MapNode.abajo.contenido = null;
                if (action == -1)
                {
                    isDestroy = true;
                    head.MapNode.contenido = null;
                    return;
                }
            }
        }

        private void MoverAbajo(int speed)
        {
            for (int i = 0; i < speed; i++)
            {
                int action = CheckNextNode(head.MapNode.abajo);
                head.MapNode = head.MapNode.abajo;
                head.MapNode.contenido = head;
                head.MapNode.arriba.contenido = null;
                if (action == -1)
                {
                    isDestroy = true;
                    head.MapNode.contenido = null;
                    return;
                }
            }
        }

        private void consumeFuel()
        {
            this.fuelConsumption += this.speed;
            if (this.fuelConsumption >= 5)
            {
                this.fuel -= (int)(this.fuelConsumption / 5);
                if (fuel <= 0) 
                { 
                    isDestroy = true;
                    head.MapNode.contenido = null; 
                }
                this.fuelConsumption = fuelConsumption % 5;
            }
        }
    }
}
