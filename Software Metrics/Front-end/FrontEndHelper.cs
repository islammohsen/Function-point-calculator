using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Software_Metrics.Front_end
{
    class FrontEndHelper
    {
        private static Software_Metrics.MainWindow mainWindow;
        public static void SetMainWindow(MainWindow window)
        {
            mainWindow = window;
        }
        public static Software_Metrics.MainWindow GetMainWindow()
        {
            return mainWindow;
        }

        public static Label CreateLabel(double width, double height, double fontSize, string content)
        {
            return new Label
            {
                Width = width,
                Height = height,
                FontSize = fontSize,
                Content = content
            };
        }
        private static void button_MouseLeave(object sender, MouseEventArgs e)
        {
            Button button = (Button)sender;
            button.Background = new SolidColorBrush(Color.FromRgb(0, 127, 175));
        }
        private static void button_MouseEnter(object sender, MouseEventArgs e)
        {
            Button button = (Button)sender;
            button.Background = new SolidColorBrush(Color.FromRgb(0, 95, 127));
        }

        public static Button CreateButton(double width, double height, string content)
        {
            Button obj = new Button
            {
                Width = width,
                Height = height,
                Content = content,
                Background = new SolidColorBrush(Color.FromRgb(0, 127, 175)),
                Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
                FontSize = 20,
                Cursor = Cursors.Hand
            };
            obj.MouseEnter += button_MouseEnter;
            obj.MouseLeave += button_MouseLeave;
            return obj;
        }
    }
}
