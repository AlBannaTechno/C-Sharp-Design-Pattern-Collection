using System;

namespace JustForFun
{
    public class CsTypes
    {
        
    }
    public struct Vec
    {
        public float x, y, z;
        public static implicit operator Vec(float v) {return new Vec(v,v,v);}

        public Vec(float a, float b, float c = 0)
        {
            x = a;
            y = b;
            z = c;
        }
        
        public static Vec operator +(Vec q, Vec r) {return new Vec(q.x+r.x, q.y+r.y, q.z+r.z);}
        public static Vec operator *(Vec q, Vec r) {return new Vec(q.x*r.x, q.y*r.y, q.z*r.z);}
        public static float operator %(Vec q, Vec r) {return q.x * r.x + q.y * r.y + q.z * r.z;}
        
        // intnv square root

        public static Vec operator !(Vec q)
        {
            return q * (1.0f/ (float) Math.Sqrt(q % q));
        }
        
    }
}