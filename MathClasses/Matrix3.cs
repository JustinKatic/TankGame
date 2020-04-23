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
        {//identity matrix
            m1 = 1; m2 = 0; m3 = 0;
            m4 = 0; m5 = 1; m6 = 0;
            m7 = 0; m8 = 0; m9 = 1;
        }
        // matrix constructor
        public Matrix3(float _m1, float _m2, float _m3, float _m4, float _m5, float _m6, float _m7, float _m8, float _m9)
        {
            m1 = _m1; m2 = _m2; m3 = _m3;
            m4 = _m4; m5 = _m5; m6 = _m6;
            m7 = _m7; m8 = _m8; m9 = _m9;
        }

        //matrix set
        public void Set(float _m1, float _m2, float _m3, float _m4, float _m5, float _m6, float _m7, float _m8, float _m9)
        {
            m1 = _m1; m2 = _m2; m3 = _m3;
            m4 = _m4; m5 = _m5; m6 = _m6;
            m7 = _m7; m8 = _m8; m9 = _m9;
        }

        //matrix set
        public void Set(Matrix3 m)
        {
            m1 = m.m1; m2 = m.m2; m3 = m.m3;
            m4 = m.m4; m5 = m.m5; m6 = m.m6;
            m7 = m.m7; m8 = m.m8; m9 = m.m9;
        }

        //sets rotate to absoloute X position
        public void SetRotateX(double radians)
        {
            Set(1, 0, 0,
            0, (float)Math.Cos(radians), (float)Math.Sin(radians),
            0, (float)-Math.Sin(radians), (float)Math.Cos(radians));
        }
        //sets rotate to absoloute Y position
        public void SetRotateY(double radians)
        {
            Set((float)Math.Cos(radians), 0, (float)-Math.Sin(radians),
            0, 1, 0,
            (float)Math.Sin(radians), 0, (float)Math.Cos(radians));
        }
        //sets rotate to absoloute Z position
        public void SetRotateZ(double radians)
        {
            Set((float)Math.Cos(radians), (float)Math.Sin(radians), 0,
            (float)-Math.Sin(radians), (float)Math.Cos(radians), 0,
            0, 0, 1);
        }
        //Rotate around exisiting X axis
        public void RotateX(double radians)
        {
            Matrix3 m = new Matrix3();
            m.SetRotateX(radians);
            Set(this * m);
        }
        //Rotate around exisiting Y axis
        public void RotateY(double radians)
        {
            Matrix3 m = new Matrix3();
            m.SetRotateY(radians);
            Set(this * m);
        }
        //Rotate around exisiting Z axis
        public void RotateZ(double radians)
        {
            Matrix3 m = new Matrix3();
            m.SetRotateZ(radians);
            Set(this * m);
        }

        // matrix 3 multiplaction
        public static Matrix3 operator *(Matrix3 lhs, Matrix3 rhs)
        {
            return new Matrix3(
            rhs.m1 * lhs.m1 + rhs.m2 * lhs.m4 + rhs.m3 * lhs.m7,
            rhs.m1 * lhs.m2 + rhs.m2 * lhs.m5 + rhs.m3 * lhs.m8,
            rhs.m1 * lhs.m3 + rhs.m2 * lhs.m6 + rhs.m3 * lhs.m9,

            rhs.m4 * lhs.m1 + rhs.m5 * lhs.m4 + rhs.m6 * lhs.m7,
            rhs.m4 * lhs.m2 + rhs.m5 * lhs.m5 + rhs.m6 * lhs.m8,
            rhs.m4 * lhs.m3 + rhs.m5 * lhs.m6 + rhs.m6 * lhs.m9,

            rhs.m7 * lhs.m1 + rhs.m8 * lhs.m4 + rhs.m9 * lhs.m7,
            rhs.m7 * lhs.m2 + rhs.m8 * lhs.m5 + rhs.m9 * lhs.m8,
            rhs.m7 * lhs.m3 + rhs.m8 * lhs.m6 + rhs.m9 * lhs.m9);
        }

        //matrix3 * vector
        public static Vector3 operator *(Matrix3 lhs, Vector3 rhs)
        {
            return new Vector3
            ((rhs.x * lhs.m1) + (rhs.y * lhs.m4) + (rhs.z * lhs.m7),
            (rhs.x * lhs.m2) + (rhs.y * lhs.m5) + (rhs.z * lhs.m8),
            (rhs.x * lhs.m3) + (rhs.y * lhs.m6) + (rhs.z * lhs.m9));
        }

        //sets translate
        public void SetTranslation(float x, float y)
        {
            m7 = x; m8 = y; m9 = 1;
        }
        //increases the translate position in the X and Y by the amount enters in parameters
        public void Translate(float x, float y)
        {
            m7 += x; m8 += y; m9 += 1;
        }

        public Vector3 GetTranslation()
        {
            return new Vector3(m7, m8, m9);
        }

        public Matrix3 GetTransposed()
        {
            return new Matrix3(
                m1, m2, m3,
                m4, m5, m6,
                m7, m8, m9);
        }

        //setting scale values 
        public void SetScaled(float x, float y, float z)
        {
            m1 = x; m2 = 0; m3 = 0;
            m4 = 0; m5 = y; m6 = 0;
            m7 = 0; m8 = 0; m9 = z;
        }

        //increases the scale of transform in the X & Y axis 
        //and Z would be 0 working in 2d space
        public void Scale(float x, float y, float z)
        {
            Matrix3 m = new Matrix3();
            m.SetScaled(x, y, z);
            Set(this * m);
        }
    }
}
