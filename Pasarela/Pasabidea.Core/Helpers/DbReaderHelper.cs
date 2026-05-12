using System;
using System.Data;

namespace Lantik.Pasabidea.Core.Data
{
    public static class DbReaderHelper
    {
        public static string GetString(IDataRecord reader, string columnName)
        {
            int ordinal = reader.GetOrdinal(columnName);
            return reader.IsDBNull(ordinal) ? string.Empty : Convert.ToString(reader.GetValue(ordinal));
        }

        public static int? GetNullableInt32(IDataRecord reader, string columnName)
        {
            int ordinal = reader.GetOrdinal(columnName);
            return reader.IsDBNull(ordinal) ? (int?)null : Convert.ToInt32(reader.GetValue(ordinal));
        }
    }
}
