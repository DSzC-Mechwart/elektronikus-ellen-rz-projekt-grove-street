using System.Windows;
using System.Windows.Controls;

namespace StudentNotesViewer;

public partial class GradesListPage
{
    private readonly Subject CurrentSubject;
    private readonly List<Grade> Grades;
    
    public static GradesListPage Instance = null!;

    public GradesListPage(Subject subject, List<Grade> grades)
    {
        Instance = this;
        CurrentSubject = subject;
        Grades = grades;
        InitializeComponent();
        InitializeGradesListBox();
        SetInfoLabel();
    }

    private void InitializeGradesListBox()
    {
        foreach (var grade in Grades)
        {
            GradesListBox.Items.Add(new ListBoxItem
            {
                Content = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    Children =
                    {
                        new Label
                        {
                            Content = grade.Value,
                            HorizontalAlignment = HorizontalAlignment.Left
                        },
                        new Label { Content = grade.Type.GetName() },
                        new Label { Content = grade.Description }
                    }
                }
            });
        }
    }

    private void SetInfoLabel()
    {
        InfoLabel.Content = $"Your {CurrentSubject.GetName()} Grades";
    }

    private void NewGrade(object sender, RoutedEventArgs e)
    {
        MainWindow.Instance.Frame.NavigationService.Navigate(new GradeCreatorPage(Grades));
    }
    
    public void Refresh()
    {
        GradesListBox.Items.Clear();
        InitializeGradesListBox();
    }

    private void GoBack(object sender, RoutedEventArgs e)
    {
        OverviewPage.Instance.Refresh();
        MainWindow.Instance.Frame.NavigationService.GoBack();
    }
}