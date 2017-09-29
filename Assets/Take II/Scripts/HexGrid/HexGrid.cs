// Generated code -- http://www.redblobgames.com/grids/hexagons/

using System;
using System.Collections.Generic;

namespace Assets.Take_II.Scripts.HexGrid
{
    struct Point
    {
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }
        public readonly double X;
        public readonly double Y;
    }

    struct Hex
    {
        public Hex(int q, int r, int s)
        {
            Q = q;
            R = r;
            S = s;
        }
        public readonly int Q;
        public readonly int R;
        public readonly int S;

        public static Hex Add(Hex a, Hex b)
        {
            return new Hex(a.Q + b.Q, a.R + b.R, a.S + b.S);
        }


        public static Hex Subtract(Hex a, Hex b)
        {
            return new Hex(a.Q - b.Q, a.R - b.R, a.S - b.S);
        }


        public static Hex Scale(Hex a, int k)
        {
            return new Hex(a.Q * k, a.R * k, a.S * k);
        }

        public static List<Hex> Directions = new List<Hex> { new Hex(1, 0, -1), new Hex(1, -1, 0), new Hex(0, -1, 1), new Hex(-1, 0, 1), new Hex(-1, 1, 0), new Hex(0, 1, -1) };

        public static Hex Direction(int direction)
        {
            return Directions[direction];
        }


        public static Hex Neighbor(Hex hex, int direction)
        {
            return Add(hex, Direction(direction));
        }

        public static List<Hex> Diagonals = new List<Hex> { new Hex(2, -1, -1), new Hex(1, -2, 1), new Hex(-1, -1, 2), new Hex(-2, 1, 1), new Hex(-1, 2, -1), new Hex(1, 1, -2) };

        public static Hex DiagonalNeighbor(Hex hex, int direction)
        {
            return Add(hex, Diagonals[direction]);
        }


        public static int Length(Hex hex)
        {
            return (Math.Abs(hex.Q) + Math.Abs(hex.R) + Math.Abs(hex.S)) / 2;
        }


        public static int Distance(Hex a, Hex b)
        {
            return Length(Subtract(a, b));
        }

    }

    struct FractionalHex
    {
        public FractionalHex(double q, double r, double s)
        {
            Q = q;
            R = r;
            S = s;
        }
        public readonly double Q;
        public readonly double R;
        public readonly double S;

        public static Hex HexRound(FractionalHex h)
        {
            var q = (int)(Math.Round(h.Q));
            var r = (int)(Math.Round(h.R));
            var s = (int)(Math.Round(h.S));
            var qDiff = Math.Abs(q - h.Q);
            var rDiff = Math.Abs(r - h.R);
            var sDiff = Math.Abs(s - h.S);
            if (qDiff > rDiff && qDiff > sDiff)
            {
                q = -r - s;
            }
            else
            if (rDiff > sDiff)
            {
                r = -q - s;
            }
            else
            {
                s = -q - r;
            }
            return new Hex(q, r, s);
        }


        public static FractionalHex HexLerp(FractionalHex a, FractionalHex b, double t)
        {
            return new FractionalHex(a.Q * (1 - t) + b.Q * t, a.R * (1 - t) + b.R * t, a.S * (1 - t) + b.S * t);
        }


        public static List<Hex> HexLinedraw(Hex a, Hex b)
        {
            var n = Hex.Distance(a, b);
            var aNudge = new FractionalHex(a.Q + 0.000001, a.R + 0.000001, a.S - 0.000002);
            var bNudge = new FractionalHex(b.Q + 0.000001, b.R + 0.000001, b.S - 0.000002);
            var results = new List<Hex>();
            var step = 1.0 / Math.Max(n, 1);
            for (var i = 0; i <= n; i++)
            {
                results.Add(HexRound(HexLerp(aNudge, bNudge, step * i)));
            }
            return results;
        }

    }

    struct OffsetCoord
    {
        public OffsetCoord(int col, int row)
        {
            Col = col;
            Row = row;
        }
        public readonly int Col;
        public readonly int Row;
        public static int Even = 1;
        public static int Odd = -1;

        public static OffsetCoord QoffsetFromCube(int offset, Hex h)
        {
            var col = h.Q;
            var row = h.R + (h.Q + offset * (h.Q & 1)) / 2;
            return new OffsetCoord(col, row);
        }


        public static Hex QoffsetToCube(int offset, OffsetCoord h)
        {
            var q = h.Col;
            var r = h.Row - (h.Col + offset * (h.Col & 1)) / 2;
            var s = -q - r;
            return new Hex(q, r, s);
        }


        public static OffsetCoord RoffsetFromCube(int offset, Hex h)
        {
            var col = h.Q + (h.R + offset * (h.R & 1)) / 2;
            var row = h.R;
            return new OffsetCoord(col, row);
        }


        public static Hex RoffsetToCube(int offset, OffsetCoord h)
        {
            var q = h.Col - (h.Row + offset * (h.Row & 1)) / 2;
            var r = h.Row;
            var s = -q - r;
            return new Hex(q, r, s);
        }

    }

    struct Orientation
    {
        public Orientation(double f0, double f1, double f2, double f3, double b0, double b1, double b2, double b3, double startAngle)
        {
            F0 = f0;
            F1 = f1;
            F2 = f2;
            F3 = f3;
            B0 = b0;
            B1 = b1;
            B2 = b2;
            B3 = b3;
            StartAngle = startAngle;
        }
        public readonly double F0;
        public readonly double F1;
        public readonly double F2;
        public readonly double F3;
        public readonly double B0;
        public readonly double B1;
        public readonly double B2;
        public readonly double B3;
        public readonly double StartAngle;
    }

    struct Layout
    {
        public Layout(Orientation orientation, Point size, Point origin)
        {
            Orientation = orientation;
            Size = size;
            Origin = origin;
        }
        public readonly Orientation Orientation;
        public readonly Point Size;
        public readonly Point Origin;
        public static Orientation Pointy = new Orientation(Math.Sqrt(3.0), Math.Sqrt(3.0) / 2.0, 0.0, 3.0 / 2.0, Math.Sqrt(3.0) / 3.0, -1.0 / 3.0, 0.0, 2.0 / 3.0, 0.5);
        public static Orientation Flat = new Orientation(3.0 / 2.0, 0.0, Math.Sqrt(3.0) / 2.0, Math.Sqrt(3.0), 2.0 / 3.0, 0.0, -1.0 / 3.0, Math.Sqrt(3.0) / 3.0, 0.0);

        public static Point HexToPixel(Layout layout, Hex h)
        {
            var m = layout.Orientation;
            var size = layout.Size;
            var origin = layout.Origin;
            var x = (m.F0 * h.Q + m.F1 * h.R) * size.X;
            var y = (m.F2 * h.Q + m.F3 * h.R) * size.Y;
            return new Point(x + origin.X, y + origin.Y);
        }


        public static FractionalHex PixelToHex(Layout layout, Point p)
        {
            var m = layout.Orientation;
            var size = layout.Size;
            var origin = layout.Origin;
            var pt = new Point((p.X - origin.X) / size.X, (p.Y - origin.Y) / size.Y);
            var q = m.B0 * pt.X + m.B1 * pt.Y;
            var r = m.B2 * pt.X + m.B3 * pt.Y;
            return new FractionalHex(q, r, -q - r);
        }


        public static Point HexCornerOffset(Layout layout, int corner)
        {
            var m = layout.Orientation;
            var size = layout.Size;
            var angle = 2.0 * Math.PI * (m.StartAngle - corner) / 6;
            return new Point(size.X * Math.Cos(angle), size.Y * Math.Sin(angle));
        }


        public static List<Point> PolygonCorners(Layout layout, Hex h)
        {
            var corners = new List<Point>();
            var center = HexToPixel(layout, h);
            for (var i = 0; i < 6; i++)
            {
                var offset = HexCornerOffset(layout, i);
                corners.Add(new Point(center.X + offset.X, center.Y + offset.Y));
            }
            return corners;
        }

    }

// Tests

    struct Tests
    {

        public static void EqualHex(string name, Hex a, Hex b)
        {
            if (!(a.Q == b.Q && a.S == b.S && a.R == b.R))
            {
                Complain(name);
            }
        }


        public static void EqualOffsetcoord(string name, OffsetCoord a, OffsetCoord b)
        {
            if (!(a.Col == b.Col && a.Row == b.Row))
            {
                Complain(name);
            }
        }


        public static void EqualInt(string name, int a, int b)
        {
            if (a != b)
            {
                Complain(name);
            }
        }


        public static void EqualHexArray(string name, List<Hex> a, List<Hex> b)
        {
            EqualInt(name, a.Count, b.Count);
            for (var i = 0; i < a.Count; i++)
            {
                EqualHex(name, a[i], b[i]);
            }
        }


        public static void TestHexArithmetic()
        {
            EqualHex("hex_add", new Hex(4, -10, 6), Hex.Add(new Hex(1, -3, 2), new Hex(3, -7, 4)));
            EqualHex("hex_subtract", new Hex(-2, 4, -2), Hex.Subtract(new Hex(1, -3, 2), new Hex(3, -7, 4)));
        }


        public static void TestHexDirection()
        {
            EqualHex("hex_direction", new Hex(0, -1, 1), Hex.Direction(2));
        }


        public static void TestHexNeighbor()
        {
            EqualHex("hex_neighbor", new Hex(1, -3, 2), Hex.Neighbor(new Hex(1, -2, 1), 2));
        }


        public static void TestHexDiagonal()
        {
            EqualHex("hex_diagonal", new Hex(-1, -1, 2), Hex.DiagonalNeighbor(new Hex(1, -2, 1), 3));
        }


        public static void TestHexDistance()
        {
            EqualInt("hex_distance", 7, Hex.Distance(new Hex(3, -7, 4), new Hex(0, 0, 0)));
        }


        public static void TestHexRound()
        {
            var a = new FractionalHex(0, 0, 0);
            var b = new FractionalHex(1, -1, 0);
            var c = new FractionalHex(0, -1, 1);
            EqualHex("hex_round 1", new Hex(5, -10, 5), FractionalHex.HexRound(FractionalHex.HexLerp(new FractionalHex(0, 0, 0), new FractionalHex(10, -20, 10), 0.5)));
            EqualHex("hex_round 2", FractionalHex.HexRound(a), FractionalHex.HexRound(FractionalHex.HexLerp(a, b, 0.499)));
            EqualHex("hex_round 3", FractionalHex.HexRound(b), FractionalHex.HexRound(FractionalHex.HexLerp(a, b, 0.501)));
            EqualHex("hex_round 4", FractionalHex.HexRound(a), FractionalHex.HexRound(new FractionalHex(a.Q * 0.4 + b.Q * 0.3 + c.Q * 0.3, a.R * 0.4 + b.R * 0.3 + c.R * 0.3, a.S * 0.4 + b.S * 0.3 + c.S * 0.3)));
            EqualHex("hex_round 5", FractionalHex.HexRound(c), FractionalHex.HexRound(new FractionalHex(a.Q * 0.3 + b.Q * 0.3 + c.Q * 0.4, a.R * 0.3 + b.R * 0.3 + c.R * 0.4, a.S * 0.3 + b.S * 0.3 + c.S * 0.4)));
        }


        public static void TestHexLinedraw()
        {
            EqualHexArray("hex_linedraw", new List<Hex> { new Hex(0, 0, 0), new Hex(0, -1, 1), new Hex(0, -2, 2), new Hex(1, -3, 2), new Hex(1, -4, 3), new Hex(1, -5, 4) }, FractionalHex.HexLinedraw(new Hex(0, 0, 0), new Hex(1, -5, 4)));
        }


        public static void TestLayout()
        {
            var h = new Hex(3, 4, -7);
            var flat = new Layout(Layout.Flat, new Point(10, 15), new Point(35, 71));
            EqualHex("layout", h, FractionalHex.HexRound(Layout.PixelToHex(flat, Layout.HexToPixel(flat, h))));
            var pointy = new Layout(Layout.Pointy, new Point(10, 15), new Point(35, 71));
            EqualHex("layout", h, FractionalHex.HexRound(Layout.PixelToHex(pointy, Layout.HexToPixel(pointy, h))));
        }


        public static void TestConversionRoundtrip()
        {
            var a = new Hex(3, 4, -7);
            var b = new OffsetCoord(1, -3);
            EqualHex("conversion_roundtrip even-q", a, OffsetCoord.QoffsetToCube(OffsetCoord.Even, OffsetCoord.QoffsetFromCube(OffsetCoord.Even, a)));
            EqualOffsetcoord("conversion_roundtrip even-q", b, OffsetCoord.QoffsetFromCube(OffsetCoord.Even, OffsetCoord.QoffsetToCube(OffsetCoord.Even, b)));
            EqualHex("conversion_roundtrip odd-q", a, OffsetCoord.QoffsetToCube(OffsetCoord.Odd, OffsetCoord.QoffsetFromCube(OffsetCoord.Odd, a)));
            EqualOffsetcoord("conversion_roundtrip odd-q", b, OffsetCoord.QoffsetFromCube(OffsetCoord.Odd, OffsetCoord.QoffsetToCube(OffsetCoord.Odd, b)));
            EqualHex("conversion_roundtrip even-r", a, OffsetCoord.RoffsetToCube(OffsetCoord.Even, OffsetCoord.RoffsetFromCube(OffsetCoord.Even, a)));
            EqualOffsetcoord("conversion_roundtrip even-r", b, OffsetCoord.RoffsetFromCube(OffsetCoord.Even, OffsetCoord.RoffsetToCube(OffsetCoord.Even, b)));
            EqualHex("conversion_roundtrip odd-r", a, OffsetCoord.RoffsetToCube(OffsetCoord.Odd, OffsetCoord.RoffsetFromCube(OffsetCoord.Odd, a)));
            EqualOffsetcoord("conversion_roundtrip odd-r", b, OffsetCoord.RoffsetFromCube(OffsetCoord.Odd, OffsetCoord.RoffsetToCube(OffsetCoord.Odd, b)));
        }


        public static void TestOffsetFromCube()
        {
            EqualOffsetcoord("offset_from_cube even-q", new OffsetCoord(1, 3), OffsetCoord.QoffsetFromCube(OffsetCoord.Even, new Hex(1, 2, -3)));
            EqualOffsetcoord("offset_from_cube odd-q", new OffsetCoord(1, 2), OffsetCoord.QoffsetFromCube(OffsetCoord.Odd, new Hex(1, 2, -3)));
        }


        public static void TestOffsetToCube()
        {
            EqualHex("offset_to_cube even-", new Hex(1, 2, -3), OffsetCoord.QoffsetToCube(OffsetCoord.Even, new OffsetCoord(1, 3)));
            EqualHex("offset_to_cube odd-q", new Hex(1, 2, -3), OffsetCoord.QoffsetToCube(OffsetCoord.Odd, new OffsetCoord(1, 2)));
        }


        public static void TestAll()
        {
            TestHexArithmetic();
            TestHexDirection();
            TestHexNeighbor();
            TestHexDiagonal();
            TestHexDistance();
            TestHexRound();
            TestHexLinedraw();
            TestLayout();
            TestConversionRoundtrip();
            TestOffsetFromCube();
            TestOffsetToCube();
        }


        public static void Main()
        {
            TestAll();
        }


        public static void Complain(string name)
        {
            Console.WriteLine("FAIL " + name);
        }

    }
}