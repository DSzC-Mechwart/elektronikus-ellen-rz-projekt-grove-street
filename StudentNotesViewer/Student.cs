namespace StudentNotesViewer;

public class Student(string name, string password, Dictionary<Subject, List<Grade>> grades)
{
    public string Name { get; } = name;
    public string Password { get; } = password;
    public Dictionary<Subject, List<Grade>> Grades { get; } = grades;
}