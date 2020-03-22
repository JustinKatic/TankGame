using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathClasses
{
    public class Matrix4
    {
        public float m1, m2, m3, m4, m5, m6, m7, m8, m9, m10, m11, m12, m13, m14, m15, m16;

        public Matrix4()
        {
            m1 = 1; m2 = 0; m3 = 0; m4 = 0;
            m5 = 0; m6 = 1; m7 = 0; m8 = 0;
            m9 = 0; m10 = 0; m11 = 1; m12 = 0;
            m13 = 0; m14 = 0; m15 = 0; m16 = 1;
        }
        public Matrix4(float _m1, float _m2, float _m3, float _m4, float _m5, float _m6, float _m7, float _m8, float _m9, float _m10, float _m11, float _m12, float _m13, float _m14, float _m15, float _m16)
        {
            m1 = _m1;
            m2 = _m2;
            m3 = _m3;
            m4 = _m4;
            m5 = _m5;
            m6 = _m6;
            m7 = _m7;
            m8 = _m8;
            m9 = _m9;
            m10 = _m10;
            m11 = _m11;
            m12 = _m12;
            m13 = _m13;
            m14 = _m14;
            m15 = _m15;
            m16 = _m16;
        }

        // matrix 3 multiplaction
        public static Matrix4 operator *(Matrix4 lhs, Matrix4 rhs)
        {
            return new Matrix4
           (lhs.m1 * rhs.m1 + lhs.m2 * rhs.m5 + lhs.m3 * rhs.m9 + lhs.m4 * rhs.m13,
            lhs.m1 * rhs.m2 + lhs.m2 * rhs.m6 + lhs.m3 * rhs.m10 + lhs.m4 * rhs.m14,
            lhs.m1 * rhs.m3 + lhs.m2 * rhs.m7 + lhs.m3 * rhs.m11 + lhs.m4 * rhs.m15,
            lhs.m1 * rhs.m4 + lhs.m2 * rhs.m8 + lhs.m3 * rhs.m12 + lhs.m4 * rhs.m16,

            lhs.m5 * rhs.m1 + lhs.m6 * rhs.m5 + lhs.m7 * rhs.m9 + lhs.m8 * rhs.m13,
            lhs.m5 * rhs.m2 + lhs.m6 * rhs.m6 + lhs.m7 * rhs.m10 + lhs.m8 * rhs.m14,
            lhs.m5 * rhs.m3 + lhs.m6 * rhs.m7 + lhs.m7 * rhs.m11 + lhs.m8 * rhs.m15,
            lhs.m5 * rhs.m4 + lhs.m6 * rhs.m8 + lhs.m7 * rhs.m12 + lhs.m8 * rhs.m16,

            lhs.m9 * rhs.m1 + lhs.m10 * rhs.m5 + lhs.m11 * rhs.m9 + lhs.m12 * rhs.m13,
            lhs.m9 * rhs.m2 + lhs.m10 * rhs.m6 + lhs.m11 * rhs.m10 + lhs.m12 * rhs.m14,
            lhs.m9 * rhs.m3 + lhs.m10 * rhs.m7 + lhs.m11 * rhs.m11 + lhs.m12 * rhs.m15,
            lhs.m9 * rhs.m4 + lhs.m10 * rhs.m8 + lhs.m11 * rhs.m12 + lhs.m12 * rhs.m16,

            lhs.m13 * rhs.m1 + lhs.m14 * rhs.m5 + lhs.m15 * rhs.m9 + lhs.m16 * rhs.m13,
            lhs.m13 * rhs.m2 + lhs.m14 * rhs.m6 + lhs.m15 * rhs.m10 + lhs.m16 * rhs.m14,
            lhs.m13 * rhs.m3 + lhs.m14 * rhs.m7 + lhs.m15 * rhs.m11 + lhs.m16 * rhs.m15,
            lhs.m13 * rhs.m4 + lhs.m14 * rhs.m8 + lhs.m15 * rhs.m12 + lhs.m16 * rhs.m16);
        }
        public void SetScaled(float x, float y, float z, float w)
        {
            m1 = x; m2 = 0; m3 = 0; m4 = 0;
            m5 = 0; m6 = y; m7 = 0; m8 = 0;
            m9 = 0; m10 = 0; m11 = z; m12 = 0;
            m13 = 0; m14 = 0; m15 = 0; m16 = w;
        }
    }
}

