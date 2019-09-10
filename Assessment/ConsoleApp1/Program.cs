using Raylib;
using rl = Raylib.Raylib;
using System;

namespace ConsoleApp1
{
    static class Program
    {
        //Pickup collision with player
        public static bool CheckCollisionV1(Player pl, Pickup pu)
        {
            bool rtn = false;
            Rectangle PickupCol;
            Rectangle PlayerCol;
            //sprites
            float w;
            float h;
            switch (pu.up)
            {
                case Pickup.PickupType.Ammo:
                    w = ((Ammo)pu).getWidth() * 1f;
                    h = ((Ammo)pu).getHeight() * 1f;
                    PickupCol = new Rectangle(pu.Position.x, pu.Position.y, w, h);
                    PlayerCol = new Rectangle(pl.Position.x - 15, pl.Position.y + 5, 10, 40);
                    rtn = rl.CheckCollisionRecs(PlayerCol, PickupCol);
                    break;
                case Pickup.PickupType.Health:
                    w = ((Health)pu).getWidth() * 1f;
                    h = ((Health)pu).getHeight() * 1f;
                    PickupCol = new Rectangle(pu.Position.x, pu.Position.y, w, h);
                    PlayerCol = new Rectangle(pl.Position.x - 15, pl.Position.y + 5, 10, 40);
                    rtn = rl.CheckCollisionRecs(PlayerCol, PickupCol);
                    break;
                case Pickup.PickupType.Score:
                    w = ((Score)pu).getWidth() * 1f;
                    h = ((Score)pu).getHeight() * 1f;
                    PickupCol = new Rectangle(pu.Position.x, pu.Position.y, w, h);
                    PlayerCol = new Rectangle(pl.Position.x - 15, pl.Position.y + 5, 10, 40);
                    rtn = rl.CheckCollisionRecs(PlayerCol, PickupCol);
                    break;
                default:
                    break;
            }

            if(rtn) { pu.Enabled = false;  }
            return rtn;
        }
        //Enemy collision with player
        public static bool CheckCollisionV2(Player pl, Enemy e)
        {
            bool rtn = false;
            
            Rectangle EnemyCol;
            //sprites
            float Ew = Enemy.Etexture.width * 1f;
            float Eh = Enemy.Etexture.height * 1f;
            EnemyCol = new Rectangle(e.Position.x, e.Position.y, Ew, Eh);

            Rectangle PlayerCol = new Rectangle(pl.Position.x - 15, pl.Position.y + 5, 10, 40);
            rtn = rl.CheckCollisionRecs(PlayerCol, EnemyCol);
            if (rtn) { e.isEnabled = false; }
            return rtn;
        }

        public static int Main()
        {
            // Initialization
            //--------------------------------------------------------------------------------------
            Random rand = new Random();
            Player player = new Player();
            Pickup[] pickup = new Pickup[70];
            Enemy[] enemies = new Enemy[50];
            Enemy enemy = new Enemy();
            Gun gun = new Gun();

            int screenWidth = 1600;
            int screenHeight = 900;

            int health = player.health;
            int ammo = player.ammo;
            int score = player.score;
            int timer = 0;
            //bool winState = false;
            //rl.InitAudioDevice();
            //var pick = rl.LoadAudioStream("powerUp1.oog");

            rl.InitWindow(screenWidth, screenHeight, "Assessment");

            //Create enemies
            Enemy.EnemyTexture("bat.png");
            for (int Enidx = 0; Enidx < enemies.Length; Enidx++)
            {
                enemies[Enidx] = new Enemy();
                enemies[Enidx].Position.x = rand.Next(20, screenWidth - 20);
                enemies[Enidx].Position.y = rand.Next(30, screenHeight - 20);
            }

            //Create pickups
            Ammo.SetTexture("platformPack_item001.png");
            Health.SetTexture("platformPack_item017.png");
            Score.SetTexture("platformPack_item009.png");
            for (int idx = 0; idx < pickup.Length; idx++)
            {
                int pickChoice = rand.Next(0, 3);
                if(pickChoice == 0)
                {
                    pickup[idx] = new Ammo();
                    pickup[idx].Position.x = rand.Next(20, screenWidth - 20);
                    pickup[idx].Position.y = rand.Next(30, screenHeight - 20);
                }
                if (pickChoice == 1)
                {
                    pickup[idx] = new Health();
                    pickup[idx].Position.x = rand.Next(20, screenWidth - 20);
                    pickup[idx].Position.y = rand.Next(30, screenHeight - 20);
                }
                if (pickChoice == 2)
                {
                    pickup[idx] = new Score();
                    pickup[idx].Position.x = rand.Next(20, screenWidth - 20);
                    pickup[idx].Position.y = rand.Next(30, screenHeight - 20);
                }
            }

            player.Position.x = rand.Next(20, screenWidth - 20);
            player.Position.y = rand.Next(30, screenHeight - 20);

            rl.SetTargetFPS(60);
            //--------------------------------------------------------------------------------------

            // Main game loop
            while (!rl.WindowShouldClose())    // Detect window close button or ESC key
            {
                // Update
                //----------------------------------------------------------------------------------
                // TODO: Update your variables here
                //----------------------------------------------------------------------------------
                player.RunUpdate();
                timer++;
                // Draw
                //----------------------------------------------------------------------------------
                rl.BeginDrawing();

                rl.ClearBackground(Color.LIGHTGRAY);
                rl.DrawText("Score: " + score, 50, 50, 12,  Color.WHITE);
                rl.DrawText("Health: " + health, 50, 75, 12, Color.WHITE);
                rl.DrawText("Time: " + timer / 60, screenWidth - 200, 50, 12, Color.WHITE);
                rl.DrawText("Ammo: " + ammo, screenWidth - 200, 80, 12, Color.WHITE);

                //if (timer/60 >= 30 && winState == false)
                //{
                //    rl.DrawText("Game Over", 250, 50, 20, Color.ORANGE);
                //    rl.DrawText("BOI", 250, 75, 20, Color.ORANGE);
                //    player.speed = 0;
                //}
                player.Draw();
                foreach (Enemy en in enemies)
                {
                    if (en.isEnabled)
                    {
                        en.Draw();
                        if (CheckCollisionV2(player, en))
                        {
                            health--;
                            score++;
                        }

                        //if (en.Position.x > player.Position.x)
                        //    en.Position.x-= 3;
                        //if (en.Position.x < player.Position.x)
                        //    en.Position.x+= 3;
                        //if (en.Position.y > player.Position.y)
                        //    en.Position.y-= 3;
                        //if (en.Position.y < player.Position.y)
                        //    en.Position.y+= 3;

                        if (health <= 0)
                        {
                            health = 0;
                            rl.DrawText("Game Over", 250, 50, 20, Color.ORANGE);
                            rl.DrawText("BOI", 250, 75, 20, Color.ORANGE);
                            player.speed = 0;
                        }
                    }
                }

                foreach (Pickup pick in pickup)
                {
                    if (pick.Enabled)
                    {
                        switch (pick.up)
                        {
                            case Pickup.PickupType.Ammo:
                                ((Ammo)pick).Draw();
                                ammo += CheckCollisionV1(player, pick) ? 1 : 0;//adds one to variable
                                break;
                            case Pickup.PickupType.Health:
                                ((Health)pick).Draw();
                                health += CheckCollisionV1(player, pick) ? 1 : 0;
                                break;
                            case Pickup.PickupType.Score:
                                ((Score)pick).Draw();
                                if (CheckCollisionV1(player, pick))
                                    score+= rand.Next(1, 11);
                                break;
                            default:
                                break;
                        }

                        //rl.PlayAudioStream(pick);
                        //score.score += CheckCollisionV1(player, pick) ? 1 : 0;
                    }
                }
                //Player screen Warps
                if (player.Position.x <= 0)
                    player.Position.x = screenWidth - 1;
                if (player.Position.x >= screenWidth)
                    player.Position.x = 0;
                if (player.Position.y <= 0)
                    player.Position.y = screenHeight - 1;
                if (player.Position.y >= screenHeight)
                    player.Position.y = 0;

                rl.EndDrawing();
                //----------------------------------------------------------------------------------
                //if (score.score >= 10)
                //{
                //    rl.DrawText("Oh goody, you survived", 250, 50, 20, Color.ORANGE);
                //    //winState = true;
                //    player.speed = 0;
                //}
            }

            // De-Initialization
            //--------------------------------------------------------------------------------------
            rl.CloseWindow();        // Close window and OpenGL context
            //--------------------------------------------------------------------------------------

            return 0;
        }
    }
}