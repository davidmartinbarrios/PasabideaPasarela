namespace Pasabidea.Business.Models
{
    public sealed class FlujoResumen
    {
        public FlujoResumen()
        {
            BaseDatos = string.Empty;
            Flow = string.Empty;
            Version = string.Empty;
        }

        public string BaseDatos { get; set; }
        public string Flow { get; set; }
        public string Version { get; set; }
        public string Comments { get; set; }
    }
}
