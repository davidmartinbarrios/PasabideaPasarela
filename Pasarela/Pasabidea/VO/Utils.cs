using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Lantik.Pasarela.Pasabidea.VO
{
    static class FontManager
    {
        private static PrivateFontCollection _fonts = new PrivateFontCollection();
        private static Font _defaultFont;

        public static Font DefaultFont
        {
            get
            {
                if (_defaultFont == null)
                    _defaultFont = LoadFontFromResources(9f); // tamaño por defecto
                return _defaultFont;
            }
        }

        private static Font LoadFontFromResources(float size)
        {
            byte[] fontData = Lantik.Pasarela.Pasabidea.Resources.Raleway_VariableFont_wght;

            IntPtr fontPtr = Marshal.AllocCoTaskMem(fontData.Length);
            Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            _fonts.AddMemoryFont(fontPtr, fontData.Length);
            Marshal.FreeCoTaskMem(fontPtr);

            return new Font(_fonts.Families[0], size, FontStyle.Regular);
        }
    }

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
