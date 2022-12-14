using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Windows.Documents.DocumentStructures;
using Microsoft.Win32;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;
using System.Xml;


namespace VectorGraphicalEditorUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        VectorCanvas _VectorCanvas = new VectorCanvas();
        bool _isTriangleCreating = false;
        Color _contourColorOfFigure, _fillColorOfFigure;
        public MainWindow()
        {
            InitializeComponent();
        }

        void FillColorsClick(object sender, RoutedEventArgs e)
        {
            MenuItem choice = sender as MenuItem;
            _fillColorOfFigure = (Color)choice.DataContext;
            FillChoice.Text = "Fill = \n" + choice.DataContext;
        }

        void ContourColorsClick(object sender, RoutedEventArgs e)
        {
            MenuItem choice = sender as MenuItem;
            _contourColorOfFigure = (Color)choice.DataContext;
            ContourChoice.Text = "Contour = \n" + choice.DataContext;

        }

        private void CircleCreation_Click(object sender, RoutedEventArgs e)
        {
            _isTriangleCreating = false;
            RadiusEnterHeader.Visibility = Visibility.Visible;
            CenterEnterHeader.Visibility = Visibility.Visible;
            CenterEnterStackPanel.Visibility = Visibility.Visible;
            RadiusEnterStackPanel.Visibility = Visibility.Visible;

            FirstVertexEnterHeader.Visibility = Visibility.Collapsed;
            SecondVertexEnterHeader.Visibility = Visibility.Collapsed;
            ThirdVertexEnterHeader.Visibility = Visibility.Collapsed;
            FirstVertexEnterStackPanel.Visibility = Visibility.Collapsed;
            SecondVertexEnterStackPanel.Visibility = Visibility.Collapsed;
            ThirdVertexEnterStackPanel.Visibility = Visibility.Collapsed;
        }

        private void TriangleCreation_Click(object sender, RoutedEventArgs e)
        {
            _isTriangleCreating = true;
            RadiusEnterHeader.Visibility = Visibility.Collapsed;
            CenterEnterHeader.Visibility = Visibility.Collapsed;
            CenterEnterStackPanel.Visibility = Visibility.Collapsed;
            RadiusEnterStackPanel.Visibility = Visibility.Collapsed;

            FirstVertexEnterHeader.Visibility = Visibility.Visible;
            SecondVertexEnterHeader.Visibility = Visibility.Visible;
            ThirdVertexEnterHeader.Visibility = Visibility.Visible;
            FirstVertexEnterStackPanel.Visibility = Visibility.Visible;
            SecondVertexEnterStackPanel.Visibility = Visibility.Visible;
            ThirdVertexEnterStackPanel.Visibility = Visibility.Visible;

        }

        private void ShiftOxOy_Click(object sender, RoutedEventArgs e)
        {
            Editor.Items.Clear();
            int ShiftX = InputShiftX.Text == "" ? 0 : Convert.ToInt32(InputShiftX.Text);
            int ShiftY = InputShiftY.Text == "" ? 0 : Convert.ToInt32(InputShiftY.Text);
            _VectorCanvas.ShiftAll(Convert.ToInt32(ShiftX), Convert.ToInt32(ShiftY));
            foreach (Figure f in _VectorCanvas.FiguresArray)
                Editor.Items.Add(f);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = ".xml";
            saveFileDialog.Filter = "XML Database (*.xml)|*.xml";
            saveFileDialog.ShowDialog();
            string path = saveFileDialog.FileName;

            XmlSerializer xs = new XmlSerializer(typeof(List<Figure>));
            using (XmlWriter writer = XmlWriter.Create(path))
                    xs.Serialize(writer, _VectorCanvas.FiguresArray);

        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            Editor.Items.Clear();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML Database (*.xml)|*.xml";
            openFileDialog.ShowDialog();
            string path = openFileDialog.FileName;
            XmlSerializer xs = new XmlSerializer(typeof(List<Figure>));
            using (XmlReader writer = XmlReader.Create(path))
                _VectorCanvas.FiguresArray = (List<Figure>)xs.Deserialize(writer);
            foreach (Figure f in _VectorCanvas.FiguresArray)
            {
                Editor.Items.Add(f);
            }
            _VectorCanvas.CalculateHeightAndWidthOfCanvas();
            CanvasWidthOutput.Text = Convert.ToString(_VectorCanvas.CanvasWidth);
            CanvasHeightOutput.Text = Convert.ToString(_VectorCanvas.CanvasHeight);
        }


        private void AddFigure_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_isTriangleCreating)
                {
                    Triangle tr = new Triangle(new Point(Convert.ToDouble(FirstVertexXCoordField.Text), Convert.ToDouble(FirstVertexYCoordField.Text)),
                                                new Point(Convert.ToDouble(SecondVertexXCoordField.Text), Convert.ToDouble(SecondVertexYCoordField.Text)),
                                                new Point(Convert.ToDouble(ThirdVertexXCoordField.Text), Convert.ToDouble(ThirdVertexYCoordField.Text)),
                                                _fillColorOfFigure, _contourColorOfFigure);
                    _VectorCanvas.AddFigureToPainting(tr);
                }
                else
                {
                    Circle cr = new Circle(new Point(Convert.ToDouble(CenterXCoordField.Text), Convert.ToDouble(CenterYCoordField.Text)),
                                            Convert.ToDouble(RadiusField.Text),
                                            _fillColorOfFigure, _contourColorOfFigure);
                    _VectorCanvas.AddFigureToPainting(cr);
                }
                _contourColorOfFigure = Color.Black;
                _fillColorOfFigure = Color.Black;
                Editor.Items.Clear();
                foreach (Figure f in _VectorCanvas.FiguresArray)
                    Editor.Items.Add(f);
                CanvasWidthOutput.Text = Convert.ToString(_VectorCanvas.CanvasWidth);
                CanvasHeightOutput.Text = Convert.ToString(_VectorCanvas.CanvasHeight);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to create figure, try again." + ex, "Creating error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
    }
}
