using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Software_Metrics.Front_end
{
    class TCFCanvas : CustomCanvas
    {
        private static TCFCanvas ufpCanvas;

        public static TCFCanvas GetInstance(Canvas canvas)
        {
            if (ufpCanvas == null)
                ufpCanvas = new TCFCanvas(canvas);
            return ufpCanvas;
        }
        private TCFCanvas(Canvas canvas) : base(canvas)
        {

        }

        public override void Initialize()
        {
            StackPanel tcfStackPanel = new StackPanel
            {
                Width = canvas.Width
            };
            canvas.Children.Add(tcfStackPanel);

            Grid tcfGrid = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition{Width = new GridLength(0.5 * canvas.Width)},
                    new ColumnDefinition{Width = new GridLength(0.5 * canvas.Width)}
                }
            };
            tcfStackPanel.Children.Add(tcfGrid);

            StackPanel labelsStackPanel = new StackPanel
            {
                Width = 0.5 * canvas.Width
            };
            Grid.SetColumn(labelsStackPanel, 0);
            tcfGrid.Children.Add(labelsStackPanel);
            
            List<Label> labels = new List<Label>
            {
                FrontEndHelper.CreateLabel(0.5 * canvas.Width, 30, 14, "Data Communication"),
                FrontEndHelper.CreateLabel(0.5 * canvas.Width, 30, 14, "Distributed Data Processing"),
                FrontEndHelper.CreateLabel(0.5 * canvas.Width, 30, 14, "Performance Criteria"),
                FrontEndHelper.CreateLabel(0.5 * canvas.Width, 30, 14, "Heavily Utilized Hardware"),
                FrontEndHelper.CreateLabel(0.5 * canvas.Width, 30, 14, "High Transactions Rates"),
                FrontEndHelper.CreateLabel(0.5 * canvas.Width, 30, 14, "Online Data Entry"),
                FrontEndHelper.CreateLabel(0.5 * canvas.Width, 30, 14, "Online Updating"),
                FrontEndHelper.CreateLabel(0.5 * canvas.Width, 30, 14, "End-user Efficiency"),
                FrontEndHelper.CreateLabel(0.5 * canvas.Width, 30, 14, "Complex Computations"),
                FrontEndHelper.CreateLabel(0.5 * canvas.Width, 30, 14, "Reusability"),
                FrontEndHelper.CreateLabel(0.5 * canvas.Width, 30, 14, "Ease of Installation"),
                FrontEndHelper.CreateLabel(0.5 * canvas.Width, 30, 14, "Ease of operation"),
                FrontEndHelper.CreateLabel(0.5 * canvas.Width, 30, 14, "Portability"),
                FrontEndHelper.CreateLabel(0.5 * canvas.Width, 30, 14, "Maintainability")
            };
            for (int i = 0; i < 14; i++)
                labelsStackPanel.Children.Add(labels[i]);

            StackPanel comboBoxStackPanel = new StackPanel
            {
                Width = 0.5 * canvas.Width
            };
            Grid.SetColumn(comboBoxStackPanel, 1);
            tcfGrid.Children.Add(comboBoxStackPanel);

            List<ComboBox> comboBoxList = new List<ComboBox>();
            List<string> optionList = new List<string>
            {
                "No Influence",
                "Incidental",
                "Moderate",
                "Average",
                "Significant",
                "Essential"
            };


            for (int i = 0; i < 14; i++)
            {
                ComboBox comboBox = new ComboBox
                {
                    Width = 0.3 * canvas.Width,
                    Height = 30,
                    ItemsSource = optionList,
                    SelectedIndex = 0
                };
                comboBoxList.Add(comboBox);
                comboBoxStackPanel.Children.Add(comboBox);
           }

            Button calculateButton = FrontEndHelper.CreateButton(100, 40, "Calculate");
            tcfStackPanel.Children.Add(calculateButton);
            calculateButton.Tag = comboBoxList;
            calculateButton.Click += Calculate_Button_Click;
        }

        private void Calculate_Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            List<ComboBox> boxes =(List<ComboBox>) btn.Tag;
            List<string> data = new List<string>();
            foreach(var combobox in boxes)
            {
                data.Add(combobox.Text);
            }
            CalculateFP.CalculateTCF(data);
            CalculateFP.CalculateFPValue();
        }
    }
}
