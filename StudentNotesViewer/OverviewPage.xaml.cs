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
        SetupTopRow();
        SetupGradesOverview();
    }

    private void SetupTopRow()
    {
        var droppingOutRisk = CurrentStudent.Grades.Values.Count(x => x.Count > 0 && x.Average(g => g.Value) < 1.75) >= 3;
        MainGrid.RowDefinitions.Add(new());
        var stackPanel = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            HorizontalAlignment = HorizontalAlignment.Center,
            Children =
            {
                new Button
                {
                    RenderTransform = new ScaleTransform(),
                    Content = "Log Out"
                },
                new Label { Content = CurrentStudent.Name },
                new Label
                {
                    Content = droppingOutRisk ? "Risk of Dropping Out" : "Doing Good!",
                    Foreground = new SolidColorBrush(droppingOutRisk ? Colors.Black : Colors.LightGray),
                    Background = new SolidColorBrush(droppingOutRisk ? Colors.Red : Colors.Transparent),
                    FontWeight = droppingOutRisk ? FontWeights.Bold : FontWeights.Light,
                    FontSize = droppingOutRisk ? 16 : 14
                },
                new Label
                {
                    Content = $"Average: {(CurrentStudent.Grades.Values.Any(x => x.Count > 0) ? CurrentStudent.Grades.Values.Where(x => x.Count > 0).Average(x => x.Average(g => g.Value)) : 0):N2}",
                    FontWeight = FontWeights.Light
                }
            }
        };
        Grid.SetRow(stackPanel, 0);
        stackPanel.Children.OfType<Button>().First().Click += (_, _) => LogOut();
        MainGrid.Children.Add(stackPanel);
    }

    private static void LogOut()
    {
        LoginPage.Instance.UsernameTextBox.Text = string.Empty;
        LoginPage.Instance.PasswordTextBox.Text = string.Empty;
        MainWindow.Instance.Frame.NavigationService.GoBack();
    }

    private void SetupGradesOverview()
    {
        foreach ((Subject subject, List<Grade> grades) in CurrentStudent.Grades)
        {
            var doomed = grades.Count > 0 && grades.Average(x => x.Value) < 2;
            
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
                            Background = new SolidColorBrush(doomed ? Colors.Red : Colors.Transparent),
                            Foreground = new SolidColorBrush(doomed ? Colors.Black : Colors.LightGray)
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

    private static void ViewGradesButtonClick(Subject subject, List<Grade> grades)
    {
        MainWindow.Instance.Frame.NavigationService.Navigate(new GradesListPage(subject, grades));
    }
    
    public void Refresh()
    {
        MainGrid.Children.Clear();
        MainGrid.RowDefinitions.Clear();
        SetupTopRow();
        SetupGradesOverview();
    }
}