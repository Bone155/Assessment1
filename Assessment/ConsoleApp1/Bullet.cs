using System;
using System.Collections.Generic;
using System.Text;
using Raylib;
using rl = Raylib.Raylib;

namespace ConsoleApp1
{
    struct Vector2B
    {
        public int x;
        public int y;
    }
    class Bullet
    {
        public Vector2B Position = new Vector2B();
        public bool Enabled = true;
        public bool bulletMove;

        //public void BulletUpdate()
        //{
        //    Position.x += ;
        //}

        public void Draw()
        {
            if (!Enabled)
                return;
            rl.DrawRectangle(Position.x, Position.y, 6, 2, Color.VIOLET);
        }
    }
}
