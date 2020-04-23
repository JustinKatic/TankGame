using System;
using System.Collections.Generic;
using System.Text;
using Raylib;
using static Raylib.Raylib;

namespace TankGame
{
    class SpriteObject : SceneObject
    {      
       public Texture2D texture = new Texture2D();
       public Image image = new Image();
      
        //gets width of texture
        public float Width
        {
            get { return texture.width; }
        }
        //gets height of texture
        public float Height
        {
            get { return texture.height; }
        }

        //loading images and texture
        public void Load(string filename)
        {
            image = LoadImage(filename);
            texture = LoadTextureFromImage(image);
        }

        //override the OnDraw() method from the base class and in it we use the RayLib drawing
        //functions to draw a textured sprite using the texture member variable as well as the
        //globalTransform.This way the image will show up positioned and orientated based on the matrix.
        public override void OnDraw()
        {            
            float rotation = (float)Math.Atan2(
            globalTransform.m2, globalTransform.m1);
            Raylib.Raylib.DrawTextureEx(texture, new Vector2(globalTransform.m7, globalTransform.m8),
            rotation * (float)(180.0f / Math.PI),
            1, Color.WHITE);
        }
    }
}
