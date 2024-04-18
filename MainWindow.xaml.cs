using Microsoft.Win32;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Graficzna
{
    public partial class MainWindow : Window
    {
        // Define commands for opening and saving images
        public static RoutedUICommand OpenImageCommand = new RoutedUICommand("Open Image", "OpenImage", typeof(MainWindow));
        public static RoutedUICommand SaveImageCommand = new RoutedUICommand("Save Image", "SaveImage", typeof(MainWindow));
        public static RoutedUICommand ApplyFilterCommand = new RoutedUICommand("Apply Filter", "ApplyFilter", typeof(MainWindow));
        public static RoutedUICommand DrawLineCommand = new RoutedUICommand("Draw Line", "DrawLine", typeof(MainWindow));

        public MainWindow()
        {
            InitializeComponent();
            CommandBindings.Add(new CommandBinding(OpenImageCommand, OpenImage_Executed, OpenImage_CanExecute));
            CommandBindings.Add(new CommandBinding(SaveImageCommand, SaveImage_Executed, SaveImage_CanExecute));
            CommandBindings.Add(new CommandBinding(ApplyFilterCommand, ApplyFilter));
            CommandBindings.Add(new CommandBinding(DrawLineCommand, DrawLine));
        }

        private void OpenImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JPEG Files (*.jpg)|*.jpg|All Files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    using (Stream stream = openFileDialog.OpenFile())
                    {
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.StreamSource = stream;
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.EndInit();

                        // Display the image in your application UI
                        imageControl.Source = bitmap;

                        // Reset zoom to 1
                        zoomSlider.Value = 1;

                        // Resize the Image control to fit the loaded image dimensions
                        imageControl.Width = bitmap.PixelWidth;
                        imageControl.Height = bitmap.PixelHeight;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading image: " + ex.Message);
                }
            }
        }

        private void SaveImage(Bitmap bitmap)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JPEG Files (*.jpg)|*.jpg|All Files (*.*)|*.*";

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    bitmap.Save(saveFileDialog.FileName, ImageFormat.Jpeg);
                    MessageBox.Show("Image saved successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving image: " + ex.Message);
                }
            }
        }

        private void OpenImage_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenImage();
        }

        private void SaveImage_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // Get the image from your UI
            // For example, if you have an Image control named imageControl:
            // Bitmap bitmap = (Bitmap)((BitmapSource)imageControl.Source).Clone();
            // SaveImage(bitmap);
        }

        private void OpenImage_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            // Enable the command if you want to allow opening an image
            e.CanExecute = true;
        }

        private void SaveImage_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            // Enable the command if there is an image available to save
            // For example, if you have an Image control named imageControl:
            // e.CanExecute = (imageControl.Source != null);
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
            if (imageControl?.Source != null)
            {
                double zoomFactor = zoomSlider.Value;
                imageControl.LayoutTransform = new ScaleTransform(zoomFactor, zoomFactor);
            }
        }
    }
}