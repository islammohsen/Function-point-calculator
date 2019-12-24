using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Software_Metrics.Front_end
{
    class ResultCanvas : CustomCanvas
    {
        private static ResultCanvas resultCanvas;

        public static ResultCanvas GetInstance(Canvas canvas)
        {
            if (resultCanvas == null)
                resultCanvas = new ResultCanvas(canvas);
            return resultCanvas;
        }

        private ResultCanvas(Canvas canvas) : base(canvas)
        {

        }

        public override void Initialize()
        {
            StackPanel resultStackPanel = new StackPanel
            {
                Width = canvas.Width
            };
            canvas.Children.Add(resultStackPanel);

            Label ufpLablel = FrontEndHelper.CreateLabel(200, 32, 16, "UFP: \t" + CalculateFP.UFP.ToString());
            ufpLablel.Margin = new Thickness(0, 0.2 * canvas.Height, 0, 0);
            resultStackPanel.Children.Add(ufpLablel);

            Label tcfLabel = FrontEndHelper.CreateLabel(200, 32, 16, "TCF: \t" + CalculateFP.TCF.ToString());
            resultStackPanel.Children.Add(tcfLabel);

            Label functionPointLabel = FrontEndHelper.CreateLabel(200, 32, 16, "FP: \t" + CalculateFP.FP.ToString());
            functionPointLabel.Margin = new Thickness(0, 0, 0, 50);
            resultStackPanel.Children.Add(functionPointLabel);

            List<string> languages = new List<string>
            {
                "Assembly Language",
                "C",
                "COBOL/Fortan",
                "Pascal",
                "Ada",
                "C++",
                "Visual Basic",
                "Object-Oriented Languages",
                "Smalltalk",
                "Code Generators (PowerBuilder)",
                "SQL/Oracle",
                "Spreadsheets",
                "Graphical Languages (icons)"
            };

            Label label = FrontEndHelper.CreateLabel(100, 32, 16, "Language");
            label.Margin = new Thickness(0, 0, 0, 5);
            resultStackPanel.Children.Add(label);
            ComboBox languageComboBox = new ComboBox
            {
                Width = 0.3 * canvas.Width,
                Height = 30,
                ItemsSource = languages
            };
            Label locLabel = FrontEndHelper.CreateLabel(200, 32, 16, "");
            languageComboBox.Tag = locLabel;
            languageComboBox.SelectionChanged += language_combobox_selection_changed;
            languageComboBox.SelectedIndex = 0;
            languageComboBox.Margin = new Thickness(0, 0, 0, 5);
            resultStackPanel.Children.Add(languageComboBox);
            resultStackPanel.Children.Add(locLabel);
        }

        private void language_combobox_selection_changed(object sender, SelectionChangedEventArgs e)
        {
            ComboBox languageComboBox = (ComboBox)sender;
            Label locLabel = (Label)languageComboBox.Tag;
            locLabel.Content = "LOC: " + CalculateFP.CalculateLOC((string)languageComboBox.SelectedItem).ToString();
        }

    }
}
