using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorGraphicalEditorUI
{
    public class Triangle : Figure
    {
        internal (double, double) _FirstVertex;
        internal (double, double) _SecondVertex;
        internal (double, double) _ThirdVertex;

        public  (double, double) FirstVertex
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
        public  (double, double) SecondVertex
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
        public (double, double) ThirdVertex
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
            _FirstVertex = (0, 0);
            _SecondVertex = (1, 0);
            _ThirdVertex = (0, 1);
        }

        public Triangle((double, double) Vertex1, (double, double) Vertex2, (double, double) Vertex3, Color FillColor, Color ContourColor) : base(FillColor, ContourColor)
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

        (double, double) SideCoordinatesCalculate((double, double) Vertex1, (double, double) Vertex2)
        {
            return (Vertex2.Item1 - Vertex1.Item1, Vertex2.Item2 - Vertex1.Item2);
        }
        double SideLengthCalculate((double, double) Side)
        {
            return Math.Sqrt(Math.Pow(Side.Item1, 2) + Math.Pow(Side.Item2, 2));
        }
        public override double PerimeterCalculate()
        {
            double firstSideLength = SideLengthCalculate(SideCoordinatesCalculate(_FirstVertex, _SecondVertex));
            double secondSideLength = SideLengthCalculate(SideCoordinatesCalculate(_SecondVertex, _ThirdVertex));
            double thirdSideLength = SideLengthCalculate(SideCoordinatesCalculate(_ThirdVertex, _FirstVertex));
            return firstSideLength + secondSideLength + thirdSideLength;
        }
        public override double SquareCalculate()
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
            return firstSideLength * secondSideLength * thirdSideLength / (4 * SquareCalculate());
        }
        public override (double, double) CenterCalc()
        {
            (double, double) CircimscribedCircleCenter;
            double D;

            D = 2 * (_FirstVertex.Item1 * (_SecondVertex.Item2 - _ThirdVertex.Item2) +
                _SecondVertex.Item1 * (_ThirdVertex.Item2 - _FirstVertex.Item2) +
                _ThirdVertex.Item1 * (_FirstVertex.Item2 - _SecondVertex.Item2));

            CircimscribedCircleCenter.Item1 = (((Math.Pow(_FirstVertex.Item1, 2) + Math.Pow(_FirstVertex.Item2, 2)) * (_SecondVertex.Item2 - _ThirdVertex.Item2)) +
                                              ((Math.Pow(_SecondVertex.Item1, 2) + Math.Pow(_SecondVertex.Item2, 2)) * (_ThirdVertex.Item2 - _FirstVertex.Item2)) +
                                              ((Math.Pow(_ThirdVertex.Item1, 2) + Math.Pow(_ThirdVertex.Item2, 2)) * (_FirstVertex.Item2 - _SecondVertex.Item2))) / D;

            CircimscribedCircleCenter.Item2 = (((Math.Pow(_FirstVertex.Item1, 2) + Math.Pow(_FirstVertex.Item2, 2)) * (_ThirdVertex.Item1 - _SecondVertex.Item1)) +
                                              ((Math.Pow(_SecondVertex.Item1, 2) + Math.Pow(_SecondVertex.Item2, 2)) * (_FirstVertex.Item1 - _ThirdVertex.Item1)) +
                                              ((Math.Pow(_ThirdVertex.Item1, 2) + Math.Pow(_ThirdVertex.Item2, 2)) * (_SecondVertex.Item1 - _FirstVertex.Item1))) / D;

            return (CircimscribedCircleCenter.Item1, CircimscribedCircleCenter.Item2);

        }
        public override void ShiftOxOy((double, double) Shift)
        {
            _FirstVertex.Item1 += Shift.Item1;
            _FirstVertex.Item2 += Shift.Item2;
            _SecondVertex.Item1 += Shift.Item1;
            _SecondVertex.Item2 += Shift.Item2;
            _ThirdVertex.Item1 += Shift.Item1;
            _ThirdVertex.Item2 += Shift.Item2;
        }
        public override string ToString()
        {
            return "Triangle\nFirst Vertex: " + FirstVertex + ";\nSecond Vertex: " + SecondVertex + ";\nThird Vertex: " + ThirdVertex + ";\nFill: " + _FillColor + "; Contour: " +_ContourColor + ";\nArea: " + Math.Round(SquareCalculate(), 3) + " quadratic units;\nPerimeter: " + Math.Round(PerimeterCalculate(), 3) + " units\n";
        }
    }
}