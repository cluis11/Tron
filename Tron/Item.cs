using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tron
{
    internal class Item
    {
        public string tipo;
        public MapNode Nodo;
        public Texture2D texture;
        public Vector2 position;

        public Rectangle Rect
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, 16, 16);
            }
        }

        public Item(string tipo, MapNode nodo, Texture2D texture, Vector2 position)
        {
            this.tipo = tipo;
            this.Nodo = nodo;
            this.Nodo.contenido = this;
            this.texture = texture;
            this.position = position;

        }
        //public abstract void AplicarEfecto(Player player);
    }
}
