using System;
using System.Collections.Generic;
using System.Text;
using Raylib;
using static Raylib.Raylib;

namespace TankGame
{
    class SpriteObject : SceneObject
    {
        Texture2D texture = new Texture2D();
        Image image = new Image();
        public float Width
        {
            get { return texture.width; }
        }
        public float Height
        {
            get { return texture.height; }
        }
        public SpriteObject()
        {

        }
        public void Load(string filename)
        {
            image = LoadImage(filename);
            texture = LoadTextureFromImage(image);
        }
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
