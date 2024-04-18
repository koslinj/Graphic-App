using Microsoft.Win32;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Graficzna
{
    public partial class MainWindow : Window
    {
        public static RoutedUICommand ApplyFilterCommand = new RoutedUICommand("Apply Filter", "ApplyFilter", typeof(MainWindow));
        public static RoutedUICommand DrawLineCommand = new RoutedUICommand("Draw Line", "DrawLine", typeof(MainWindow));

        public MainWindow()
        {
            InitializeComponent();
            CommandBindings.Add(new CommandBinding(OpenImage.Command, OpenImage.Execute, OpenImage.CanExecute));
            CommandBindings.Add(new CommandBinding(SaveImage.Command, SaveImage.Execute, SaveImage.CanExecute));
            CommandBindings.Add(new CommandBinding(ApplyFilterCommand, ApplyFilter));
            CommandBindings.Add(new CommandBinding(DrawLineCommand, DrawLine));
        }

        private void ApplyFilter(object sender, ExecutedRoutedEventArgs e)
        {
            // Obsługa stosowania filtru
        }

        private void DrawLine(object sender, ExecutedRoutedEventArgs e)
        {
            // Obsługa rysowania linii
        }

        private void ZoomSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ImageHelper.UpdateZoom(imageControl, zoomSlider);
        }
    }
}