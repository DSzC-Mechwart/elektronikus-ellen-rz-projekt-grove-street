using System.Windows.Controls;

namespace StudentNotesViewer;

public partial class OverviewPage : Page
{
    private readonly Student CurrentStudent;
    public OverviewPage(Student student)
    {
        CurrentStudent = student;
        InitializeComponent();
    }

    void SetupGradesOverview()
    {
        foreach (var subject in CurrentStudent.Grades.Keys)
        {
            MainGrid.RowDefinitions.Add(new());
            
            // TODO: Border around this row, 3 columns (subject name, grades average, button to view all grades)
        }
    }
}