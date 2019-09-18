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
        public static int width;
        public static int height;

        public int ammo;
        public static Texture2D texture;

        public Ammo()
        {
            up = PickupType.Ammo;
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
        public static Texture2D texture;

        public int health;
        public int width;
        public int height;

        public Health()
        {
            up = PickupType.Health;
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
        public static Texture2D texture;

        public int score;
        public int width;
        public int height;

        public Score()
        {
            up = PickupType.Score;
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
