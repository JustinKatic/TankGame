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
            m1 = _m1; m2 = _m2; m3 = _m3; m4 = _m4;
            m5 = _m5; m6 = _m6; m7 = _m7; m8 = _m8;
            m9 = _m9; m10 = _m10; m11 = _m11; m12 = _m12;
            m13 = _m13; m14 = _m14; m15 = _m15; m16 = _m16;
        }

        public void Set(float _m1, float _m2, float _m3, float _m4, float _m5, float _m6, float _m7, float _m8, float _m9, float _m10, float _m11, float _m12, float _m13, float _m14, float _m15, float _m16)
        {
            m1 = _m1; m2 = _m2; m3 = _m3; m4 = _m4;
            m5 = _m5; m6 = _m6; m7 = _m7; m8 = _m8;
            m9 = _m9; m10 = _m10; m11 = _m11; m12 = _m12;
            m13 = _m13; m14 = _m14; m15 = _m15; m16 = _m16;
        }

        public void SetRotateX(double radians)
        {
            Set(
            1, 0, 0, 0,
            0, (float)Math.Cos(radians), (float)Math.Sin(radians), 0,
            0, (float)-Math.Sin(radians), (float)Math.Cos(radians), 0,
            0, 0, 0, 1);
        }
        public void SetRotateY(double radians)
        {
            Set(
            (float)Math.Cos(radians), 0, (float)-Math.Sin(radians), 0,
            0, 1, 0, 0,
            (float)Math.Sin(radians), 0, (float)Math.Cos(radians), 0,
            0, 0, 0, 1);
        }
        public void SetRotateZ(double radians)
        {
            Set(
            (float)Math.Cos(radians), (float)Math.Sin(radians), 0, 0,
            (float)-Math.Sin(radians), (float)Math.Cos(radians), 0, 0,
            0, 0, 1, 0,
            0, 0, 0, 1);
        }

        // matrix 3 multiplaction
        public static Matrix4 operator *(Matrix4 lhs, Matrix4 rhs)
        {
            return new Matrix4
           (rhs.m1 * lhs.m1 + rhs.m2 * lhs.m5 + rhs.m3 * lhs.m9 + rhs.m4 * lhs.m13,
            rhs.m1 * lhs.m2 + rhs.m2 * lhs.m6 + rhs.m3 * lhs.m10 + rhs.m4 * lhs.m14,
            rhs.m1 * lhs.m3 + rhs.m2 * lhs.m7 + rhs.m3 * lhs.m11 + rhs.m4 * lhs.m15,
            rhs.m1 * lhs.m4 + rhs.m2 * lhs.m8 + rhs.m3 * lhs.m12 + rhs.m4 * lhs.m16,

            rhs.m5 * lhs.m1 + rhs.m6 * lhs.m5 + rhs.m7 * lhs.m9 + rhs.m8 * lhs.m13,
            rhs.m5 * lhs.m2 + rhs.m6 * lhs.m6 + rhs.m7 * lhs.m10 + rhs.m8 * lhs.m14,
            rhs.m5 * lhs.m3 + rhs.m6 * lhs.m7 + rhs.m7 * lhs.m11 + rhs.m8 * lhs.m15,
            rhs.m5 * lhs.m4 + rhs.m6 * lhs.m8 + rhs.m7 * lhs.m12 + rhs.m8 * lhs.m16,

            rhs.m9 * lhs.m1 + rhs.m10 * lhs.m5 + rhs.m11 * lhs.m9 + rhs.m12 * lhs.m13,
            rhs.m9 * lhs.m2 + rhs.m10 * lhs.m6 + rhs.m11 * lhs.m10 + rhs.m12 * lhs.m14,
            rhs.m9 * lhs.m3 + rhs.m10 * lhs.m7 + rhs.m11 * lhs.m11 + rhs.m12 * lhs.m15,
            rhs.m9 * lhs.m4 + rhs.m10 * lhs.m8 + rhs.m11 * lhs.m12 + rhs.m12 * lhs.m16,

            rhs.m13 * lhs.m1 + rhs.m14 * lhs.m5 + rhs.m15 * lhs.m9 + rhs.m16 * lhs.m13,
            rhs.m13 * lhs.m2 + rhs.m14 * lhs.m6 + rhs.m15 * lhs.m10 + rhs.m16 * lhs.m14,
            rhs.m13 * lhs.m3 + rhs.m14 * lhs.m7 + rhs.m15 * lhs.m11 + rhs.m16 * lhs.m15,
            rhs.m13 * lhs.m4 + rhs.m14 * lhs.m8 + rhs.m15 * lhs.m12 + rhs.m16 * lhs.m16);
        }
        public static Vector4 operator *(Matrix4 lhs, Vector4 rhs)
        {//row major
            return new Vector4((rhs.x * lhs.m1) + (rhs.y * lhs.m5) + (rhs.z * lhs.m9) + (rhs.w * lhs.m13),
            (rhs.x * lhs.m2) + (rhs.y * lhs.m6) + (rhs.z * lhs.m10) + (rhs.w * lhs.m14),
            (rhs.x * lhs.m3) + (rhs.y * lhs.m7) + (rhs.z * lhs.m11) + (rhs.w * lhs.m15),
            (rhs.x * lhs.m4) + (rhs.y * lhs.m8) + (rhs.z * lhs.m12) + (rhs.w * lhs.m16));

        }
    }
}

