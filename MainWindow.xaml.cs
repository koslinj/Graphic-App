using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Point = System.Windows.Point;

namespace Graficzna
{
    public partial class MainWindow : Window
    {
        public static RoutedUICommand ApplyFilterCommand = new RoutedUICommand("Apply Filter", "ApplyFilter", typeof(MainWindow));
        public static RoutedUICommand DrawLineCommand = new RoutedUICommand("Draw Line", "DrawLine", typeof(MainWindow));
        private Point startPoint;
        private List<Point> points = new List<Point>();

         private void StartDrawing(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                startPoint = e.GetPosition(imageControl);
                points.Add(startPoint);
            }
        }

        private void ContinueDrawing(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var currentPoint = e.GetPosition(imageControl);
                points.Add(currentPoint);

                DrawLineSegment(points[points.Count - 2], currentPoint);
            }
        }

        private void EndDrawing(object sender, MouseButtonEventArgs e)
        {
            points.Clear();
        }

        private void DrawLineSegment(Point start, Point end)
        {
            var line = new Line
            {
                X1 = start.X,
                Y1 = start.Y,
                X2 = end.X,
                Y2 = end.Y,
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };

            canvas.Children.Add(line);
        }

        public MainWindow()
        {
            InitializeComponent();
            CommandBindings.Add(new CommandBinding(OpenImage.Command, OpenImage.Execute, OpenImage.CanExecute));
            CommandBindings.Add(new CommandBinding(SaveImage.Command, SaveImage.Execute, SaveImage.CanExecute));
            CommandBindings.Add(new CommandBinding(ApplyFilterCommand, ApplyFilter));
            CommandBindings.Add(new CommandBinding(DrawLineCommand, DrawLine));

            imageControl.MouseDown += StartDrawing;
            imageControl.MouseMove += ContinueDrawing;
            imageControl.MouseUp += EndDrawing;
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
            ImageHelper.UpdateZoom(imageContainer, zoomSlider);
        }
    }
}