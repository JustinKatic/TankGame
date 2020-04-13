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

        SceneObject tankObject = new SceneObject();
        SceneObject turretObject = new SceneObject();

        SpriteObject tankSprite = new SpriteObject();
        SpriteObject turretSprite = new SpriteObject();

        private List<Projectile> bullets = new List<Projectile>();

        MathClasses.AABB bulletCollider = new MathClasses.AABB(new MathClasses.Vector3(0, 0, 0), new MathClasses.Vector3(0, 0, 0));

        //initiating player collider
        MathClasses.AABB playerCollider = new MathClasses.AABB(new MathClasses.Vector3(0, 0, 0), new MathClasses.Vector3(0, 0, 0));

        //initiating wall colliders
        MathClasses.AABB leftWall = new MathClasses.AABB(new MathClasses.Vector3(0, 0, 0), new MathClasses.Vector3(0, 900, 0));
        MathClasses.AABB rightWall = new MathClasses.AABB(new MathClasses.Vector3(1400, 0, 0), new MathClasses.Vector3(1400, 900, 0));
        MathClasses.AABB topWall = new MathClasses.AABB(new MathClasses.Vector3(0, 0, 0), new MathClasses.Vector3(1400, 0, 0));
        MathClasses.AABB bottomWall = new MathClasses.AABB(new MathClasses.Vector3(0, 900, 0), new MathClasses.Vector3(1400, 900, 0));


        MathClasses.AABB testbox = new MathClasses.AABB(new MathClasses.Vector3(700, 400, 0), new MathClasses.Vector3(1000, 700, 0));

        public void Init()
        {
            stopwatch.Start();
            lastTime = stopwatch.ElapsedMilliseconds;

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
                MoveForward();
            }
            //move player backwards on S pressed
            if (rl.Raylib.IsKeyDown(rl.KeyboardKey.KEY_S))
            {
                MoveBackwards();
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
                    if (bulletCollider.Overlaps(testbox))
                    {
                        Console.WriteLine("I AM WORKING!!!!");
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


            //check collision between players collider boxes
            PlayerCollision(leftWall);
            PlayerCollision(rightWall);
            PlayerCollision(topWall);
            PlayerCollision(bottomWall);
            PlayerCollision(testbox);


            lastTime = currentTime;
        }



        //player collisions with aabb boxes
        public void PlayerCollision(AABB collisionBox)
        {
            if (playerCollider.Overlaps(collisionBox) && (rl.Raylib.IsKeyDown(rl.KeyboardKey.KEY_W)))
            {
                MoveBackwards();
            }
            else if (playerCollider.Overlaps(collisionBox) && (rl.Raylib.IsKeyDown(rl.KeyboardKey.KEY_S)))
            {
                MoveForward();
            }
        }

        //move thank forward
        public void MoveForward()
        {
            Vector3 facing = new Vector3(
            tankObject.LocalTransform.m1,
            tankObject.LocalTransform.m2, 1) * deltaTime * 300;
            tankObject.Translate(facing.x, facing.y);
        }
        //move tank backwards
        public void MoveBackwards()
        {
            Vector3 facing = new Vector3(
            tankObject.LocalTransform.m1,
            tankObject.LocalTransform.m2, 1) * deltaTime * -300;
            tankObject.Translate(facing.x, facing.y);
        }

        public void Draw()
        {
            rl.Raylib.BeginDrawing();

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


            //debug drawing players collision box
            rl.Raylib.DrawRectangleLines(Convert.ToInt32(tankObject.globalTransform.m7 - tankSprite.Width / 2), Convert.ToInt32(tankObject.globalTransform.m8 - tankSprite.Height / 2), 83, 78, rl.Color.RED);
            //debug drawing test box
            rl.Raylib.DrawRectangleLines(700, 400, 300, 300, rl.Color.RED);

            rl.Raylib.EndDrawing();
        }

        public void Shutdown()
        {

        }
    }
}


