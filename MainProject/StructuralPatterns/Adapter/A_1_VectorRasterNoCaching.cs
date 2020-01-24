using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MoreLinq.Extensions;

namespace MainProject.StructuralPatterns.Adapter
{
    namespace A1VectorRasterNoCaching
    {
        class Point
        {
            public int X, Y;

            public Point(int x, int y)
            {
                X = x;
                Y = y;
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

        class LineToPointsAdapter : Collection<Point>
        {
            // just for diagnostic purpose
            private static int _callingCount;

            public LineToPointsAdapter(Line line)
            {
                // Console.WriteLine($"{++_callingCount}: Generating points for line [{line.Start.X},{line.Start.Y}]-[{line.End.X},{line.End.Y}] (no caching)");

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
                        Add(new Point(left, y));
                    }
                }

                // straight [horizontal] line
                else if (dy == 0)
                {
                    for (int x = left; x <= right; x++)
                    {
                        Add(new Point(x, top));
                    }
                }

                // diagonal line
                else
                {
                    // we will implement it later
                    // since we will not need it with drawing rectangle without rotation
                }

            }
        }

        public static class VectorRasterNoCaching
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
                // Console.SetCursorPosition(0,0);
            }

            static void Draw(char point = '8')
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
                Draw();
                Draw('#');
            }
        }
    }
}
