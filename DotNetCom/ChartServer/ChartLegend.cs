using Newtonsoft.Json;
using System.ComponentModel;

namespace DotNetCom.ChartServer
{
    [JsonObject(MemberSerialization.OptIn)]
    [DesignTimeVisible(false)]
    public class ChartLegend : Component
    {
        [JsonProperty]
        [Category("General")]
        [DisplayName("Display")]
        [Description("Show/hide chart datasets legend.")]
        public bool display { get; set; } = true;
    }
}
