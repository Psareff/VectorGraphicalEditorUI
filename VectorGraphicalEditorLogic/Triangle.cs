using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace VectorGraphicalEditorUI
{
    [Serializable]

    public class Triangle : Figure
    {
        public Point _FirstVertex;
        public Point _SecondVertex;
        public Point _ThirdVertex;

        public  Point FirstVertex
        {
            get { return _FirstVertex; }
            set 
            { 
                double firstSideLength = SideLengthCalculate(SideCoordinatesCalculate(value, _SecondVertex));
                double secondSideLength = SideLengthCalculate(SideCoordinatesCalculate(_SecondVertex, _ThirdVertex));
                double thirdSideLength = SideLengthCalculate(SideCoordinatesCalculate(_ThirdVertex, value));
                Console.WriteLine(firstSideLength + "; " + secondSideLength + "; " + thirdSideLength);
                if (firstSideLength + secondSideLength > thirdSideLength &&
                    firstSideLength + thirdSideLength > secondSideLength &&
                    secondSideLength + thirdSideLength > firstSideLength)
                    _FirstVertex = value;
                else throw new SystemException("Invalid vertex pushed!");
            }
        }
        public  Point SecondVertex
        {
            get { return _SecondVertex; }
            set 
            {
                double firstSideLength = SideLengthCalculate(SideCoordinatesCalculate(_FirstVertex, value));
                double secondSideLength = SideLengthCalculate(SideCoordinatesCalculate(value, _ThirdVertex));
                double thirdSideLength = SideLengthCalculate(SideCoordinatesCalculate(_ThirdVertex, _FirstVertex));
                Console.WriteLine(firstSideLength + "; " + secondSideLength + "; " + thirdSideLength);
                if (firstSideLength + secondSideLength > thirdSideLength &&
                    firstSideLength + thirdSideLength > secondSideLength &&
                    secondSideLength + thirdSideLength > firstSideLength)
                    _SecondVertex = value;
                else throw new SystemException("Invalid vertex pushed!");

            }
        }
        public Point ThirdVertex
        {
            get { return _ThirdVertex; }
            set 
            {
                double firstSideLength = SideLengthCalculate(SideCoordinatesCalculate(_FirstVertex, _SecondVertex));
                double secondSideLength = SideLengthCalculate(SideCoordinatesCalculate(value, _SecondVertex));
                double thirdSideLength = SideLengthCalculate(SideCoordinatesCalculate(_FirstVertex, value));
                Console.WriteLine(firstSideLength + "; " + secondSideLength + "; " + thirdSideLength);

                if (firstSideLength + secondSideLength > thirdSideLength &&
                    firstSideLength + thirdSideLength > secondSideLength &&
                    secondSideLength + thirdSideLength > firstSideLength)
                    _ThirdVertex = value;
                else throw new SystemException("Invalid vertex pushed!");

            }
        }

        public Triangle() : base(Color.White, Color.White)
        {
            _FirstVertex = new Point(0, 0);
            _SecondVertex = new Point (1, 0);
            _ThirdVertex = new Point (0, 1);
        }

        public Triangle(Point Vertex1, Point Vertex2, Point Vertex3, Color FillColor, Color ContourColor) : base(FillColor, ContourColor)
        {
            double firstSideLength = SideLengthCalculate(SideCoordinatesCalculate(Vertex1, Vertex2));
            double secondSideLength = SideLengthCalculate(SideCoordinatesCalculate(Vertex2, Vertex3));
            double thirdSideLength = SideLengthCalculate(SideCoordinatesCalculate(Vertex3, Vertex1));
            if (firstSideLength + secondSideLength > thirdSideLength &&
                firstSideLength + thirdSideLength > secondSideLength &&
                secondSideLength + thirdSideLength > firstSideLength)
            {
                _FirstVertex = Vertex1;
                _SecondVertex = Vertex2;
                _ThirdVertex = Vertex3;
            }
            else throw new SystemException("Invalid vertexes pushed!");
            FillColor = Color.White;
            ContourColor = Color.White;
        }

        Point SideCoordinatesCalculate(Point Vertex1, Point Vertex2)
        {
            return new Point(Vertex2.X - Vertex1.X, Vertex2.Y - Vertex1.Y);
        }
        double SideLengthCalculate(Point Side)
        {
            return Math.Sqrt(Math.Pow(Side.X, 2) + Math.Pow(Side.Y, 2));
        }
        public override double PerimeterCalculate()
        {
            double firstSideLength = SideLengthCalculate(SideCoordinatesCalculate(_FirstVertex, _SecondVertex));
            double secondSideLength = SideLengthCalculate(SideCoordinatesCalculate(_SecondVertex, _ThirdVertex));
            double thirdSideLength = SideLengthCalculate(SideCoordinatesCalculate(_ThirdVertex, _FirstVertex));
            return firstSideLength + secondSideLength + thirdSideLength;
        }
        public override double AreaCalculate()
        {
            double firstSideLength = SideLengthCalculate(SideCoordinatesCalculate(_FirstVertex, _SecondVertex));
            double secondSideLength = SideLengthCalculate(SideCoordinatesCalculate(_SecondVertex, _ThirdVertex));
            double thirdSideLength = SideLengthCalculate(SideCoordinatesCalculate(_ThirdVertex, _FirstVertex));
            double semiPerimeter = (firstSideLength + secondSideLength + thirdSideLength) / 2;
            return Math.Sqrt(semiPerimeter * (semiPerimeter - firstSideLength) * (semiPerimeter - secondSideLength) * (semiPerimeter - thirdSideLength));
        }
        public override double RadiusCalc()
        {
            double firstSideLength = SideLengthCalculate(SideCoordinatesCalculate(_FirstVertex, _SecondVertex));
            double secondSideLength = SideLengthCalculate(SideCoordinatesCalculate(_SecondVertex, _ThirdVertex));
            double thirdSideLength = SideLengthCalculate(SideCoordinatesCalculate(_ThirdVertex, _FirstVertex));
            return firstSideLength * secondSideLength * thirdSideLength / (4 * AreaCalculate());
        }
        public override Point CenterCalc()
        {
            Point CircumscribedCircleCenter;
            double D;

            D = 2 * (_FirstVertex.X * (_SecondVertex.Y - _ThirdVertex.Y) +
                _SecondVertex.X * (_ThirdVertex.Y - _FirstVertex.Y) +
                _ThirdVertex.X * (_FirstVertex.Y - _SecondVertex.Y));

            CircumscribedCircleCenter.X = (((Math.Pow(_FirstVertex.X, 2) + Math.Pow(_FirstVertex.Y, 2)) * (_SecondVertex.Y - _ThirdVertex.Y)) +
                                              ((Math.Pow(_SecondVertex.X, 2) + Math.Pow(_SecondVertex.Y, 2)) * (_ThirdVertex.Y - _FirstVertex.Y)) +
                                              ((Math.Pow(_ThirdVertex.X, 2) + Math.Pow(_ThirdVertex.Y, 2)) * (_FirstVertex.Y - _SecondVertex.Y))) / D;

            CircumscribedCircleCenter.Y = (((Math.Pow(_FirstVertex.X, 2) + Math.Pow(_FirstVertex.Y, 2)) * (_ThirdVertex.X - _SecondVertex.X)) +
                                              ((Math.Pow(_SecondVertex.X, 2) + Math.Pow(_SecondVertex.Y, 2)) * (_FirstVertex.X - _ThirdVertex.X)) +
                                              ((Math.Pow(_ThirdVertex.X, 2) + Math.Pow(_ThirdVertex.Y, 2)) * (_SecondVertex.X - _FirstVertex.X))) / D;

            return new Point(CircumscribedCircleCenter.X, CircumscribedCircleCenter.Y);

        }
        public override void ShiftOxOy(Point Shift)
        {
            _FirstVertex.X += Shift.X;
            _FirstVertex.Y+= Shift.Y;
            _SecondVertex.X+= Shift.X;
            _SecondVertex.Y+= Shift.Y;
            _ThirdVertex.X+= Shift.X;
            _ThirdVertex.Y+= Shift.Y;
        }
        public override string ToString()
        {
            return "Triangle\nFirst Vertex: " + FirstVertex + ";\nSecond Vertex: " + SecondVertex + ";\nThird Vertex: " + ThirdVertex + ";\nFill: " + _FillColor + "; Contour: " +_ContourColor + ";\nArea: " + Math.Round(AreaCalculate(), 3) + " quadratic units;\nPerimeter: " + Math.Round(PerimeterCalculate(), 3) + " units\n";
        }

        public string Serialize()
            => JsonSerializer.Serialize(this);
        public Figure Deserialize(string json)
            => JsonSerializer.Deserialize<Figure>(json);
    }
}