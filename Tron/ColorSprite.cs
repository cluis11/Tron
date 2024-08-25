﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tron
{
    internal class ColorSprite: ScaleSprite
    {
        public Color color;
        public ColorSprite(Texture2D texture, Vector2 position, Color color) : base(texture, position) { 
            this.color = color;
        }
    }
}
