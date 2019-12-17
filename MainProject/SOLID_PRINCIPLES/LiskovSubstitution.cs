using static System.Console;
namespace MainProject.SOLID_PRINCIPLES
{
    public class LiskovSubstitution
    {
        public static void Run()
        {
            WriteLine("START __ [SOLID] [LiskovSubstitution]");
            int Area(Rectangle rect)
            {
                return rect.Width * rect.Height;
            }
            
            Rectangle rect = new Rectangle(10,5);
            Rectangle square = new Square();
            square.Width = 10;
            WriteLine($"React {rect} : Has Area = {Area(rect)}");
            WriteLine($"Square {square} : Has Area = {Area(square)}");
        }
    }
    
    #region Base Classes

    class Rectangle
    {
        public int Width { get; set; }
        public int Height { get; set; }

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
        public new int Width
        {
            set => base.Width = base.Height = value;
        }

        public new int Height
        {
            set => base.Width = base.Height = value;
        }
    }

    #endregion
}