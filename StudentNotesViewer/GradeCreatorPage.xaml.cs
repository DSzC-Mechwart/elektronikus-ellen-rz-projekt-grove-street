using System.Windows;
using System.Windows.Controls;

namespace StudentNotesViewer;

public partial class GradeCreatorPage
{
    private readonly List<Grade> Grades;

    public GradeCreatorPage(List<Grade> grades)
    {
        Grades = grades;
        InitializeComponent();
        SetupComboBoxes();
    }

    private void SetupComboBoxes()
    {
        GradeComboBox.ItemsSource = Enumerable.Range(1, 5);
        TypeComboBox.ItemsSource = Enum.GetValues<GradeType>().Select(x => x.GetName());
        new List<ComboBox> { GradeComboBox, TypeComboBox }.ForEach(x => x.SelectedIndex = 0);
    }

    private void AddGrade(object sender, RoutedEventArgs e)
    {
        var description = DescriptionTextBox.Text;
        var type = Enum.Parse<GradeType>((TypeComboBox.SelectedItem.ToString() ?? string.Empty).Replace(" ", ""));
        var value = (byte)(GradeComboBox.SelectedIndex + 1);
        Grades.Add(new(description, type, value));
        GradesListPage.Instance.Refresh();
        MainWindow.Instance.Frame.NavigationService.GoBack();
    }
}