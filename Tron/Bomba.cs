using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace Tron
{
    internal class Bomba : Item
    {
        public Bomba(MapNode nodo, Texture2D texture, Vector2 position) : base("bomba", nodo, texture, position) { }

        public override void ApplyEffect(Player player)
        {
            Debug.WriteLine("soy una bomba sexy");
            player.Explode();  // Método que manejaría la explosión
        }
    }
}
