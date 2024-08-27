using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tron
{
    internal class Poder : Sprite
    {
        public string tipo;
        public MapNode Nodo;

        public Poder(string tipo, MapNode nodo, Texture2D texture, Vector2 position) : base(texture, position)
        {
            this.tipo = tipo;
            this.Nodo = nodo;
            this.Nodo.contenido = this;
        }
        //public abstract void UsarPoder(Player player);
    }
}
