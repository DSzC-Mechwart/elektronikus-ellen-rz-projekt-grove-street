namespace StudentNotesViewer;

public class Grade(string description, GradeType type, byte value)
{
    public string Description { get; } = description;
    public GradeType Type { get; } = type;
    public byte Value { get; } = value;
}