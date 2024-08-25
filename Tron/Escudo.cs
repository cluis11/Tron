using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tron
{
    internal class Escudo : Poder
    {
        public int Duration;

        public Escudo(MapNode nodo) : base("escudo", nodo)
        {
            Duration = new Random().Next(5, 15);  // Duración aleatoria
        }

        //public override void Activate(Player player)
        //{
        //   player.ActivateShield(Duration);
        // }
    }
}
