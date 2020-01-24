using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MoreLinq.Extensions;

namespace MainProject.StructuralPatterns.Adapter
{
    namespace A2VectorRasterWithCaching
    {
        class Point
        {
            public readonly int X, Y;

            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }

            protected bool Equals(Point other)
            {
                return X == other.X && Y == other.Y;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((Point) obj);
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(X, Y);
            }
        }

        class Line
        {
            public Point Start, End;

            public Line(Point start, Point end)
            {
                Start = start ?? throw new ArgumentNullException(nameof(start));
                End = end ?? throw new ArgumentNullException(nameof(end));
            }

            protected bool Equals(Line other)
            {
                return Start.Equals(other.Start) && End.Equals(other.End);
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((Line) obj);
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Start, End);
            }
        }

        class VectorObject : Collection<Line>
        {

        }

        class VectorRectangle : VectorObject
        {
            public VectorRectangle(int x, int y, int width, int height)
            {
                Add(new Line(new Point(x, y), new Point(x + width, y)));
                Add(new Line(new Point(x + width, y), new Point(x + width, y + height)));
                Add(new Line(new Point(x + width, y + height), new Point(x, y + height)));
                Add(new Line(new Point(x, y + height), new Point(x, y)));
            }
        }

        class LineToPointsAdapter : IEnumerable<Point>
        {
            // just for diagnostic purpose
            private static int _callingCount;
            private static int _printingCount;
            public static int CallingCount => _callingCount;
            public static int PrintingCount => _printingCount;

            private static readonly Dictionary<int, List<Point>> Cache = new Dictionary<int, List<Point>>();


            public LineToPointsAdapter(Line line)
            {
                // Console.WriteLine($"{++_callingCount}: Generating points for line [{line.Start.X},{line.Start.Y}]-[{line.End.X},{line.End.Y}] (no caching)");
                _callingCount++;
                var hash = line.GetHashCode();
                if (Cache.ContainsKey(hash)) return;
                _printingCount++;
                var points = new List<Point>();

                int left = Math.Min(line.Start.X, line.End.X);
                int right = Math.Max(line.Start.X, line.End.X);
                int top = Math.Min(line.Start.Y, line.End.Y);
                int bottom = Math.Max(line.Start.Y, line.End.Y);
                int dx = right - left;
                int dy = line.End.Y - line.Start.Y;

                // straight [vertical] line
                if (dx == 0)
                {
                    for (int y = top; y <= bottom; y++)
                    {
                        points.Add(new Point(left, y));
                    }
                }

                // straight [horizontal] line
                else if (dy == 0)
                {
                    for (int x = left; x <= right; x++)
                    {
                        points.Add(new Point(x, top));
                    }
                }

                // diagonal line
                else
                {
                    // we will implement it later
                    // since we will not need it with drawing rectangle without rotation
                }

                Cache.Add(hash, points);

            }

            public IEnumerator<Point> GetEnumerator()
            {
                // to flat lists into single list `SelectMany Functional Programming`
                return Cache.Values.SelectMany(x => x).GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        public static class VectorRasterWithCaching
        {
            // we need an adapter to print vector objects so we need it to adapt vectorObject to be used in/with
            // DrawPoint, so we need to convert it to points
            //
            private static readonly List<VectorObject> vectorObjects = new List<VectorObject>()
            {
                new VectorRectangle(0,0,5,5),
                new VectorRectangle(2,2,6,7)
            };


            // The only functionality available , is draw a point
            static void DrawPoint(Point point, char pointCharacter = '*')
            {
                // we multiply : by 3 because we print in console , so width `horizontal space` is compact
                // compared to vertical `new line`
                // so we do that to make it equal as possible
                Console.SetCursorPosition(point.X * 3,point.Y);
                Console.Write(pointCharacter);
                Console.SetCursorPosition(0, point.Y + 2); // next 2 lines
            }

            static void Draw(char point = '*')
            {
                foreach (var vectorObject in vectorObjects)
                {
                    foreach (var line in vectorObject)
                    {
                        var adapter = new LineToPointsAdapter(line);
                        adapter.ForEach(p => DrawPoint(p, point));
                    }
                }
            }
            public static void Run()
            {
                Console.Clear();
                // notice if we use default Draw() , `* character`
                // and call Draw() twice or more , the function will work and re draw every thing
                // but this is  unnecessary , but we can not prevent this because we does not use caching
                // until now

                // notice : when executing next two lines ,
                // the values stored in Cash object will be 8 not 16
                // but we still will override drawing shapr from '*' to '#'
                // we can prevent this by adding static property to LineToPointsAdapter , to check last adding status
                // or latest length , but no need to do that
                // because we use caching only to prevent additional operations/calculations
                // and in drawing we may need to draw shape over shape [layering] so we can not prevent this behaviour
                Draw();
                Draw('#');
                // Draw('#');
                Console.WriteLine(LineToPointsAdapter.CallingCount);
                Console.WriteLine(LineToPointsAdapter.PrintingCount);

                var point1 = new Point(0,5);
                var point2 = new Point(0,5);
                var line1 = new Line(point1, point2);
                var line2 = new Line(point1, point2);
                Console.WriteLine(point1.GetHashCode());
                Console.WriteLine(point2.GetHashCode());
                Console.WriteLine(line1.GetHashCode());
                Console.WriteLine(line2.GetHashCode());
            }
        }
    }
}
