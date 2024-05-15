using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Brush = System.Windows.Media.Brush;
using Brushes = System.Windows.Media.Brushes;
using Cursors = System.Windows.Input.Cursors;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using Point = System.Windows.Point;

namespace Graficzna
{
    public partial class MainWindow : Window
    {
        public static RoutedUICommand DrawLineCommand = new RoutedUICommand("Draw Line", "DrawLine", typeof(MainWindow));
        private Point startPoint;
        private List<Point> points = new List<Point>();
        private bool isDrawingEnabled = false;
        public BitmapSource? rgb = null;
        private Brush strokeColor = Brushes.Black;
        private double strokeWidth = 2.0;
        public ImageOperation imageOperation = ImageOperation.NOTHING;

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
            if (e.LeftButton == MouseButtonState.Pressed && isDrawingEnabled)
            {
                var currentPoint = e.GetPosition(imageControl);
                points.Add(currentPoint);

                if (points.Count >= 2)
                {
                    DrawLineSegment(points[points.Count - 2], currentPoint);
                }
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
                Stroke = strokeColor,
                StrokeThickness = strokeWidth
            };

            canvas.Children.Add(line);
        }

        public MainWindow()
        {
            InitializeComponent();
            CommandBindings.Add(new CommandBinding(OpenImage.Command, OpenImage.Execute, OpenImage.CanExecute));
            CommandBindings.Add(new CommandBinding(SaveImage.Command, SaveImage.Execute, SaveImage.CanExecute));
            CommandBindings.Add(new CommandBinding(ApplyFilter.Command, ApplyFilter.Execute, ApplyFilter.CanExecute));
            CommandBindings.Add(new CommandBinding(ApplyThreshold.Command, ApplyThreshold.Execute, ApplyThreshold.CanExecute));
            CommandBindings.Add(new CommandBinding(ApplyEdgeDetection.Command, ApplyEdgeDetection.Execute, ApplyEdgeDetection.CanExecute));
            CommandBindings.Add(new CommandBinding(DrawLineCommand, DrawLine));

            generalContainer.MouseDown += StartDrawing;
            generalContainer.MouseMove += ContinueDrawing;
            generalContainer.MouseUp += EndDrawing;
        }

        private void DrawLine(object sender, ExecutedRoutedEventArgs e)
        {
            if (isDrawingEnabled)
            {
                isDrawingEnabled = false;
                drawBtn.Content = "Start Drawing";
                generalContainer.Cursor = Cursors.Arrow;
            }
            else
            {
                isDrawingEnabled = true;
                drawBtn.Content = "Stop Drawing";
                generalContainer.Cursor = Cursors.Cross;
            }

        }

        private void ZoomSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ImageHelper.UpdateZoom(imageContainer, zoomSlider);
        }

        private void ChangeColor_Click(object sender, RoutedEventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.Drawing.Color selectedColor = colorDialog.Color;
                System.Windows.Media.Color mediaColor = System.Windows.Media.Color.FromArgb(selectedColor.A, selectedColor.R, selectedColor.G, selectedColor.B);
                // Create SolidColorBrush from the mediaColor
                strokeColor = new SolidColorBrush(mediaColor);
                drawBtn.Foreground = new SolidColorBrush(mediaColor);
            }
        }

        private void ThicknessSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double newValue = e.NewValue;
            // Update stroke thickness
            strokeWidth = newValue;
        }
    }
}