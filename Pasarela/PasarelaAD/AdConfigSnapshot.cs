using System;
using System.Collections.Generic;

namespace Artez.AdReader
{
    public sealed class AdConfigSnapshot
    {
        public AdConfigSnapshot()
        {
            Tables = new Dictionary<string, IReadOnlyList<IReadOnlyDictionary<string, object>>>();
        }

        // Tabla -> filas (cada fila = diccionario columna->valor).
        public IReadOnlyDictionary<string, IReadOnlyList<IReadOnlyDictionary<string, object>>> Tables { get; set; }

        public IReadOnlyList<IReadOnlyDictionary<string, object>> Get(string tableName)
        {
            IReadOnlyList<IReadOnlyDictionary<string, object>> rows;
            return Tables.TryGetValue(tableName, out rows)
                ? rows
                : Array.Empty<IReadOnlyDictionary<string, object>>();
        }
    }
}
