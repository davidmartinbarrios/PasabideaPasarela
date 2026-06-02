namespace Lantik.Pasabidea.Core.Models.Mugi
{
    public sealed class MugiWfFlowAction
    {
        public string Flow { get; set; }

        public int Version { get; set; }

        public int FlowOrder { get; set; }

        public int Id { get; set; }

        public string Action { get; set; }

        public string Path { get; set; }

        public string Param { get; set; }

        public string Value { get; set; }

        public string Comments { get; set; }
    }
}
