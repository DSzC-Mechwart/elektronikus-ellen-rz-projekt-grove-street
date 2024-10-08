using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace StudentNotesViewer;

public partial class OverviewPage : Page
{
    private readonly Student CurrentStudent;
    
    public OverviewPage(Student student)
    {
        CurrentStudent = student;
        InitializeComponent();
        SetupGradesOverview();
    }

    void SetupGradesOverview()
    {
        foreach ((Subject subject, List<Grade> grades) in CurrentStudent.Grades)
        {
            MainGrid.RowDefinitions.Add(new());
            
            var border = new Border
            {
                Background = new SolidColorBrush(Color.FromRgb(28, 28, 28)),
                CornerRadius = new(20),
                Padding = new(10),
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
                            Content = grades.Count == 0 ? "-" : Math.Round(grades.Average(x => x.Value), 2)
                        },
                        new Label
                        {
                            Content = $"{grades.Count} Grades"
                        },
                        new Button
                        {
                            RenderTransform = new ScaleTransform(),
                            Content = "View Grades",
                            Tag = $"ViewGradeButton-{subject}"
                        }
                    }
                }
            };
            Grid.SetRow(border, MainGrid.RowDefinitions.Count - 1);
            var button = ((StackPanel)border.Child).Children.OfType<Button>().First();
            button.Click += (_, _) => ViewGradesButtonClick(button);
            MainGrid.Children.Add(border);
        }
    }

    private void ViewGradesButtonClick(Button button)
    {
        var subject = Enum.Parse<Subject>(button.Tag.ToString()!.Split('-')[1]);
        MainWindow.Instance.Frame.NavigationService.Navigate(new GradesListPage(subject, CurrentStudent.Grades[subject]));
    }
}