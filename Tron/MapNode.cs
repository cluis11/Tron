using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tron
{
    internal class MapNode
    {
        public int x;
        public int y;
        public MapNode arriba;
        public MapNode abajo;
        public MapNode derecha;
        public MapNode izquierda;
        public Object contenido;

        public MapNode(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.arriba = null;
            this.abajo = null;
            this.derecha = null;
            this.izquierda = null;
            this.contenido = null;
        }
    }
}
