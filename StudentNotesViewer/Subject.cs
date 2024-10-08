namespace StudentNotesViewer;

public enum Subject
{
    ForeignLanguage,
    Math,
    Literature,
    HungarianGrammar,
    History,
    Sport,
    DesktopAppDevelopment,
    ProjectWork,
    Physics,
    WebsiteDevelopment,
    DatabaseManagement,
    SoftwareTesting
}

public static class SubjectExtensions
{
    public static string GetName(this Subject subject)
    {
        var str = subject.ToString();
        for (var i = 1; i < str.Length; i++)
        {
            if (char.IsUpper(str[i]))
                str = str.Insert(i, " ");
        }

        return str;
    }
}