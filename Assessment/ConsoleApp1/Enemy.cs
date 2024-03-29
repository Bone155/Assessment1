﻿using System;
using System.Collections.Generic;
using System.Text;
using Raylib;
using rl = Raylib.Raylib;

namespace ConsoleApp1
{
    struct Vector2E
    {
        public int x;
        public int y;
    }

    class Enemy
    {
        public static Texture2D Etexture;

        public Vector2E Position = new Vector2E();
        public Color color = Color.DARKPURPLE;

        public bool Enabled = true;

        public static void EnemyTexture(string file)
        {
            Etexture = rl.LoadTexture(file);
        }

        public void Draw()
        {
            if (!Enabled)
                return;

            rl.DrawTextureEx(Etexture, new Vector2(Position.x, Position.y), 0f, 1f, color);
        }
    }
}
