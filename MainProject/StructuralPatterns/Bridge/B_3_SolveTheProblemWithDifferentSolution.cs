using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace MainProject.CreationalPatterns.Bridge
{
    namespace B3SolveTheProblemWithDifferentSolution
    {
        /*
         * Render : [Vector, Raster]
         * Shape ............................
         */

        struct Point
        {
            public float X;
            public float Y;
            public float Z;
            public float W;

            public static implicit operator Point((float x, float y , float z, float w) val)
            {
                return new Point() {X = val.x, Y = val.y, Z = val.z, W = val.w};
            }

        }
        abstract class Render
        {
            // Center of drawing [Axis Gizmo]
            protected (float x, float y, float z) Gizmo;
            protected Render((float x, float y , float z) gizmo = default)
            {
                Gizmo = gizmo;
            }
            public abstract void RenderUnit(float x = 0, float y = 0, float z = 0, float w = 1);
        }

        class VectorRender : Render
        {
            public VectorRender((float x, float y , float z) gizmo = default) : base(gizmo)
            {

            }

            public override void RenderUnit(float x = 0, float y = 0, float z = 0, float w = 1)
            {
                Console.Write($"Render Vector Unit : {x}, {y}, {z}, {w} ");
                Console.WriteLine($"With Gizmo : {Gizmo.x}, {Gizmo.y}, {Gizmo.z} ");
            }
        }

        class RasterRender : Render
        {
            public RasterRender((float x, float y , float z) gizmo = default) : base(gizmo)
            { }

            public override void RenderUnit(float x = 0, float y = 0, float z = 0, float w = 1)
            {
                Console.Write($"Render Raster Unit : {x}, {y}, {z}, {w} ");
                Console.WriteLine($"With Gizmo : {Gizmo.x}, {Gizmo.y}, {Gizmo.z} ");
            }
        }

        abstract class Shape
        {
            protected readonly Render Render;

            protected Shape(Render render)
            {
                Render = render;
            }

            public abstract void Draw();
            public abstract void Scale(float factor);
        }

        class Circle : Shape
        {
            private readonly float _radius;

            public Circle(Render render, float radius) : base(render)
            {
                _radius = radius;
            }

            public override void Draw()
            {
                var permiter = 3.14 * 2 * _radius;
                // do any calculations
                var pointCollection = new List<(float x, float y, float z, float w)> {(_radius, 0, 0.0f, 1)};
                // ....... add many points
                foreach (var point in pointCollection)
                {
                    Render.RenderUnit(point.x, point.y, 0, 0);
                }
            }

            public override void Scale(float factor)
            {
                // do stuff
            }
        }

        public class Demo
        {
            public static void Run()
            {
                var circle = new Circle(new RasterRender(), 5.0f);
                circle.Scale(0.3f);
                circle.Draw();
            }
        }
    }
}
