namespace Lantik.Pasabidea.Core.Models.Mugi
{
    public sealed class MugiWfFlow
    {
        public string Flow { get; set; }

        public int Version { get; set; }

        public string Active { get; set; }

        public string FlowName { get; set; }

        public string Comments { get; set; }

        public string Running { get; set; }

        public string Start { get; set; }

        public string StopOlderVersions { get; set; }
    }
}
