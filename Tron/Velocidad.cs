using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tron
{
    internal class Velocidad : Poder
    {
        public int aumentoVelocidad;
        public int duracion;

        public Velocidad(MapNode nodo) : base("velocidad", nodo)
        {
            aumentoVelocidad = new Random().Next(2, 5);  // Aumento de velocidad aleatorio
            duracion = new Random().Next(5, 15);  // Duración aleatoria
        }

        //public override void Activate(Player player)
        //{
        //  player.ActivateHyperSpeed(SpeedIncrease, Duration);
        //}
    }
}
