using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Tron
{
    internal class Enemy : Player
    {

        private int cambiarMov = 0;
        public Enemy() : base() 
        {

        }

        public Enemy(MapNode mapNode, Texture2D texture, Vector2 position, Texture2D estelaTexuture)
        {
            this.head = new PlayerNode(mapNode, 2, texture, position);
            this.estelas = 3;
            this.fuel = 100;
            this.direction = Direction.Right;//(Direction)new Random().Next(0, 3);
            this.estelaTexuture = estelaTexuture;

            double[] speeds = { 1.0, 0.9, 0.8, 0.7, 0.6, 0.5, 0.4, 0.3, 0.2, 0.1 };
            this.speed = 0.5;//speeds[new Random().Next(speeds.Length)];
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
                HandleInput();
                MovePlayer();
                consumeFuel();
                timeElapsed = 0f;  // Reinicia el temporizador después de mover al jugador
            }
            if (colaItem.Front() != null)
            {
                ApplyItems(gameTime);
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

        private void cambiarDir() 
        {
            Direction newDirection = (Direction)new Random().Next(0, 3);
            if (direction == Direction.Right && (newDirection == Direction.Left || newDirection == Direction.Right))
            {
                newDirection = (Direction)new Random().Next(0, 2);
            }
            else if (direction == Direction.Left && (newDirection != Direction.Right || newDirection != Direction.Left))
            {
                newDirection = (Direction)new Random().Next(0, 2);
            }
            else if (direction == Direction.Up && (newDirection != Direction.Down || newDirection != Direction.Up))
            {
                newDirection = (Direction)new Random().Next(2, 4);
            }
            else if (direction == Direction.Down && (newDirection != Direction.Up || newDirection != Direction.Down))
            {
                newDirection = (Direction)new Random().Next(2, 4);
            }
            direction = newDirection;
            cambiarMov = 0;
        }
    }
}
