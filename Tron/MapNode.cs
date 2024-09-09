using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tron
{
    internal class MapNode
    {
        public int fila { get; private set; }
        public int columna { get; private set; }
        public MapNode arriba { get; set; }
        public MapNode abajo { get; set; }

        public MapNode derecha { get; set; }

        public MapNode izquierda { get; set; }

        public Sprite contenido { get; set; }


        public MapNode(int fila, int columna)
        {
            this.fila = fila;
            this.columna = columna;
            this.arriba = null;
            this.abajo = null;
            this.derecha = null;
            this.izquierda = null;
            this.contenido = null;
        }
    }
}
