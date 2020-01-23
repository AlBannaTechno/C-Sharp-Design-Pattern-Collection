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
    namespace D7StaticDecoratorCompositionWithInternalAccess
    {
        abstract class Shape
        {
            public abstract string AsString();
        }

        class Circle : Shape
        {
            public  float Radius;

            public Circle() : this(0.0f)
            {

            }
            public Circle(float radius)
            {
                Radius = radius;
            }
            public override string AsString() => $"Circle With Radius : {Radius}";
        }

        class Square : Shape
        {
            public  float Side;
            public Square(float side)
            {
                Side = side;
            }

            public Square() : this(0.0f)
            {

            }

            public override string AsString() => $"Square With Side Length : {Side}";
        }

        class ColoredShape<T> : Shape where T: Shape,new()
        {
            public T Shape = new T();
            public string Color;

            // we still need empty constructor because of { Shape where T: Shape,new()}
            public ColoredShape() : this("Black")
            {

            }
            public ColoredShape(string color)
            {
                Color = color ?? throw new ArgumentNullException(nameof(color));
            }

            public override string AsString() => $"{Shape.AsString()} has a color : {Color}";
        }

        class TransparentShape<T> : Shape where T: Shape,new()
        {
            public float Transparency;
            public T Shape = new T();

            public TransparentShape(): this(0.0f)
            {

            }
            public TransparentShape(float transparency)
            {
                Transparency = transparency;
            }

            public override string AsString() => $"{Shape.AsString()} , With {Transparency}% Transparency";
        }


        public static class StaticDecoratorWithInternalAccess
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

                // we can not access nested properties with casting to lower version of class
                // since no inheritance here , but we can still access every thing in smoth
                // way if we make it public
                blackTransparentCircle.Shape.Color = "Green";
                blackTransparentCircle.Shape.Shape.Radius = 23.0f;
                WriteLine(blackTransparentCircle.AsString());

            }
        }
    }
}
