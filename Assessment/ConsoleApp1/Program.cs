using Raylib;
using rl = Raylib.Raylib;
using System;
using System.IO;

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

            if(rtn)
                pu.Enabled = false;

            return rtn;
        }

        //Enemy collision with player
        public static bool CheckCollisionV2(Player pl, Enemy e)
        {
            Rectangle EnemyCol;

            bool rtn = false;

            //sprites
            float Ew = Enemy.Etexture.width * 1f;
            float Eh = Enemy.Etexture.height * 1f;

            EnemyCol = new Rectangle(e.Position.x, e.Position.y, Ew, Eh);

            Rectangle PlayerCol = new Rectangle(pl.Position.x - 15, pl.Position.y + 5, 10, 40);
            rtn = rl.CheckCollisionRecs(PlayerCol, EnemyCol);

            if (rtn)
                e.Enabled = false;

            return rtn;
        }
        //Bullet collision with enemy
        public static bool CheckCollisionV3(Bullet b, Enemy e)
        {
            Rectangle EnemyCol;

            if (!b.Enabled)
                return false ;

            bool rtn = false;
            
            //sprites
            float Ew = Enemy.Etexture.width * 1f;
            float Eh = Enemy.Etexture.height * 1f;
            EnemyCol = new Rectangle(e.Position.x, e.Position.y, Ew, Eh);

            Rectangle BulletCol;
            BulletCol = new Rectangle(b.Position.x, b.Position.y, 6, 2);
            rtn = rl.CheckCollisionRecs(BulletCol, EnemyCol);

            if (rtn)
                e.Enabled = false;

            if(rtn)
            Console.WriteLine("Collision check resulted in: " + rtn);
            return rtn;
        }

        public static int Main()
        {
            // Initialization
            //--------------------------------------------------------------------------------------
            Random rand = new Random();
            Player player = new Player();
            Bullet[] bullets = new Bullet[10];
            Pickup[] pickup = new Pickup[70];
            Enemy[] enemies = new Enemy[50];
            string[] contents = File.LoadFile("GO.txt");

            int screenWidth = 1000;
            int screenHeight = 900;

            int health = player.health;
            int ammo = player.ammo;
            int score = player.score;
            int timer = 0;
            int bulletpointer;
            int gameOverTimer = 0;
            bool winState = false;
            string username = "";

            Console.WriteLine("What is your name?");
            username = Console.ReadLine();

            rl.InitWindow(screenWidth, screenHeight, "Assessment");

            //Create enemies
            Enemy.EnemyTexture("bat.png");

            for (int Enidx = 0; Enidx < enemies.Length; Enidx++)
            {
                enemies[Enidx] = new Enemy();

                enemies[Enidx].Position.x = rand.Next(20, screenWidth - 20);
                enemies[Enidx].Position.y = rand.Next(30, screenHeight - 20);
            }

            //Create bullets
            for (int idx = 0; idx < bullets.Length; idx++)
                bullets[idx] = new Bullet();

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
            while (!rl.WindowShouldClose()) // Detect window close button or ESC key
            {
                // Update
                player.RunUpdate();
                timer++;

                // Draw
                rl.BeginDrawing();

                rl.ClearBackground(Color.LIGHTGRAY);
                rl.DrawText("Score: " + score, 50, 50, 12,  Color.BLACK);
                rl.DrawText("Health: " + health, 50, 75, 12, Color.BLACK);
                rl.DrawText("Time: " + timer / 60, screenWidth - 200, 50, 12, Color.BLACK);
                rl.DrawText("Ammo: " + ammo, screenWidth - 200, 80, 12, Color.BLACK);

                if (timer / 60 >= 60 && winState == false)
                {
                    rl.DrawText(contents[0], 250, 50, 20, Color.ORANGE);
                    Console.WriteLine(gameOverTimer);

                    player.speed = 0;
                    ++gameOverTimer;

                    if (gameOverTimer > 60 * 5)
                    {
                        File.SaveScore(username, score);
                        return 0;
                    }
                }

                player.Draw();

                bool NotAllEnemiesDead = false;

                foreach (Enemy en in enemies)
                {
                    if (en.Enabled)
                    {
                        NotAllEnemiesDead = true;

                        en.Draw();

                        if (CheckCollisionV2(player, en))
                            health--;

                        if (en.Position.x > player.Position.x)
                            en.Position.x -= 1 + rand.Next(0, 1);

                        if (en.Position.x < player.Position.x)
                            en.Position.x += 1 + rand.Next(0, 1);

                        if (en.Position.y > player.Position.y)
                            en.Position.y -= 1 + rand.Next(0, 1);

                        if (en.Position.y < player.Position.y)
                            en.Position.y += 1 + rand.Next(0, 1);

                        if (health <= 0)
                        {
                            health = 0;
                            rl.DrawText(contents[0], 250, 50, 20, Color.ORANGE);
                            player.speed = 0;

                            ++gameOverTimer;

                            if (gameOverTimer > 60 * 5)
                            {
                                File.SaveScore(username, score);
                                return 0;
                            }
                        }
                    }
                }

                if(NotAllEnemiesDead == false)
                {
                    //Display win
                    rl.DrawText("You won!", 250, 50, 20, Color.ORANGE);

                    winState = true;
                    ++gameOverTimer;

                    if (gameOverTimer > 60 * 5)
                    {
                        File.SaveScore(username, score);
                        return 0;
                    }
                }

                if (rl.IsKeyPressed(KeyboardKey.KEY_SPACE) && ammo > 0)
                {
                    bulletpointer = 1; //bullet.findDisableBullet(bullets);
                    bullets[bulletpointer].Position.x = player.Position.x + 11;
                    bullets[bulletpointer].Position.y = player.Position.y + 10;
                    bullets[bulletpointer].bulletMove = true;
                    bullets[bulletpointer].Enabled = true;
                    ammo--;
                }

                foreach (Bullet bu in bullets)
                {
                    if (bu.Enabled)
                    {
                        bu.Draw();

                        if (bu.bulletMove)
                            bu.Position.x += 5;

                        for (int i = 0; i < enemies.Length; ++i)
                            score += CheckCollisionV3(bu, enemies[i]) ? 1 : 0;

                        if (ammo <= 0)
                        {
                            ammo = 0;

                            bu.Enabled = false;

                            if (ammo > -1)
                                bu.Enabled = true;
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

            }

            // De-Initialization
            //--------------------------------------------------------------------------------------
            rl.CloseWindow(); // Close window and OpenGL context
            //--------------------------------------------------------------------------------------

            return 0;
        }
    }
}