using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tron
{
    internal class ScaleSprite : Sprite 
    {
        public Rectangle Rect
        {
            get {
                return new Rectangle((int)position.X, (int)position.Y, 16, 16);
            }
        }
        public ScaleSprite(Texture2D texture, Microsoft.Xna.Framework.Vector2 position) :base(texture, position)
        {
            
        }
    }
}
