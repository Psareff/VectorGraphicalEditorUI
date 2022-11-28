using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace VectorGraphicalEditorUI
{
    public class VectorCanvas
    {
        private double _CanvasHeight = 0;
        private double _CanvasWidth = 0;

        List<Figure> _FiguresArray = new List<Figure>();

        public VectorCanvas()
        {
            _CanvasHeight = 0;
            _CanvasWidth = 0;
        }
        public string Serialize()
            => JsonSerializer.Serialize(this);
        public Figure Deserialize(string json)
            => JsonSerializer.Deserialize<Figure>(json);
        public VectorCanvas(Figure figure)
        {
            if (figure is null) throw new SystemException("Figure, pushed to constructor can't be null!");
            _FiguresArray.Add(figure);
            _CanvasHeight = Convert.ToDouble(Math.Ceiling(figure.RadiusCalc()));
            _CanvasWidth = _CanvasHeight;
        }
        public VectorCanvas(params Figure[] figures)
        {
            foreach (Figure f in figures)
                _FiguresArray.Add(f);
            DefineHeightAndWidthOfCanvas();
        }
        public double CanvasHeight
        {
            get { return _CanvasHeight; }
        }
        public double CanvasWidth
        {
            get { return _CanvasWidth; }
        }

        public void AddFigureToPainting(Figure figure)
        {
            if (figure is null) throw new SystemException("Figure, pushed to adding op can't be null!");
            else
            {
                _FiguresArray.Add(figure);
                DefineHeightAndWidthOfCanvas();

            }
        }
        public void DefineHeightAndWidthOfCanvas()
        {
            foreach (Figure figureIterator in _FiguresArray)
            {
                double widthBuffer = (figureIterator.RadiusCalc() + Math.Abs(figureIterator.CenterCalc().X)) * 2;
                double heightBuffer = (figureIterator.RadiusCalc() + Math.Abs(figureIterator.CenterCalc().Y)) * 2;
                if (heightBuffer > _CanvasHeight)
                    _CanvasHeight = heightBuffer;
                if (widthBuffer > _CanvasWidth)
                    _CanvasWidth = widthBuffer;
            }
        }
        public void AddMultipleFiguresToPainting(params Figure[] figures)
        {
            if (figures is null) throw new SystemException("Figure, pushed to adding op can't be null!");
            else
            {
                foreach (Figure f in figures)
                    _FiguresArray.Add(f);
                foreach (Figure figureIterator in _FiguresArray)
                {
                    double widthBuffer = (figureIterator.RadiusCalc() + Math.Abs(figureIterator.CenterCalc().X)) * 2;
                    double heightBuffer = (figureIterator.RadiusCalc() + Math.Abs(figureIterator.CenterCalc().Y)) * 2;
                    if (heightBuffer > _CanvasHeight)
                        _CanvasHeight = heightBuffer;
                    if (widthBuffer > _CanvasWidth)
                        _CanvasWidth = widthBuffer;
                }

            }
        }
        public (uint circleQty, uint trisQty) QtyOfFiguresOnCanvas()
        {
            uint trisQty = 0, circleQty = 0;
            foreach (Figure figureIterator in _FiguresArray)
            {
                _ = figureIterator is Circle ? circleQty ++ : trisQty ++;
            }
            return (circleQty, trisQty);
        }
        public void DeleteByIndex(int index)
        { if (index < _FiguresArray.Count)
                _FiguresArray.RemoveAt(index);
            else throw new SystemException("Invalid index to delete!");
        }
        public Figure ReturnIndividual (int index)
        {
            return _FiguresArray.ElementAt(index);
        }
        public List<Figure> ReturnAllFigures()
        {
            return _FiguresArray;
        }
        public void ShiftAll(int shiftOx, int shiftOy)
        {
            foreach (Figure figureIterator in _FiguresArray)
            {
                figureIterator.ShiftOxOy(new Point(shiftOx, shiftOy));
            }
        }

        public List<Figure>FiguresArray
        {
            get => _FiguresArray;
        }
    }
}