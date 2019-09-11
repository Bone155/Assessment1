using System;
using System.Collections.Generic;
using System.Text;
using Raylib;
using rl = Raylib.Raylib;

namespace ConsoleApp1
{
    struct Vector2B
    {
        public float x;
        public float y;
    }
    class Bullet
    {
        public Vector2B Position = new Vector2B();
        public bool Enabled = true;
        public static Texture2D texture;

        public void BulletUpdate()
        {
            Position.x = ;
        }

        public static void SetTexture(string file)
        {
            texture = rl.LoadTexture(file);
        }

        public void Draw()
        {
            if (!Enabled)
                return;
            rl.DrawTextureEx(texture, new Vector2(Position.x, Position.y), 0f, 1f, Color.BROWN);
        }
    }
}
