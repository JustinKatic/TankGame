using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using MathClasses;
using rl = Raylib;

namespace TankGame
{

    class TankGame
    {
        Stopwatch stopwatch = new Stopwatch();
        private long currentTime = 0;
        private long lastTime = 0;
        private float bulletShootTimer = 2;
        private float explosionDeletetimer = 0;     
        private float deltaTime = 0.005f;

        SceneObject tankObject = new SceneObject();
        SceneObject turretObject = new SceneObject();

        SpriteObject tankSprite = new SpriteObject();
        SpriteObject turretSprite = new SpriteObject();


        //Walls
        private rl.Image leftWall;
        private rl.Texture2D leftWallTexture;
        private int leftWallPosX = 0;
        private int leftWallPosY = 0;
        public rl.Rectangle leftWallRec;

        private rl.Image rightWall;
        private rl.Texture2D rightWallTexture;
        private int rightWallPosX = 1350;
        private int rightWallPosY = 0;
        public rl.Rectangle rightWallRec;

        private rl.Image topWall;
        private rl.Texture2D topWallTexture;
        private int topWallPosX = 0;
        private int topWallPosY = 0;
        public rl.Rectangle topWallRec;

        private rl.Image bottomWall;
        private rl.Texture2D bottomWallTexture;
        private int bottomWallPosX = 0;
        private int bottomWallPosY = 850;
        public rl.Rectangle bottomWallRec;

        //bullet rec
        private rl.Rectangle bulletRec;

        private List<SpriteObject> BulletsList = new List<SpriteObject>();
        private List<SpriteObject> ExplosionList = new List<SpriteObject>();

        public void Init()
        {
            stopwatch.Start();
            lastTime = stopwatch.ElapsedMilliseconds;

            Colour colour = new Colour();
            colour.GetRed();
            Console.WriteLine("get red colour = " + colour.GetRed());
            colour.GetGreen();
            Console.WriteLine("get green colour =" + colour.GetGreen());
            colour.SetRed(94);
            colour.GetRed();
            Console.WriteLine("get red colour = " + colour.GetRed());
            colour.ChangeColour();
            Console.WriteLine("get red colour = " + colour.GetRed());
            Console.WriteLine("get green colour = " + colour.GetGreen());

            tankSprite.Load("../../Images/tankBlue_outline.png");
            tankObject.SetPosition(rl.Raylib.GetScreenWidth() / 2.0f, rl.Raylib.GetScreenHeight() / 2.0f);
            tankSprite.SetRotate(-90 * (float)(Math.PI / 180.0f));
            // sets an offset for the base, so it rotates around the centre
            tankSprite.SetPosition(-tankSprite.Width / 2.0f, tankSprite.Height / 2.0f);

            turretSprite.Load("../../Images/barrelBlue.png");
            turretSprite.SetRotate(-90 * (float)(Math.PI / 180.0f));
            // sets an offset for the base, so it rotates around the centre
            turretSprite.SetPosition(0, turretSprite.Width / 2.0f);

            // set up the scene object hierarchy - parent the turret to the base,
            // then the base to the tank sceneObject
            turretObject.AddChild(turretSprite);
            tankObject.AddChild(tankSprite);
            tankObject.AddChild(turretObject);
            

            leftWall = rl.Raylib.LoadImage("../../Images/dirtSideWall.png");
            leftWallTexture = rl.Raylib.LoadTextureFromImage(leftWall);

            rightWall = rl.Raylib.LoadImage("../../Images/dirtSideWall.png");
            rightWallTexture = rl.Raylib.LoadTextureFromImage(rightWall);

            topWall = rl.Raylib.LoadImage("../../Images/dirtTopBottomWall.png");
            topWallTexture = rl.Raylib.LoadTextureFromImage(topWall);

            bottomWall = rl.Raylib.LoadImage("../../Images/dirtTopBottomWall.png");
            bottomWallTexture = rl.Raylib.LoadTextureFromImage(bottomWall);

            //left wall collision rec box
            leftWallRec.x = 0;
            leftWallRec.y = 0;
            leftWallRec.width = 50;
            leftWallRec.height = 900;

            //right wall collision rec box
            rightWallRec.x = 1350;
            rightWallRec.y = 0;
            rightWallRec.width = 50;
            rightWallRec.height = 900;

            //top wall collision rec box
            topWallRec.x = 50;
            topWallRec.y = 0;
            topWallRec.width = 1300;
            topWallRec.height = 50;

            //bottom wall collision rec box
            bottomWallRec.x = 50;
            bottomWallRec.y = 850;
            bottomWallRec.width = 1300;
            bottomWallRec.height = 50;

            // having an empty object for the tank parent means we can set the
            // position/rotation of the tank without
            // affecting the offset of the base sprite
            tankObject.SetPosition(rl.Raylib.GetScreenWidth() / 2.0f, rl.Raylib.GetScreenHeight() / 2.0f);
        }

        public void Update()
        {
            lastTime = currentTime;
            currentTime = stopwatch.ElapsedMilliseconds;
            deltaTime = (currentTime - lastTime) / 1000.0f;
            explosionDeletetimer += deltaTime;
            bulletShootTimer += deltaTime;
          

            //rotate player left on A pressed
            if (rl.Raylib.IsKeyDown(rl.KeyboardKey.KEY_A))
            {
                tankObject.Rotate(-deltaTime * 5);
            }
            //rotate player right on D pressed
            if (rl.Raylib.IsKeyDown(rl.KeyboardKey.KEY_D))
            {
                tankObject.Rotate(deltaTime * 5);
            }
            //move player forward on W pressed
            if (rl.Raylib.IsKeyDown(rl.KeyboardKey.KEY_W))
            {
                Vector3 facing = new Vector3(
               tankObject.LocalTransform.m1,
               tankObject.LocalTransform.m2, 1) * deltaTime * 300;
                tankObject.Translate(facing.x, facing.y);
            }
            //move player backwards on S pressed
            if (rl.Raylib.IsKeyDown(rl.KeyboardKey.KEY_S))
            {
                Vector3 facing = new Vector3(
               tankObject.LocalTransform.m1,
               tankObject.LocalTransform.m2, 1) * deltaTime * -300;
                tankObject.Translate(facing.x, facing.y);
            }
            //rotate turret top left on Q pressed
            if (rl.Raylib.IsKeyDown(rl.KeyboardKey.KEY_Q))
            {
                turretObject.Rotate(-deltaTime * 2);
            }
            //rotate turret top right on E pressed
            if (rl.Raylib.IsKeyDown(rl.KeyboardKey.KEY_E))
            {
                turretObject.Rotate(deltaTime * 2);
            }
            //updates tank in delta time
            tankObject.Update(deltaTime);


            //shoots bullet when SPACE pressed and timer is >= 1.5 seconds
            if (rl.Raylib.IsKeyPressed(rl.KeyboardKey.KEY_SPACE) && bulletShootTimer >=1.5)
            {
                {
                    //creates a new bullet object
                    SpriteObject bullet = new SpriteObject();
                    //adds bullet in bullet list
                    BulletsList.Add(bullet);
                    bullet.Load("../../Images/bulletBlue.png");
                    bullet.localTransform = tankObject.GetChild(1).GlobalTransform.GetTransposed();
                    //creates a new explosion object 
                    SpriteObject explosion = new SpriteObject();
                    //adds explosion in explosion list
                    ExplosionList.Add(explosion);
                    explosion.Load("../../Images/smokeOrange.png");
                    //sets explosion to be = turrets transposed (matrix)
                    explosion.localTransform = tankObject.GetChild(1).GlobalTransform.GetTransposed();
                    //resets explosion timer back to 0
                    explosionDeletetimer = 0;
                    //resets shoot timer back to 0
                    bulletShootTimer = 0;
                }
            }

            if (ExplosionList.Count > 0)
                foreach (SpriteObject explosion in ExplosionList)
                {
                    if (explosion != null)
                    {
                        //sets explosions pos
                        Vector3 facing = new Vector3(
                           explosion.LocalTransform.m1,
                           explosion.LocalTransform.m2, 1);
                        explosion.Translate(facing.x, facing.y);

                        //shrink explosion every frame
                        explosion.texture.height--;
                         explosion.texture.width--;                                           
                    }
                }

            if (BulletsList.Count > 0)
                foreach (SpriteObject bullet in BulletsList)
                {
                    if (bullet != null)
                    {
                        Vector3 facing = new Vector3(
                            bullet.LocalTransform.m1,
                            bullet.LocalTransform.m2, 1) * deltaTime * 700;
                        bullet.Translate(facing.x, facing.y);
                        bulletRec.x = bullet.localTransform.m7;
                        bulletRec.y = bullet.localTransform.m8;
                        bulletRec.width = 20;
                        bulletRec.height = 10;

                        // collision test draw rec of bullet
                        //rl.Raylib.DrawRectangle(Convert.ToInt32(bullet.localTransform.m7), Convert.ToInt32(bullet.localTransform.m8), 20, 10, rl.Color.WHITE);
                    }
                }

            //collision detection with bullet and walls          
            if (BulletsList.Count > 0)
            {
                if (rl.Raylib.CheckCollisionRecs(bulletRec, leftWallRec) || (rl.Raylib.CheckCollisionRecs(bulletRec, rightWallRec))|| (rl.Raylib.CheckCollisionRecs(bulletRec, topWallRec)) || (rl.Raylib.CheckCollisionRecs(bulletRec, bottomWallRec)))
                {
                    Console.WriteLine("HIT WALL");
                    DeleteBullet(ref BulletsList, BulletsList.Count - 1);
                }
            }

            //deleting explosion if there is one after 1.2 seconds
            if (ExplosionList.Count > 0)
            {
                if (explosionDeletetimer > 1.2)
                {
                    DeleteExplosion(ref ExplosionList, ExplosionList.Count - 1);
                }
            }

            lastTime = currentTime;
        }

        public void Draw()
        {
            rl.Raylib.BeginDrawing();
            rl.Raylib.ClearBackground(rl.Color.BLACK);

            //if bullet list is > 0 draw bullet
            if (BulletsList.Count > 0)
                foreach (SceneObject bullet in BulletsList)
                {
                    bullet.Draw();
                }

            //if explosion list is > 0 draw explosion
            if (ExplosionList.Count > 0)
                foreach (SceneObject explosion in ExplosionList)
                {
                    explosion.Draw();
                }

            tankObject.Draw();

            //Drawing dirt wall objects
            rl.Raylib.DrawTexture(leftWallTexture, leftWallPosX, leftWallPosY, rl.Color.WHITE);
            rl.Raylib.DrawTexture(rightWallTexture, rightWallPosX, rightWallPosY, rl.Color.WHITE);
            rl.Raylib.DrawTexture(topWallTexture, topWallPosX, topWallPosY, rl.Color.WHITE);
            rl.Raylib.DrawTexture(bottomWallTexture, bottomWallPosX, bottomWallPosY, rl.Color.WHITE);

            
            //Collision box draw test area

            //rl.Raylib.DrawRectangle(0, 0, 50, 900, rl.Color.BLUE); //leftwall
            //rl.Raylib.DrawRectangle(1350, 0, 50, 900, rl.Color.WHITE); // right wall
            //rl.Raylib.DrawRectangle(50, 0, 1300, 50, rl.Color.GREEN); //topwall
            //rl.Raylib.DrawRectangle(50, 850, 1300, 50, rl.Color.GOLD); //bottomwall

            rl.Raylib.EndDrawing();
        }

        public void Shutdown()
        {

        }

        //delete bullet function
        public void DeleteBullet(ref List<SpriteObject> bullet, int index)
        {
            //removes bullet image
            rl.Raylib.UnloadImage(bullet[index].image);
            //sets bullet in list to = null
            bullet[index] = null;
            //removes bullet from list
            bullet.RemoveAt(index);
            Console.WriteLine("deleted bullet");
        }

        //delete explosion function
        public void DeleteExplosion(ref List<SpriteObject> explosion, int index)
        {
            //removes explosion image
            rl.Raylib.UnloadImage(explosion[index].image);
            //sets exposion in list to = null
            explosion[index] = null;
            //removes bullet from list
            explosion.RemoveAt(index);
            Console.WriteLine("explosion bullet");
        }
    }
}


