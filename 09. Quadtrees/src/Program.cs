using System.Numerics;
using Raylib_cs;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Net.NetworkInformation;

public class RectangleBoundary
{
    public float centerX { get; set; }
    public float centerY { get; set; }
    public float semiWidth { get; set; }
    public float semiHeight { get; set; }
        
    public RectangleBoundary(float centerX, float centerY, float semiWidth, float semiHeight)
    {
        this.centerX = centerX;
        this.centerY = centerY;
        this.semiWidth = semiWidth;
        this.semiHeight = semiHeight;
    }

    public bool Contains(Vector2 point)
    {
        if ((point.X >= centerX - semiWidth && point.X < centerX + semiWidth) &&
            (point.Y >= centerY - semiHeight && point.Y < centerY + semiHeight))
        {
            return true;
        }

        return false;
    }

    public bool Intersects(RectangleBoundary boundary)
    {
        if ((boundary.centerX - boundary.semiWidth > centerX + semiWidth) ||
            (boundary.centerX + boundary.semiWidth < centerX - semiWidth) ||
            (boundary.centerY - boundary.semiHeight > centerY + semiHeight) ||
            (boundary.centerY + boundary.semiHeight < centerY - semiHeight))
        {
            return false;
        }

        return true;
    }
}

public class QuadTree
{
    private readonly RectangleBoundary _boundary;
    private readonly int _capacity;
    private bool _divided = false;
    private List<Vector2> _points = new List<Vector2>();
    private QuadTree _nw, _ne, _se, _sw;

    public QuadTree(RectangleBoundary boundary, int capacity)
    {
        _boundary = boundary;
        _capacity = capacity;
    }

    public void Subdivide()
    {
        RectangleBoundary nwBoundary = new RectangleBoundary(
            _boundary.centerX - _boundary.semiWidth / 2,
            _boundary.centerY - _boundary.semiHeight / 2,
            _boundary.semiWidth / 2,
            _boundary.semiHeight / 2
            ),
        neBoundary = new RectangleBoundary(
             _boundary.centerX + _boundary.semiWidth / 2,
            _boundary.centerY - _boundary.semiHeight / 2,
            _boundary.semiWidth / 2,
            _boundary.semiHeight / 2
            ),
        seBoundary = new RectangleBoundary(
             _boundary.centerX + _boundary.semiWidth / 2,
            _boundary.centerY + _boundary.semiHeight / 2,
            _boundary.semiWidth / 2,
            _boundary.semiHeight / 2
            ),
        swBoundary = new RectangleBoundary(
             _boundary.centerX - _boundary.semiWidth / 2,
            _boundary.centerY + _boundary.semiHeight / 2,
            _boundary.semiWidth / 2,
            _boundary.semiHeight / 2
            );

        _nw = new QuadTree(nwBoundary, _capacity);
        _ne = new QuadTree(neBoundary, _capacity);
        _se = new QuadTree(seBoundary, _capacity);
        _sw = new QuadTree(swBoundary, _capacity);

        _divided = true;
    }

    public bool Insert(Vector2 point)
    {
        if (!_boundary.Contains(point))
        {
            return false;
        }

        if (_points.Count < _capacity && !_divided)
        {
            _points.Add(point);
            return true;
        }

        if (!_divided)
        {
            Subdivide();
            foreach (Vector2 p in _points)
            {
                bool isInserted = (_nw.Insert(p) || _ne.Insert(p) || _se.Insert(p) || _sw.Insert(p));
            }
            _points.Clear();
        }

        return (_nw.Insert(point) || _ne.Insert(point) || _se.Insert(point) || _sw.Insert(point));
    }

    public void Query(RectangleBoundary range, List<Vector2> results)
    {
        if (!_boundary.Intersects(range))
        {
            return;
        }

        if (!_divided)
        {
            foreach (Vector2 point in _points)
            {
                if (range.Contains(point))
                {
                    results.Add(point);
                }
            }
            return;
        }

        _nw.Query(range, results);
        _ne.Query(range, results);
        _se.Query(range, results);
        _sw.Query(range, results);
    }

    public void Draw()
    {
        Raylib.DrawRectangleLinesEx(
            new Rectangle(
                _boundary.centerX - _boundary.semiWidth,
                _boundary.centerY - _boundary.semiHeight,
                _boundary.semiWidth * 2,
                _boundary.semiHeight * 2
            ),
            1,
            new Color(60, 60, 60, 255)
        );

        if (!_divided)
        {
            foreach (Vector2 point in _points)
            {
                Raylib.DrawCircleV(point, 3, Color.White);
            }
        }
        else
        {
            _nw.Draw();
            _ne.Draw();
            _se.Draw();
            _sw.Draw();
        }
    }
}

public class Program
{
    private const int Width = 900;
    private const int Height = 900;

    public static void Main()
    {
        Raylib.InitWindow(Width, Height, "Interactive QuadTree Demo");
        Raylib.SetTargetFPS(60);

        RectangleBoundary worldBounds = new RectangleBoundary(Width / 2, Height / 2, Width / 2, Height / 2);
        QuadTree quadTree = new QuadTree(worldBounds, capacity: 2);

        Random rand = new Random();
        //for (int i = 0; i < 50; i++)
        //{
        //    quadTree.Insert(new Vector2(rand.Next(10, Width - 10), rand.Next(10, Height - 10)));
        //}

        while (!Raylib.WindowShouldClose())
        {
            Vector2 mousePos = Raylib.GetMousePosition();

            if (Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                quadTree.Insert(mousePos);
            }

            RectangleBoundary searchWindow = new RectangleBoundary(mousePos.X, mousePos.Y, 50, 50);

            List<Vector2> foundPoints = new List<Vector2>();
            quadTree.Query(searchWindow, foundPoints);

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);

            quadTree.Draw();

            Raylib.DrawRectangleLinesEx(
                new Rectangle(
                    searchWindow.centerX - searchWindow.semiWidth,
                    searchWindow.centerY - searchWindow.semiHeight,
                    searchWindow.semiWidth * 2,
                    searchWindow.semiHeight * 2
                ),
                2,
                Color.Blue
            );

            foreach (Vector2 point in foundPoints)
            {
                Raylib.DrawCircleV(point, 3, Color.Red);
            }

            Raylib.DrawText($"Points found in the query region: {foundPoints.Count}", 15, 40, 20, Color.Orange);

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}