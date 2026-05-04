using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Lantik.Pasarela.Pasarela.VO
{
    public static class Utils
    {
        public static DataTable ToDataTable<T>(IEnumerable<T> data)
        {
            FieldInfo[] fields = typeof(T).GetFields();
            DataTable table = new DataTable();
            foreach (FieldInfo field in fields)
                table.Columns.Add(field.Name, Nullable.GetUnderlyingType(field.FieldType) ?? field.FieldType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (FieldInfo field in fields)
                    row[field.Name] = field.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }
    }
}
