using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Tron
{
    internal class NodoPila 
    {
        public Poder Poder { get; set; }
        public NodoPila Next { get; set; }

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

        public void Draw(SpriteBatch spriteBatch, Texture2D arrow)
        {
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

        public void Draw1(SpriteBatch spriteBatch)
        {
            NodoPila current = Top;
            int xPos = 870;
            int yPos = 210;
            while (current != null)
            {
                spriteBatch.Draw(current.Poder.texture, new Rectangle(xPos, yPos, 20, 20), Color.White);
                xPos += 20;
                current = current.Next;
            }
        }

        public void Draw2(SpriteBatch spriteBatch)
        {
            NodoPila current = Top;
            int xPos = 870;
            int yPos = 350;
            while (current != null)
            {
                spriteBatch.Draw(current.Poder.texture, new Rectangle(xPos, yPos, 20, 20), Color.White);
                xPos += 20;
                current = current.Next;
            }
        }

        public void Draw3(SpriteBatch spriteBatch)
        {
            NodoPila current = Top;
            int xPos = 870;
            int yPos = 490;
            while (current != null)
            {
                spriteBatch.Draw(current.Poder.texture, new Rectangle(xPos, yPos, 20, 20), Color.White);
                xPos += 20;
                current = current.Next;
            }
        }

        public void Draw4(SpriteBatch spriteBatch)
        {
            NodoPila current = Top;
            int xPos = 870;
            int yPos = 630;
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
