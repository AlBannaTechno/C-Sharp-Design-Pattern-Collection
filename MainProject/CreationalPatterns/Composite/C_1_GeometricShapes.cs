using System;
using System.Collections.Generic;
using System.Text;

namespace MainProject.CreationalPatterns.Composite
{
    namespace C1GeometricShapes
    {
        class GraphicObject
        {
            public virtual string Name { get; set; } = "Group";
            public string Color = string.Empty;
            private readonly Lazy<List<GraphicObject>> _children = new Lazy<List<GraphicObject>>();
            public List<GraphicObject> Children => _children.Value;

            private void Print(StringBuilder sb, int depth)
            {
                sb
                    .Append(depth == 0 ? string.Empty : new string('|', depth -1) +  '|' +  "-> ")
                    .Append(string.IsNullOrWhiteSpace(Color) ? string.Empty : $"{Color} ")
                    .AppendLine(Name);
                foreach (var child in Children)
                {
                    child.Print(sb, depth + 1);
                }

            }
            public override string ToString()
            {
                var sb = new StringBuilder();
                Print(sb, 0);
                return sb.ToString();
            }
        }

        class Circle : GraphicObject
        {
            public override string Name => "Circle";
        }

        class Square : GraphicObject
        {
            public override string Name => "Square";
        }

        public class CompositeGeoMetricDemo
        {
            public static void Run()
            {
                var root = new GraphicObject(){Name = "Root"};
                root.Children.Add(new Circle(){Color = "Red"});
                root.Children.Add(new Square(){Color = "Green"});
                var subSquareGroup = new Square(){Name = "1St Sub Square", Color = "Yellow"};
                var magentaCircle = new Circle() {Name = "MY Circle", Color = "Magenta"};
                var greenCircle = new Circle() {Name = "Circle", Color = "Green"};
                magentaCircle.Children.Add(new Circle(){Name = "Circle 33", Color = "Golden"});
                magentaCircle.Children.Add(new Circle(){Name = "Circle 22", Color = "Red"});
                magentaCircle.Children.Add(new Square(){Name = "Square 43", Color = "Green" , Children =
                {
                    new Circle(){Name = "Sub3 Circle", Color = "DarkBlue"},
                    new Square(){Name = "Sub3 Square 52", Color = "ShinyGreen"},
                }});
                magentaCircle.Children.Add(new Circle(){Name = "Circle 53", Color = "Yellow"});
                magentaCircle.Children.Add(new Circle(){Name = "Circle 34", Color = "DarkOrange"});
                subSquareGroup.Children.Add(magentaCircle);
                root.Children.Add(subSquareGroup);
                root.Children.Add(greenCircle);
                Console.WriteLine(root);
                /*
                 * Result
                    Root
                    |-> Red Circle
                    |-> Green Square
                    |-> Yellow Square
                    ||-> Magenta Circle
                    |||-> Golden Circle
                    |||-> Red Circle
                    |||-> Green Square
                    ||||-> DarkBlue Circle
                    ||||-> ShinyGreen Square
                    |||-> Yellow Circle
                    |||-> DarkOrange Circle
                    |-> Green Circle
                 */
                Console.WriteLine(subSquareGroup);
            }
        }
    }
}
