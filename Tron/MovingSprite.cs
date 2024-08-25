using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Tron
{
    internal class MovingSprite : ScaleSprite
    {
        private float speed;

        private float stepSize = 16f; // Tamaño del paso en píxeles
        private float timeElapsed = 0f; // Tiempo transcurrido desde el último movimiento
        private float moveInterval = 1f; // Intervalo de tiempo en segundos para mover el sprite
        public MovingSprite(Texture2D texture, Microsoft.Xna.Framework.Vector2 position, float speed) :base(texture, position) {
            this.speed = speed;    
        }

        public override void Update(GameTime gameTime) { 
            base.Update(gameTime);
            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeElapsed >= moveInterval)
            {
                // Mover el sprite 16 píxeles en la coordenada X
                position.X += stepSize;

                // Reiniciar el temporizador
                timeElapsed = 0f;
            }

        }
    }
}
