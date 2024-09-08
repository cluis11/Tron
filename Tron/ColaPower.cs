
using System.Diagnostics;
using System.Runtime;

namespace Tron
{

    internal class NodoColaP
    {
        public Poder poder { get; private set; }
        public int prioridad { get; private set; }
        public NodoColaP Next { get; set; }

        public NodoColaP(Poder poder, int prioridad)
        {
            this.poder = poder;
            this.prioridad = prioridad;
            this.Next = null;
        }
    }
    internal class ColaPoder
    {
        private NodoColaP front;
        private NodoColaP rear;
        private static int largo = 0;

        public ColaPoder()
        {
            front = null;
            rear = null;
        }

        public void Enqueue(Poder poder, int prioridad)
        {
            NodoColaP nodo = new NodoColaP(poder, prioridad);
            if (front == null)
            {
                front = nodo;
                rear = nodo;
            }
            else if (prioridad > front.prioridad)
            {
                nodo.Next = front;
                front = nodo;
            }
            else
            {
                NodoColaP current = front;
                while (current.Next != null && current.Next.prioridad >= prioridad)
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
            largo++;
            Debug.WriteLine($"Se agrego item a la cola de largo {largo}");
        }

        public NodoColaP Dequeue()
        {
            if (front == null)
            {
                return null;
            }
            else
            {
                NodoColaP nodo = front;
                front = front.Next;
                return nodo;
            }
        }

        public NodoColaP Front()
        {
            if (front == null) { return null; }
            else
            {
                return front;
            }
        }
    }
}
