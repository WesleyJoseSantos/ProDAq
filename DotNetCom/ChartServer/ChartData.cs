using Newtonsoft.Json;
using System.ComponentModel;

namespace DotNetCom.ChartServer
{
    [JsonObject(MemberSerialization.OptIn)]
    [DesignTimeVisible(false)]
    public class ChartData : Component
    {
        [JsonProperty]
        [Category("General")]
        [DisplayName("Datasets")]
        [Description("Char datasets configuration.")]
        public ChartDataset[] datasets { get; set; } = new ChartDataset[0];

        //public ChartDataset GetDataset(string labelName)
        //{
        //    for (int i = 0; i < datasets.Length; i++)
        //    {
        //        ChartDataset item = datasets[i];
        //        if (item.label == labelName)
        //        {
        //            return item;
        //        }
        //    }
        //    return null;
        //}
    }
}
