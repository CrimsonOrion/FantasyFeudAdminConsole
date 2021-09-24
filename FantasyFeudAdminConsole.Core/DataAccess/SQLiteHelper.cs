using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyFeudAdminConsole.Core.DataAccess
{
    public static class SQLiteHelper
    {
        public static string EscapeName(this string name)
        {
            if (name.Contains("'"))
            {
                name = name.Replace("'", "''");
            }
            return name;
        }
    }
}