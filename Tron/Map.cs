using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public Player player { get; set; }
        public bool playerDestroyed = false;

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

        public void Initialize_Item_Power()
        {
            Random random = new Random();
            for (int i = 0; i < 25; i++)
            {
                int x = random.Next(0, this.Filas - 1);
                int y = random.Next(0, this.Columnas - 1);
                while (this.NodeHasContent(x, y))
                {
                    x = random.Next(0, this.Filas - 1);
                    y = random.Next(0, this.Columnas - 1);
                }
                if (i < 5)
                    new Combustible(this.GetMapNode(x, y), itemTextures[0], new Vector2(y * SquareSize, x * SquareSize));
                else if (i < 10)
                    new Bomba(this.GetMapNode(x, y), itemTextures[1], new Vector2(y * SquareSize, x * SquareSize));
                else if (i < 15)
                    new Aumentar(this.GetMapNode(x, y), itemTextures[2], new Vector2(y * SquareSize, x * SquareSize));
                else if (i < 20)
                    new Escudo(this.GetMapNode(x, y), itemTextures[3], new Vector2(y * SquareSize, x * SquareSize));
                else if (i < 25)
                    new Velocidad(this.GetMapNode(x, y), itemTextures[4], new Vector2(y * SquareSize, x * SquareSize));
            }
        }

        public void Initialize_Player()
        {
            Random random = new Random();
            int x = random.Next(0, this.Filas - 1);
            int y = random.Next(0, this.Columnas - 1);
            while (this.NodeHasContent(x, y))
            {
                x = random.Next(0, this.Filas - 1);
                y = random.Next(0, this.Columnas - 1);
            }
            player = Player.CreateInstance(GetMapNode(0, 0), itemTextures[5], new Vector2(0/*y*/ * SquareSize, 0/*x*/ * SquareSize), itemTextures[6]);
        }

        public void update(GameTime gameTime) 
        {
            if (player != null)
            {
                player.Update(gameTime);
                if (player.isDestroy) { player = null; }
            }
            else 
            {
                playerDestroyed = true;
            }

        }

        public void LoadContent(ContentManager Content) {
            Texture2D fuelTexture = Content.Load<Texture2D>("fuel");
            Texture2D bombTexture = Content.Load<Texture2D>("bomba");
            Texture2D increaseTexture = Content.Load<Texture2D>("increase");
            Texture2D shieldTexture = Content.Load<Texture2D>("escudo");
            Texture2D speedTexture = Content.Load<Texture2D>("speed");
            Texture2D headTexture = Content.Load<Texture2D>("head");
            Texture2D bodyTexture = Content.Load<Texture2D>("body");
            itemTextures.AddRange(new Texture2D[] { fuelTexture, bombTexture, increaseTexture, shieldTexture, speedTexture, headTexture, bodyTexture});
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            for (int i = 0; i < this.Filas; i++) {
                for (int j = 0; j < this.Columnas; j++) {
                    if (this.NodeHasContent(i, j)) {
                        _spriteBatch.Draw(GetMapNode(i, j).contenido.texture, GetMapNode(i, j).contenido.Rect, Color.White);
                    }
                }
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
