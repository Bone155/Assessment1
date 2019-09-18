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

        public bool Enabled = false;
        public bool bulletMove;

        public int findDisableBullet(int[] array)
        {
            int index = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if (!Enabled)
                {
                    index = array[i];
                    return index;
                }
            }
            
            return index;
        }

        public void Draw()
        {
            if (!Enabled)
                return;

            rl.DrawRectangle(Position.x, Position.y, 6, 2, Color.VIOLET);
        }
    }
}
