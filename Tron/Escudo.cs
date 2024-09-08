using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
namespace Tron
{
    internal class Escudo : Poder
    {
        public int Duration;

        public Escudo(MapNode nodo, Texture2D texture, Vector2 position) : base("escudo", nodo, texture, position)
        {
            this.Duration = new Random().Next(5, 15);  // Duración aleatoria
        }

        public override void ApplyEffect(Player player)
        {
           player.ActivateShield(Duration);
         }
    }
}
