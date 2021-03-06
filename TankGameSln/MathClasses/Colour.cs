﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathClasses
{
    public class Colour
    {
        public UInt32 colour;

        public Colour()
        {
            colour = 0;            
        }

        public Colour(byte red, byte green, byte blue, byte alpha)
        {
            colour |= (UInt32)red << 24;
            colour |= (UInt32)green << 16;
            colour |= (UInt32)blue << 8;
            colour |= (UInt32)alpha << 0;
        }

        public void SetRed(byte red)
        {
            colour = colour & 0x00ffffff; //clears red component to 0
            colour |= (UInt32)red << 24;  //moves final GBA value into colour variable
        }
        public byte GetRed()
        {
            return (byte)((colour & 0xff000000) >> 24);  //variable only contain red component value of colour
        }
        public byte GetGreen()
        {
            return (byte)((colour & 0x00ff0000) >> 16);
        }
        public void SetGreen(byte green)
        {
            colour = colour & 0xff00ffff;
            colour |= (UInt32)green << 16;
        }
        public byte GetBlue()
        {
            return (byte)((colour & 0x0000ff00) >> 8);
        }
        public void SetBlue(byte blue)
        {
            colour = colour & 0xffff00ff;
            colour |= (UInt32)blue << 8;
        }
        public byte GetAlpha()
        {
            return (byte)((colour & 0x000000ff) >> 0);
        }
        public void SetAlpha(byte alpha)
        {
            colour = colour & 0xffffff00;
            colour |= (UInt32)alpha << 0;
        }

        public void ChangeRedToGreen()
        {         
            SetGreen(GetRed());
            SetRed(0);          
        }
    }
}
