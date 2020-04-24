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
        private float player1ShootTimer = 0;
        private float player2ShootTimer = 0;
        private float shootCoolDown = 0.2f;

        private float deltaTime = 0.005f;

        //player texts variables
        public float player1Health = 100;
        public float player2Health = 100;
        public int player1Score = 0;
        public int player2Score = 0;
         
        //adding new scene and sprite variables
        SceneObject tankObject1 = new SceneObject();
        SpriteObject tankSprite1 = new SpriteObject();

        SceneObject turretObject1 = new SceneObject();
        SpriteObject turretSprite1 = new SpriteObject();

        SceneObject tankObject2 = new SceneObject();
        SpriteObject tankSprite2 = new SpriteObject();

        SceneObject turretObject2 = new SceneObject();
        SpriteObject turretSprite2 = new SpriteObject();

        SceneObject bladeObject = new SceneObject();
        SpriteObject bladeSprite = new SpriteObject();

        SceneObject targetObject1 = new SceneObject();
        SpriteObject targetSprite1 = new SpriteObject();
            
        SceneObject targetObject2 = new SceneObject();
        SpriteObject targetSprite2 = new SpriteObject();

        SceneObject middleWallObject = new SceneObject();
        SpriteObject middleWallSprite = new SpriteObject();


        //creating list for bullets to live in
        private List<Projectile> bulletsList1 = new List<Projectile>();
        private List<Projectile> bulletsList2 = new List<Projectile>();

        //initiating bullet collider
        AABB bulletCollider1 = new AABB(new Vector3(0, 0, 0), new Vector3(0, 0, 0));
        AABB bulletCollider2 = new AABB(new Vector3(0, 0, 0), new Vector3(0, 0, 0));

        //initiating player collider
        AABB player1Collider = new AABB(new Vector3(0, 0, 0), new Vector3(0, 0, 0));
        AABB player2Collider = new AABB(new Vector3(0, 0, 0), new Vector3(0, 0, 0));

        //initiating wall colliders
        AABB leftWall = new AABB(new Vector3(0, 0, 0), new Vector3(0, 900, 0));
        AABB rightWall = new AABB(new Vector3(1400, 0, 0), new Vector3(1400, 900, 0));
        AABB topWall = new AABB(new Vector3(0, 0, 0), new Vector3(1400, 0, 0));
        AABB bottomWall = new AABB(new Vector3(0, 900, 0), new Vector3(1400, 900, 0));
        AABB middleWall = new AABB(new Vector3(700, 0, 0), new Vector3(750, 900, 0));

        //initiating blade box collider
        AABB bladeBoxCollider = new AABB(new Vector3(0, 0, 0), new Vector3(0, 0, 0));

        //initiating target1 collider
        AABB targetCollider1 = new AABB(new Vector3(0, 0, 0), new Vector3(0, 0, 0));
        AABB targetCollider2 = new AABB(new Vector3(0, 0, 0), new Vector3(0, 0, 0));


        //init happening when game first opens
        public void Init()
        {
            stopwatch.Start();
            lastTime = stopwatch.ElapsedMilliseconds;



            //blade object init positions and adding sprite as child to obj
            bladeSprite.Load("../../Images/blade.png");
            bladeSprite.SetRotateZ(-90 * (float)(Math.PI / 180.0f));
            bladeSprite.SetPosition(-bladeSprite.Width / 2.0f, bladeSprite.Height / 2.0f);
            bladeObject.AddChild(bladeSprite);
            bladeObject.SetPosition(1800, 450);

            //left side target init positions 
            targetSprite1.Load("../../Images/target.png");
            targetSprite1.SetRotateZ(-90 * (float)(Math.PI / 180.0f));
            targetSprite1.SetPosition(-targetSprite1.Width / 2.0f, targetSprite1.Height / 2.0f);
            targetObject1.AddChild(targetSprite1);
            targetObject1.SetPosition(100, 700);
            targetObject1.RotateZ(-90 * (float)(Math.PI / 180.0f));

            //right side target init positions 
            targetSprite2.Load("../../Images/target.png");
            targetSprite2.SetRotateZ(-90 * (float)(Math.PI / 180.0f));
            targetSprite2.SetPosition(-targetSprite2.Width / 2.0f, targetSprite2.Height / 2.0f);
            targetObject2.AddChild(targetSprite2);
            targetObject2.SetPosition(1300, 700);
            targetObject2.RotateZ(-90 * (float)(Math.PI / 180.0f));

            //middle wall init position
            middleWallSprite.Load("../../Images/dirtMiddleWall.png");
            middleWallObject.AddChild(middleWallSprite);
            middleWallObject.SetPosition(rl.Raylib.GetScreenWidth() / 2.0f, 0);

            //TANK 1
            tankSprite1.Load("../../Images/tankBlue_outline.png");
            tankSprite1.SetRotateZ(-90 * (float)(Math.PI / 180.0f));
            // sets an offset for the base, so it rotates around the centre
            tankSprite1.SetPosition(-tankSprite1.Width / 2.0f, tankSprite1.Height / 2.0f);

            turretSprite1.Load("../../Images/barrelBlue.png");
            turretSprite1.SetRotateZ(-90 * (float)(Math.PI / 180.0f));
            // sets an offset for the base, so it rotates around the centre
            turretSprite1.SetPosition(0, turretSprite1.Width / 2.0f);

            // set up the scene object hierarchy - parent the turret to the base,
            // then the base to the tank sceneObject
            turretObject1.AddChild(turretSprite1);
            tankObject1.AddChild(tankSprite1);
            tankObject1.AddChild(turretObject1);

            // having an empty object for the tank parent means we can set the
            // position/rotation of the tank without
            // affecting the offset of the base sprite
            tankObject1.SetPosition(rl.Raylib.GetScreenWidth() / 3.0f, rl.Raylib.GetScreenHeight() / 2.0f);

            //TANK 2
            tankSprite2.Load("../../Images/tankRed_outline.png");
            tankSprite2.SetRotateZ(-90 * (float)(Math.PI / 180.0f));
            tankSprite2.SetPosition(-tankSprite2.Width / 2.0f, tankSprite2.Height / 2.0f);
            turretSprite2.Load("../../Images/barrelRed.png");
            turretSprite2.SetRotateZ(-90 * (float)(Math.PI / 180.0f));
            turretSprite2.SetPosition(0, turretSprite2.Width / 2.0f);
            tankObject2.RotateZ(-180 * (float)(Math.PI / 180.0f));
            turretObject2.AddChild(turretSprite2);
            tankObject2.AddChild(tankSprite2);
            tankObject2.AddChild(turretObject2);
            tankObject2.SetPosition(1200, rl.Raylib.GetScreenHeight() / 2.0f);
        }

        //update happening every frame after init
        public void Update()
        {
            lastTime = currentTime;
            currentTime = stopwatch.ElapsedMilliseconds;
            deltaTime = (currentTime - lastTime) / 1000.0f;
            player1ShootTimer += deltaTime;
            player2ShootTimer += deltaTime;

            if (rl.Raylib.IsKeyDown(rl.KeyboardKey.KEY_P))
            {
                // empty to stop the game ok?
            }
            else
            {
                //TANK 1 Movements
                TankMovement(tankObject1, turretObject1, rl.KeyboardKey.KEY_A, rl.KeyboardKey.KEY_D, rl.KeyboardKey.KEY_W, rl.KeyboardKey.KEY_S, rl.KeyboardKey.KEY_Q, rl.KeyboardKey.KEY_E, rl.KeyboardKey.KEY_SPACE, bulletsList1, ref player1ShootTimer);
                //TANK 2 Movements
                TankMovement(tankObject2, turretObject2, rl.KeyboardKey.KEY_KP_4, rl.KeyboardKey.KEY_KP_6, rl.KeyboardKey.KEY_KP_8, rl.KeyboardKey.KEY_KP_5, rl.KeyboardKey.KEY_KP_7, rl.KeyboardKey.KEY_KP_9, rl.KeyboardKey.KEY_KP_0, bulletsList2, ref player2ShootTimer);

                if (player1Health <= 0)
                {
                    Console.WriteLine("died");
                    tankObject1.SetPosition(rl.Raylib.GetScreenWidth() / 3.0f, rl.Raylib.GetScreenHeight() / 2.0f);
                    player1Health = 100;
                }


                //interaction each bullet has in world when bullet collides with certain game objects
                ForEachBullet(bulletsList1, bulletCollider1, ref player1Score, targetCollider2, player2Collider, ref player2Health);
                ForEachBullet(bulletsList2, bulletCollider2, ref player2Score, targetCollider1, player1Collider, ref player1Health);

                //resizing the collider around each player object every update
                ResizingCollider(player1Collider, tankObject1, tankSprite1);
                ResizingCollider(player2Collider, tankObject2, tankSprite2);

                //resizing the collider around each target object every update
                ResizingCollider(targetCollider1, targetObject1, targetSprite1);
                ResizingCollider(targetCollider2, targetObject2, targetSprite2);

                //resizing the collider around each blade object every update
                ResizingCollider(bladeBoxCollider, bladeObject, bladeSprite);

                //moving target objects forward 
                MoveTargetForward(targetObject1);
                MoveTargetForward(targetObject2);

                //check collision between player 1 collider boxes if collision players speed is minus what ever direction player is moving
                PlayerCollision(player1Collider, leftWall, rl.KeyboardKey.KEY_W, rl.KeyboardKey.KEY_S, tankObject1);
                PlayerCollision(player1Collider, rightWall, rl.KeyboardKey.KEY_W, rl.KeyboardKey.KEY_S, tankObject1);
                PlayerCollision(player1Collider, topWall, rl.KeyboardKey.KEY_W, rl.KeyboardKey.KEY_S, tankObject1);
                PlayerCollision(player1Collider, bottomWall, rl.KeyboardKey.KEY_W, rl.KeyboardKey.KEY_S, tankObject1);
                PlayerCollision(player1Collider, middleWall, rl.KeyboardKey.KEY_W, rl.KeyboardKey.KEY_S, tankObject1);
                //check collision between player 2 collider boxes if collision players speed is minus what ever direction player is moving
                PlayerCollision(player2Collider, leftWall, rl.KeyboardKey.KEY_KP_8, rl.KeyboardKey.KEY_KP_5, tankObject2);
                PlayerCollision(player2Collider, rightWall, rl.KeyboardKey.KEY_KP_8, rl.KeyboardKey.KEY_KP_5, tankObject2);
                PlayerCollision(player2Collider, topWall, rl.KeyboardKey.KEY_KP_8, rl.KeyboardKey.KEY_KP_5, tankObject2);
                PlayerCollision(player2Collider, bottomWall, rl.KeyboardKey.KEY_KP_8, rl.KeyboardKey.KEY_KP_5, tankObject2);
                PlayerCollision(player2Collider, middleWall, rl.KeyboardKey.KEY_KP_8, rl.KeyboardKey.KEY_KP_5, tankObject2);


                //rotating blade object to cause it to spin on spot
                bladeObject.RotateZ(deltaTime * 8);
                //moves blade objects m7(x pos) left once of screen reset its pos back on right side of screen
                bladeObject.localTransform.m7 -= 150 * deltaTime;
                if (bladeObject.LocalTransform.m7 <= -400)
                {
                    bladeObject.localTransform.m7 = 1800;
                }
                //PlayerCollision with blade box if player hits box health -= 30/sec while in box
                if (player1Collider.Overlaps(bladeBoxCollider))
                {
                    player1Health -= (deltaTime * 30);
                }
                if (player2Collider.Overlaps(bladeBoxCollider))
                {
                    player2Health -= (deltaTime * 30);
                }

                //checks which player has died and set them back to that players starting pos
                PlayerDead(ref player1Health, tankObject1);
                PlayerDead(ref player2Health, tankObject2);

                lastTime = currentTime;
            }
        }

        public void Draw()
        {
            rl.Raylib.BeginDrawing();

            //Player 1 health stats
            rl.Raylib.DrawText("player 1 health =", 50, 50, 30, rl.Color.WHITE);
            rl.Raylib.DrawText(Convert.ToString(Convert.ToInt32(player1Health)), 320, 50, 30, rl.Color.WHITE);
            //player 1 score stats
            rl.Raylib.DrawText("player 1 score =", 50, 100, 30, rl.Color.WHITE);
            rl.Raylib.DrawText(Convert.ToString(player1Score), 320, 100, 30, rl.Color.WHITE);
            //player 2 health stats
            rl.Raylib.DrawText("player 2 health =", 1000, 50, 30, rl.Color.WHITE);
            rl.Raylib.DrawText(Convert.ToString(Convert.ToInt32(player2Health)), 1270, 50, 30, rl.Color.WHITE);
            //player 2 score stats
            rl.Raylib.DrawText("player 2 score =", 1000, 100, 30, rl.Color.WHITE);
            rl.Raylib.DrawText(Convert.ToString(player2Score), 1270, 100, 30, rl.Color.WHITE);

            rl.Raylib.DrawText("Hold P for pause/help", 20, 850, 40, rl.Color.GREEN);
            rl.Raylib.DrawText("Hold H for debug Hitboxes", 850, 850, 40, rl.Color.GREEN);




            //sets background to black
            rl.Raylib.ClearBackground(rl.Color.BLACK);

            //if bullet list is >= 1 draw bullet
            if (bulletsList1.Count >= 1)
                foreach (SceneObject newBullet in bulletsList1)
                {
                    newBullet.Draw();
                }

            if (bulletsList2.Count >= 1)
                foreach (SceneObject newBullet in bulletsList2)
                {
                    newBullet.Draw();
                }

            //draw tank
            tankObject1.Draw();
            tankObject2.Draw();

            middleWallObject.Draw();
            targetObject1.Draw();
            targetObject2.Draw();
            bladeObject.Draw();

            if (rl.Raylib.IsKeyDown(rl.KeyboardKey.KEY_H))
            {
                //debug drawing player1 collision box
                rl.Raylib.DrawRectangleLines(Convert.ToInt32(tankObject1.globalTransform.m7 - tankSprite1.Width / 2), Convert.ToInt32(tankObject1.globalTransform.m8 - tankSprite1.Height / 2), 83, 78, rl.Color.RED);
                //debug drawing player2 collision box
                rl.Raylib.DrawRectangleLines(Convert.ToInt32(tankObject2.globalTransform.m7 - tankSprite2.Width / 2), Convert.ToInt32(tankObject2.globalTransform.m8 - tankSprite2.Height / 2), 83, 78, rl.Color.RED);
                //debug drawing blade box
                rl.Raylib.DrawRectangleLines(Convert.ToInt32(bladeObject.globalTransform.m7 - bladeSprite.Width / 2), Convert.ToInt32(bladeObject.globalTransform.m8 - bladeSprite.Height / 2), 300, 300, rl.Color.RED);
                //debug drawing target box
                rl.Raylib.DrawRectangleLines(Convert.ToInt32(targetObject1.globalTransform.m7 - targetSprite1.Width / 2), Convert.ToInt32(targetObject1.globalTransform.m8 - targetSprite1.Height / 2), 128, 128, rl.Color.RED);
                //debug drawing target box
                rl.Raylib.DrawRectangleLines(Convert.ToInt32(targetObject2.globalTransform.m7 - targetSprite2.Width / 2), Convert.ToInt32(targetObject2.globalTransform.m8 - targetSprite2.Height / 2), 128, 128, rl.Color.RED);
            }

            if (rl.Raylib.IsKeyDown(rl.KeyboardKey.KEY_P))
            {
                rl.Raylib.DrawText("CONTROLS", 600, 100, 40, rl.Color.GREEN);
                rl.Raylib.DrawText("PLAYER 1", 200, 200, 40, rl.Color.GREEN);
                rl.Raylib.DrawText("Forward = W", 200, 250, 40, rl.Color.GREEN);
                rl.Raylib.DrawText("Backwards = S", 200, 300, 40, rl.Color.GREEN);
                rl.Raylib.DrawText("Rotate right = D", 200, 350, 40, rl.Color.GREEN);
                rl.Raylib.DrawText("Rotate left = A", 200, 400, 40, rl.Color.GREEN);
                rl.Raylib.DrawText("Rotate turret right = D", 200, 450, 40, rl.Color.GREEN);
                rl.Raylib.DrawText("Rotate turret left = A", 200, 500, 40, rl.Color.GREEN);
                rl.Raylib.DrawText("Shoot = SpaceBar", 200, 550, 40, rl.Color.GREEN);

                rl.Raylib.DrawText("PLAYER 2", 800, 200, 40, rl.Color.GREEN);
                rl.Raylib.DrawText("Forward = Num 8", 800, 250, 40, rl.Color.GREEN);
                rl.Raylib.DrawText("Backwards = Num 5", 800, 300, 40, rl.Color.GREEN);
                rl.Raylib.DrawText("Rotate right = Num 6", 800, 350, 40, rl.Color.GREEN);
                rl.Raylib.DrawText("Rotate left = Num 4", 800, 400, 40, rl.Color.GREEN);
                rl.Raylib.DrawText("Rotate turret right = Num 7", 800, 450, 40, rl.Color.GREEN);
                rl.Raylib.DrawText("Rotate turret left = Num 9", 800, 500, 40, rl.Color.GREEN);
                rl.Raylib.DrawText("Shoot = Num 0", 800, 550, 40, rl.Color.GREEN);

                rl.Raylib.DrawText("Objective", 600, 650, 40, rl.Color.GOLD);
                rl.Raylib.DrawText("Earn 1 point by shooting opposing teams target", 200, 700, 40, rl.Color.GOLD);
                rl.Raylib.DrawText("20 points for destroying opposing teams tank", 200, 750, 40, rl.Color.GOLD);
              
            }

            rl.Raylib.EndDrawing();
        }

        public void Shutdown()
        {

        }

        //FUNCTIONS 
        //__________________________________________________________________________________________________________________________________________

        //reszing collider boxes around objects/sprites so collision box updates with the objects pos   
        public void ResizingCollider(AABB whichCollider, SceneObject whichObject, SpriteObject whichSprite)
        {
            whichCollider.Resize(new Vector3(whichObject.globalTransform.m7 - (whichSprite.Width / 2), whichObject.globalTransform.m8 - (whichSprite.Height / 2), 0),
                                  new Vector3(whichObject.globalTransform.m7 + (whichSprite.Width / 2), whichObject.globalTransform.m8 + (whichSprite.Height / 2), 0));
        }

        //interacting each bullet has with objects in the scene such as destroying them, damageing player hitting collision boxes etc
        public void ForEachBullet(List<Projectile> whichList, AABB whichBulletCollider, ref int Whichscore, AABB whichTargetcollider, AABB whichPlayerCollider, ref float health)
        {
            if (whichList.Count > 0)
            {
                foreach (Projectile newBullet in whichList)
                {
                    //moves bullet forward in facing direction * speed
                    newBullet.OnUpdate(deltaTime);

                    //debug drawing collider box updateing with bullets pos
                    if (rl.Raylib.IsKeyDown(rl.KeyboardKey.KEY_H))
                    {
                        rl.Raylib.DrawRectangleLines(Convert.ToInt32(newBullet.globalTransform.m7 - 10), Convert.ToInt32(newBullet.globalTransform.m8) - 10, 15, 15, rl.Color.GREEN);
                    }
                    //bullet collider box updating with bullets pos
                    whichBulletCollider.Resize(new Vector3(newBullet.globalTransform.m7 - 10, newBullet.globalTransform.m8 - 10, 0),
                                          new Vector3(newBullet.globalTransform.m7 + 10, newBullet.globalTransform.m8 + 10, 0));

                    //if bullet enters testbox do somthing
                    if (whichBulletCollider.Overlaps(whichTargetcollider))
                    {
                        Console.WriteLine("HIT TARGET");
                        Whichscore += 1;
                        Console.WriteLine(player1Score);
                        whichList.Remove(newBullet);
                        break;
                    }
                    if (whichBulletCollider.Overlaps(whichPlayerCollider))
                    {
                        health -= 10;
                        whichList.Remove(newBullet);
                        break;
                    }
                    if (newBullet.localTransform.m7 < 0.0f || newBullet.localTransform.m7 > rl.Raylib.GetScreenWidth() || newBullet.localTransform.m8 < 0.0f || newBullet.localTransform.m8 > rl.Raylib.GetScreenHeight())
                    {
                        Console.WriteLine("HIT WALL");
                        whichList.Remove(newBullet);
                        break;
                    }
                }
            }
        }

        //moves target in forward direction
        public void MoveTargetForward(SceneObject WhichTarget)
        {
            MoveForward(WhichTarget);
            if (WhichTarget.localTransform.m8 <= -200)
            {
                WhichTarget.localTransform.m8 = 1000;
            }
        }

        //player collisions with aabb boxes
        public void PlayerCollision(AABB whichPlayerCollider, AABB collidingWith, rl.KeyboardKey forwardKey, rl.KeyboardKey backwardsKey, SceneObject whichTankObj)
        {
            if (whichPlayerCollider.Overlaps(collidingWith) && (rl.Raylib.IsKeyDown(forwardKey)))
            {
                MoveBackwards(whichTankObj);
            }
            else if (whichPlayerCollider.Overlaps(collidingWith) && (rl.Raylib.IsKeyDown(backwardsKey)))
            {
                MoveForward(whichTankObj);
            }
        }

        //player movements / shoot
        public void TankMovement(SceneObject whichTank, SceneObject whichTurret, rl.KeyboardKey left, rl.KeyboardKey right, rl.KeyboardKey forward,
            rl.KeyboardKey reverse, rl.KeyboardKey turretLeft, rl.KeyboardKey turretRight, rl.KeyboardKey shoot, List<Projectile> whichList, ref float WhichPlayersShootTimer)
        {
            //rotate player left on A pressed
            if (rl.Raylib.IsKeyDown(left))
            {
                whichTank.RotateZ(-deltaTime * 5);
            }
            //rotate player right on D pressed
            if (rl.Raylib.IsKeyDown(right))
            {
                whichTank.RotateZ(deltaTime * 5);
            }

            //move player forward on W pressed
            if (rl.Raylib.IsKeyDown(forward))
            {
                MoveForward(whichTank);
            }
            //move player backwards on S pressed
            if (rl.Raylib.IsKeyDown(reverse))
            {
                MoveBackwards(whichTank);
            }
            //rotate turret top left on Q pressed
            if (rl.Raylib.IsKeyDown(turretLeft))
            {
                whichTurret.RotateZ(-deltaTime * 2);
            }
            //rotate turret top right on E pressed
            if (rl.Raylib.IsKeyDown(turretRight))
            {
                whichTurret.RotateZ(deltaTime * 2);
            }
            //updates tank in delta time
            whichTank.Update(deltaTime);

            //shoots bullet when SPACE pressed and timer is >= 1 seconds
            if (rl.Raylib.IsKeyPressed(shoot) && WhichPlayersShootTimer >= shootCoolDown)
            {
                WhichPlayersShootTimer = 0;
                Projectile newBullet = new Projectile(whichTurret.globalTransform.m5, -whichTurret.globalTransform.m4);
                newBullet.SetPosition(whichTurret.globalTransform.m7, whichTurret.globalTransform.m8);
                //adds bullet to list
                whichList.Add(newBullet);
            }
        }

        //move thank forward
        public void MoveForward(SceneObject whichObj)
        {
            Vector3 facing = new Vector3(
            whichObj.LocalTransform.m1,
            whichObj.LocalTransform.m2, 1) * deltaTime * 300;
            whichObj.Translate(facing.x, facing.y);
        }

        //move tank backwards
        public void MoveBackwards(SceneObject whichObj)
        {
            Vector3 facing = new Vector3(
            whichObj.LocalTransform.m1,
            whichObj.LocalTransform.m2, 1) * deltaTime * -300;
            whichObj.Translate(facing.x, facing.y);
        }

        //checks which player has died and set them back to that players starting pos
        public void PlayerDead(ref float whichPlayersHealth,SceneObject whichTankObj)
        {
            if (whichPlayersHealth <= 0)
            {
                Console.WriteLine("died");
                if (whichTankObj == tankObject1)
                {
                    player2Score += 20;
                    whichTankObj.SetPosition(rl.Raylib.GetScreenWidth() / 3.0f, rl.Raylib.GetScreenHeight() / 2.0f);
                }
                else if (whichTankObj == tankObject2)
                {
                    tankObject2.SetPosition(1200, rl.Raylib.GetScreenHeight() / 2.0f);
                    player1Score += 20;
                }
                whichPlayersHealth = 100;
            }
        }
}
}


