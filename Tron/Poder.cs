

using System;

namespace Tron
{
    internal class Poder
    {
        public string tipo;
        public MapNode Nodo;

        public Poder(String tipo, MapNode nodo)
        {
            this.tipo = tipo;
            this.Nodo = nodo;
        }
        //public abstract void UsarPoder(Player player);
    }
}
