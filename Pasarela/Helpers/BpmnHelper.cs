using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public static class JsEscaping
    {
        public static string ToJsStringLiteral(string s)
        {
            if (s == null) return "''";
            return "'" + s.Replace("\\", "\\\\").Replace("'", "\\'").Replace("\r", "").Replace("\n", "\\n") + "'";
        }
    }


}
