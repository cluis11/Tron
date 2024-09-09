using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tron
{
    internal class PlayerNode : Sprite
    {
        public MapNode MapNode { get; set; }
        public int tipo { get; private set; }
        public PlayerNode Next { get; set; }
        public bool isCrash { get; set; }

        public PlayerNode(MapNode nodo, int tipo, Texture2D texture, Vector2 position) : base(texture, position)
        {
            this.MapNode = nodo;
            this.MapNode.contenido = this;
            this.Next = null;
            this.tipo = tipo;
            this.isCrash = false;

        }
    }
}
