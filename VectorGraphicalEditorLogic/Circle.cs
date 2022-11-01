using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorGraphicalEditorUI
{
    internal class Circle : Figure
    {
        internal (double, double) _Center;
        internal double _Radius;
        (double, double) Center
        {
            get { return _Center; }
            set { _Center = value; }
        }

        internal double Radius
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
        public override (double, double) CenterCalc()
        {
            return _Center;
        }
        public Circle() : base(Color.White, Color.White)
        {
            _Radius = 1;
            _Center = (0, 0);
        }

        public Circle((double, double) Center, double Radius, Color FillColor, Color ContourColor) : base(FillColor, ContourColor)
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
        public override void ShiftOxOy((double, double) Shift)
        {
            _Center.Item1 += Shift.Item1;
            _Center.Item2 += Shift.Item2;
        }
        public override string ToString()
        {
            return "Circle\nCenter = " + _Center + "; Radius = " + Radius + ";\nFill: " + _FillColor + "; Contour: " + _ContourColor + "\nArea: " + Math.Round(AreaCalculate(), 3) + " quadratic units;\nPerimeter: " + Math.Round(PerimeterCalculate(), 3) + " units\n";
        }
    }
}