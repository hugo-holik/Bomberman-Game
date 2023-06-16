using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Bomberman.Utils
{
    public enum ShapeType
    {
        Rectangle,
        Ellipse,
        Triangle
    }

    internal class CanvasShape
    {
        private Canvas _canvas;
        private Shape _shape;

        public int X
        {
            get { return (int)Canvas.GetLeft(_shape); }
            set { Canvas.SetLeft(_shape, value); }
        }
        public int Y
        {
            get { return (int)Canvas.GetTop(_shape); }
            set { Canvas.SetTop(_shape, value); }
        }
        public double Width
        {
            get { return _shape.Width; }
            set { _shape.Width = value; }
        }
        public double Height
        {
            get { return _shape.Height; }
            set { _shape.Width = value; }
        }

        public CanvasShape(int x, int y, int width, int height, ShapeType shapeType, Canvas canvas)
        {
            _canvas = canvas;
            if (shapeType == ShapeType.Rectangle)
            {
                _shape = CreateRectangle(width, height);

            }
            else if (shapeType == ShapeType.Ellipse)
            {
                _shape = CreateEllipse(width, height);
            }
            else if (shapeType == ShapeType.Triangle)
            {
                _shape = CreateTriangle(width, height);
            }
            X = x;
            Y = y;
            Width = width;
            Height = height;

            canvas.Children.Add(_shape);
        }

        public void Delete()
        {
            _canvas.Children.Remove(_shape);
        }

        public void ChangeColor(Brush color)
        {
            _shape.Fill = color;
        }

        public void MoveHorizontally(int moveAmount)
        {
            Redraw(_shape, X + moveAmount, Y);
        }

        public void MoveVertically(int moveAmount)
        {
            Redraw(_shape, X, Y + moveAmount);
        }

        private Rectangle CreateRectangle(double width, double height)
        {
            return new Rectangle
            {
                Width = width,
                Height = height,
                Fill = Brushes.DarkGray,
                Stroke = Brushes.Black,
                StrokeThickness = 1
            };
        }
        private Ellipse CreateEllipse(double width, double height)
        {
            return new Ellipse
            {
                Width = width,
                Height = height,
                Fill = Brushes.DarkGray,
                Stroke = Brushes.Black,
                StrokeThickness = 1
            };
        }

        private Polygon CreateTriangle(double width, double height)
        {
            Polygon triangle = new Polygon();

            double halfWidth = width / 2;
            double halfHeight = height / 2;


            PointCollection points = new PointCollection()
            {
                new Point(0, height),                  // Bottom-left
                new Point(width, height),              // Bottom-right
                new Point(halfWidth, 0)                // Top-center
            };

            triangle.Points = points;
            triangle.Fill = Brushes.DarkGray;
            triangle.Stroke = Brushes.Black;
            triangle.StrokeThickness = 1;

            return triangle;
        }


        private void Draw(Shape shape, int x, int y)
        {
            //_canvas.Children.Remove(shape);
            Canvas.SetLeft(shape, x);
            Canvas.SetTop(shape, y);
            _canvas.Children.Add(shape);
        }

        private void Redraw(Shape shape, int x, int y)
        {
            Canvas.SetLeft(shape, x);
            Canvas.SetTop(shape, y);
        }
    }
}
