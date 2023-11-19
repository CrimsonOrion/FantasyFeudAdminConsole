namespace FantasyFeudAdminConsole.Core.DataAccess;

public static class SQLiteHelper
{
    public static string EscapeName(this string name)
    {
        if (name.Contains('\''))
        {
            name = name.Replace("'", "''");
        }
        return name;
    }
}