using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Tron
{
    internal class Velocidad : Poder
    {
        public int aumentoVelocidad;
        public int duracion;

        public Velocidad(MapNode nodo, Texture2D texture, Vector2 position) : base("velocidad", nodo, texture, position)
        {
            this.aumentoVelocidad = new Random().Next(2, 5);  // Aumento de velocidad aleatorio
            this.duracion = new Random().Next(5, 15);  // Duración aleatoria
        }

        //public override void Activate(Player player)
        //{
        //  player.ActivateHyperSpeed(SpeedIncrease, Duration);
        //}
    }
}
