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

namespace VectorGraphicalEditorUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        VectorCanvas canvas = new VectorCanvas();
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
            VectorCanvas.ShiftAll();
        }

        private void AddFigure_Click(object sender, RoutedEventArgs e)
        {
            if (_isTriangleCreating)
            {
                Triangle tr = new Triangle( (Convert.ToDouble(FirstVertexXCoordField.Text), Convert.ToDouble(FirstVertexYCoordField.Text)),
                                            (Convert.ToDouble(SecondVertexXCoordField.Text), Convert.ToDouble(SecondVertexYCoordField.Text)),
                                            (Convert.ToDouble(ThirdVertexXCoordField.Text), Convert.ToDouble(ThirdVertexYCoordField.Text)),
                                            _fillColorOfFigure, _contourColorOfFigure);
                figures.Add(tr);
                Editor.Items.Add(tr);
                
            }
            else
            {
                Circle cr = new Circle((Convert.ToDouble(CenterXCoordField.Text), Convert.ToDouble(CenterYCoordField.Text)),
                                        Convert.ToDouble(RadiusField.Text), 
                                        _fillColorOfFigure, _contourColorOfFigure);
                figures.Add(cr);
                Editor.Items.Add(cr);
            }
                _contourColorOfFigure = Color.Black;
                _fillColorOfFigure = Color.Black;
        }
        
    }
}
