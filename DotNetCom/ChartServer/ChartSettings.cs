using Newtonsoft.Json;
using System.ComponentModel;

namespace DotNetCom.ChartServer
{
    [JsonObject(MemberSerialization.OptIn)]
    [DesignTimeVisible(false)]
    public class ChartSettings : Component
    {
        [Browsable(false)]
        [JsonProperty]
        public string type { get => "line"; }

        [JsonProperty]
        [Category("General")]
        [DisplayName("Data")]
        [Description("Chart configuration data.")]
        public ChartData data { get; set; }

        [JsonProperty]
        [Category("General")]
        [DisplayName("Options")]
        [Description("Char advanced options.")]
        public ChartOptions options { get; set; }

        public ChartSettings()
        {
            data = new ChartData();
            options = new ChartOptions();
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
