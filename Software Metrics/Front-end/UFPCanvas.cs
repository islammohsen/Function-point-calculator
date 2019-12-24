﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Software_Metrics.Front_end
{
    class UFPCanvas : CustomCanvas
    {
        private static UFPCanvas ufpCanvas;
        private List<Tuple<string, string, int>> data;

        public static UFPCanvas GetInstance(Canvas canvas)
        {
            if (ufpCanvas == null)
                ufpCanvas = new UFPCanvas(canvas);
            return ufpCanvas;
        }
        private UFPCanvas(Canvas canvas) : base(canvas)
        {
            data = new List<Tuple<string, string, int>>();
        }

        public override void Initialize()
        {
            StackPanel ufpStackPanel = new StackPanel
            {
                Width = canvas.Width
            };
            canvas.Children.Add(ufpStackPanel);

            Grid ufpInputDataGrid = new Grid
            {
                Margin = new Thickness
                {
                    Left = 30,
                    Top = 50
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(0.3 * canvas.Width) },
                    new ColumnDefinition { Width = new GridLength(0.3 * canvas.Width) },
                    new ColumnDefinition { Width = new GridLength(0.3 * canvas.Width) }
                }
            };
            ufpStackPanel.Children.Add(ufpInputDataGrid);

            ComboBox inputParameterComboBox = new ComboBox
            {
                Height = 30,
                ItemsSource = new List<String>
                {
                   "External Input",
                   "External Output",
                   "External Inquiry",
                   "Internal Logical Files",
                   "External Interface Files"
                },
                SelectedIndex = 0
            };
            Grid.SetColumn(inputParameterComboBox, 0);
            ufpInputDataGrid.Children.Add(inputParameterComboBox);

            ComboBox inputTypeComboBox = new ComboBox
            {
                Height = 30,
                ItemsSource = new List<String>
                {
                    "Simple",
                    "Average",
                    "Complex"
                },
                SelectedIndex = 0
            };
            Grid.SetColumn(inputTypeComboBox, 1);
            ufpInputDataGrid.Children.Add(inputTypeComboBox);

            TextBox inputCountTextBox = new TextBox
            {
                Height = 30,
                FontSize = 14,
            };
            Grid.SetColumn(inputCountTextBox, 2);
            ufpInputDataGrid.Children.Add(inputCountTextBox);

            Grid ufpButtonsGrid = new Grid
            {
                Width = canvas.Width,
                Margin = new Thickness
                {
                    Left = 15,
                    Top = 20
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(0.7 * canvas.Width) },
                    new ColumnDefinition { Width = new GridLength(0.3 * canvas.Width) }
                }
            };
            ufpStackPanel.Children.Add(ufpButtonsGrid);

            Button addButton = FrontEndHelper.CreateButton(220, 45, "Insert");
            addButton.Tag = new List<object>();
            addButton.Click += Add_Button_Click;
            ((List<object>)addButton.Tag).Add(inputParameterComboBox);
            ((List<object>)addButton.Tag).Add(inputTypeComboBox);
            ((List<object>)addButton.Tag).Add(inputCountTextBox);
            Grid.SetColumn(addButton, 0);
            ufpButtonsGrid.Children.Add(addButton);

            Button nextButton = FrontEndHelper.CreateButton(110, 45, "Next");
            nextButton.Click += Next_Button_Click;
            Grid.SetColumn(nextButton, 1);
            ufpButtonsGrid.Children.Add(nextButton);

            Expander addedItemsExpander = new Expander
            {
                Width = canvas.Width / 2 + 200,
                Height = 300,
                Header = "Input Data",
                IsExpanded = true,
                Margin = new Thickness
                {
                    Top = 30,
                    Left = 20
                }
            };
            addedItemsExpander.HorizontalContentAlignment = HorizontalAlignment.Center;
            ufpStackPanel.Children.Add(addedItemsExpander);
            ScrollViewer addedItemsScrollViewer = new ScrollViewer()
            {
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto
            };
            StackPanel addedItemsStackPanel = new StackPanel();
            addedItemsExpander.Content = addedItemsScrollViewer;
            addedItemsScrollViewer.Content = addedItemsStackPanel;
            ((List<object>)addButton.Tag).Add(addedItemsStackPanel);
            addedItemsStackPanel.Tag = new List<Tuple<string, string, int>>();
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            List<object> list = (List<object>)((Button)sender).Tag;
            ComboBox inputParameterComboBox = (ComboBox)list[0];
            ComboBox inputTypeComboBox = (ComboBox)list[1];
            TextBox inputCountTextBox = (TextBox)list[2];
            StackPanel addedItemsStackPanel = (StackPanel)list[3];
            int count;
            if (int.TryParse(inputCountTextBox.Text, out count))
            {
                Grid itemsGrid = new Grid();
                addedItemsStackPanel.Children.Add(itemsGrid);
                Label addedItemLabel = FrontEndHelper.CreateLabel(canvas.Width, 30, 15,
                    inputParameterComboBox.SelectedValue
                    + ", " + inputTypeComboBox.SelectedValue
                    + ", "
                    + inputCountTextBox.Text);
                Button deleteButton = FrontEndHelper.CreateButton(40, 15, "Del");
                deleteButton.FontSize = 10;
                Grid.SetColumn(deleteButton,1);
                Grid.SetColumn(addedItemLabel,0);
                itemsGrid.Children.Add(addedItemLabel);
                itemsGrid.Children.Add(deleteButton);

                var insertedData = Tuple.Create((string) inputParameterComboBox.SelectedValue,
                    (string) inputTypeComboBox.SelectedValue, count);
                deleteButton.Tag = new List<Object>();
                ((List<object>) deleteButton.Tag).Add(insertedData);
                ((List<object>) deleteButton.Tag).Add(itemsGrid);
                deleteButton.Click += DeleteButtonOnClick;
                data.Add(insertedData);
                ((List<Tuple<string, string, int>>)addedItemsStackPanel.Tag).Add(insertedData);
                ((List<object>)deleteButton.Tag).Add(addedItemsStackPanel);
                // TODO: remove Message Box
                //MessageBox.Show("Added");
            }
            else
            {
                MessageBox.Show("Count is not a number!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteButtonOnClick(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            var tags = (List<Object>) btn.Tag;
            var toBeDeletedItem = (Tuple<string, string, int>)tags[0];
            var toBeDeletedGrid = (Grid) tags[1];
            var addedItemsStackPanel = (StackPanel) tags[2];
            data.Remove(toBeDeletedItem);
            addedItemsStackPanel.Children.Remove(toBeDeletedGrid);
            //MessageBox.Show(toBeDeletedItem.ToString());
        }

        private void Next_Button_Click(object sender, RoutedEventArgs e)
        {
            CalculateFP.CalculateUFP(data);
            // TODO: remove Message Box
            MessageBox.Show("UFP = " + CalculateFP.UFP.ToString(), "UFP Result", 
                MessageBoxButton.OK, MessageBoxImage.Information);
            MainWindow mainWindow = FrontEndHelper.GetMainWindow();
            if (mainWindow.CurrentCanvas != null)
                mainWindow.CurrentCanvas.Hide();
            mainWindow.InitializeTCFCanvas();
        }
    }
}
