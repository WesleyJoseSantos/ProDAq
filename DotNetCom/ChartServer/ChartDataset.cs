using Newtonsoft.Json;
using System.ComponentModel;
using System.Drawing;

namespace DotNetCom.ChartServer
{
    [JsonObject(MemberSerialization.OptIn)]
    [DesignTimeVisible(false)]
    public class ChartDataset : Component
    {
        [JsonProperty]
        [Category("General")]
        [DisplayName("Tag Name")]
        [Description("Tag to be linked to this chart data set.")]
        public string label { get; set; }

        [Category("General")]
        [DisplayName("Border Color")]
        [Description("Color of dataset pen.")]
        public Color BorderColor { get; set; }

        [JsonProperty]
        [Browsable(false)]
        public string borderColor
        {
            get => BorderColor.Name;
            set => BorderColor = Color.FromName(value);
        }

        [JsonProperty]
        [Category("General")]
        [DisplayName("Fill")]
        [Description("Designer option.")]
        public bool fill { get; set; }

    }
}
