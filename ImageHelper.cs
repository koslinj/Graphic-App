using AForge.Imaging.Filters;
using Microsoft.Win32;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Image = System.Windows.Controls.Image;

namespace Graficzna
{
    public static class ImageHelper
    {
        public static BitmapSource toGrayscale(Image imageControl)
        {
            Grayscale filter = new Grayscale(0.2125, 0.7154, 0.0721);
            BitmapSource source = (BitmapSource)imageControl.Source;
            Bitmap bitmap = BitmapFromSource(source);
            Bitmap gray = filter.Apply(bitmap);
            BitmapImage bitmapImage = BitmapToBitmapImage(gray);
            imageControl.Source = bitmapImage;
            return source;
        }
        public static void toRGB(Image imageControl, BitmapSource src)
        {
            imageControl.Source = src;
        }

        public static Bitmap BitmapFromSource(BitmapSource bitmapsource)
        {
            Bitmap bitmap;
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapsource));
                encoder.Save(outStream);
                bitmap = new Bitmap(outStream);
            }
            return bitmap;
        }

        public static BitmapImage BitmapToBitmapImage(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                memory.Position = 0;

                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = memory;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }

        public static void OpenImage(Image imageControl, Slider zoomSlider)
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
                    System.Windows.MessageBox.Show("Error loading image: " + ex.Message);
                }
            }
        }

        public static void SaveImage(Image imageControl, Canvas canvas)
        {
            RenderTargetBitmap renderBitmap = new RenderTargetBitmap(
                (int)imageControl.ActualWidth, (int)imageControl.ActualHeight, 96, 96, PixelFormats.Pbgra32);

            renderBitmap.Render(imageControl);

            renderBitmap.Render(canvas);

            BitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderBitmap));

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JPEG Files (*.jpg)|*.jpg|All Files (*.*)|*.*";

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    using (Stream stream = File.Create(saveFileDialog.FileName))
                    {
                        encoder.Save(stream);
                        MessageBox.Show("Image saved successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving image: " + ex.Message);
                }
            }
        }

        public static void UpdateZoom(Grid imageContainer, Slider zoomSlider)
        {
            if (imageContainer?.Children != null)
            {
                double zoomFactor = zoomSlider.Value;
                imageContainer.LayoutTransform = new System.Windows.Media.ScaleTransform(zoomFactor, zoomFactor);
            }
        }
    }
}
