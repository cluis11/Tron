using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public PilaPoder() 
        {
            Top = null;
        }

        public void Push(Poder poder) 
        {
            NodoPila nodo = new NodoPila(poder);
            nodo.Next = Top;
            Top = nodo;
            largo ++;
            Debug.WriteLine($"Se agrego poder a la pila de largo {largo}");
        }

        public NodoPila Pop() 
        {
            if (Top == null) { return null; }
            else 
            {
                NodoPila nodo = Top;
                Top = Top.Next;
                return nodo;
            }
        }

        public NodoPila TopPila() 
        {
            return Top;
        }
    }
}
