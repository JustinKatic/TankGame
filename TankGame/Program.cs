using System;
using MathClasses;
using rl = Raylib;
using Raylib;
using static Raylib.Raylib;

namespace TankGame
{
    class Program
    {
        static void Main(string[] args)
        {
            TankGame tankGame = new TankGame();
            //sets fps to 60 fps
            SetTargetFPS(60);
            //creates a screen 1400 width 900 height
            InitWindow(1400, 900, "Tank World");

            //calls init(function)2
            tankGame.Init();

            //calls update and draw after init and keeps looping through them
            while (!WindowShouldClose())
            {
                tankGame.Update();
                tankGame.Draw();
            }

            tankGame.Shutdown();

            CloseWindow();
        }
    }
}