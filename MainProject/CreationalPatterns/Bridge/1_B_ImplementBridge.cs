using System;
using Autofac;
using static System.Console;
namespace MainProject.CreationalPatterns.Bridge
{
    namespace B1ImplementBridge
    {
        #region Render
        interface IRender
        {
            void RenderCircle(float radius);
        }

        class VectorRender : IRender
        {
            public void RenderCircle(float radius)
            {
                WriteLine($"Drawing a Circle[Vector] With {radius} Radius");
            }
        }

        // pixel
        class RasterRender : IRender
        {
            public void RenderCircle(float radius)
            {
                WriteLine($"Drawing a Circle[Pixel] With {radius} Radius");
            }
        }
        #endregion

        // [Bridge] build a bridge between shape and IRender
        // [Anti- Bridge] : Make shape decide how it will render [limit]
        abstract class Shape
        {
            protected  IRender Render;

            public Shape(IRender render)
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

        public class Demo
        {
            public void Run()
            {

                var cb = new ContainerBuilder();
                cb.RegisterType<VectorRender>().As<IRender>();
                cb.Register(((context, parameters) =>
                    new Circle(context.Resolve<IRender>(), parameters.Named<float>("radius"))
                    ));
                using var c = cb.Build();
                var circle = c.Resolve<Circle>(new NamedParameter("radius", 2.3f));
                circle.Draw();
                circle.Resize(.2f);
                circle.Draw();

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
