using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorGraphicalEditorUI
{
    public enum Color { Red, Green, Blue, Cyan, Magenta, Yellow, Black, White}
    [Serializable]
    public abstract class Figure
    {
        
        public Color _FillColor;
        public Color _ContourColor;

        public Figure(Color FillColor, Color ContourColor)
        {
            _FillColor = FillColor;
            _ContourColor = ContourColor;
        }
        public Color ContourColor
        {
            get { return _ContourColor; }
            set { _ContourColor = value; }
        }
            
        public Color FillColor
        {
            get { return _FillColor; }
            set { _FillColor = value; }
        }


        public abstract double PerimeterCalculate();
        public abstract double AreaCalculate();
        public abstract void ShiftOxOy((double, double) Shift);
        public abstract double RadiusCalc();
        public abstract (double, double) CenterCalc();
    }
}