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

        public float playerHealth = 100;
        public int playerScore = 0;

        SceneObject tankObject = new SceneObject();
        SceneObject turretObject = new SceneObject();

        SpriteObject tankSprite = new SpriteObject();
        SpriteObject turretSprite = new SpriteObject();

        SceneObject bladeObject = new SceneObject();
        SpriteObject bladeSprite = new SpriteObject();

        SceneObject targetObject = new SceneObject();
        SpriteObject targetSprite = new SpriteObject();

        private List<Projectile> bullets = new List<Projectile>();

        MathClasses.AABB bulletCollider = new MathClasses.AABB(new MathClasses.Vector3(0, 0, 0), new MathClasses.Vector3(0, 0, 0));

        //initiating player collider
        MathClasses.AABB playerCollider = new MathClasses.AABB(new MathClasses.Vector3(0, 0, 0), new MathClasses.Vector3(0, 0, 0));

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

            tankSprite.Load("../../Images/tankBlue_outline.png");
            tankSprite.SetRotateZ(-90 * (float)(Math.PI / 180.0f));
            // sets an offset for the base, so it rotates around the centre
            tankSprite.SetPosition(-tankSprite.Width / 2.0f, tankSprite.Height / 2.0f);

            turretSprite.Load("../../Images/barrelBlue.png");
            turretSprite.SetRotateZ(-90 * (float)(Math.PI / 180.0f));
            // sets an offset for the base, so it rotates around the centre
            turretSprite.SetPosition(0, turretSprite.Width / 2.0f);

            // set up the scene object hierarchy - parent the turret to the base,
            // then the base to the tank sceneObject
            turretObject.AddChild(turretSprite);
            tankObject.AddChild(tankSprite);
            tankObject.AddChild(turretObject);

            // having an empty object for the tank parent means we can set the
            // position/rotation of the tank without
            // affecting the offset of the base sprite
            tankObject.SetPosition(rl.Raylib.GetScreenWidth() / 3.0f, rl.Raylib.GetScreenHeight() / 2.0f);
        }

        public void Update()
        {
            lastTime = currentTime;
            currentTime = stopwatch.ElapsedMilliseconds;
            deltaTime = (currentTime - lastTime) / 1000.0f;

            bladeObject.RotateZ(deltaTime * 8);

            //rotate player left on A pressed
            if (rl.Raylib.IsKeyDown(rl.KeyboardKey.KEY_A))
            {
                tankObject.RotateZ(-deltaTime * 5);
            }
            //rotate player right on D pressed
            if (rl.Raylib.IsKeyDown(rl.KeyboardKey.KEY_D))
            {
                tankObject.RotateZ(deltaTime * 5);
            }

            //move player forward on W pressed
            if (rl.Raylib.IsKeyDown(rl.KeyboardKey.KEY_W))
            {
                TankMoveForward();
            }
            //move player backwards on S pressed
            if (rl.Raylib.IsKeyDown(rl.KeyboardKey.KEY_S))
            {
                TankMoveBackwards();
            }

            //rotate turret top left on Q pressed
            if (rl.Raylib.IsKeyDown(rl.KeyboardKey.KEY_Q))
            {
                turretObject.RotateZ(-deltaTime * 2);
            }
            //rotate turret top right on E pressed
            if (rl.Raylib.IsKeyDown(rl.KeyboardKey.KEY_E))
            {
                turretObject.RotateZ(deltaTime * 2);
            }
            //updates tank in delta time
            tankObject.Update(deltaTime);

            //shoots bullet when SPACE pressed and timer is >= 1.5 seconds
            if (rl.Raylib.IsKeyPressed(rl.KeyboardKey.KEY_SPACE))
            {
                Projectile newBullet = new Projectile(turretObject.globalTransform.m5, -turretObject.globalTransform.m4);
                newBullet.SetPosition(turretObject.globalTransform.m7, turretObject.globalTransform.m8);
                //adds bullet to list
                bullets.Add(newBullet);
            }

            if (bullets.Count > 0)
            {
                foreach (Projectile newBullet in bullets)
                {
                    //moves bullet forward in facing direction * speed
                    newBullet.OnUpdate(deltaTime);

                    //debug drawing collider box updateing with bullets pos
                    rl.Raylib.DrawRectangleLines(Convert.ToInt32(newBullet.globalTransform.m7 - 10), Convert.ToInt32(newBullet.globalTransform.m8) - 10, 15, 15, rl.Color.GREEN);

                    //bullet collider box updating with bullets pos
                    bulletCollider.Resize(new MathClasses.Vector3(newBullet.globalTransform.m7 - 15, newBullet.globalTransform.m8 - 15, 0),
                                          new MathClasses.Vector3(newBullet.globalTransform.m7 + 15, newBullet.globalTransform.m8 + 15, 0));

                    //if bullet enters testbox do somthing
                    if (bulletCollider.Overlaps(targetCollider))
                    {
                        Console.WriteLine("HIT TARGET");
                        playerScore += 1;
                        bullets.Remove(newBullet);
                        break;
                    }

                    if (newBullet.localTransform.m7 < 0.0f || newBullet.localTransform.m7 > rl.Raylib.GetScreenWidth() || newBullet.localTransform.m8 < 0.0f || newBullet.localTransform.m8 > rl.Raylib.GetScreenHeight())
                    {
                        Console.WriteLine("HIT WALL");
                        bullets.Remove(newBullet);
                        break;
                    }
                }
            }

            //collider box around player
            playerCollider.Resize(new MathClasses.Vector3(tankObject.globalTransform.m7 - (tankSprite.Width / 2), tankObject.globalTransform.m8 - (tankSprite.Height / 2), 0),
                                  new MathClasses.Vector3(tankObject.globalTransform.m7 + (tankSprite.Width / 2), tankObject.globalTransform.m8 + (tankSprite.Height / 2), 0));
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



            //check collision between players collider boxes
            PlayerCollision(leftWall);
            PlayerCollision(rightWall);
            PlayerCollision(topWall);
            PlayerCollision(bottomWall);
            //PlayerCollision(bladebox);
            if (playerCollider.Overlaps(bladeBox))
            {
                playerHealth -= 0.2f;
            }

            lastTime = currentTime;
        }

        //player collisions with aabb boxes
        public void PlayerCollision(AABB collisionBox)
        {
            if (playerCollider.Overlaps(collisionBox) && (rl.Raylib.IsKeyDown(rl.KeyboardKey.KEY_W)))
            {
                TankMoveBackwards();
            }
            else if (playerCollider.Overlaps(collisionBox) && (rl.Raylib.IsKeyDown(rl.KeyboardKey.KEY_S)))
            {
                TankMoveForward();
            }
        }

        //move thank forward
        public void TankMoveForward()
        {
            Vector3 facing = new Vector3(
            tankObject.LocalTransform.m1,
            tankObject.LocalTransform.m2, 1) * deltaTime * 300;
            tankObject.Translate(facing.x, facing.y);
        }
        //move tank backwards
        public void TankMoveBackwards()
        {
            Vector3 facing = new Vector3(
            tankObject.LocalTransform.m1,
            tankObject.LocalTransform.m2, 1) * deltaTime * -300;
            tankObject.Translate(facing.x, facing.y);
        }

        public void Draw()
        {
            rl.Raylib.BeginDrawing();

            rl.Raylib.DrawText("player health =", 50, 50, 30, rl.Color.WHITE);
            rl.Raylib.DrawText(Convert.ToString(playerHealth), 300, 50, 30, rl.Color.WHITE);

            rl.Raylib.DrawText("player score =", 600, 50, 30, rl.Color.WHITE);
            rl.Raylib.DrawText(Convert.ToString(playerScore), 850, 50, 30, rl.Color.WHITE);

            //sets background to black
            rl.Raylib.ClearBackground(rl.Color.BLACK);

            //if bullet list is >= 1 draw bullet
            if (bullets.Count >= 1)
                foreach (SceneObject newBullet in bullets)
                {
                    newBullet.Draw();
                }
            //draw tank
            tankObject.Draw();
            bladeObject.Draw();
            targetObject.Draw();


            //debug drawing players collision box
            rl.Raylib.DrawRectangleLines(Convert.ToInt32(tankObject.globalTransform.m7 - tankSprite.Width / 2), Convert.ToInt32(tankObject.globalTransform.m8 - tankSprite.Height / 2), 83, 78, rl.Color.RED);
            //debug drawing blade box
            rl.Raylib.DrawRectangleLines(700, 400, 300, 300, rl.Color.RED);
            //debug drawing target box
            rl.Raylib.DrawRectangleLines(Convert.ToInt32(targetObject.globalTransform.m7 - targetSprite.Width / 2), Convert.ToInt32(targetObject.globalTransform.m8 - targetSprite.Height / 2), 128, 128, rl.Color.RED);



            rl.Raylib.EndDrawing();
        }

        public void Shutdown()
        {

        }
    }
}


