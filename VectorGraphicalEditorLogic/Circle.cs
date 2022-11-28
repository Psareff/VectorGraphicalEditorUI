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
    internal class Circle : Figure
    {
        public Point _Center;
        public double _Radius;
        private string json;
        public Point Center
        {
            get { return _Center; }
            set { _Center = value; }
        }

        public  double Radius
        {
            get { return _Radius; }
            set 
            {
                if (value > 0) _Radius = value;
                else throw new SystemException("Invalid radius pushed!");
            }
        }
        public override double RadiusCalc()
        {
            return _Radius;
        }
        public override Point CenterCalc()
        {
            return _Center;
        }
        public Circle() : base(Color.White, Color.White)
        {
            _Radius = 1;
            _Center = new Point(0, 0);
        }

        public Circle(Point Center, double Radius, Color FillColor, Color ContourColor) : base(FillColor, ContourColor)
        {
            _Center = Center;
            if (Radius > 0) _Radius = Radius;
            else throw new SystemException("Invalid radius pushed!");

        }

        public override double PerimeterCalculate()
        {
            return 2 * Math.PI * _Radius;
        }
        public override double AreaCalculate()
        {
            return Math.PI * Math.Pow(_Radius, 2);
        }
        public override void ShiftOxOy(Point Shift)
        {
            _Center.X += Shift.X;
            _Center.Y+= Shift.Y;
        }
        public override string ToString()
        {
            return "Circle\nCenter = " + Center + "; Radius = " + Radius + ";\nFill: " + _FillColor + "; Contour: " + _ContourColor + "\nArea: " + Math.Round(AreaCalculate(), 3) + " quadratic units;\nPerimeter: " + Math.Round(PerimeterCalculate(), 3) + " units\n";
        }


    }
}