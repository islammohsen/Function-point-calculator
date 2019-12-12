using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Software_Metrics.Front_end
{
    class MainWindowCanvas : CustomCanvas
    {
        private static MainWindowCanvas mainWindowCanvas;

        public static MainWindowCanvas GetInstance(Canvas canvas)
        {
            if (mainWindowCanvas == null)
                mainWindowCanvas = new MainWindowCanvas(canvas);
            return mainWindowCanvas;
        }
        private MainWindowCanvas(Canvas canvas) : base(canvas)
        {
            CalculateFP.Initialize();
        }

        public override void Initialize()
        {
            StackPanel mainWindowStackPanel = new StackPanel
            {
                Width = canvas.Width
            };
            canvas.Children.Add(mainWindowStackPanel);

            Label welcomeLabel = FrontEndHelper.CreateLabel(300, 50, 24, "Function Point Calculator");
            welcomeLabel.Margin = new Thickness
            {
                Top = canvas.Height / 3
            };
            mainWindowStackPanel.Children.Add(welcomeLabel);

            Button calculateButton = FrontEndHelper.CreateButton(100, 50, "Calculate");
            mainWindowStackPanel.Children.Add(calculateButton);
            calculateButton.Click += FrontEndHelper.GetMainWindow().Calculate_Button_Click;
        }
    }
}
