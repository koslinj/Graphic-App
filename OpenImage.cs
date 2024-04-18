using System;
using System.Windows;
using System.Windows.Input;

namespace Graficzna
{
    public static class OpenImage
    {
        public static RoutedUICommand Command = new RoutedUICommand("Open Image", "OpenImage", typeof(OpenImage));

        public static void Execute(object sender, ExecutedRoutedEventArgs e)
        {
            var mainWindow = sender as MainWindow;
            if (mainWindow != null)
            {
                ImageHelper.OpenImage(mainWindow.imageControl, mainWindow.zoomSlider);
            }
        }

        public static void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true; // Always enable the command for now
        }
    }
}


