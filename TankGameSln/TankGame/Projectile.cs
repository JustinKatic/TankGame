using System;
using System.Collections.Generic;
using System.Text;
using Raylib;

namespace TankGame
{
    class Projectile : SceneObject
    {
        //bullet speed
        float speed = 700;
        //variable to store direction into
        MathClasses.Vector3 direction = new MathClasses.Vector3(0, 0, 0);
       

        //get a x and y direction for bullet
        public Projectile(float xDirection, float yDirection)
        {
            direction.x = xDirection;
            direction.y = yDirection;         
        }

        // when update is called move bullet in the forward facing pos
        public override void OnUpdate(float deltaTime)
        {
            Vector3 facing = new Vector3(
                direction.x,
                direction.y, 1) * deltaTime * speed;
            Translate(facing.x, facing.y);
        }
        
        //draws a raylib circle and gives it a m7 and m8 pos
        public override void OnDraw()
        {                     
            Raylib.Raylib.DrawCircle((int)globalTransform.m7, (int)globalTransform.m8, 10, Color.RED);
        }
    }
}
