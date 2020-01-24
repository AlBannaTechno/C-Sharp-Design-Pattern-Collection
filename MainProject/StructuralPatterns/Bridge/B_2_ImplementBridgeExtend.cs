using System;
using System.Linq;
using Autofac;
using Autofac.Core;
using static System.Console;
namespace MainProject.CreationalPatterns.Bridge
{
    namespace B1ImplementBridgeExtend
    {
        /**
         * Note :
         * Implementing System like this is very dangerous and need to be very careful
         * when working with and may leeds to enormous complexity
         * Also this may violate Single Responsibility Principle
         *
         *  Another solution at : B3SolveTheProblemWithDifferentSolution
         */
        #region Render
        interface IRender
        {
            void RenderCircle(float radius);
            void RenderRectangle(float width, float height);
        }

        class VectorRender : IRender
        {
            public void RenderCircle(float radius)
            {
                WriteLine($"Drawing a Circle[Vector] With {radius} Radius");
            }

            public void RenderRectangle(float width, float height)
            {
                WriteLine($"Drawing a Rectangle[Vector] With : [Width : {width}, Height : {height}]");
            }
        }

        // pixel
        class RasterRender : IRender
        {
            public void RenderCircle(float radius)
            {
                WriteLine($"Drawing a Circle[Pixel] With {radius} Radius");
            }

            public void RenderRectangle(float width, float height)
            {
                WriteLine($"Drawing a Rectangle[Pixel] With : [Width : {width}, Height : {height}]");
            }
        }
        #endregion

        // [Bridge] build a bridge between shape and IRender
        // [Anti- Bridge] : Make shape decide how it will render [limit]
        abstract class Shape
        {
            protected  IRender Render;

            protected Shape(IRender render)
            {
                Render = render ?? throw new ArgumentNullException();
            }

            public abstract void Draw();
            public abstract void Resize(float factor);
        }

        class Circle : Shape
        {
            private float _radius;
            public Circle(IRender render, float radius) : base(render)
            {
                _radius = radius;
            }

            public override void Draw()
            {
                Render.RenderCircle(_radius);
            }

            public override void Resize(float factor)
            {
                _radius *= factor;
            }
        }


        class Rectangle : Shape
        {
            private float _width;
            private float _height;

            public Rectangle(IRender render, float width, float height) : base(render)
            {
                _width = width;
                _height = height;
            }

            public override void Draw()
            {
                Render.RenderRectangle(_width, _height);
            }

            public override void Resize(float factor)
            {
                _height *= factor;
                _width *= factor;
            }
        }
        public class Demo
        {
            public void Run()
            {

                var cb = new ContainerBuilder();
                cb.RegisterType<VectorRender>().As<IRender>();
                cb.Register(((context, parameters) =>
                    new Circle(context.Resolve<IRender>(), parameters.Named<float>("radius"))
                    ));
                cb.Register(((context, parameters) =>
                {
                    var enumerable = parameters as Parameter[] ?? parameters.ToArray();
                    return new Rectangle(context.Resolve<IRender>(), enumerable.Positional<float>(0),
                            enumerable.Positional<float>(1));
                }));
                using var c = cb.Build();
                var circle = c.Resolve<Circle>(new NamedParameter("radius", 2.3f));
                var rectangle = c.Resolve<Rectangle>(new PositionalParameter(0, 20.0f),new PositionalParameter(1, 30.0f));
                circle.Draw();
                circle.Resize(.2f);
                circle.Draw();

                rectangle.Draw();
                rectangle.Resize(0.333f);
                rectangle.Draw();

                /*
                // IRender render = new RasterRender();
                IRender render = new VectorRender();();
                var circle = new Circle(render, 10);
                circle.Draw();
                circle.Resize(3);
                circle.Draw();*/
            }
        }


    }
}
