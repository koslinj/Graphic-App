using System;
using System.Windows;
using System.Windows.Input;

namespace Graficzna
{
    public static class SaveImage
    {
        public static RoutedUICommand Command = new RoutedUICommand("Save Image", "SaveImage", typeof(SaveImage));

        public static void Execute(object sender, ExecutedRoutedEventArgs e)
        {
            var mainWindow = sender as MainWindow;
            if (mainWindow != null)
            {
                ImageHelper.SaveImage(mainWindow.imageControl, mainWindow.canvas);
            }
        }

        public static void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var mainWindow = sender as MainWindow;
            if (mainWindow != null)
            {
                e.CanExecute = (mainWindow.imageControl.Source != null);
            }
        }
    }
}

