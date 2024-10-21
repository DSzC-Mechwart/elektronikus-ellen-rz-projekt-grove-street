using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace StudentNotesViewer;

public partial class AdminView
{
    public AdminView()
    {
        InitializeComponent();
        InitializeMainGrid();
        InitializeStudentAveragesGrid();
        InitializeGradesAveragesGrid();
        InitializeAllAveragesLabel();
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
                FontSize = 8,
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

    private void InitializeStudentAveragesGrid()
    {
        AveragesGrid.RowDefinitions.Add(new());
        AveragesGrid.ColumnDefinitions.Add(new());
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

        int index = 1;
        foreach (var student in MainWindow.Students)
        {
            var label = new Label
            {
                Content = $"{(student.Grades.Values.Any(x => x.Count > 0) ? student.Grades.Values.Where(x => x.Count > 0).Average(x => x.Average(g => g.Value)) : 0):N2}",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Foreground = Brushes.LightGray
            };
            AveragesGrid.ColumnDefinitions.Add(new());
            Grid.SetRow(label, MainGrid.RowDefinitions.Count - 1);
            Grid.SetColumn(label, index);
            AveragesGrid.Children.Add(label);
            index++;
        }
    }

    private void InitializeGradesAveragesGrid()
    {
        MainGrid.ColumnDefinitions.Add(new());
        
        var averageLabel = new Label
        {
            Content = "Average",
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            FontSize = 14,
            Foreground = Brushes.LightGray,
            FontWeight = FontWeights.Bold
        };
        
        Grid.SetRow(averageLabel, 0);
        Grid.SetColumn(averageLabel, MainGrid.ColumnDefinitions.Count - 1);
        
        MainGrid.Children.Add(averageLabel);
        
        foreach (var subject in Enum.GetValues<Subject>())
        {
            var students = MainWindow.Students.Where(x => x.Grades.TryGetValue(subject, out var grades) && grades.Count > 0).ToArray();
            var average = students.Length == 0 ? "-" : students.Select(x => x.Grades[subject]).Average(x => x.Average(g => g.Value)).ToString("0.00");
            var label = new Label
            {
                Content = average,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Foreground = Brushes.LightGray,
                FontWeight = FontWeights.Bold
            };
            
            Grid.SetRow(label, (int)subject + 1);
            Grid.SetColumn(label, MainGrid.ColumnDefinitions.Count - 1);
            
            MainGrid.Children.Add(label);
        }
    }

    private void InitializeAllAveragesLabel()
    {
        var average = MainGrid.Children.OfType<Label>().Where(x => Grid.GetRow(x) > 0 && Grid.GetColumn(x) == MainGrid.ColumnDefinitions.Count - 1).Where(x => double.TryParse(x.Content.ToString(), out _)).Average(x => double.Parse(x.Content.ToString() ?? string.Empty));
        var text = average == 0 ? "-" : average.ToString("0.00");
        var label = new Label
        {
            Content = text,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Foreground = Brushes.LightGray,
            FontWeight = FontWeights.Bold
        };
        
        Grid.SetRow(label, 0);
        Grid.SetColumn(label, AveragesGrid.ColumnDefinitions.Count - 1);
        
        AveragesGrid.Children.Add(label);
    }

    private void GoBack(object sender, RoutedEventArgs e)
    {
        MainWindow.Instance.Frame.NavigationService.GoBack();
    }
}