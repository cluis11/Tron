using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.Globalization;

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
        protected PlayerNode head;
        protected int estelas;
        protected double speed;
        protected int speedIdx;
        public int fuel;
        protected int fuelConsumption;
        public Direction direction;
        public ColaItem colaItem = new ColaItem();
        public PilaPoder pilaPoder = new PilaPoder();
        public bool isDestroy = false;
        protected Texture2D estelaTexuture;

        protected float stepSize = 16f; // Tamaño del paso en píxeles
        protected float timeElapsed = 0f; // Tiempo transcurrido desde el último movimiento
        protected float applyItem = 1f;
        protected float itemTimeElapsed = 0f;

        private bool isEnter = false;
        private bool isQ = false;
        private bool isE = false;
        public bool isShield = false;
        protected int shield = 0;
        protected int hyperspeed = 0;


        protected double[] speeds = { 1.0, 0.9, 0.8, 0.7, 0.6, 0.5, 0.4, 0.3, 0.2, 0.1 };



        public Player() {}

        public Player(MapNode mapNode, Texture2D texture, Vector2 position, Texture2D estelaTexuture)
        {
            this.head = new PlayerNode(mapNode, 0, texture, position);
            this.estelas = 3;
            this.fuel = 100;
            this.direction = (Direction)new Random().Next(0, 3);
            this.estelaTexuture = estelaTexuture;
            
            this.speedIdx = new Random().Next(speeds.Length);
            this.speed = speeds[speedIdx];
        }

        public static Player CreateInstance(MapNode mapNode, Texture2D texture, Vector2 position, Texture2D estelaTexture) {
            return new Player(mapNode, texture, position, estelaTexture);
        }


        public void IncreaseEstela() { estelas++; }
        protected void AddEstela() {
            if (estelas != 0)
            {
                PlayerNode current = head;
                MapNode previus = head.MapNode;
                while (current.Next != null)
                {
                    current = current.Next;
                    previus = current.MapNode;
                }
                current.Next = new PlayerNode(previus, 1, estelaTexuture, new Vector2(previus.y * 16f, previus.x * 16f));
                estelas--;
            }
        }



        public virtual void Update(GameTime gameTime)
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

        protected void ApplyItems(GameTime gameTime) 
        {
            itemTimeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (itemTimeElapsed >= applyItem && colaItem.Front() != null)
            {
                NodoCola nodoItem = colaItem.Dequeue();
                nodoItem.item.ApplyEffect(this);
                itemTimeElapsed = 0f;
            }
        }

        protected virtual void HandleInput()
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

            if (keyboardState.IsKeyDown(Keys.Q) && !isQ) 
            {
                isQ = true;
                if (pilaPoder.TopPila() != null)
                {
                    pilaPoder.Update(-20);
                }
            }
            if (keyboardState.IsKeyDown(Keys.E) && !isE)
            {
                isE = true;
                if (pilaPoder.TopPila() != null)
                {
                    pilaPoder.Update(20);
                }
            }
            if (keyboardState.IsKeyDown(Keys.Enter) && !isEnter) 
            {
                isEnter = true;
                AplicarPoder();
            }
            if (Keyboard.GetState().IsKeyUp(Keys.Enter)) { isEnter = false; }
            if (Keyboard.GetState().IsKeyUp(Keys.Q)) { isQ = false; }
            if (Keyboard.GetState().IsKeyUp(Keys.E)) { isE = false; }
        }

        protected void AplicarPoder() 
        {
            if (pilaPoder.TopPila() != null) 
            {
                pilaPoder.OrdenarPila();
                NodoPila poder = pilaPoder.Pop();
                poder.Poder.ApplyEffect(this);
                

            }
        }

        protected void MovePlayer()
        {
            switch (direction)
            {
                case Direction.Right:
                    MoverDerecha();
                    //head.position.X += stepSize;
                    break;
                case Direction.Left:
                    MoverIzquierda();
                    //head.position.X -= stepSize;
                    break;
                case Direction.Up:
                    MoverArriba();
                    //head.position.Y -= stepSize;
                    break;
                case Direction.Down:
                    MoverAbajo();
                    //head.position.Y += stepSize;
                    break;
            }
            CheckPowerDuration();
            UpdatePosition();
        }


        protected void CheckPowerDuration() 
        {
            if (shield > 0) { shield--; }
            if (shield == 0) { isShield = false; }
            if (hyperspeed > 0) { hyperspeed--; }
            if (hyperspeed == 0) { speed = speeds[speedIdx]; }
        }

        protected void UpdatePosition() 
        {
            PlayerNode current = head;
            while (current != null) 
            {
                current.position.X = current.MapNode.y * 16f;
                current.position.Y = current.MapNode.x * 16f;
                current = current.Next;
            }
        }

        protected bool CheckNextNode(MapNode node) 
        {
            if (node == null)
            {
                Explode();
                return false;
            }
            else if (node.contenido is PlayerNode) 
            {
                if (!isShield) 
                {
                    Explode();
                    return false;
                }
            }
            else if (node.contenido is Item)
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
            return true;
        }

        protected void MoverEstelas(MapNode previous) 
        {
            PlayerNode current = head;
            MapNode temp = head.MapNode;
            while (current.Next != null)
            {
                current = current.Next;
                if (current.MapNode != previous) 
                {
                    temp = current.MapNode;
                    current.MapNode = previous;
                    current.MapNode.contenido = current;
                    previous = temp;
                    if (current.Next == null) { previous.contenido = null; }
                }
            }
        }

        protected void MoverDerecha()
        {
            if (CheckNextNode(head.MapNode.derecha))
            {
                AddEstela();
                head.MapNode = head.MapNode.derecha;
                head.MapNode.contenido = head;
                MoverEstelas(head.MapNode.izquierda);
            }
        }

        protected void MoverIzquierda()
        {
            if (CheckNextNode(head.MapNode.izquierda)){
                AddEstela();
                head.MapNode = head.MapNode.izquierda;
                head.MapNode.contenido = head;
                MoverEstelas(head.MapNode.derecha);
            }
        }

        protected void MoverArriba()
        {
            if (CheckNextNode(head.MapNode.arriba))
            {
                AddEstela();
                head.MapNode = head.MapNode.arriba;
                head.MapNode.contenido = head;
                MoverEstelas(head.MapNode.abajo);
            }
        }

        protected void MoverAbajo()
        {
            if (CheckNextNode(head.MapNode.abajo))
            {
                AddEstela();
                head.MapNode = head.MapNode.abajo;
                head.MapNode.contenido = head;
                MoverEstelas(head.MapNode.arriba);
            }
        }

        protected void consumeFuel()
        {
            this.fuelConsumption ++;
            if (this.fuelConsumption >= 5)
            {
                this.fuel -= (int)(this.fuelConsumption / 5);
                if (fuel <= 0) 
                {
                    Explode();
                }
                this.fuelConsumption = fuelConsumption % 5;
            }
        }

        public void ActivateShield(int duration) 
        {
            shield = duration;
            isShield = true;
        }

        public void HyperSpeed(int aumento, int duracion) 
        {
            hyperspeed = duracion;
            if (speedIdx + aumento > 9)
            {
                speed = speeds[9];
            }
            else 
            {
                speed = speeds[speedIdx + aumento];
            }
        }

        public void Explode() 
        {
            if (!isShield)
            {
                isDestroy = true;
                PlayerNode current = head;
                do
                {
                    current.MapNode.contenido = null;
                    current = current.Next;
                } while (current != null);
            }
            
        }

        public void DrawInfo(SpriteBatch spriteBatch, SpriteFont font, Texture2D arrow)
        {
            spriteBatch.DrawString(font, "Player fuel:" + fuel, new Vector2(810, 10), Color.White);
            if (pilaPoder.TopPila() != null)
            {
                pilaPoder.Draw(spriteBatch, font, arrow);
            }
        }

        public void Draw(SpriteBatch _spriteBatch) 
        {
            PlayerNode node = head;
            while (node != null) 
            {
                if (node == head && isShield)
                {
                    _spriteBatch.Draw(node.texture, node.Rect, Color.Blue);
                }
                else if (node == head && hyperspeed > 0) 
                {
                    _spriteBatch.Draw(node.texture, node.Rect, Color.Red);
                }
                else
                {
                    _spriteBatch.Draw(node.texture, node.Rect, Color.White);
                }
                node= node.Next;
            }

        }

   
    }

    
}
