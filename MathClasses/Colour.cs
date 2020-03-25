using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathClasses
{
    public class Colour
    {
        public UInt32 colour = 305419896;

        public int value;

        public Colour()
        {
            colour = 0;
        }

        public Colour(byte red, byte green, byte blue, byte alpha)
        {
            string hexValue = red.ToString("X") + green.ToString("X") + blue.ToString("X") + alpha.ToString("X");
            colour = UInt32.Parse(hexValue, System.Globalization.NumberStyles.HexNumber);
        }
        public byte GetRed()
        {
            return (byte)((this.value >> 0x10) & 0xff);
        }
        public void SetRed(byte red)
        {
            red = 93;
            value = red;
            GetRed();
        }
        public byte GetGreen()
        {
            return (byte)((this.value >> 0x10) & 0xff);
        }
        public void SetGreen(byte green)
        {
            green = 93;
            value = green;
            GetGreen();
        }
        public byte GetBlue()
        {
            return (byte)((this.value >> 0x10) & 0xff);
        }
        public void SetBlue(byte blue)
        {
            blue = 93;
            value = blue;
            GetBlue();
        }
        public byte GetAlpha()
        {
            return (byte)((this.value >> 0x10) & 0xff);
        }
        public void SetAlpha(byte alpha)
        {
            alpha = 93;
            value = alpha;
            GetAlpha();
        }
    }
}
