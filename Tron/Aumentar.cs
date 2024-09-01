using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace Tron
{
    internal class Aumentar : Item
    {
        public Aumentar(MapNode nodo, Texture2D texture, Vector2 position) : base("aumentar", nodo, texture, position) { }

        public override void ApplyEffect(Player player)
        {
            player.IncreaseEstela();
        }
    }
}
