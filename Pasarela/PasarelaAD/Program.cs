using System;
using System.Threading.Tasks;
using Artez.AdReader;
using Microsoft.Extensions.Configuration;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        var cfg = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        var opt = cfg.GetSection("AdReader").Get<AdReaderOptions>()
                  ?? throw new Exception("No se pudo cargar AdReader de appsettings.json");

        var reader = new AdConfigReader(opt);
        var snapshot = await reader.LoadAsync();

        foreach (var kvp in snapshot.Tables)
        {
            var table = kvp.Key;
            var rows = kvp.Value;
            Console.WriteLine($"{table}: {rows.Count} filas");
        }
    }
}