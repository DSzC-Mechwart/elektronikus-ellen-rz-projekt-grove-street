using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace StudentNotesViewer;

public partial class AdminView : Page
{
    public AdminView()
    {
        InitializeComponent();
        InitializeMainGrid();
    }
    
    private void InitializeMainGrid()
    {
        MainGrid.RowDefinitions.Add(new());
        MainGrid.ColumnDefinitions.Add(new());
        
        foreach (var subject in Enum.GetValues<Subject>())
        {
            MainGrid.RowDefinitions.Add(new());
            var label = new Label
            {
                Content = subject.GetName(),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 9,
                Foreground = Brushes.LightGray
            };
            Grid.SetRow(label, MainGrid.RowDefinitions.Count - 1);
            Grid.SetColumn(label, 0);
            MainGrid.Children.Add(label);
        }
        
        foreach (var student in MainWindow.Students)
        {
            MainGrid.ColumnDefinitions.Add(new());
            var nameParts = student.Name.Split(' ');
            var label = new Label
            {
                Content = nameParts[0][0] + ". " + nameParts[1],
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 14,
                Foreground = Brushes.LightGray
            };
            Grid.SetRow(label, 0);
            Grid.SetColumn(label, MainGrid.ColumnDefinitions.Count - 1);
            MainGrid.Children.Add(label);
            
            foreach (var subject in Enum.GetValues<Subject>())
            {
                var text = !student.Grades.TryGetValue(subject, out var grades) || grades.Count == 0 ? "-" : grades.Average(x => x.Value).ToString("0.00");
                var label2 = new Label
                {
                    Content = text,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Foreground = Brushes.LightGray
                };
                Grid.SetRow(label2, (int)subject + 1);
                Grid.SetColumn(label2, MainGrid.ColumnDefinitions.Count - 1);
                MainGrid.Children.Add(label2);
            }
        }
    }

    private void InitializeAveragesGrid()
    {
        AveragesGrid.RowDefinitions.Add(new());
        AveragesGrid.ColumnDefinitions.Add(new());
        
        var averageLabel = new Label
        {
            Content = "Average",
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Center,
            FontSize = 14,
            Foreground = Brushes.LightGray
        };
        Grid.SetRow(averageLabel, 0);
        Grid.SetColumn(averageLabel, 0);
        AveragesGrid.Children.Add(averageLabel);
        
        foreach (var subject in Enum.GetValues<Subject>())
        {
            // TODO: Calculate average for each student
            var label = new Label
            {
                Content = "",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Foreground = Brushes.LightGray
            };
            Grid.SetRow(label, 0);
            Grid.SetColumn(label, (int)subject + 1);
            AveragesGrid.Children.Add(label);
        }
    }

    private void GoBack(object sender, RoutedEventArgs e)
    {
        MainWindow.Instance.Frame.NavigationService.GoBack();
    }
}