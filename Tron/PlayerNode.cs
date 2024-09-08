using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace Tron
{
    internal class PlayerNode : Sprite
    {
        public MapNode MapNode;
        public int tipo;
        public PlayerNode Next;
        public bool isCrash = false;

        public PlayerNode(MapNode nodo, int tipo, Texture2D texture, Vector2 position) : base(texture, position)
        {
            this.MapNode = nodo;
            this.MapNode.contenido = this;
            this.Next = null;
            this.tipo = tipo;

        }
    }
}
