using System;
using System.Collections.Generic;
using System.Text;
using Raylib;
using rl = Raylib.Raylib;

namespace ConsoleApp1
{
    struct Vector2Int
    {
        public int x;
        public int y;
    }
    class Player
    {
        public int speed = 5;
        public int timer = 0;
        public int health = 5;
        public int ammo = 5;
        public int score = 0;
        public Vector2Int Position = new Vector2Int();
        public Color color = Color.BLUE;
        public void RunUpdate()
        {
            if (rl.IsKeyDown(KeyboardKey.KEY_W))
            {
                Position.y-= speed;
            }
            if (rl.IsKeyDown(KeyboardKey.KEY_S))
            {
                Position.y+= speed;
            }
            if (rl.IsKeyDown(KeyboardKey.KEY_A))
            {
                Position.x-= speed;
            }
            if (rl.IsKeyDown(KeyboardKey.KEY_D))
            {
                Position.x+= speed;
            }
        }

        public void Draw()
        {
            rl.DrawCircle(Position.x, Position.y, 5f, color);
            rl.DrawRectangle(Position.x-5, Position.y+5, 10, 30, color);
            rl.DrawRectangle(Position.x + 5, Position.y + 15, 3, 10, Color.GREEN);
            rl.DrawRectangle(Position.x + 10, Position.y + 10, 8, 2, Color.GREEN);
        }

    }
}
