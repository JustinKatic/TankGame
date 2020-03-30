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

        private float timer = 0;
        private int fps = 1;
        private int frames;
        private float deltaTime = 0.005f;

        SceneObject tankObject = new SceneObject();
        SceneObject turretObject = new SceneObject();

        SpriteObject tankSprite = new SpriteObject();
        SpriteObject turretSprite = new SpriteObject();

        private List<SpriteObject> Bullets = new List<SpriteObject>();



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
            tankObject.SetPosition(rl.Raylib.GetScreenWidth() / 2.0f, rl.Raylib.GetScreenHeight() / 2.0f);

        }

        public void Update()
        {
            currentTime = stopwatch.ElapsedMilliseconds;
            deltaTime = (currentTime - lastTime) / 1000.0f;

            timer += deltaTime;
            if (timer >= 1)
            {
                fps = frames;
                frames = 0;
                timer -= 1;
            }
            frames++;


            if (rl.Raylib.IsKeyDown(rl.KeyboardKey.KEY_A))
            {
                tankObject.Rotate(-deltaTime * 5);
            }
            if (rl.Raylib.IsKeyDown(rl.KeyboardKey.KEY_D))
            {
                tankObject.Rotate(deltaTime * 5);
            }

            if (rl.Raylib.IsKeyDown(rl.KeyboardKey.KEY_W))
            {
                Vector3 facing = new Vector3(
               tankObject.LocalTransform.m1,
               tankObject.LocalTransform.m2, 1) * deltaTime * 300;
                tankObject.Translate(facing.x, facing.y);
            }
            if (rl.Raylib.IsKeyDown(rl.KeyboardKey.KEY_S))
            {
                Vector3 facing = new Vector3(
               tankObject.LocalTransform.m1,
               tankObject.LocalTransform.m2, 1) * deltaTime * -300;
                tankObject.Translate(facing.x, facing.y);
            }

            if (rl.Raylib.IsKeyDown(rl.KeyboardKey.KEY_Q))
            {
                turretObject.Rotate(-deltaTime * 2);
            }
            if (rl.Raylib.IsKeyDown(rl.KeyboardKey.KEY_E))
            {
                turretObject.Rotate(deltaTime * 2);
            }

            tankObject.Update(deltaTime);

            if (rl.Raylib.IsKeyPressed(rl.KeyboardKey.KEY_SPACE))
            {
                {
                    SpriteObject bullet = new SpriteObject();
                    Bullets.Add(bullet);
                    bullet.Load("../../Images/bulletBlue.png");

                    bullet.localTransform = tankObject.GetChild(1).GlobalTransform.GetTransposed();


                    Vector3 length = new Vector3(
                        bullet.LocalTransform.m1,
                        bullet.LocalTransform.m2, 1) * ((tankSprite.Width / 2f) - 5f);

                    Vector3 height = new Vector3(
                        bullet.LocalTransform.m4,
                        bullet.LocalTransform.m5, 1) * -((tankSprite.Height / 6f) - 3f);

                    bullet.Translate(length.x, length.y);
                    bullet.Translate(height.x, height.y);

                }
            }

            foreach (SpriteObject bullet in Bullets)
            {
                if (bullet != null)
                {
                    Vector3 facing = new Vector3(
                        bullet.LocalTransform.m1,
                        bullet.LocalTransform.m2, 1) * deltaTime * 700;
                    bullet.Translate(facing.x, facing.y);
                }
            }





            lastTime = currentTime;
        }

        public void Draw()
        {

            rl.Raylib.BeginDrawing();
            rl.Raylib.ClearBackground(rl.Color.BLACK);

            rl.Raylib.DrawText(fps.ToString(), 10, 10, 30, rl.Color.RED);

            foreach (SceneObject bullet in Bullets)
            {
                bullet.Draw();
            }

            tankObject.Draw();
            

            rl.Raylib.EndDrawing();
        }

        public void Shutdown()
        {

        }
    }
}


