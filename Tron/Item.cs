﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tron
{
    internal abstract class Item : Sprite
    {
        public string tipo { get; private set; }
        public MapNode Nodo { get; set; }

        public Item(string tipo, MapNode nodo, Texture2D texture, Vector2 position) : base(texture, position)
        {
            this.tipo = tipo;
            this.Nodo = nodo;
            this.Nodo.contenido = this;

        }
        
        public abstract void ApplyEffect(Player player);
    }
}
