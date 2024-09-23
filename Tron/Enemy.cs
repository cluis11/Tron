using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Xml.Linq;

namespace Tron
{
    internal class Enemy : Player
    {

        private int cambiarMov = 0;
        private float powerTimeElapsed = 0f;
        private float applyPower = 5f;
        public Enemy() : base() 
        {

        }

        public Enemy(MapNode mapNode, Texture2D texture, Vector2 position, Texture2D estelaTexuture)
        {
            this.head = new PlayerNode(mapNode, 2, texture, position);
            this.estelas = 3;
            this.fuel = 100;
            this.direction = (Direction)new Random().Next(0, 3);
            this.estelaTexuture = estelaTexuture;

            this.speedIdx = new Random().Next(speeds.Length);
            this.speed = speeds[speedIdx];

            this.colaItem = new ColaItem();
            this.pilaPoder = new PilaPoder();
            this.isDestroy = false;
        }

        public static Enemy CreateInstanceE(MapNode mapNode, Texture2D texture, Vector2 position, Texture2D estelaTexture)
        {
            return new Enemy(mapNode, texture, position, estelaTexture);
        }



        private bool CheckAhead()
        {
            MapNode node = head.MapNode;
            switch (direction)
            {
                case Direction.Right:
                    if (node.derecha == null || node.derecha.contenido is Bomba || node.derecha.contenido is PlayerNode)
                    {
                        return false;
                    }
                    break;
                case Direction.Left:
                    if (node.izquierda == null || node.izquierda.contenido is Bomba || node.izquierda.contenido is PlayerNode)
                    {
                        return false;
                    }
                    break;
                case Direction.Up:
                    if (node.arriba == null || node.arriba.contenido is Bomba || node.arriba.contenido is PlayerNode)
                    {
                        return false;
                    }
                    break;
                case Direction.Down:
                    if (node.abajo == null || node.abajo.contenido is Bomba || node.abajo.contenido is PlayerNode)
                    {
                        return false;
                    }
                    break;
            }
            return true;
        }

        private Direction CheckPower() 
        {
            MapNode node = head.MapNode;
            if (node.arriba != null && node.arriba.contenido is Poder)
            {
                return Direction.Up;
            }
            else if (node.abajo != null && node.abajo.contenido is Poder)
            {
                return Direction.Down;
            }
            else if (node.izquierda != null && node.izquierda.contenido is Poder)
            {
                return Direction.Left;
            }
            else if (node.derecha != null && node.derecha.contenido is Poder)
            {
                return Direction.Right;
            }
            else 
            {
                return direction;
            }
        }

        private Direction CheckItem()
        {
            MapNode node = head.MapNode;
            if (node.arriba != null && (node.arriba.contenido is Aumentar || node.arriba.contenido is Combustible))
            {
                return Direction.Up;
            }
            else if (node.abajo != null && (node.abajo.contenido is Aumentar || node.abajo.contenido is Combustible))
            {
                return Direction.Down;
            }
            else if (node.izquierda != null && (node.izquierda.contenido is Aumentar || node.izquierda.contenido is Combustible))
            {
                return Direction.Left;
            }
            else if (node.derecha != null && (node.derecha.contenido is Aumentar || node.derecha.contenido is Combustible))
            {
                return Direction.Right;
            }
            else
            {
                cambiarMov++;
                return direction;
            }
        }

        public override void Update(GameTime gameTime)
        {
            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeElapsed >= speed && !isDestroy)
            {
                if (head.isCrash) { Explode(); }
                HandleInput();
                MovePlayer();
                consumeFuel();
                
                timeElapsed = 0f;  // Reinicia el temporizador después de mover al jugador
            }
            if (colaItem.Front() != null)
            {
                ApplyItems(gameTime);
            }
            if (pilaPoder.TopPila() != null) 
            {
                AplicarPoder(gameTime);
            }
        }

        protected override void HandleInput() 
        {
            if (!CheckAhead())
            {
                cambiarDir();
            }
            else if (direction != CheckPower())
            {
                direction = CheckPower();
                cambiarMov = 0;
            }
            else if (direction != CheckItem())
            {
                direction = CheckItem();
                cambiarMov = 0;
            }
            else if (cambiarMov >= 10)
            {
                cambiarDir();
            }
        }


        protected void AplicarPoder(GameTime gameTime)
        {
            powerTimeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (powerTimeElapsed >= applyPower && pilaPoder.TopPila() != null)
            {
                NodoPila nodoPila = pilaPoder.Pop();
                nodoPila.Poder.ApplyEffect(this);
                powerTimeElapsed = 0f;
            }
        }

        private void cambiarDir() 
        {
            Direction newDirection = (Direction)new Random().Next(0, 3);
            if (direction == Direction.Right && (newDirection == Direction.Left || newDirection == Direction.Right))
            {
                newDirection = (Direction)new Random().Next(0, 2);
                if (newDirection == Direction.Down && head.MapNode.abajo == null) { newDirection = Direction.Up; }
                else if (newDirection == Direction.Up && head.MapNode.arriba == null) { newDirection = Direction.Down; }
            }
            else if (direction == Direction.Left && (newDirection != Direction.Right || newDirection != Direction.Left))
            {
                newDirection = (Direction)new Random().Next(0, 2);
                if (newDirection == Direction.Down && head.MapNode.abajo == null) { newDirection = Direction.Up; }
                else if (newDirection == Direction.Up && head.MapNode.arriba == null) { newDirection = Direction.Down; }
            }
            else if (direction == Direction.Up && (newDirection != Direction.Down || newDirection != Direction.Up))
            {
                newDirection = (Direction)new Random().Next(2, 4);
                if (newDirection == Direction.Right && head.MapNode.derecha == null) { newDirection = Direction.Left; }
                else if (newDirection == Direction.Left && head.MapNode.izquierda == null) { newDirection = Direction.Right; }
            }
            else if (direction == Direction.Down && (newDirection != Direction.Up || newDirection != Direction.Down))
            {
                newDirection = (Direction)new Random().Next(2, 4);
                if (newDirection == Direction.Right && head.MapNode.derecha == null) { newDirection = Direction.Left; }
                else if (newDirection == Direction.Left && head.MapNode.izquierda == null) { newDirection = Direction.Right; }
            }
            direction = newDirection;
            cambiarMov = 0;
        }

        public void Draw(SpriteBatch spriteBatch, int e)
        {
            PlayerNode node = head;
            while (node != null)
            {
                if (node == head && shield > 0)
                {
                    spriteBatch.Draw(node.texture, node.Rect, Color.Blue);
                }
                else if (node == head && hyperspeed > 0)
                {
                    spriteBatch.Draw(node.texture, node.Rect, Color.Red);
                }
                else
                {
                    switch (e) 
                    {
                        case 0:
                            spriteBatch.Draw(node.texture, node.Rect, Color.Yellow);
                            break;
                        case 1:
                            spriteBatch.Draw(node.texture, node.Rect, Color.Purple);
                            break;
                        case 2:
                            spriteBatch.Draw(node.texture, node.Rect, Color.Black);
                            break;
                        case 3:
                            spriteBatch.Draw(node.texture, node.Rect, Color.Orange);
                            break;
                    }
                }
                node = node.Next;
            }
        }

        public void DrawInfo(SpriteBatch spriteBatch, SpriteFont font, int e) 
        {
            switch (e) 
            {
                case 1:
                    DrawInfo1(spriteBatch, font, e);
                    break;
                case 2:
                    DrawInfo2(spriteBatch, font, e);
                    break;
                case 3:
                    DrawInfo3(spriteBatch, font, e);
                    break;
                case 4:
                    DrawInfo4(spriteBatch, font, e);
                    break;
            }
        }

        public void DrawInfo1(SpriteBatch spriteBatch, SpriteFont font, int e)
        {
            spriteBatch.DrawString(font, "Enemy 1 fuel: " + fuel, new Vector2(810, 180), Color.Black);
            spriteBatch.DrawString(font, "Powers:", new Vector2(810, 210), Color.Black);
            spriteBatch.DrawString(font, "Items:", new Vector2(810, 240), Color.Black);
            if (pilaPoder.TopPila() != null)
            {
                pilaPoder.Draw1(spriteBatch);
            }
            if (colaItem.Front() != null)
            {
                colaItem.Draw1(spriteBatch);
            }
        }

        public void DrawInfo2(SpriteBatch spriteBatch, SpriteFont font, int e)
        {
            spriteBatch.DrawString(font, "Enemy 2 fuel: " + fuel, new Vector2(810, 320), Color.Black);
            spriteBatch.DrawString(font, "Powers:", new Vector2(810, 350), Color.Black);
            spriteBatch.DrawString(font, "Items:", new Vector2(810, 380), Color.Black);
            if (pilaPoder.TopPila() != null)
            {
                pilaPoder.Draw2(spriteBatch);
            }
            if (colaItem.Front() != null)
            {
                colaItem.Draw2(spriteBatch);
            }
        }

        public void DrawInfo3(SpriteBatch spriteBatch, SpriteFont font, int e)
        {
            spriteBatch.DrawString(font, "Enemy 3 fuel: " + fuel, new Vector2(810, 460), Color.Black);
            spriteBatch.DrawString(font, "Powers:", new Vector2(810, 490), Color.Black);
            spriteBatch.DrawString(font, "Items:", new Vector2(810, 520), Color.Black);
            if (pilaPoder.TopPila() != null)
            {
                pilaPoder.Draw3(spriteBatch);
            }
            if (colaItem.Front() != null)
            {
                colaItem.Draw3(spriteBatch);
            }
        }

        public void DrawInfo4(SpriteBatch spriteBatch, SpriteFont font, int e)
        {
            spriteBatch.DrawString(font, "Enemy 4 fuel: " + fuel, new Vector2(810, 600), Color.Black);
            spriteBatch.DrawString(font, "Powers:", new Vector2(810, 630), Color.Black);
            spriteBatch.DrawString(font, "Items:", new Vector2(810, 660), Color.Black);
            if (pilaPoder.TopPila() != null)
            {
                pilaPoder.Draw4(spriteBatch);
            }
            if (colaItem.Front() != null)
            {
                colaItem.Draw4(spriteBatch);
            }
        }
    }
}
