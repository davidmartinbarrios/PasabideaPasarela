using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Artez.AdReader;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        var opt = LoadOptions("appsettings.json");

        var reader = new AdConfigReader(opt);
        var snapshot = await reader.LoadAsync();

        foreach (var kvp in snapshot.Tables)
        {
            var table = kvp.Key;
            var rows = kvp.Value;
            Console.WriteLine($"{table}: {rows.Count} filas");
        }
    }

    private static AdReaderOptions LoadOptions(string path)
    {
        if (!File.Exists(path))
            throw new FileNotFoundException("No se encuentra appsettings.json", path);

        var serializer = new JavaScriptSerializer();
        var root = serializer.Deserialize<Dictionary<string, object>>(File.ReadAllText(path));

        object sectionObject;
        if (root == null || !root.TryGetValue("AdReader", out sectionObject))
            throw new InvalidOperationException("No se pudo cargar AdReader de appsettings.json");

        var section = sectionObject as Dictionary<string, object>;
        if (section == null)
            throw new InvalidOperationException("La seccion AdReader no tiene el formato esperado.");

        var opt = new AdReaderOptions
        {
            ConnectionString = GetString(section, "ConnectionString"),
            CommandTimeoutSeconds = GetInt(section, "CommandTimeoutSeconds", 60)
        };

        object tablesObject;
        if (section.TryGetValue("Tables", out tablesObject))
        {
            var tables = tablesObject as object[];
            if (tables != null)
            {
                foreach (var tableObject in tables)
                {
                    var table = tableObject as Dictionary<string, object>;
                    if (table == null)
                        continue;

                    opt.Tables.Add(new AdTableSpec
                    {
                        Name = GetString(table, "Name"),
                        Sql = GetString(table, "Sql")
                    });
                }
            }
        }

        return opt;
    }

    private static string GetString(IDictionary<string, object> values, string key)
    {
        object value;
        return values.TryGetValue(key, out value) && value != null
            ? Convert.ToString(value)
            : string.Empty;
    }

    private static int GetInt(IDictionary<string, object> values, string key, int defaultValue)
    {
        object value;
        if (!values.TryGetValue(key, out value) || value == null)
            return defaultValue;

        int parsed;
        return int.TryParse(Convert.ToString(value), out parsed)
            ? parsed
            : defaultValue;
    }
}
