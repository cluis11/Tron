using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tron
{
    internal class Enemy : Player
    {
        public Enemy() : base() 
        {

        }

        public Enemy(MapNode mapNode, Texture2D texture, Vector2 position, Texture2D estelaTexuture)
        {
            this.head = new PlayerNode(mapNode, texture, position);
            this.estelas = 3;
            this.fuel = 100;
            this.direction = Direction.Right;//(Direction)new Random().Next(0, 3);
            this.estelaTexuture = estelaTexuture;

            double[] speeds = { 1.0, 0.9, 0.8, 0.7, 0.6, 0.5, 0.4, 0.3, 0.2, 0.1 };
            this.speed = speeds[new Random().Next(speeds.Length)];
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

        protected override void HandleInput() 
        {
            if (!CheckAhead()) 
            {
                direction = Direction.Down;
            }
        }
    }
}
