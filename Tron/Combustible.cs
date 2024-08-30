using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tron
{
    internal class Combustible : Item
    {
        public int Capacidad;

        public Combustible(MapNode nodo, Texture2D texture, Vector2 position) : base("Combustible", nodo, texture, position)
        {
            this.Capacidad = new Random().Next(10, 100);  // Capacidad aleatoria
        }

        public override void ApplyEffect(Player player)
        {
           player.fuel += Capacidad;
        }
    }
}
