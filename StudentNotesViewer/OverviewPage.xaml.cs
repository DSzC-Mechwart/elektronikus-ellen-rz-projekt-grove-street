using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace StudentNotesViewer;

public partial class OverviewPage
{
    private readonly Student CurrentStudent;
    
    public static OverviewPage Instance = null!;
    
    public OverviewPage(Student student)
    {
        Instance = this;
        CurrentStudent = student;
        InitializeComponent();
        SetupGradesOverview();
    }

    private void SetupGradesOverview()
    {
        foreach ((Subject subject, List<Grade> grades) in CurrentStudent.Grades)
        {
            MainGrid.RowDefinitions.Add(new());
            
            var border = new Border
            {
                Background = new SolidColorBrush(Color.FromRgb(28, 28, 28)),
                CornerRadius = new(20),
                Padding = new(0),
                Child = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    Children =
                    {
                        new Label
                        {
                            Content = subject.GetName(),
                            HorizontalAlignment = HorizontalAlignment.Left
                        },
                        new Label
                        {
                            Content = grades.Count == 0 ? "-" : Math.Round(grades.Average(x => x.Value), 2),
                            Background = grades.Count > 0 && grades.Average(x => x.Value) <= 1.75 ? new(Colors.Red) : new SolidColorBrush(Colors.Transparent),
                            Foreground = grades.Count > 0 && grades.Average(x => x.Value) <= 1.75 ? new(Colors.Black) : new SolidColorBrush(Colors.LightGray)
                        },
                        new Label
                        {
                            Content = $"{grades.Count} Grades"
                        },
                        new Button
                        {
                            RenderTransform = new ScaleTransform(),
                            Content = "View Grades"
                        }
                    }
                }
            };
            Grid.SetRow(border, MainGrid.RowDefinitions.Count - 1);
            var button = ((StackPanel)border.Child).Children.OfType<Button>().First();
            button.Click += (_, _) => ViewGradesButtonClick(subject, grades);
            MainGrid.Children.Add(border);
        }
    }

    private void ViewGradesButtonClick(Subject subject, List<Grade> grades)
    {
        MainWindow.Instance.Frame.NavigationService.Navigate(new GradesListPage(subject, grades));
    }
    
    public void Refresh()
    {
        MainGrid.Children.Clear();
        MainGrid.RowDefinitions.Clear();
        SetupGradesOverview();
    }
}