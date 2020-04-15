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

        private float deltaTime = 0.005f;

        public float player1Health = 100;
        public int player1Score = 0;

        public float player2Health = 100;
        public int player2Score = 0;

        SceneObject tankObject1 = new SceneObject();
        SceneObject turretObject1 = new SceneObject();

        SpriteObject tankSprite1 = new SpriteObject();
        SpriteObject turretSprite1 = new SpriteObject();

        SceneObject tankObject2 = new SceneObject();
        SceneObject turretObject2 = new SceneObject();

        SpriteObject tankSprite2 = new SpriteObject();
        SpriteObject turretSprite2 = new SpriteObject();


        SceneObject bladeObject = new SceneObject();
        SpriteObject bladeSprite = new SpriteObject();

        SceneObject targetObject = new SceneObject();
        SpriteObject targetSprite = new SpriteObject();

        private List<Projectile> bulletsList1 = new List<Projectile>();
        private List<Projectile> bulletsList2 = new List<Projectile>();

        MathClasses.AABB bulletCollider1 = new MathClasses.AABB(new MathClasses.Vector3(0, 0, 0), new MathClasses.Vector3(0, 0, 0));
        MathClasses.AABB bulletCollider2 = new MathClasses.AABB(new MathClasses.Vector3(0, 0, 0), new MathClasses.Vector3(0, 0, 0));

        //initiating player collider
        MathClasses.AABB playerCollider1 = new MathClasses.AABB(new MathClasses.Vector3(0, 0, 0), new MathClasses.Vector3(0, 0, 0));
        MathClasses.AABB playerCollider2 = new MathClasses.AABB(new MathClasses.Vector3(0, 0, 0), new MathClasses.Vector3(0, 0, 0));

        //initiating wall colliders
        MathClasses.AABB leftWall = new MathClasses.AABB(new MathClasses.Vector3(0, 0, 0), new MathClasses.Vector3(0, 900, 0));
        MathClasses.AABB rightWall = new MathClasses.AABB(new MathClasses.Vector3(1400, 0, 0), new MathClasses.Vector3(1400, 900, 0));
        MathClasses.AABB topWall = new MathClasses.AABB(new MathClasses.Vector3(0, 0, 0), new MathClasses.Vector3(1400, 0, 0));
        MathClasses.AABB bottomWall = new MathClasses.AABB(new MathClasses.Vector3(0, 900, 0), new MathClasses.Vector3(1400, 900, 0));

        //initiating blade box collider
        MathClasses.AABB bladeBox = new MathClasses.AABB(new MathClasses.Vector3(700, 400, 0), new MathClasses.Vector3(1000, 700, 0));

        //initiating target collider
        MathClasses.AABB targetCollider = new MathClasses.AABB(new MathClasses.Vector3(0, 0, 0), new MathClasses.Vector3(0, 0, 0));

        public void Init()
        {
            stopwatch.Start();
            lastTime = stopwatch.ElapsedMilliseconds;

            bladeSprite.Load("../../Images/blade.png");
            bladeSprite.SetRotateZ(-90 * (float)(Math.PI / 180.0f));
            bladeSprite.SetPosition(-bladeSprite.Width / 2.0f, bladeSprite.Height / 2.0f);
            bladeObject.AddChild(bladeSprite);
            bladeObject.SetPosition(850, 550);

            targetSprite.Load("../../Images/target.png");
            targetSprite.SetRotateZ(-90 * (float)(Math.PI / 180.0f));
            targetObject.RotateZ(270 * (float)(Math.PI / 180.0f));
            targetSprite.SetPosition(-targetSprite.Width / 2.0f, targetSprite.Height / 2.0f);
            targetObject.AddChild(targetSprite);
            targetObject.SetPosition(200, 600);


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
            // sets an offset for the base, so it rotates around the centre
            tankSprite2.SetPosition(-tankSprite2.Width / 2.0f, tankSprite2.Height / 2.0f);
            turretSprite2.Load("../../Images/barrelRed.png");
            turretSprite2.SetRotateZ(-90 * (float)(Math.PI / 180.0f));
            // sets an offset for the base, so it rotates around the centre
            turretSprite2.SetPosition(0, turretSprite2.Width / 2.0f);
            tankObject2.RotateZ(-180 * (float)(Math.PI / 180.0f));

            // set up the scene object hierarchy - parent the turret to the base,
            // then the base to the tank sceneObject
            turretObject2.AddChild(turretSprite2);
            tankObject2.AddChild(tankSprite2);
            tankObject2.AddChild(turretObject2);

            // having an empty object for the tank parent means we can set the
            // position/rotation of the tank without
            // affecting the offset of the base sprite
            tankObject2.SetPosition(1200, rl.Raylib.GetScreenHeight() / 2.0f);
        }

        public void Update()
        {
            lastTime = currentTime;
            currentTime = stopwatch.ElapsedMilliseconds;
            deltaTime = (currentTime - lastTime) / 1000.0f;

            bladeObject.RotateZ(deltaTime * 8);


            //TANK 1 Movements
            TankMovement(tankObject1, turretObject1, rl.KeyboardKey.KEY_A, rl.KeyboardKey.KEY_D, rl.KeyboardKey.KEY_W, rl.KeyboardKey.KEY_S, rl.KeyboardKey.KEY_Q, rl.KeyboardKey.KEY_E, rl.KeyboardKey.KEY_SPACE, bulletsList1);

            //TANK 2 Movements
            TankMovement(tankObject2, turretObject2, rl.KeyboardKey.KEY_KP_4, rl.KeyboardKey.KEY_KP_6, rl.KeyboardKey.KEY_KP_8, rl.KeyboardKey.KEY_KP_5, rl.KeyboardKey.KEY_KP_7, rl.KeyboardKey.KEY_KP_9, rl.KeyboardKey.KEY_KP_0, bulletsList2);

            ForEachBullet(bulletsList1,bulletCollider1,ref player1Score);
            ForEachBullet(bulletsList2,bulletCollider2,ref player2Score);

            PlayerCollider(playerCollider1, tankObject1, tankSprite1);
            PlayerCollider(playerCollider2, tankObject2, tankSprite2);
           

            //collider box around target
            targetCollider.Resize(new MathClasses.Vector3(targetObject.globalTransform.m7 - (targetSprite.Width / 2), targetObject.globalTransform.m8 - (targetSprite.Height / 2), 0),
                                 new MathClasses.Vector3(targetObject.globalTransform.m7 + (targetSprite.Width / 2), targetObject.globalTransform.m8 + (targetSprite.Height / 2), 0));


            Vector3 facing = new Vector3(
            targetObject.LocalTransform.m1,
            targetObject.LocalTransform.m2, 1) * deltaTime * 200;
            targetObject.Translate(facing.x, facing.y);

            if (targetObject.localTransform.m8 <= -100)
            {
                targetObject.localTransform.m8 = 1000;
            }



            //check collision between player 1 collider boxes
            Player1Collision(leftWall);
            Player1Collision(rightWall);
            Player1Collision(topWall);
            Player1Collision(bottomWall);

            Player2Collision(leftWall);
            Player2Collision(rightWall);
            Player2Collision(topWall);
            Player2Collision(bottomWall);

            //PlayerCollision(bladebox);
            if (playerCollider1.Overlaps(bladeBox))
            {
                player1Health -= (deltaTime * 10);
            }

            lastTime = currentTime;
        }

            //collider box around player
        public void PlayerCollider(AABB whichCollider,SceneObject whichTankObject, SpriteObject whichTankSprite)
        {
            whichCollider.Resize(new MathClasses.Vector3(whichTankObject.globalTransform.m7 - (whichTankSprite.Width / 2), whichTankObject.globalTransform.m8 - (whichTankSprite.Height / 2), 0),
                                  new MathClasses.Vector3(whichTankObject.globalTransform.m7 + (whichTankSprite.Width / 2), whichTankObject.globalTransform.m8 + (whichTankSprite.Height / 2), 0));
        }

        public void ForEachBullet(List<Projectile> whichList, AABB whichBulletCollider,ref int score) //add which bullet collider
        {
            if (whichList.Count > 0)
            {
                foreach (Projectile newBullet in whichList)
                {
                    //moves bullet forward in facing direction * speed
                    newBullet.OnUpdate(deltaTime);

                    //debug drawing collider box updateing with bullets pos
                    if (rl.Raylib.IsKeyDown(rl.KeyboardKey.KEY_T))
                    {
                        rl.Raylib.DrawRectangleLines(Convert.ToInt32(newBullet.globalTransform.m7 - 10), Convert.ToInt32(newBullet.globalTransform.m8) - 10, 15, 15, rl.Color.GREEN);
                    }
                    //bullet collider box updating with bullets pos
                    whichBulletCollider.Resize(new MathClasses.Vector3(newBullet.globalTransform.m7 - 15, newBullet.globalTransform.m8 - 15, 0),
                                          new MathClasses.Vector3(newBullet.globalTransform.m7 + 15, newBullet.globalTransform.m8 + 15, 0));

                    //if bullet enters testbox do somthing
                    if (whichBulletCollider.Overlaps(targetCollider))
                    {
                        Console.WriteLine("HIT TARGET");
                        score += 1;
                        Console.WriteLine(player1Score);
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


        //player collisions with aabb boxes
        public void Player1Collision(AABB collisionBox)
        {
            if (playerCollider1.Overlaps(collisionBox) && (rl.Raylib.IsKeyDown(rl.KeyboardKey.KEY_W)))
            {
                TankMoveBackwards(tankObject1);
            }
            else if (playerCollider1.Overlaps(collisionBox) && (rl.Raylib.IsKeyDown(rl.KeyboardKey.KEY_S)))
            {
                TankMoveForward(tankObject1);
            }
        }

        public void Player2Collision(AABB collisionBox)
        {
            if (playerCollider2.Overlaps(collisionBox) && (rl.Raylib.IsKeyDown(rl.KeyboardKey.KEY_KP_8)))
            {
                TankMoveBackwards(tankObject2);
            }

            else if (playerCollider2.Overlaps(collisionBox) && (rl.Raylib.IsKeyDown(rl.KeyboardKey.KEY_KP_5)))
            {
                TankMoveForward(tankObject2);
            }
        }

        public void TankMovement(SceneObject whichTank, SceneObject whichTurret, rl.KeyboardKey left, rl.KeyboardKey right, rl.KeyboardKey forward, rl.KeyboardKey reverse, rl.KeyboardKey turretLeft, rl.KeyboardKey turretRight, rl.KeyboardKey shoot, List<Projectile> whichList)
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
                TankMoveForward(whichTank);
            }
            //move player backwards on S pressed
            if (rl.Raylib.IsKeyDown(reverse))
            {
                TankMoveBackwards(whichTank);
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

            //shoots bullet when SPACE pressed and timer is >= 1.5 seconds
            if (rl.Raylib.IsKeyPressed(shoot))
            {
                Projectile newBullet = new Projectile(whichTurret.globalTransform.m5, -whichTurret.globalTransform.m4);
                newBullet.SetPosition(whichTurret.globalTransform.m7, whichTurret.globalTransform.m8);
                //adds bullet to list
                whichList.Add(newBullet);
            }
        }

        //move thank forward
        public void TankMoveForward(SceneObject whichTank)
        {
            Vector3 facing = new Vector3(
            whichTank.LocalTransform.m1,
            whichTank.LocalTransform.m2, 1) * deltaTime * 300;
            whichTank.Translate(facing.x, facing.y);
        }
        //move tank backwards
        public void TankMoveBackwards(SceneObject whichTank)
        {
            Vector3 facing = new Vector3(
            whichTank.LocalTransform.m1,
            whichTank.LocalTransform.m2, 1) * deltaTime * -300;
            whichTank.Translate(facing.x, facing.y);
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


            bladeObject.Draw();
            targetObject.Draw();


            if (rl.Raylib.IsKeyDown(rl.KeyboardKey.KEY_T))
            {
                //debug drawing players collision box
                rl.Raylib.DrawRectangleLines(Convert.ToInt32(tankObject1.globalTransform.m7 - tankSprite1.Width / 2), Convert.ToInt32(tankObject1.globalTransform.m8 - tankSprite1.Height / 2), 83, 78, rl.Color.RED);
                //debug drawing blade box
                rl.Raylib.DrawRectangleLines(700, 400, 300, 300, rl.Color.RED);
                //debug drawing target box
                rl.Raylib.DrawRectangleLines(Convert.ToInt32(targetObject.globalTransform.m7 - targetSprite.Width / 2), Convert.ToInt32(targetObject.globalTransform.m8 - targetSprite.Height / 2), 128, 128, rl.Color.RED);
            }


            rl.Raylib.EndDrawing();
        }

        public void Shutdown()
        {

        }
    }
}


