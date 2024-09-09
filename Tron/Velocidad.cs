using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Tron
{
    internal class Velocidad : Poder
    {
        private int aumento;
        private int duracion;

        public Velocidad(MapNode nodo, Texture2D texture, Vector2 position) : base("velocidad", nodo, texture, position)
        {
            this.aumento = new Random().Next(2, 7);  // Aumento de velocidad aleatorio
            this.duracion = new Random().Next(5, 15);  // Duración aleatoria
        }

        public override void ApplyEffect(Player player)
        {
          player.HyperSpeed(aumento, duracion);
        }
    }
}
