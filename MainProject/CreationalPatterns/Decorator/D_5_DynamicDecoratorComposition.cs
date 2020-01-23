using System;
using static System.Console;
using MainProject.CreationalPatterns.Bridge.B1ImplementBridge;

namespace MainProject.CreationalPatterns.Decorator
{
    namespace D5DynamicDecoratorComposition
    {
        interface IShape
        {
            string AsString();
        }

        class Circle : IShape
        {
            private readonly float _radius;
            public Circle(float radius)
            {
                _radius = radius;
            }
            public string AsString() => $"Circle With Radius : {_radius}";
        }

        class Square : IShape
        {
            private readonly float _side;
            public Square(float side)
            {
                _side = side;
            }
            public string AsString() => $"Square With Side Length : {_side}";
        }

        class ColoredShape : IShape
        {
            private readonly IShape _shape;
            private readonly string _color;

            public ColoredShape(IShape shape, string color)
            {
                _shape = shape ?? throw new ArgumentNullException(nameof(shape));
                _color = color ?? throw new ArgumentNullException(nameof(color));
            }

            public string AsString() => $"{_shape.AsString()} has a color : {_color}";
        }

        class TransparentShape : IShape
        {
            private readonly float _transparency;
            private readonly IShape _shape;

            public TransparentShape(IShape shape, float transparency)
            {
                _transparency = transparency;
                _shape = shape;
            }

            public string AsString()
            {
                return $"{_shape.AsString()} , With {_transparency}% Transparency";
            }
        }
        public static class DynamicDecorator
        {
            /**
             * The problem With dynamic decorator , that we can apply dynamic[run-time] decorator at the same type
             * {itself} means we can apply ColoredShape on other ColoredShape , which not make sense!
             * And there is no way to prevent this behaviour without affect decorate dynamic features
             *
             */
            public static void Run()
            {

                var square = new Square(1.5f);
                WriteLine(square.AsString());
                var blackSquare = new ColoredShape(square, "Black");
                WriteLine(blackSquare.AsString());
                var transparentBlackSquare = new TransparentShape(blackSquare, 75.4f);
                WriteLine(transparentBlackSquare.AsString());
            }
        }
    }
}
