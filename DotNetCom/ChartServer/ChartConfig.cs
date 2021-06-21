using Newtonsoft.Json;
using System.ComponentModel;

namespace DotNetCom.ChartServer
{
    [JsonObject(MemberSerialization.OptIn)]
    [DesignTimeVisible(false)]
    public class ChartOptions : Component
    {

        [JsonProperty]
        [Category("Options")]
        [DisplayName("Legend")]
        [Description("Chart legend advanced options.")]
        public ChartLegend legend { get; set; }

        [JsonProperty]
        [Category("Options")]
        [DisplayName("Responsive")]
        [Description("Resizes the chart canvas when its container does.")]
        public bool responsive { get; set; } = true;

        [JsonProperty]
        [Category("Options")]
        [DisplayName("Maintain Aspect Ratio")]
        [Description("Maintain the original canvas aspect ratio when resizing.")]
        public bool maintainAspectRatio { get; set; } = false;

        public ChartOptions()
        {
            legend = new ChartLegend();
        }
    }
}
