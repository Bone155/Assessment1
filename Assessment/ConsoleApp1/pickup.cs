using System;
using System.Collections.Generic;
using System.Text;
using Raylib;
using rl = Raylib.Raylib;

namespace ConsoleApp1
{
    struct Vector2P
    {
        public int x;
        public int y;
    }

    class Pickup
    {
        public enum PickupType  { Ammo, Health, Score};
        public PickupType up;
        public Vector2P Position = new Vector2P();
        public bool Enabled = true;
    }

    class Ammo : Pickup
    {
        public int ammo;
        public static Texture2D texture;
        public static int width;
        public static int height;
        public Ammo()
        {
            up = PickupType.Ammo;
            ammo = 5;
        }

        public int getWidth()
        {
            return texture.width;
        }

        public int getHeight()
        {
            return texture.height;
        }

        public static void SetTexture(string file)
        {
            texture = rl.LoadTexture(file);
        }

        public void Draw()
        {
            if (!(Enabled))
                return;
            rl.DrawTextureEx(texture, new Vector2(Position.x, Position.y), 0f, 1f, Color.RED);
        }
    }

    class Health : Pickup
    {
        public int health;
        public static Texture2D texture;
        public int width;
        public int height;
        public Health()
        {
            up = PickupType.Health;
            health = 5;
        }

        public int getWidth()
        {
            return texture.width;
        }

        public int getHeight()
        {
            return texture.height;
        }

        public static void SetTexture(string file)
        {
            texture = rl.LoadTexture(file);
        }

        public void Draw()
        {
            if (!(Enabled))
                return;
            rl.DrawTextureEx(texture, new Vector2(Position.x, Position.y), 0f, 1f, Color.RED);
        }
    }

    class Score : Pickup
    {
        public int score;
        public static Texture2D texture;
        public int width;
        public int height;
        public Score()
        {
            up = PickupType.Score;
            score = 0;
        }

        public int getWidth()
        {
            return texture.width;
        }

        public int getHeight()
        {
            return texture.height;
        }

        public static void SetTexture(string file)
        {
            texture = rl.LoadTexture(file);
        }

        public void Draw()
        {
            if (!(Enabled))
                return;
            rl.DrawTextureEx(texture, new Vector2(Position.x, Position.y), 0f, 1f, Color.RED);
        }
    }
}
