using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathClasses
{
    public class Matrix3
    {
        public float m1, m2, m3, m4, m5, m6, m7, m8, m9;

        public Matrix3()
        {
            m1 = 1; m2 = 0; m3 = 0;
            m4 = 0; m5 = 1; m6 = 0;
            m7 = 0; m8 = 0; m9 = 1;
        }

        public Matrix3(float _m1, float _m2, float _m3, float _m4, float _m5, float _m6, float _m7, float _m8, float _m9)
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
        }

        // matrix 3 multiplaction
        public static Matrix3 operator *(Matrix3 lhs, Matrix3 rhs)
        {
            return new Matrix3
           (lhs.m1 * rhs.m1 + lhs.m2 * rhs.m4 + lhs.m3 * rhs.m7,
            lhs.m1 * rhs.m2 + lhs.m2 * rhs.m5 + lhs.m3 * rhs.m8,
            lhs.m1 * rhs.m3 + lhs.m2 * rhs.m6 + lhs.m3 * rhs.m9,

            lhs.m4 * rhs.m1 + lhs.m5 * rhs.m4 + lhs.m6 * rhs.m7,
            lhs.m4 * rhs.m2 + lhs.m5 * rhs.m5 + lhs.m6 * rhs.m8,
            lhs.m4 * rhs.m3 + lhs.m5 * rhs.m6 + lhs.m6 * rhs.m9,

            lhs.m7 * rhs.m1 + lhs.m8 * rhs.m4 + lhs.m9 * rhs.m7,
            lhs.m7 * rhs.m2 + lhs.m8 * rhs.m5 + lhs.m9 * rhs.m8,
            lhs.m7 * rhs.m3 + lhs.m8 * rhs.m6 + lhs.m9 * rhs.m9);
        }
        public void SetScaled(float x, float y, float z)
        {
            m1 = x; m2 = 0; m3 = 0;
            m4 = 0; m5 = y; m6 = 0;
            m7 = 0; m8 = 0; m9 = z;
        }

        public void Set(Matrix3 m)
        {
            m1 = 1; m2 = 0; m3 = 0;
            m4 = 0; m5 = 1; m6 = 0;
            m7 = 0; m8 = 0; m9 = 1;
        }

        public void Scale(float x, float y, float z)
        {
            Matrix3 m = new Matrix3();
            m.SetScaled(x, y, z);
            Set(this * m);
        }

        //public void SetRotateX(double radians)
        //{
        //    Set
        //       (1, 0, 0,
        //    0, (float)Math.Cos(radians), (float)Math.Sin(radians),
        //    0, (float)-Math.Sin(radians), (float)Math.Cos(radians));
        //}

    }
}
