
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Tron
{

    internal class NodoCola 
    {
        public Item item { get; private set; }
        public int prioridad { get; private set; }
        public NodoCola Next { get; set; }

        public NodoCola(Item item, int prioridad) 
        {
            this.item = item;
            this.prioridad = prioridad;
            this.Next = null;
        }
    }
    internal class ColaItem
    {
        private NodoCola front;
        private NodoCola rear;
        private static int largo = 0;

        public ColaItem() 
        {
            front = null;
            rear = null;
        }

        public void Enqueue(Item item, int prioridad) 
        {
            NodoCola nodo = new NodoCola(item, prioridad);
            if (front == null)
            {
                front = nodo;
                rear = nodo;
            }
            else if (prioridad < front.prioridad)
            {
                nodo.Next = front;
                front = nodo;
            }
            else 
            {
                NodoCola current = front;
                while (current.Next != null && current.Next.prioridad <= prioridad) 
                {
                    current = current.Next;
                }
                nodo.Next = current.Next;
                current.Next = nodo;
                if (nodo.Next == null) 
                {
                    rear = nodo;
                }
            }
            largo ++;
            Debug.WriteLine($"Se agrego item a la cola de largo {largo}");
        }

        public NodoCola Dequeue() 
        {
            if (front == null)
            {
                return null;
            }
            else 
            {
                NodoCola nodo = front;
                front = front.Next;
                return nodo;
            }
        }

        public NodoCola Front() 
        {
            if (front == null) { return null; }
            else 
            {
                return front;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            NodoCola current = front;
            int xPos = 865;
            int yPos = 100;
            while (current != null)
            {
                spriteBatch.Draw(current.item.texture, new Rectangle(xPos, yPos, 20, 20), Color.White);
                xPos += 20;
                current = current.Next;
            }
        }

        public void Draw1(SpriteBatch spriteBatch)
        {
            NodoCola current = front;
            int xPos = 865;
            int yPos = 235;
            while (current != null)
            {
                spriteBatch.Draw(current.item.texture, new Rectangle(xPos, yPos, 20, 20), Color.White);
                xPos += 20;
                current = current.Next;
            }
        }

        public void Draw2(SpriteBatch spriteBatch)
        {
            NodoCola current = front;
            int xPos = 865;
            int yPos = 375;
            while (current != null)
            {
                spriteBatch.Draw(current.item.texture, new Rectangle(xPos, yPos, 20, 20), Color.White);
                xPos += 20;
                current = current.Next;
            }
        }

        public void Draw3(SpriteBatch spriteBatch)
        {
            NodoCola current = front;
            int xPos = 865;
            int yPos = 515;
            while (current != null)
            {
                spriteBatch.Draw(current.item.texture, new Rectangle(xPos, yPos, 20, 20), Color.White);
                xPos += 20;
                current = current.Next;
            }
        }

        public void Draw4(SpriteBatch spriteBatch)
        {
            NodoCola current = front;
            int xPos = 865;
            int yPos = 655;
            while (current != null)
            {
                spriteBatch.Draw(current.item.texture, new Rectangle(xPos, yPos, 20, 20), Color.White);
                xPos += 20;
                current = current.Next;
            }
        }
    }
}
