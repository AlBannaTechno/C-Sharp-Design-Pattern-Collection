using static System.Console;
namespace MainProject.SOLID_PRINCIPLES
{
    
    // we should always able to upcast to the base type and behaviour should still the same
    namespace Solve
    {
        public static class LiskovSubstitution
        {
            public static void Run()
            {
                WriteLine("START __ [SOLID] [LiskovSubstitution]");
                int Area(Rectangle rect)
                {
                    return rect.Width * rect.Height;
                }
            
                Rectangle rect = new Rectangle(10,5);
                Rectangle square = new Square(); // upcasting
                square.Width = 10;
                WriteLine($"React {rect} : Has Area = {Area(rect)}");
                WriteLine($"Square {square} : Has Area = {Area(square)}");
            }
        }
    
        #region Base Classes

        class Rectangle
        {
            public virtual int Width { get; set; }
            public virtual int Height { get; set; }

            // To allow inheritance without needing to re define constructor
            public Rectangle()
            {
            }

            public Rectangle(int width, int height)
            {
                Width = width;
                Height = height;
            }

            public override string ToString()
            {
                return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
            }
        }

        class Square: Rectangle
        {
            public override int Width
            {
                set => base.Width = base.Height = value;
            }

            public override int Height
            {
                set => base.Width = base.Height = value;
            }
        }

        #endregion
    }
 
}