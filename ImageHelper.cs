﻿using Microsoft.Win32;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Image = System.Windows.Controls.Image;

namespace Graficzna
{
    public static class ImageHelper
    {
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

        public static void SaveImage(Image imageControl)
        {
            BitmapSource bitmapSource = (BitmapSource)imageControl.Source;
            if (bitmapSource != null)
            {
                BitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapSource));

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "JPEG Files (*.jpg)|*.jpg|All Files (*.*)|*.*";

                if (saveFileDialog.ShowDialog() == true)
                {
                    try
                    {
                        using (Stream stream = File.Create(saveFileDialog.FileName))
                        {
                            encoder.Save(stream);
                            System.Windows.MessageBox.Show("Image saved successfully.");
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show("Error saving image: " + ex.Message);
                    }
                }
            }
        }

        public static void UpdateZoom(Image imageControl, Slider zoomSlider)
        {
            if (imageControl?.Source != null)
            {
                double zoomFactor = zoomSlider.Value;
                imageControl.LayoutTransform = new System.Windows.Media.ScaleTransform(zoomFactor, zoomFactor);
            }
        }
    }
}