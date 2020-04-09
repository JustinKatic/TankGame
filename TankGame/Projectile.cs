using System;
using System.Collections.Generic;
using System.Text;
using Raylib;

namespace TankGame
{
    class Projectile : SceneObject
    {
        float speed = 700;
        MathClasses.Vector3 direction = new MathClasses.Vector3(0, 0, 0);

        

        public Projectile(float xDirection, float yDirection)
        {
            direction.x = xDirection;
            direction.y = yDirection;

            
        }


        public override void OnUpdate(float deltaTime)
        {
            Vector3 facing = new Vector3(
                direction.x,
                direction.y, 1) * deltaTime * speed;
            Translate(facing.x, facing.y);
        }

       
        public override void OnDraw()
        {                     
            Raylib.Raylib.DrawCircle((int)globalTransform.m7, (int)globalTransform.m8, 10, Color.RED);
        }
    }
}
