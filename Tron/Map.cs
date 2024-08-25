using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Tron
{
    internal class Map
    {
        public MapNode MapaHead;
        public MapNode MapaTail;
        public int Filas;
        public int Columnas;
        public float SquareSize = 16f;
        private List<Texture2D> itemTextures = new List<Texture2D>();
        private List<Bomba> bombas = new List<Bomba>();

        public Map(int Filas, int Columnas)
        {
            this.Filas = Filas;
            this.Columnas = Columnas;
            this.MapaHead = new MapNode(0, 0);
            CrearMapa();
        }

        private void CrearMapa()
        {
            MapNode currenthead = MapaHead;
            MapNode current = currenthead;
            MapNode currentIzq = null;
            MapNode currentArriba = null;
            for (int fila = 0; fila < this.Filas; fila++)
            {
                for (int columna = 1; columna < this.Columnas; columna++)
                {
                    if (fila == 0)
                    {
                        current.derecha = new MapNode(fila, columna);
                        currentIzq = current;
                        current = current.derecha;
                        current.izquierda = currentIzq;
                    }
                    else
                    {
                        current.derecha = new MapNode(fila, columna);
                        currentIzq = current;
                        current = current.derecha;
                        current.izquierda = currentIzq;
                        currentArriba = currentArriba.derecha;
                        current.arriba = currentArriba;
                        currentArriba.abajo = current;
                    }
                }
                if (fila != Filas - 1)
                {
                    currenthead.abajo = new MapNode(fila + 1, 0);
                    current = currenthead.abajo;
                    current.arriba = currenthead;
                    currentArriba = currenthead;
                    currenthead = current;
                }
            }
            MapaTail = current;
        }

        public void InitializeBombs() {
            Random random = new Random();
            for (int i = 0; i < 10; i++)
            {
                int x = random.Next(0, this.Filas - 1);
                int y = random.Next(0, this.Columnas - 1);
                while (this.NodeHasContent(x, y)) 
                {
                  x = random.Next(0, this.Filas - 1);
                 y = random.Next(0, this.Columnas - 1);
                }
                Bomba bomb = new Bomba(this.GetMapNode(x, y), itemTextures[1], new Vector2(x * SquareSize, y * SquareSize));
                bombas.Add(bomb);
            }
        }

        public void LoadContent(ContentManager Content) {
            Texture2D fuelTexture = Content.Load<Texture2D>("fuel");
            Texture2D bombTexture = Content.Load<Texture2D>("bomba");
            Texture2D increaseTexture = Content.Load<Texture2D>("increase");
            itemTextures.AddRange(new Texture2D[] { fuelTexture, bombTexture, increaseTexture });
        }

        public void Draw_Bombs(SpriteBatch _spriteBatch)
        {
            foreach (Bomba bomb in bombas)
            {
                _spriteBatch.Draw(bomb.texture, bomb.Rect, Color.White);
            }
        }

        public MapNode GetMapNode(int fila, int columna)
        {
            MapNode current = this.MapaHead;
            MapNode currentHead = current;
            for (int i = 0; i < fila + 1; i++)
            {
                for (int j = 0; j < columna + 1; j++)
                {
                    if (j != columna)
                    {
                        current = current.derecha;
                    }
                }
                if (i != fila)
                {
                    current = currentHead.abajo;
                    currentHead = current;
                }
            }
            return current;
        }

        public bool NodeHasContent(int fila, int columna)
        {
            MapNode current = this.MapaHead;
            MapNode currentHead = current;
            for (int i = 0; i < fila + 1; i++)
            {
                for (int j = 0; j < columna + 1; j++)
                {
                    if (j != columna)
                    {
                        current = current.derecha;
                    }
                }
                if (i != fila)
                {
                    current = currentHead.abajo;
                    currentHead = current;
                }
            }
            return current.contenido != null;
        }
    }
}
