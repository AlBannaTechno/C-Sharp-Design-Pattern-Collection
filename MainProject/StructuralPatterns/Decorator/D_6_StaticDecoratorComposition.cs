using System;
using static System.Console;
using MainProject.CreationalPatterns.Bridge.B1ImplementBridge;

namespace MainProject.CreationalPatterns.Decorator
{
    /*
     * Notice : C# unlike C++ , does not support CRTP : https://stackoverflow.com/questions/10939907/how-to-write-a-good-curiously-recurring-template-pattern-crtp-in-c-sharp
     * Which will prevent us from implementing static decorator in C#
     * But in this file we will try to reach the maximum edge of C#.
     */
    namespace D6StaticDecoratorComposition
    {
        abstract class Shape
        {
            public abstract string AsString();
        }

        class Circle : Shape
        {
            private readonly float _radius;

            public Circle() : this(0.0f)
            {

            }
            public Circle(float radius)
            {
                _radius = radius;
            }
            public override string AsString() => $"Circle With Radius : {_radius}";
        }

        class Square : Shape
        {
            private readonly float _side;
            public Square(float side)
            {
                _side = side;
            }

            public Square() : this(0.0f)
            {

            }

            public override string AsString() => $"Square With Side Length : {_side}";
        }

        class ColoredShape<T> : Shape where T: Shape,new()
        {
            private readonly T _shape = new T();
            private readonly string _color;

            // we still need empty constructor because of { Shape where T: Shape,new()}
            public ColoredShape() : this("Black")
            {

            }
            public ColoredShape(string color)
            {
                _color = color ?? throw new ArgumentNullException(nameof(color));
            }

            public override string AsString() => $"{_shape.AsString()} has a color : {_color}";
        }

        class TransparentShape<T> : Shape where T: Shape,new()
        {
            private readonly float _transparency;
            private readonly T _shape = new T();

            public TransparentShape(): this(0.0f)
            {

            }
            public TransparentShape(float transparency)
            {
                _transparency = transparency;
            }

            public override string AsString() => $"{_shape.AsString()} , With {_transparency}% Transparency";
        }


        public static class StaticDecorator
        {
            /**
             * The problem With dynamic decorator , that we can apply dynamic[run-time] decorator at the same type
             * {itself} means we can apply ColoredShape on other ColoredShape , which not make sense!
             * And there is no way to prevent this behaviour without affect decorate dynamic features
             *
             */
            public static void Run()
            {
                // there is no constructor parameters forwarding in C# , unlike C++
                // so we can not pass the side of the redSquare to ColoredShape constructor
                // i know, With complex properties passing solution we can solve this problem
                // but this will leads to very complex behaviours , so No.
                var redSquare = new ColoredShape<Square>("red");
                WriteLine(redSquare.AsString());

                // static decorator with composition
                var blackTransparentCircle = new TransparentShape<ColoredShape<Circle>>(30.2f);
                // No we can not change circle radius or ColoredShape Color
                // But There is a Solution in
                // D_7_StaticDecoratorCompositionWithInternalAccess.cs
                WriteLine(blackTransparentCircle.AsString());
                // Just notice with static decorator composition solution in C# we ends with instances hell
                // with the fact that we must make every thing public, [review: {D_7_StaticDecoratorCompositionWithInternalAccess.cs}]
                // To Allow access fields , so remember: here the cost of instantiate any object is very high
                // since we does not use inheritance , So Just Don't Use It With C#
            }
        }
    }
}
