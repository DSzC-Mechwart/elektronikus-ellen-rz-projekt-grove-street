using System.Windows;
using System.Windows.Controls;

namespace StudentNotesViewer;

public partial class GradesListPage : Page
{
    private Subject CurrentSubject;
    private List<Grade> Grades;
    
    public GradesListPage(Subject subject, List<Grade> grades)
    {
        CurrentSubject = subject;
        Grades = grades;
        InitializeComponent();
        InitializeGradesListBox();
    }

    void InitializeGradesListBox()
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
                        new Label {Content = grade.Type},
                        new Label {Content = grade.Description}
                    }
                }
            });
        }
    }
}