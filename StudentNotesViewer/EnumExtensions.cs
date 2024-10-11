namespace StudentNotesViewer;

public static class EnumExtensions
{
    public static string GetName<T>(this T e) where T : Enum
    {
        var str = e.ToString() ?? string.Empty;
        for (var i = 1; i < str.Length; i++)
        {
            if (char.IsUpper(str[i]))
            {
                str = str.Insert(i, " ");
                i++;
            }
        }

        return str;
    }
}