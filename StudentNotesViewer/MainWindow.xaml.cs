using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StudentNotesViewer;

public partial class MainWindow
{
    private const string Path = "DummyStudents.txt";
    public static List<Student> Students = [];
    public static readonly Random Random = new();
    public static MainWindow Instance = null!;
    public MainWindow()
    {
        Instance = this;
        // CreateDummyData();
        ImportStudentsFromFile();
        InitializeComponent();
        Frame.NavigationService.Navigate(new LoginPage());
    }

    private void CreateDummyData()
    {
        List<Student> dummyStudents = [];
        string[] dummyNames = ["Bíró Marcell", "Lovász Dominik", "Molnár Roland", "Csesznok Attila", "Csarnai Zsombor", "Bakk Levente", "Pelei Attila", "Kéri Balázs", "Seres Péter"];
        var allSubjects = Enum.GetValues<Subject>();

        foreach (var name in dummyNames)
        {
            var numSubjects = Random.Next(allSubjects.Length);
            var subjects = new Subject[numSubjects];
            for (int i = 0; i < numSubjects; i++)
            {
                var index = Random.Next(allSubjects.Length);
                subjects[i] = allSubjects[index];
            }

            var password = string.Empty;
            var passwordLength = Random.Next(3, 7);
            for (int i = 0; i < passwordLength; i++)
            {
                password += Random.Next(10).ToString();
            }

            dummyStudents.Add(new(name, password, subjects.Distinct().ToDictionary(x => x, _ => new List<Grade>())));
        }

        var json = JsonSerializer.Serialize(dummyStudents);
        File.WriteAllText(Path, json, Encoding.UTF8);
    }

    private void ImportStudentsFromFile()
    {
        var json = File.ReadAllText(Path, Encoding.UTF8);
        Students = JsonSerializer.Deserialize<List<Student>>(json) ?? throw new JsonException("Failed to import dummy students, JSON deserialization failed in MainWindow.cs at ImportStudentsFromFile() method");
        foreach (var student in Students)
        {
            Console.WriteLine($"Name: {student.Name}, Password: {student.Password}, Subjects: {string.Join(", ", student.Grades.Keys)}");
        }
    }
}