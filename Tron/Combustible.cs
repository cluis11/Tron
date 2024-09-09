using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tron
{
    internal class Combustible : Item
    {
        private int Capacidad;

        public Combustible(MapNode nodo, Texture2D texture, Vector2 position) : base("Combustible", nodo, texture, position)
        {
            this.Capacidad = new Random().Next(10, 50);  // Capacidad aleatoria
        }

        public override void ApplyEffect(Player player)
        {
            if (player.fuel == 100)
            {
                player.colaItem.Enqueue(this, 0);
            }
            else if (player.fuel + Capacidad > 100)
            {
                player.fuel = 100;
            }
            else
            {
                player.fuel += Capacidad;
            }
        }
    }
}
