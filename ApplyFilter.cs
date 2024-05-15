using AForge.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Graficzna
{
    public static class ApplyFilter
    {
        public static RoutedUICommand Command = new RoutedUICommand("Apply Filter", "ApplyFilter", typeof(ApplyFilter));

        public static void Execute(object sender, ExecutedRoutedEventArgs e)
        {
            var mainWindow = sender as MainWindow;
            if (mainWindow != null)
            {
                if (mainWindow.rgb != null)
                {
                    ImageHelper.toRGB(mainWindow.imageControl, mainWindow.rgb);
                    mainWindow.filterBtn.Content = "to GRAY";
                    mainWindow.rgb = null;
                }
                else
                {
                    mainWindow.rgb = ImageHelper.toGrayscale(mainWindow.imageControl);
                    mainWindow.filterBtn.Content = "to RGB";
                }
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
