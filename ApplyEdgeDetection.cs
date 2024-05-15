using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Graficzna
{
    public static class ApplyEdgeDetection
    {
        public static RoutedUICommand Command = new RoutedUICommand("Apply Edge Detection", "ApplyEdgeDetection", typeof(ApplyEdgeDetection));

        public static void Execute(object sender, ExecutedRoutedEventArgs e)
        {
            var mainWindow = sender as MainWindow;
            if (mainWindow != null)
            {
                if (mainWindow.rgb != null)
                {
                    ImageHelper.toRGB(mainWindow.imageControl, mainWindow.rgb);
                    mainWindow.edgeBtn.Content = "EDGE DETECTION";
                    mainWindow.rgb = null;
                    mainWindow.imageOperation = ImageOperation.NOTHING;
                }
                else
                {
                    mainWindow.rgb = ImageHelper.edgeDetection(mainWindow.imageControl);
                    mainWindow.edgeBtn.Content = "revert";
                    mainWindow.imageOperation = ImageOperation.EDGEDETECTION;
                }
            }
        }

        public static void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var mainWindow = sender as MainWindow;
            if (mainWindow != null)
            {
                e.CanExecute = (mainWindow.imageControl.Source != null && (mainWindow.imageOperation == ImageOperation.NOTHING || mainWindow.imageOperation == ImageOperation.EDGEDETECTION));
            }
        }
    }
}
