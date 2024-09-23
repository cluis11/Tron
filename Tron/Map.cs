using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Tron
{
    internal class Map
    {
        private MapNode MapaHead;
        private int Filas;
        private int Columnas;
        private float SquareSize = 16f;
        private List<Texture2D> Textures = new List<Texture2D>();
        public Player player { get; private set; }
        public Enemy[] enemies { get; private set; }
        public bool[] enemiesB = new bool[] { true, true, true, true, true };

        public Map(int Filas, int Columnas)
        {
            this.Filas = Filas;
            this.Columnas = Columnas;
            this.MapaHead = new MapNode(0, 0);
            enemies = new Enemy[4];
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
        }

        private MapNode GetMapNode(int fila, int columna)
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

        private bool NodeHasContent(int fila, int columna)
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

        public void Initialize_Item_Power()
        {
            Random random = new Random();
            for (int i = 0; i < 45; i++)
            {
                int fila = random.Next(0, this.Filas - 1);
                int col = random.Next(0, this.Columnas - 1);
                while (this.NodeHasContent(fila, col))
                {
                    fila = random.Next(0, this.Filas - 1);
                    col = random.Next(0, this.Columnas - 1);
                }
                if (i < 10)
                    new Combustible(this.GetMapNode(fila, col), Textures[0], new Vector2(col * SquareSize, fila * SquareSize));
                else if (i < 20)
                    new Bomba(this.GetMapNode(fila, col), Textures[1], new Vector2(col * SquareSize, fila * SquareSize));
                else if (i < 30)
                    new Aumentar(this.GetMapNode(fila, col), Textures[2], new Vector2(col * SquareSize, fila * SquareSize));
                else if (i < 38)
                    new Escudo(this.GetMapNode(fila, col), Textures[3], new Vector2(col * SquareSize, fila * SquareSize));
                else if (i < 45)
                    new Velocidad(this.GetMapNode(fila, col), Textures[4], new Vector2(col * SquareSize, fila * SquareSize));
            }
        }

        private void Return_Item(ColaItem cola)
        {
            while (cola.Front() != null) { 
                Random random = new Random();
                int fila = random.Next(0, this.Filas - 1);
                int col = random.Next(0, this.Columnas - 1);
                while (this.NodeHasContent(fila, col))
                {
                    fila = random.Next(0, this.Filas - 1);
                    col = random.Next(0, this.Columnas - 1);
                }
                NodoCola node = cola.Dequeue();
                Item item = node.item;
                if (item is Bomba)
                {
                    new Bomba(this.GetMapNode(fila, col), Textures[1], new Vector2(col * SquareSize, fila * SquareSize));
                }
                else if (item is Combustible)
                {
                    new Combustible(this.GetMapNode(fila, col), Textures[0], new Vector2(col * SquareSize, fila * SquareSize));
                }
                else 
                {
                    new Aumentar(this.GetMapNode(fila, col), Textures[2], new Vector2(col * SquareSize, fila * SquareSize));
                }
            }
        }

        private void Return_Power(PilaPoder pila)
        {
            while (pila.TopPila() != null) {
                Random random = new Random();
                int fila = random.Next(0, this.Filas - 1);
                int col = random.Next(0, this.Columnas - 1);
                while (this.NodeHasContent(fila, col))
                {
                    fila = random.Next(0, this.Filas - 1);
                    col = random.Next(0, this.Columnas - 1);
                }
                NodoPila node = pila.Pop();
                Poder power = node.Poder;
                if (power is Escudo)
                {
                    new Escudo(this.GetMapNode(fila, col), Textures[3], new Vector2(col * SquareSize, fila * SquareSize));
                }
                else
                {
                    new Velocidad(this.GetMapNode(fila, col), Textures[4], new Vector2(col * SquareSize, fila * SquareSize));
                }
            }
        }

        public void Initialize_Player()
        {
            Random random = new Random();
            int fila = random.Next(0, this.Filas - 1);
            int col = random.Next(0, this.Columnas - 1);
            while (this.NodeHasContent(fila, col))
            {
                fila = random.Next(0, this.Filas - 1);
                col = random.Next(0, this.Columnas - 1);
            }
            player = Player.CreateInstance(GetMapNode(fila, col), Textures[5], new Vector2(col * SquareSize, fila * SquareSize), Textures[6], Textures[7]);
        }

        public void Initialize_Enemy()
        {
            for (int e = 0; e < enemies.Length; e++)
            {
                Random random = new Random();
                int fila = random.Next(0, this.Filas - 1);
                int col = random.Next(0, this.Columnas - 1);
                while (this.NodeHasContent(fila, col))
                {
                    fila = random.Next(0, this.Filas - 1);
                    col = random.Next(0, this.Columnas - 1);
                }
                enemies[e] = Enemy.CreateInstanceE(GetMapNode(fila, col), Textures[5], new Vector2(col * SquareSize, fila * SquareSize), Textures[6], Textures[7]);
            }
        }

        public void LoadContent(ContentManager Content)
        {
            Texture2D fuelTexture = Content.Load<Texture2D>("fuel");
            Texture2D bombTexture = Content.Load<Texture2D>("bomba");
            Texture2D increaseTexture = Content.Load<Texture2D>("increase");
            Texture2D shieldTexture = Content.Load<Texture2D>("escudo");
            Texture2D speedTexture = Content.Load<Texture2D>("speed");
            Texture2D headTexture = Content.Load<Texture2D>("head");
            Texture2D bodyTexture = Content.Load<Texture2D>("body");
            Texture2D ExTexture = Content.Load<Texture2D>("explosion");
            Textures.AddRange(new Texture2D[] { fuelTexture, bombTexture, increaseTexture, shieldTexture, speedTexture, headTexture, bodyTexture, ExTexture });
        }

        public void Update(GameTime gameTime) 
        {
            if (player != null)
            {
                player.Update(gameTime);
                if (player.isDestroy) 
                {
                    PilaPoder pila = player.pilaPoder;
                    ColaItem cola = player.colaItem;
                    Return_Power(pila);
                    Return_Item(cola);
                    player = null;
                    enemiesB[0] = false;
                }
            }
            for(int e = 0; e < enemies.Length; e++)
            {
                if (enemies[e] != null)
                {
                    enemies[e].Update(gameTime);
                    if (enemies[e].isDestroy) 
                    {
                        PilaPoder pila = enemies[e].pilaPoder;
                        ColaItem cola = enemies[e].colaItem;
                        Return_Power(pila);
                        Return_Item(cola);
                        enemies[e] = null;
                        enemiesB[e+1] = false;
                    }
                }
            }

        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            for (int i = 0; i < this.Filas; i++) {
                for (int j = 0; j < this.Columnas; j++) {
                    if (this.NodeHasContent(i, j)) {
                        MapNode node = GetMapNode(i, j);
                        if (node.contenido is PlayerNode)
                        {
                            
                        }
                        else
                        {
                            _spriteBatch.Draw(node.contenido.texture, node.contenido.Rect, Color.White);
                        }
                    }
                }
            }
        }
    }
}
