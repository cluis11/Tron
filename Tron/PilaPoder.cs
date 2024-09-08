using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Reflection.Metadata;

namespace Tron
{
    internal class NodoPila 
    {
        public Poder Poder;
        public NodoPila Next;

        public NodoPila(Poder poder) 
        {
            Poder = poder;
            Next = null;
        }
    }

    internal class PilaPoder
    {
        private NodoPila Top;
        private static int largo = 0;
        private Rectangle arrowRect = new Rectangle(810, 65, 20, 20);

        public PilaPoder() 
        {
            Top = null;
        }

        public void Push(Poder poder) 
        {
            NodoPila nodo = new NodoPila(poder);
            nodo.Next = Top;
            Top = nodo;
            largo++;
        }

        public NodoPila Pop() 
        {
            if (Top == null) { return null; }
            else 
            {
                NodoPila nodo = Top;
                Top = Top.Next;
                largo--;
                return nodo;
            }
        }

        public NodoPila TopPila() 
        {
            if (Top == null) 
            {
                return null;
            }
            return Top;
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font, Texture2D arrow)
        {
            //spriteBatch.DrawString(font, "Player fuel:" + fuel, new Vector2(850, 50), Color.White);
            spriteBatch.DrawString(font, "Poderes:", new Vector2(810, 50), Color.White);
            spriteBatch.Draw(arrow, arrowRect, Color.White);
            NodoPila current = Top;
            int xPos = 810;
            int yPos = 85;
            while (current != null)
            {
                spriteBatch.Draw(current.Poder.texture, new Rectangle(xPos, yPos, 20, 20), Color.White);
                xPos += 20;
                current = current.Next;
            }
        }

        public void Update(int x) 
        {
            if (arrowRect.X > 810 && x == -20)
            {
                arrowRect.X += x;
            }
            else if (arrowRect.X < (810 + ((largo-1) * 20)) && x == 20) 
            {
                arrowRect.X += x;
            }
        }

        public void OrdenarPila() 
        {
            if (arrowRect.X != 810)
            {
                int pos = ((arrowRect.X - 810) / 20) + 1;
                ColaPoder temp = new ColaPoder();
                for (int i = 0; i < pos; i++)
                {
                    NodoPila node = this.Pop();
                    if (i + 1 == pos)
                    {
                        temp.Enqueue(node.Poder, -1);
                    }
                    else
                    {
                        temp.Enqueue(node.Poder, i);
                    }
                }
                while (temp.Front() != null)
                {
                    NodoColaP current = temp.Dequeue();
                    this.Push(current.poder);
                }
                arrowRect.X -= 20;
            }
        }
    }
}
