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

            InitWindow(640, 480, "Hello World");

            tankGame.Init();

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