namespace StudentNotesViewer;

public class Student(string name, string password)
{
    public string Name { get; } = name;
    public string Password { get; } = password;
    public Dictionary<Subject, List<Grade>> Grades = [];
}