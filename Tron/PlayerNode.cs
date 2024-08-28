using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace Tron
{
    internal class PlayerNode : Sprite
    {
        public MapNode MapNode;
        public PlayerNode Next;

        public PlayerNode(MapNode nodo, Texture2D texture, Vector2 position) : base (texture, position)
        {
            this.MapNode = nodo;
            this.MapNode.contenido = this;
            //this.Next = null;

        }

        public void MoverDerecha(int speed) {
            for (int i = 0; i < speed; i++)
            {
                this.MapNode = this.MapNode.derecha;
                this.MapNode.contenido = this;
                this.MapNode.izquierda.contenido = null;
            }
        }

        public void MoverIzquierda(int speed)
        {
            for (int i = 0; i < speed; i++)
            {
                this.MapNode = this.MapNode.izquierda;
                this.MapNode.contenido = this;
                this.MapNode.derecha.contenido = null;
            }
        }

        public void MoverArriba(int speed)
        {
            for (int i = 0; i < speed; i++)
            {
                this.MapNode = this.MapNode.arriba;
                this.MapNode.contenido = this;
                this.MapNode.abajo.contenido = null;
            }
        }

        public void MoverAbajo(int speed)
        {
            for (int i = 0; i < speed; i++)
            {
                this.MapNode = this.MapNode.abajo;
                this.MapNode.contenido = this;
                this.MapNode.arriba.contenido = null;
            }
        }
    }
}
