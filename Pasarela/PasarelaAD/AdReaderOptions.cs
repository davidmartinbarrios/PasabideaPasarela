using System.Collections.Generic;

namespace Artez.AdReader
{
    public sealed class AdReaderOptions
    {
        public AdReaderOptions()
        {
            ConnectionString = "";
            CommandTimeoutSeconds = 60;
            Tables = new List<AdTableSpec>();
        }

        public string ConnectionString { get; set; }
        public int CommandTimeoutSeconds { get; set; }
        public List<AdTableSpec> Tables { get; set; }
    }

    public sealed class AdTableSpec
    {
        public AdTableSpec()
        {
            Name = "";
            Sql = "";
        }

        public string Name { get; set; }          // Ej: "dbo.AD_SERVICIOS"
        public string Sql { get; set; }           // Ej: "SELECT * FROM dbo.AD_SERVICIOS WHERE ACTIVO=1"
    }
}
