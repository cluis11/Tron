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
        private PlayerNode head;
        private int estelas;
        private double speed;
        public int fuel;
        private int fuelConsumption;
        public Direction direction;
        public ColaItem colaItem = new ColaItem();
        public PilaPoder pilaPoder = new PilaPoder();
        public bool isDestroy = false;
        private Texture2D estelaTexuture;

        private float stepSize = 16f; // Tamaño del paso en píxeles
        private float timeElapsed = 0f; // Tiempo transcurrido desde el último movimiento
        private float applyItem = 1f;
        private float itemTimeElapsed = 0f;


        public Player() {}

        public Player(MapNode mapNode, Texture2D texture, Vector2 position, Texture2D estelaTexuture)
        {
            this.head = new PlayerNode(mapNode, texture, position);
            this.estelas = 3;
            this.fuel = 100;
            this.direction = Direction.Right;//(Direction)new Random().Next(0, 3);
            this.estelaTexuture = estelaTexuture;

            double[] speeds = { 1.0, 0.9, 0.8, 0.7, 0.6, 0.5, 0.4, 0.3, 0.2, 0.1 };
            this.speed = speeds[new Random().Next(speeds.Length)];
        }

        public static Player CreateInstance(MapNode mapNode, Texture2D texture, Vector2 position, Texture2D estelaTexture) {
            return new Player(mapNode, texture, position, estelaTexture);
        }

        public void AddEstela() {
            
            
        }

        public void Update(GameTime gameTime)
        {
            HandleInput();

            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeElapsed >= speed && !isDestroy)
            {
                MovePlayer();
                consumeFuel();
                timeElapsed = 0f;  // Reinicia el temporizador después de mover al jugador
            }
            if (colaItem.Front() != null) 
            {
               ApplyItems(gameTime);
            }
        }

        private void ApplyItems(GameTime gameTime) 
        {
            itemTimeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (itemTimeElapsed >= applyItem && colaItem.Front() != null)
            {
                NodoCola nodoItem = colaItem.Dequeue();
                nodoItem.item.ApplyEffect(this);
                itemTimeElapsed = 0f;
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
                    MoverDerecha();
                    head.position.X += stepSize;
                    break;
                case Direction.Left:
                    MoverIzquierda();
                    head.position.X -= stepSize;
                    break;
                case Direction.Up:
                    MoverArriba();
                    head.position.Y -= stepSize;
                    break;
                case Direction.Down:
                    MoverAbajo();
                    head.position.Y += stepSize;
                    break;
            }
        }

        private void CheckNextNode(MapNode node) 
        {
            if (node.contenido is Item)
            {
                if (node.contenido is Combustible)
                {
                    colaItem.Enqueue((Item)node.contenido, 0);
                }
                else
                {
                    colaItem.Enqueue((Item)node.contenido, 1);
                }
            }
            else if (node.contenido is Poder) 
            {
                pilaPoder.Push((Poder)node.contenido);
            }
        }

        public void MoverDerecha()
        {
            CheckNextNode(head.MapNode.derecha);
            head.MapNode = head.MapNode.derecha;
            head.MapNode.contenido = head;
            head.MapNode.izquierda.contenido = null;
        }

        private void MoverIzquierda()
        {
            for (int i = 0; i < speed; i++)
            {
                CheckNextNode(head.MapNode.izquierda);
                head.MapNode = head.MapNode.izquierda;
                head.MapNode.contenido = head;
                head.MapNode.derecha.contenido = null;
            }
        }

        private void MoverArriba()
        {
                CheckNextNode(head.MapNode.arriba);
                head.MapNode = head.MapNode.arriba;
                head.MapNode.contenido = head;
                head.MapNode.abajo.contenido = null;
        }

        private void MoverAbajo()
        {
                CheckNextNode(head.MapNode.abajo);
                head.MapNode = head.MapNode.abajo;
                head.MapNode.contenido = head;
                head.MapNode.arriba.contenido = null;
        }

        private void consumeFuel()
        {
            this.fuelConsumption ++;
            if (this.fuelConsumption >= 5)
            {
                this.fuel -= (int)(this.fuelConsumption / 5);
                Debug.WriteLine($"{fuel}");
                if (fuel <= 0) 
                { 
                    isDestroy = true;
                    head.MapNode.contenido = null; 
                }
                this.fuelConsumption = fuelConsumption % 5;
            }
        }

        public void Explode() 
        {
            isDestroy = true;
            head.MapNode.contenido = null;
        }
    }
}
