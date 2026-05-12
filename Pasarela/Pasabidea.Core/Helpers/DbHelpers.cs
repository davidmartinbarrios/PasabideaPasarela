using System;
using System.Data;
using System.Data.SqlClient;

namespace Lantik.Pasabidea.Core.Helpers
{
    internal static class DbHelpers
    {
        public static object DbValue(object value)
        {
            return value == null || value == DBNull.Value || string.IsNullOrWhiteSpace(value.ToString())
                ? DBNull.Value
                : value;
        }

        public static object GetStringOrDbNull(DataRow row, string columnName)
        {
            if (row == null || !row.Table.Columns.Contains(columnName))
                return DBNull.Value;

            return DbValue(row[columnName]);
        }

        public static object GetIntOrDbNull(DataRow row, string columnName)
        {
            if (row == null || !row.Table.Columns.Contains(columnName))
                return DBNull.Value;

            object value = row[columnName];

            if (value == null || value == DBNull.Value)
                return DBNull.Value;

            return Convert.ToInt32(value);
        }

        public static SqlParameter P(string name, SqlDbType type, object value)
        {
            return new SqlParameter(name, type)
            {
                Value = value ?? DBNull.Value
            };
        }

        public static SqlParameter P(string name, SqlDbType type, int size, object value)
        {
            return new SqlParameter(name, type, size)
            {
                Value = value ?? DBNull.Value
            };
        }
    }
}
